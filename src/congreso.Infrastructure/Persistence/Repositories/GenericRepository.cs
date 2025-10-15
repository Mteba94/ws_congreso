using congreso.Application.Interfaces.Persistence;
using congreso.Domain.Entities;
using congreso.Infrastructure.Persistence.Context;
using congreso.Utilities.Static;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace congreso.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _entity;
        private readonly HttpContext _httpContextAccessor;

        public GenericRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _entity = _context.Set<T>();
            _httpContextAccessor = httpContextAccessor.HttpContext;
        }

        public IQueryable<T> GetAllQueryable()
        {
            var query = _entity.AsQueryable();
            return query;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var getAll = await _entity
                .Where(x => x.Estado.Equals((int)TipoEstado.Activo) && x.usuarioEliminacion == null && x.fechaEliminacion == null).AsNoTracking().ToListAsync();

            return getAll;
        }
        public async Task<T> GetByIdAsync(int id)
        {
            var getById = await _entity.SingleOrDefaultAsync(x => x.Id.Equals(id) && x.Estado.Equals((int)TipoEstado.Activo) && x.usuarioEliminacion == null && x.fechaEliminacion == null);
            return getById!;
        }
        public async Task CreateAsync(T entity)
        {
            var userIdString = _httpContextAccessor.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int userId;

            if (entity is User userEntity && userEntity.TipoParticipanteId != null)
            {
                if (string.IsNullOrEmpty(userIdString))
                {
                    userId = 1;
                }
                else
                {
                    userId = int.Parse(userIdString);
                }
            }
            else
            {
                userId = int.Parse(userIdString!);
            }

            entity.usuarioCreacion = userId;
            entity.fechaCreacion = DateTime.UtcNow;

            if(entity.Estado == 0)
            {
                if (entity is User)
                {
                    entity.Estado = (int)TipoEstado.Pendiente;
                }
                else
                {
                    entity.Estado = (int)TipoEstado.Activo;
                }
            }
            
            await _context.AddAsync(entity);
        }
        public void Update(T entity)
        {
            var userIdString = _httpContextAccessor.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int userId;

            if (entity is User userEntity)
            {
                if (string.IsNullOrEmpty(userIdString))
                {
                    userId = 1;
                }
                else
                {
                    userId = int.Parse(userIdString);
                }
            }
            else
            {
                userId = int.Parse(userIdString!);
            }

            entity.fechaModificacion = DateTime.UtcNow;
            entity.usuarioModificacion = userId;

            _context.Update(entity);
        }
        public async Task DeleteAsync(int id)
        {
            var userId = _httpContextAccessor.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            T entity = await GetByIdAsync(id);

            entity.fechaEliminacion = DateTime.UtcNow;
            entity.usuarioEliminacion = int.Parse(userId!);

            entity.Estado = (int)TipoEstado.Inactivo;

            _context.Update(entity);
        }
    }
}

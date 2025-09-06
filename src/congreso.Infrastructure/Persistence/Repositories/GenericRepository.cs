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
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
            //_httpContextAccessor = httpContextAccessor;
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
            var getById = await _entity.SingleOrDefaultAsync(x => x.Id.Equals(id));
            return getById!;
        }
        public async Task CreateAsync(T entity)
        {
            entity.usuarioCreacion = 1;
            entity.fechaCreacion = DateTime.UtcNow;
            entity.Estado = (int)TipoEstado.Activo;

            await _context.AddAsync(entity);
        }
        public void Update(T entity)
        {
            //entity.usuarioModificacion = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            entity.fechaCreacion = DateTime.UtcNow;

            _context.Update(entity);
        }
        public async Task DeleteAsync(int id)
        {
            T entity = await GetByIdAsync(id);

            _context.Remove(entity);
        }
    }
}

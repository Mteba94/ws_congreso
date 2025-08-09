using congreso.Application.Dtos.TipoIdentificacion;
using congreso.Application.Interfaces.Persistence;
using congreso.Domain.Entities;
using congreso.Infrastructure.Persistence.Context;
using congreso.Utilities.Static;
using Microsoft.EntityFrameworkCore;

namespace congreso.Infrastructure.Persistence.Repositories;

public class TipoIdentificacionRepository : ITipoIdentificacionRepository
{
    private readonly ApplicationDbContext _context;

    public TipoIdentificacionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<TipoIdentificacion> GetAllQueryable()
    {
        var query = _context.TiposIdentificacion
            .AsQueryable();

        return query;
    }

    public async Task<IEnumerable<TipoIdentificacion>> GetAllAsync()
    {
        var getAll = await _context.TiposIdentificacion
                .Where(x => x.Estado.Equals((int)TipoEstado.Activo))
                .AsNoTracking()
                .ToListAsync();

        return getAll;
    }

    public async Task<TipoIdentificacion> GetByIdAsync(int id)
    {
        var getById = await _context.TiposIdentificacion
                .SingleOrDefaultAsync(x => x.Id.Equals(id));

        return getById!;
    }

    public async Task CreateAsync(TipoIdentificacion entity)
    {
        await _context.AddAsync(entity);
    }
    public void Update(TipoIdentificacion entity)
    {
        _context.Update(entity);
    }

    public async Task DeleteAsync(int id)
    {
        TipoIdentificacion entity = await GetByIdAsync(id);

        _context.Remove(entity);
    }
}
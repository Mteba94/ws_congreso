using congreso.Application.Interfaces.Persistence;
using congreso.Domain.Entities;
using congreso.Infrastructure.Persistence.Context;
using congreso.Utilities.Static;
using Microsoft.EntityFrameworkCore;

namespace congreso.Infrastructure.Persistence.Repositories;

public class TipoParticipanteRepository : ITipoParticipanteRepository
{
    private readonly ApplicationDbContext _context;

    public TipoParticipanteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<TipoParticipante> GetAllQueryable()
    {
        var query = _context.TiposParticipante
            .AsQueryable();

        return query;
    }

    public async Task<IEnumerable<TipoParticipante>> GetAllAsync()
    {
        var getAll = await _context.TiposParticipante
                .Where(x => x.Estado.Equals((int)TipoEstado.Activo))
                .AsNoTracking()
                .ToListAsync();

        return getAll;
    }

    public async Task<TipoParticipante> GetByIdAsync(int id)
    {
        var getById = await _context.TiposParticipante
                .SingleOrDefaultAsync(x => x.Id.Equals(id));

        return getById!;
    }
    public async Task CreateAsync(TipoParticipante entity)
    {
        await _context.AddAsync(entity);
    }

    public void Update(TipoParticipante entity)
    {
        _context.Update(entity);
    }

    public async Task DeleteAsync(int id)
    {
        TipoParticipante entity = await GetByIdAsync(id);

        _context.Remove(entity);
    }

}

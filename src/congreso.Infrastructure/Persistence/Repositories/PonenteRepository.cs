using congreso.Application.Interfaces.Persistence;
using congreso.Domain.Entities;
using congreso.Infrastructure.Persistence.Context;
using congreso.Utilities.Static;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace congreso.Infrastructure.Persistence.Repositories;

public class PonenteRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : GenericRepository<Ponente>(context, httpContextAccessor), IPonenteRepository
{
    private readonly ApplicationDbContext _context = context;
    public async Task<IEnumerable<Ponente>> GetPopularPonentes()
    {
        var populares = await _context.ActividadesPonentes
        .AsNoTracking()
        .Where(p => p.Estado == (int)TipoEstado.Activo)
        .GroupBy(p => p.Ponente)
        .OrderByDescending(x => x.Count())
        .Select(g => g.Key)
        .Take(6)
        .ToListAsync();

        if (populares.Count < 6)
        {
            var ids = populares.Select(p => p.Id).ToList();

            var faltantes = await _context.Ponentes
                .AsNoTracking()
                .Where(p => p.Estado == (int)TipoEstado.Activo && !ids.Contains(p.Id))
                .Take(6 - populares.Count)
                .ToListAsync();

            populares.AddRange(faltantes);
        }

        return populares;

    }
}

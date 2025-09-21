using congreso.Application.Interfaces.Persistence;
using congreso.Domain.Entities;
using congreso.Infrastructure.Persistence.Context;
using congreso.Utilities.Static;
using Microsoft.EntityFrameworkCore;

namespace congreso.Infrastructure.Persistence.Repositories;

public class PonenteTagRepository(ApplicationDbContext context) : CommonRepository<PonenteTag>(context), IPonenteTagRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<PonenteTag>> GetTagPonentesByPonenteId(int ponenteId)
    {
        var response = await _context.PonenteTags
            .AsNoTracking()
            .Where(rp => rp.PonenteId == ponenteId && rp.Estado == (int)TipoEstado.Activo)
            .ToListAsync();

        return response;
    }

    public async Task<bool> RegistrarPonenteTags(IEnumerable<PonenteTag> ponenteTags)
    {
        foreach (var ponenteTag in ponenteTags)
        {
            var existingTag = await _context.PonenteTags
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(rp => rp.TagId == ponenteTag.TagId && rp.PonenteId == ponenteTag.PonenteId);

            if (existingTag != null)
            {
                existingTag.Estado = 1;

                _context.PonenteTags.Update(existingTag);
            }
            else
            {
                ponenteTag.Estado = 1;

                _context.PonenteTags.Add(ponenteTag);
            }
        }

        var recordsAffected = await _context.SaveChangesAsync();
        return recordsAffected > 0;
    }

    public async Task<bool> EliminarPonenteTags(IEnumerable<PonenteTag> ponenteTags)
    {

        foreach (var pt in ponenteTags)
        {
            pt.Estado = 0;
        }

        _context.PonenteTags.UpdateRange(ponenteTags);

        var recordsAffected = await _context.SaveChangesAsync();
        return recordsAffected > 0;
    }

    public async Task<bool> EliminarPonenteTagsByPonenteId(int ponenteId)
    {
        var ponenteTags = await _context.PonenteTags
            .AsNoTracking()
            .Where(rp => rp.PonenteId == ponenteId && rp.Estado == (int)TipoEstado.Activo)
            .ToListAsync();

        foreach(var pt in ponenteTags)
        {
            pt.Estado = 0;
        }

        _context.PonenteTags.UpdateRange(ponenteTags);

        var recordsAffected = await _context.SaveChangesAsync();
        return recordsAffected > 0;
    }
}

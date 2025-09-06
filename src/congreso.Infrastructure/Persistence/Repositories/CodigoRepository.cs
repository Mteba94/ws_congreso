using congreso.Application.Interfaces.Persistence;
using congreso.Domain.Entities;
using congreso.Infrastructure.Persistence.Context;
using congreso.Utilities.Static;
using Microsoft.EntityFrameworkCore;

namespace congreso.Infrastructure.Persistence.Repositories;

public class CodigoRepository(ApplicationDbContext context) : CommonRepository<CodigoVerificacion>(context), ICodigoRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<CodigoVerificacion> ValidarCodigoAsync(int userId, string purpose)
    {
        var codigoVal = await _context.CodigosVerificacion
            .AsNoTracking()
            .Where(c => c.UserId == userId && c.Purpose == purpose && c.Estado == (int)TipoEstado.Activo)
            .OrderByDescending(c => c.FechaCreacion)
            .FirstOrDefaultAsync();

        return codigoVal!;
    }
}

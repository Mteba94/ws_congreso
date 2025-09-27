using congreso.Application.Interfaces.Persistence;
using congreso.Domain.Entities;
using congreso.Infrastructure.Persistence.Context;

namespace congreso.Infrastructure.Persistence.Repositories;

public class MaterialActividadRepository(ApplicationDbContext context) : CommonRepository<MaterialActividad>(context), IMaterialActividadRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<bool> RegistrarMaterialesActividad(IEnumerable<MaterialActividad> materialesActividad)
    {
        foreach (var materialActividad in materialesActividad)
        {
            materialActividad.Estado = 1;

            _context.MaterialesActividad.Add(materialActividad);
        }

        var recordsAffected = await _context.SaveChangesAsync();
        return recordsAffected > 0;
    }
}

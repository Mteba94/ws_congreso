using congreso.Application.Interfaces.Persistence;
using congreso.Domain.Entities;
using congreso.Infrastructure.Persistence.Context;

namespace congreso.Infrastructure.Persistence.Repositories;

public class ObjetivoActividadRepository(ApplicationDbContext context) : CommonRepository<ObjetivoActividad>(context), IObjetivoActividadRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<bool> RegistrarObjetivosActividad(IEnumerable<ObjetivoActividad> objetivosActividad)
    {

        foreach (var objetivoActividad in objetivosActividad)
        {
            objetivoActividad.Estado = 1;

            _context.ObjetivosActividad.Add(objetivoActividad);
        }

        var recordsAffected = await _context.SaveChangesAsync();
        return recordsAffected > 0;
    }
}

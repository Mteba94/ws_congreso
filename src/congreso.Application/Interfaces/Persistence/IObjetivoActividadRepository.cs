using congreso.Domain.Entities;

namespace congreso.Application.Interfaces.Persistence;

public interface IObjetivoActividadRepository : ICommonRepository<ObjetivoActividad>
{
    Task<bool> RegistrarObjetivosActividad(IEnumerable<ObjetivoActividad> objetivosActividad);
}

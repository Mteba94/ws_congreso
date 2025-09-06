using congreso.Domain.Entities;

namespace congreso.Application.Interfaces.Persistence;

public interface IActividadRepository : IGenericRepository<Actividad>
{
    Task<Actividad> ActivitiesByUser(string userId);
}

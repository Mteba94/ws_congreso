using congreso.Domain.Entities;

namespace congreso.Application.Interfaces.Persistence;

public interface IMaterialActividadRepository : ICommonRepository<MaterialActividad>
{
    Task<bool> RegistrarMaterialesActividad(IEnumerable<MaterialActividad> materialesActividad);
}

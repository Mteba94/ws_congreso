using congreso.Domain.Entities;

namespace congreso.Application.Interfaces.Persistence;

public interface IInscripcionRepository : IGenericRepository<Inscripcion>
{
    Task<bool> ValidateQuota(int ActividadId);
    Task<bool> validateRegistration(int UsuarioId, int ActividadId);
    Task<IEnumerable<Inscripcion>> IncsripcionesByUserId(int UsuarioId);
    Task<Inscripcion> GetByActividadIdUserId(int actividadId, int usuarioId);
    Task<IEnumerable<Inscripcion>> GetInscripcionesByActividadId(int actividadId);
}

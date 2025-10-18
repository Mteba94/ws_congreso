using congreso.Domain.Entities;

namespace congreso.Application.Interfaces.Persistence;

public interface IAsistenciaRepository : IGenericRepository<Asistencia>
{
    Task<bool> HasAttendanceForInscripcion(int inscripcionId);
}
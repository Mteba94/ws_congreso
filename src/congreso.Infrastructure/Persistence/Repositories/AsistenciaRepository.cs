using congreso.Application.Interfaces.Persistence;
using congreso.Domain.Entities;
using congreso.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace congreso.Infrastructure.Persistence.Repositories;

public class AsistenciaRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : GenericRepository<Asistencia>(context, httpContextAccessor), IAsistenciaRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<bool> HasAttendanceForInscripcion(int inscripcionId)
    {
        return await _context.Asistencias.AnyAsync(a => a.InscripcionId == inscripcionId);
    }
}
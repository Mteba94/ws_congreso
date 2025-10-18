using congreso.Application.Interfaces.Persistence;
using congreso.Domain.Entities;
using congreso.Infrastructure.Persistence.Context;
using congreso.Utilities.Static;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congreso.Infrastructure.Persistence.Repositories;

public class InscripcionRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : GenericRepository<Inscripcion>(context, httpContextAccessor), IInscripcionRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly HttpContext _httpContextAccessor = httpContextAccessor.HttpContext;

    public async Task<Inscripcion> GetByActividadIdUserId(int actividadId, int usuarioId)
    {
        // 1. Validación de entrada (Fail-Fast)
        if (actividadId <= 0 || usuarioId <= 0)
        {
            return null!;
        }

        var inscripcion = await _context.Inscripciones
            .FirstOrDefaultAsync(i => i.ActividadId == actividadId && i.UserId == usuarioId);

        if(inscripcion == null)
        {
            return null!;
        }

        return inscripcion;
    }

    public async Task<IEnumerable<Inscripcion>> IncsripcionesByUserId(int UsuarioId)
    {
        if (UsuarioId <= 0)
        {
            return Enumerable.Empty<Inscripcion>();
        }

        return await _context.Inscripciones
            .AsNoTracking()
            .Include(i => i.Actividad)
            .Where(i => i.UserId == UsuarioId && i.Estado == (int)TipoEstado.Activo)
            .OrderByDescending(i => i.FechaInscripcion)
            .ToListAsync();
    }

    public async Task<bool> ValidateQuota(int ActividadId)
    {
        var actividad = await _context.Actividades
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == ActividadId);

        if (actividad == null)
        {
            return false;
        }

        var inscripcionesCount = await _context.Inscripciones
            .CountAsync(i => i.ActividadId == ActividadId);

        return inscripcionesCount <= actividad!.CuposTotales;
    }

    public async Task<bool> validateRegistration(int UsuarioId, int ActividadId)
    {
        var validateRegister = await _context.Inscripciones
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.ActividadId == ActividadId && i.UserId == UsuarioId);

        return validateRegister != null;
    }

    public async Task<IEnumerable<Inscripcion>> GetInscripcionesByActividadId(int actividadId)
    {
        if (actividadId <= 0)
        {
            return Enumerable.Empty<Inscripcion>();
        }

        return await _context.Inscripciones
            .AsNoTracking()
            .Include(i => i.Actividad)
            .Where(i => i.ActividadId == actividadId)
            .OrderByDescending(i => i.FechaInscripcion)
            .ToListAsync();
    }
}

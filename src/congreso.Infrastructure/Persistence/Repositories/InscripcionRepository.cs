using congreso.Application.Interfaces.Persistence;
using congreso.Domain.Entities;
using congreso.Infrastructure.Persistence.Context;
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
}

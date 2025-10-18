using congreso.Application.Interfaces.Persistence;
using congreso.Domain.Entities;
using congreso.Infrastructure.Persistence.Context;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace congreso.Infrastructure.Persistence.Repositories;

public class ActividadRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : GenericRepository<Actividad>(context, httpContextAccessor), IActividadRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly HttpContext _httpContextAccessor = httpContextAccessor.HttpContext;

    public Task<Actividad> ActividadDestacada()
    {
        throw new NotImplementedException();
    }

    public async Task<Actividad> ActivitiesByUser(string userId)
    {
        var activivad = await _context.Actividades
            .FirstOrDefaultAsync();

        return activivad!;
    }

    public async Task<Actividad?> GetActividadForUpdate(int actividadId)
    {
        return await _context.Actividades
        .Include(a => a.ObjetivosActividades)
        .Include(a => a.MaterialesActividades)
        .Include(a => a.ActividadPonentes) // Asumiendo que el nombre de la propiedad de navegación es este
        .FirstOrDefaultAsync(a => a.Id == actividadId);
    }
}

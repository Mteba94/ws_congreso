using congreso.Application.Interfaces.Persistence;
using congreso.Domain.Entities;
using congreso.Infrastructure.Persistence.Context;
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
}

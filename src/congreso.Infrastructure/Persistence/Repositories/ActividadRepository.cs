using congreso.Application.Interfaces.Persistence;
using congreso.Domain.Entities;
using congreso.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace congreso.Infrastructure.Persistence.Repositories;

public class ActividadRepository(ApplicationDbContext context) : GenericRepository<Actividad>(context), IActividadRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Actividad> ActivitiesByUser(string userId)
    {
        var activivad = await _context.Actividades
            .FirstOrDefaultAsync();

        return activivad!;
    }
}

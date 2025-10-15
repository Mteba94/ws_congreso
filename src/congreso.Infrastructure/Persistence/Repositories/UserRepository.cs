using congreso.Application.Interfaces.Persistence;
using congreso.Domain.Entities;
using congreso.Infrastructure.Persistence.Context;
using congreso.Utilities.Static;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace congreso.Infrastructure.Persistence.Repositories;

public class UserRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : GenericRepository<User>(context, httpContextAccessor), IUserRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly HttpContext _httpContextAccessor = httpContextAccessor.HttpContext;

    public async Task<User> UserByEmailAsync(string email)
    {
        var user = await _context.Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email && u.Estado != (int)TipoEstado.Inactivo);
        return user!;
    }

    public async Task<User> UserByIdentificacion(string identificacion)
    {
        var user = await _context.Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.NumeroIdentificacion == identificacion && u.Estado == (int)TipoEstado.Activo);

        return user!;
    }
}

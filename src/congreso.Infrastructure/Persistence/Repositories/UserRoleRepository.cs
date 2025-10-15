using congreso.Application.Interfaces.Persistence;
using congreso.Domain.Entities;
using congreso.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace congreso.Infrastructure.Persistence.Repositories;

internal class UserRoleRepository(ApplicationDbContext context) : CommonRepository<RoleUsuario>(context), IUserRoleRepository
{
    private readonly ApplicationDbContext _context = context;
    public async Task<RoleUsuario> GetByUserId(int userId)
    {
        var roleUsuario = await _context.RolesUsuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(ru => ru.UserId == userId);

        return roleUsuario!;
    }
}

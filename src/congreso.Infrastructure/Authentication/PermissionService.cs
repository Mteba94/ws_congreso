
using congreso.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace congreso.Infrastructure.Authentication;

public class PermissionService : IPermissionService
{
    private readonly ApplicationDbContext _context;

    public PermissionService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<HashSet<string>> GetPermissionAsync(int userId)
    {
        var permissions = await _context.RolesUsuarios
                .Where(ur => ur.UserId == userId)
                .SelectMany(ur => _context.RolesPermisos
                .Where(rp => rp.RoleId == ur.RoleId)
                .Select(rp => rp.Permission.Nombre)
        )
        .ToListAsync();

        return new HashSet<string>(permissions);
    }
}

using congreso.Application.Interfaces.Persistence;
using congreso.Domain.Entities;
using congreso.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace congreso.Infrastructure.Persistence.Repositories;

public class PermisosRepository(ApplicationDbContext context) : IPermisosRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<RolePermission>> GetPermisosRolesByRoleId(int roleId)
    {
        var response = await _context.RolesPermisos
            .AsNoTracking()
            .Where(rp => rp.RoleId == roleId)
            .ToListAsync();

        return response;
    }

    public async Task<bool> RegistrarRolePermisos(IEnumerable<RolePermission> rolePermissions)
    {
        foreach (var rolePermission in rolePermissions)
        {
            rolePermission.Estado = 1;

            _context.RolesPermisos.Add(rolePermission);
        }

        var recordsAffected = await _context.SaveChangesAsync();
        return recordsAffected > 0;
    }

    public async Task<bool> EliminarRolePermisos(IEnumerable<RolePermission> rolePermissions)
    {
        _context.RolesPermisos.RemoveRange(rolePermissions);
        var recordsAffected = await _context.SaveChangesAsync();
        return recordsAffected > 0;
    }
}

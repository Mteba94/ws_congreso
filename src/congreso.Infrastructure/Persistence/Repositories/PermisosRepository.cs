using congreso.Application.Interfaces.Persistence;
using congreso.Domain.Entities;
using congreso.Infrastructure.Persistence.Context;
using congreso.Utilities.Static;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace congreso.Infrastructure.Persistence.Repositories;

public class PermisosRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : IPermisosRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly HttpContext _httpContextAccessor = httpContextAccessor.HttpContext;

    public async Task<IEnumerable<RolePermission>> GetPermisosRolesByRoleId(int roleId)
    {
        var response = await _context.RolesPermisos
            .AsNoTracking()
            .Where(rp => rp.RoleId == roleId && rp.Estado == (int)TipoEstado.Activo)
            .ToListAsync();

        return response;
    }

    public async Task<bool> RegistrarRolePermisos(IEnumerable<RolePermission> rolePermissions)
    {
        var userId = _httpContextAccessor.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        int parsedUserId = int.Parse(userId!);

        foreach (var rolePermission in rolePermissions)
        {

            // 1. Busca si ya existe un registro "eliminado" (Estado = 0)
            var existingPermission = await _context.RolesPermisos
                .IgnoreQueryFilters() // Importante: Ignora el filtro global para poder encontrar registros "eliminados"
                .FirstOrDefaultAsync(rp => rp.RoleId == rolePermission.RoleId && rp.PermissionId == rolePermission.PermissionId);

            if (existingPermission != null)
            {
                // 2. Si existe, actualiza los campos para "reactivarlo"
                existingPermission.usuarioEliminacion = null;
                existingPermission.fechaEliminacion = null;

                existingPermission.Estado = 1;
                existingPermission.fechaModificacion = DateTime.UtcNow;
                existingPermission.usuarioModificacion = parsedUserId;

                _context.RolesPermisos.Update(existingPermission);
            }
            else
            {
                // 3. Si no existe, procede con la creación del nuevo registro
                rolePermission.Estado = 1;
                rolePermission.fechaCreacion = DateTime.UtcNow;
                rolePermission.usuarioCreacion = parsedUserId;

                _context.RolesPermisos.Add(rolePermission);
            }
        }

        var recordsAffected = await _context.SaveChangesAsync();
        return recordsAffected > 0;
    }

    public async Task<bool> EliminarRolePermisos(IEnumerable<RolePermission> rolePermissions)
    {
        //_context.RolesPermisos.RemoveRange(rolePermissions);

        var userId = _httpContextAccessor.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        foreach(var rp in rolePermissions)
        {
            rp.fechaEliminacion = DateTime.UtcNow;
            rp.Estado = 0;
            rp.usuarioEliminacion = int.Parse(userId!);
        }

        _context.RolesPermisos.UpdateRange(rolePermissions);
        var recordsAffected = await _context.SaveChangesAsync();
        return recordsAffected > 0;
    }

    public async Task<IEnumerable<Permission>> GetPermissionsByMenuId(int menuId)
    {
        var menuPermissions = await _context.Permisos
                .AsNoTracking()
                .Where(x => x.MenuId == menuId)
                .ToListAsync();

        return menuPermissions;
    }

    public async Task<IEnumerable<Permission>> GetRolePermissionsByMenuId(int roleId, int menuId)
    {
        var rolePermissions = _context.RolesPermisos
                .Where(pr => pr.RoleId == roleId && pr.Permission.MenuId == menuId && pr.Estado == (int)TipoEstado.Activo)
                .Select(pr => pr.Permission);

        var data = await rolePermissions.ToListAsync();

        return rolePermissions;
    }

    public async Task<bool> EliminarRolePermisosByRoleId(int roleId)
    {
        var userId = _httpContextAccessor.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var rolePermisos = await _context.RolesPermisos
            .AsNoTracking()
            .Where(rp => rp.RoleId == roleId && rp.Estado == (int)TipoEstado.Activo)
            .ToListAsync();

        foreach (var rp in rolePermisos)
        {
            rp.fechaEliminacion = DateTime.UtcNow;
            rp.Estado = 0;
            rp.usuarioEliminacion = int.Parse(userId!);
        }

        _context.RolesPermisos.UpdateRange(rolePermisos);
        var recordsAffected = await _context.SaveChangesAsync();
        return recordsAffected > 0;
    }
}

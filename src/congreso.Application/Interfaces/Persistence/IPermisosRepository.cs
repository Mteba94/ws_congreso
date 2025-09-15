using congreso.Domain.Entities;

namespace congreso.Application.Interfaces.Persistence;

public interface IPermisosRepository
{
    Task<bool> RegistrarRolePermisos(IEnumerable<RolePermission> rolePermissions);
    Task<IEnumerable<Permission>> GetPermissionsByMenuId(int menuId);
    Task<IEnumerable<Permission>> GetRolePermissionsByMenuId(int roleId, int menuId);
    Task<IEnumerable<RolePermission>> GetPermisosRolesByRoleId(int roleId);
    Task<bool> EliminarRolePermisos(IEnumerable<RolePermission> rolePermissions);
}

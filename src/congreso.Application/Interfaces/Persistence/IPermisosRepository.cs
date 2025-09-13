using congreso.Domain.Entities;

namespace congreso.Application.Interfaces.Persistence;

public interface IPermisosRepository
{
    Task<bool> RegistrarRolePermisos(IEnumerable<RolePermission> rolePermissions);
    Task<IEnumerable<RolePermission>> GetPermisosRolesByRoleId(int roleId);
    Task<bool> EliminarRolePermisos(IEnumerable<RolePermission> rolePermissions);
}

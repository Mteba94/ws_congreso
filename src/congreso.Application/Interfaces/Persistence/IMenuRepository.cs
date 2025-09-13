using congreso.Domain.Entities;

namespace congreso.Application.Interfaces.Persistence;

public interface IMenuRepository
{
    Task<bool> RegistrarRoleMenus(IEnumerable<MenuRole> menuRoles);
    Task<IEnumerable<MenuRole>> GetMenuRolesByRoleId(int roleId);
    Task<bool> EliminarRoleMenus(IEnumerable<MenuRole> menuRoles);
}

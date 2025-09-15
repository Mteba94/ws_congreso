using congreso.Application.Interfaces.Persistence;
using congreso.Domain.Entities;
using congreso.Infrastructure.Persistence.Context;
using congreso.Utilities.Static;
using Microsoft.EntityFrameworkCore;

namespace congreso.Infrastructure.Persistence.Repositories;

public class MenuRepository(ApplicationDbContext _context) : IMenuRepository
{
    private readonly ApplicationDbContext _context = _context;

    public async Task<IEnumerable<MenuRole>> GetMenuRolesByRoleId(int roleId)
    {
        var response = await _context.MenusRoles
            .AsNoTracking()
            .Where(mr => mr.RoleId == roleId)
            .ToListAsync();

        return response;
    }

    public async Task<bool> RegistrarRoleMenus(IEnumerable<MenuRole> menuRoles)
    {
        foreach (var menuRole in menuRoles)
        {
            menuRole.Estado = 1;
            _context.MenusRoles.Add(menuRole);
        }

        var recordsAffected = await _context.SaveChangesAsync();

        return recordsAffected > 0;
    }

    public async Task<bool> EliminarRoleMenus(IEnumerable<MenuRole> menuRoles)
    {
        _context.MenusRoles.RemoveRange(menuRoles);

        var recordsAffected = await _context.SaveChangesAsync();

        return recordsAffected > 0;
    }

    public async Task<IEnumerable<Menu>> GetMenuPermissionAsync()
    {
        var query = _context.Menus
            .AsNoTracking()
            .AsSplitQuery()
            .Where(m => m.Url != null && m.Estado == (int)TipoEstado.Activo);

        var menus = await query.ToListAsync();

        return menus;
    }

    public async Task<IEnumerable<Menu>> GetMenuByUserIdAsync(int userId)
    {
        var userRole = await _context.RolesUsuarios.FirstOrDefaultAsync(x => x.UserId == userId);

        var menus = await _context.Menus
            .AsNoTracking()
            .AsSplitQuery()
            .Join(_context.MenusRoles, m => m.Id, mr => mr.MenuId, (m, mr) => new { Menu = m, MenuRole = mr })
            .Where(x => x.MenuRole.RoleId == userRole!.RoleId && x.Menu.Estado == (int)TipoEstado.Activo)
            .Select(x => x.Menu)
            .OrderBy(x => x.Position)
            .ToListAsync();

        return menus;
    }
}

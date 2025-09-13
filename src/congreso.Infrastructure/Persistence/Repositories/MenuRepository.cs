using congreso.Application.Interfaces.Persistence;
using congreso.Domain.Entities;
using congreso.Infrastructure.Persistence.Context;
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
}

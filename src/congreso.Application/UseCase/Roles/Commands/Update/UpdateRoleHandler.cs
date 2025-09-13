using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congreso.Application.UseCase.Roles.Commands.Update;

internal sealed class UpdateRoleHandler(IUnitOfWork unitOfWork) : ICommandHandler<UpdateRoleCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<BaseResponse<bool>> Handle(UpdateRoleCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();
        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            var role = command.Adapt<Role>();
            role.Id = command.RoleId;

            _unitOfWork.Role.Update(role);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var existePermisos = await _unitOfWork.Permisos
                .GetPermisosRolesByRoleId(command.RoleId);

            var existeMenus = await _unitOfWork.Menus
                .GetMenuRolesByRoleId(command.RoleId);

            var newPermisos = command.Permisos
                .Where(p => p.Seleccionado && !existePermisos.Any(ep => ep.PermissionId == p.permisoId))
                .Select(p => new RolePermission
                {
                    RoleId = role.Id,
                    PermissionId = p.permisoId,
                });

            await _unitOfWork.Permisos.RegistrarRolePermisos(newPermisos);

            var newMenus = command.Menus
                .Where(m => !existeMenus.Any(em => em.MenuId == m.MenuId))
                .Select(m => new MenuRole
                {
                    RoleId = role.Id,
                    MenuId = m.MenuId,
                });

            await _unitOfWork.Menus.RegistrarRoleMenus(newMenus);

            var permisosEliminar = existePermisos
                .Where(ep => !command.Permisos.Any(p => p.Seleccionado && p.permisoId == ep.PermissionId))
                .ToList();

            await _unitOfWork.Permisos.EliminarRolePermisos(permisosEliminar);

            var menusEliminar = existeMenus
                .Where(em => !command.Menus.Any(m => m.MenuId == em.MenuId))
                .ToList();

            await _unitOfWork.Menus.EliminarRoleMenus(menusEliminar);

            transaction.Commit();
            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_UPDATE;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }

        return response;
    }
}

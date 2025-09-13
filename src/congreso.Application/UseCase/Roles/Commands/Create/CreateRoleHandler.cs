using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.UseCase.Roles.Commands.Create;

internal sealed class CreateRoleHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateRoleCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BaseResponse<bool>> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();
        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            var role = command.Adapt<Role>();
            await _unitOfWork.Role.CreateAsync(role);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var menus = command.Menus
                .Select(menuId => new MenuRole
                {
                    RoleId = role.Id,
                    MenuId = menuId.MenuId
                })
                .ToList();

            var permisos = command.Permisos
                .Select(permisos => new RolePermission
                {
                    RoleId = role.Id,
                    PermissionId = permisos.permisoId,
                })
                .ToList();

            await _unitOfWork.Permisos.RegistrarRolePermisos(permisos);
            await _unitOfWork.Menus.RegistrarRoleMenus(menus);

            transaction.Commit();

            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_SAVE;

        }
        catch(Exception ex)
        {
            transaction.Rollback();
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }

        return response;
    }
}

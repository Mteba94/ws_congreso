using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.UseCase.UserRoles.Commands.Update;

internal sealed class UpdateUserRoleHandler(IUnitOfWork unitOfWork) : ICommandHandler<UpdateUserRoleCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<BaseResponse<bool>> Handle(UpdateUserRoleCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var userRole = command.Adapt<RoleUsuario>();
            userRole.Id = command.UserRoleId;

            _unitOfWork.RoleUsuario.Update(userRole);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_UPDATE;

        }catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }

        return response;
    }
}

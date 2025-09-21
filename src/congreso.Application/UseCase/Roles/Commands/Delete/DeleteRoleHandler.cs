using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;

namespace congreso.Application.UseCase.Roles.Commands.Delete;

internal sealed class DeleteRoleHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeleteRoleCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BaseResponse<bool>> Handle(DeleteRoleCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();
        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            var existsRole = await _unitOfWork.Role.GetByIdAsync(command.RoleId);

            if (existsRole is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            await _unitOfWork.Role.DeleteAsync(command.RoleId);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _unitOfWork.Permisos.EliminarRolePermisosByRoleId(command.RoleId);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            transaction.Commit();

            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_DELETE;
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

using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Application.UseCase.Users.Comands.DeleteUser;
using congreso.Utilities.Static;

namespace TallerIdentity.Application.UseCases.Users.Commands.DeleteUser;

internal sealed class DeleteUserHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeleteUserCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BaseResponse<bool>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var existsUser = await _unitOfWork.User.GetByIdAsync(command.UserId);

            if (existsUser is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            existsUser.Estado = (int)TipoEstado.Inactivo;
            existsUser.usuarioEliminacion = 1;
            existsUser.fechaEliminacion = DateTime.Now;

            _unitOfWork.User.Update(existsUser);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_DELETE;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }

        return response;
    }
}

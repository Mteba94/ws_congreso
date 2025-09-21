using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using logging.Interface;
using Mapster;
using BC = BCrypt.Net.BCrypt;

namespace congreso.Application.UseCase.Users.Comands.UpdateUser;

internal sealed class UpdateUserHandler(IUnitOfWork unitOfWork, HandlerExecutor executor, IFileLogger fileLogger) : ICommandHandler<UpdateUserCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;
    private readonly IFileLogger _fileLogger = fileLogger;

    public async Task<BaseResponse<bool>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(
                command,
                () => UpdateUserAsync(command, cancellationToken),
                cancellationToken
            );
    }

    private async Task<BaseResponse<bool>> UpdateUserAsync(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            _fileLogger.Log("ws_congreso", "UpdateUser", "0", command);

            var validUser = await _unitOfWork.User.GetByIdAsync(command.UserId);

            if (validUser is not null)
            {
                command.Adapt(validUser);

                validUser.Password = BC.HashPassword(validUser.Password);

                validUser.usuarioModificacion = 1;
                validUser.fechaModificacion = DateTime.Now;

                _unitOfWork.User.Update(validUser);
                await _unitOfWork.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_UPDATE;

                _fileLogger.Log("ws_congreso", "UpdateUser", "1", response);

                return response;
            }

            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

            _fileLogger.Log("ws_congreso", "UpdateUser", "1", response);

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;

            _fileLogger.Log("ws_congreso", "UpdateUser", "1", response, ex.Message);
        }

        return response;
    }
}

using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Application.UseCase.CodigosVerificacion.Queries.ValidacionCodigo;
using congreso.Utilities.Static;
using logging.Interface;
using Mapster;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using BC = BCrypt.Net.BCrypt;

namespace congreso.Application.UseCase.Users.Comands.RecoveryPass;

internal sealed class RecoveryPassUserHandler(IUnitOfWork unitOfWork, HandlerExecutor executor, IFileLogger fileLogger) : ICommandHandler<RecoveryPassUserCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;
    private readonly IFileLogger _fileLogger = fileLogger;

    public async Task<BaseResponse<bool>> Handle(RecoveryPassUserCommand command, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(
                command,
                () => UpdatePasswordAsync(command, cancellationToken),
                cancellationToken
            );
    }

    private async Task<BaseResponse<bool>> UpdatePasswordAsync(RecoveryPassUserCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var validUser = await _unitOfWork.User.UserByEmailAsync(command.Email);

            if (validUser == null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;

                return response;
            }

            var validCode = await _unitOfWork.CodigoVerificacion.ValidarCodigoAsync(validUser.Id, command.Purpose);

            if (validCode == null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;

                return response;
            }

            if(!BC.Verify(command.Codigo, validCode!.Codigo))
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_CODE_ERROR;

                return response;
            }

            validCode.Estado = (int)TipoEstado.Inactivo;

            _unitOfWork.CodigoVerificacion.Update(validCode);
            await _unitOfWork.SaveChangesAsync();

            var user = command.Adapt(validUser);

            user.Password = BC.HashPassword(command.NewPassword);
            user.AccessFailedCount = 0;
            user.Estado = (int)TipoEstado.Activo;

            _unitOfWork.User.Update(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_SAVE;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }

        return response;
    }
}

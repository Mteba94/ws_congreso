using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using logging.Interface;
using Mapster;
using BC = BCrypt.Net.BCrypt;

namespace congreso.Application.UseCase.Participantes.Commands.Update;

internal sealed class UpdateParticipanteHandler(IUnitOfWork unitOfWork, HandlerExecutor executor, IFileLogger fileLogger) : ICommandHandler<UpdateParticipanteCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;
    private readonly IFileLogger _fileLogger = fileLogger;
    public async Task<BaseResponse<bool>> Handle(UpdateParticipanteCommand command, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(
                command,
                () => UpdateParticipanteAsync(command, cancellationToken),
                cancellationToken
            );
    }

    private async Task<BaseResponse<bool>> UpdateParticipanteAsync(UpdateParticipanteCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            _fileLogger.Log("ws_congreso", "UpdateParticipante", "0", command);

            var validUser = await _unitOfWork.User.GetByIdAsync(command.ParticipanteId);

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

                _fileLogger.Log("ws_congreso", "UpdateParticipante", "1", response);

                return response;
            }

            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

            _fileLogger.Log("ws_congreso", "UpdateParticipante", "1", response);

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;

            _fileLogger.Log("ws_congreso", "UpdateParticipante", "1", response, ex.Message);
        }

        return response;
    }
}

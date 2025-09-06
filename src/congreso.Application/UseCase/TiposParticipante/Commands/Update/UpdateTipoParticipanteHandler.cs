using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using logging.Interface;
using Mapster;
using System.Text.Json;

namespace congreso.Application.UseCase.TiposParticipante.Commands.Update;

internal sealed class UpdateTipoParticipanteHandler(IUnitOfWork unitOfWork, HandlerExecutor executor, IFileLogger fileLogger) : ICommandHandler<UpdateTipoParticipanteCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;
    private readonly IFileLogger _fileLogger = fileLogger;

    public async Task<BaseResponse<bool>> Handle(UpdateTipoParticipanteCommand command, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(
                command,
                () => UpdateTipoParticipanteAsync(command, cancellationToken),
                cancellationToken
            );
    }

    private async Task<BaseResponse<bool>> UpdateTipoParticipanteAsync(UpdateTipoParticipanteCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            _fileLogger.Log("ws_congreso", "UpdateTipoParticipante", "0", command);

            var validTipoParticipante = await _unitOfWork.TipoParticipante.GetByIdAsync(command.tipoParticipanteId);

            if (validTipoParticipante is not null)
            {
                command.Adapt(validTipoParticipante);

                _unitOfWork.TipoParticipante.Update(validTipoParticipante);
                await _unitOfWork.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_UPDATE;

                _fileLogger.Log("ws_congreso", "UpdateUser", "1", response);

                return response;
            }

            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

            _fileLogger.Log("ws_congreso", "UpdateTipoParticipante", "1", response);

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;

            _fileLogger.Log("ws_congreso", "UpdateTipoParticipante", "1", response, ex.Message);
        }

        return response;
    }
}

using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using logging.Interface;
using Mapster;

namespace congreso.Application.UseCase.TiposParticipante.Commands.Create;

internal sealed class CreateTipoParticipanteHandler(IUnitOfWork unitOfWork, HandlerExecutor handlerExecutor, IFileLogger fileLogger) : ICommandHandler<CreateTipoParticipanteCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = handlerExecutor;
    private readonly IFileLogger _fileLogger = fileLogger;

    public async Task<BaseResponse<bool>> Handle(CreateTipoParticipanteCommand command, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(command, () => CreateTipoParticipanteAsync(command, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<bool>> CreateTipoParticipanteAsync(CreateTipoParticipanteCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            _fileLogger.Log("ws_congreso", "CreateTipoParticipante", "0", command);

            var tipoParticipante = command.Adapt<TipoParticipante>();

            await _unitOfWork.TipoParticipante.CreateAsync(tipoParticipante);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_SAVE;

            _fileLogger.Log("ws_congreso", "CreateTipoParticipante", "1", response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_EXCEPTION;

            _fileLogger.Log("ws_congreso", "CreateTipoParticipante", "1", response, ex.Message);
        }

        return response;
    }
}

using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using logging.Interface;
using Mapster;

namespace congreso.Application.UseCase.NivelesDificultad.Commands.Update;

internal sealed class UpdateNivelHandler(IUnitOfWork unitOfWork, HandlerExecutor executor, IFileLogger fileLogger) : ICommandHandler<UpdateNivelCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;
    private readonly IFileLogger _fileLogger = fileLogger;
    public async Task<BaseResponse<bool>> Handle(UpdateNivelCommand command, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(
                command,
                () => UpdateNivelAsync(command, cancellationToken),
                cancellationToken
            );
    }

    private async Task<BaseResponse<bool>> UpdateNivelAsync(UpdateNivelCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            _fileLogger.Log("ws_congreso", "UpdateNivel", "0", command);

            var validNivel = await _unitOfWork.NivelDificultad.GetByIdAsync(command.nivelId);

            if (validNivel is not null)
            {
                command.Adapt(validNivel);

                _unitOfWork.NivelDificultad.Update(validNivel);
                await _unitOfWork.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_UPDATE;

                _fileLogger.Log("ws_congreso", "UpdateNivel", "1", response);

                return response;
            }

            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

            _fileLogger.Log("ws_congreso", "UpdateNivel", "1", response);

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;

            _fileLogger.Log("ws_congreso", "UpdateNivel", "1", response, ex.Message);
        }

        return response;
    }
}

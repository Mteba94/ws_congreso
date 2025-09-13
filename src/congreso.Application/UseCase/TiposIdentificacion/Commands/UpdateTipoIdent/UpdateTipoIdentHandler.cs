using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using logging.Interface;
using Mapster;

namespace congreso.Application.UseCase.TiposIdentificacion.Commands.UpdateTipoIdent;

internal sealed class UpdateTipoIdentHandler(IUnitOfWork unitOfWork, HandlerExecutor executor, IFileLogger fileLogger) : ICommandHandler<UpdateTipoIdentCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;
    private readonly IFileLogger _fileLogger = fileLogger;

    public async Task<BaseResponse<bool>> Handle(UpdateTipoIdentCommand command, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(command, () => UpdateTipoIdentAsync(command, cancellationToken), cancellationToken);
    }

    public async Task<BaseResponse<bool>> UpdateTipoIdentAsync(UpdateTipoIdentCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            _fileLogger.Log("ws_congreso", "UpdateTipoIdent", "0", command);

            var validTidentificacion = await _unitOfWork.TipoIdentificacion.GetByIdAsync(command.Id);

            if (validTidentificacion is not null)
            {
                command.Adapt(validTidentificacion);

                _unitOfWork.TipoIdentificacion.Update(validTidentificacion);
                await _unitOfWork.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_UPDATE;

                _fileLogger.Log("ws_congreso", "UpdateTipoIdent", "1", response);

                return response;
            }

            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

            _fileLogger.Log("ws_congreso", "UpdateUser", "1", response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_EXCEPTION;

            _fileLogger.Log("ws_congreso", "UpdateTipoIdent", "1", response, ex.Message);
        }

        return response;
    }
}

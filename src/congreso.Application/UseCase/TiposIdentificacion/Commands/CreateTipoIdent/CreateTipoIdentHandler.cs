using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Application.UseCase.Users.Comands.CreateUser;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using logging.Interface;
using Mapster;
using System.Text.Json;

namespace congreso.Application.UseCase.TiposIdentificacion.Commands.CreateTipoIdent;

internal sealed class CreateTipoIdentHandler(IUnitOfWork unitOfWork, IFileLogger fileLogger, HandlerExecutor executor) : ICommandHandler<CreateTipoIdentCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;
    private readonly IFileLogger _fileLogger = fileLogger;

    public async Task<BaseResponse<bool>> Handle(CreateTipoIdentCommand command, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(command, () => CreateTipoIdentAsync(command, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<bool>> CreateTipoIdentAsync(CreateTipoIdentCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            _fileLogger.Log("ws_congreso", "CreateTipoIdent", "0", JsonSerializer.Serialize(command));

            var tipoIdent = command.Adapt<TipoIdentificacion>();
            tipoIdent.Estado = (int)TipoEstado.Activo;

            await _unitOfWork.TipoIdentificacion.CreateAsync(tipoIdent);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_SAVE;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;

            _fileLogger.Log("ws_congreso", "CreateTipoIdent", "1", JsonSerializer.Serialize(response), ex.Message);
        }

        return response;
    }
}

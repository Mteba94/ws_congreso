using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using logging.Interface;

namespace congreso.Application.UseCase.NivelesDificultad.Commands.Delete;

internal sealed class DeleteNivelDificultadHandler(IUnitOfWork unitOfWork, IFileLogger fileLogger) : ICommandHandler<DeleteNivelDificultadCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileLogger _fileLogger = fileLogger;
    public async Task<BaseResponse<bool>> Handle(DeleteNivelDificultadCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            _fileLogger.Log("ws_congreso", "DeleteNivelDificultad", "0", command);

            var existsNivel = await _unitOfWork.NivelDificultad.GetByIdAsync(command.NivelId);

            if (existsNivel is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                _fileLogger.Log("ws_congreso", "DeleteNivelDificultad", "1", response);
                return response;
            }

            existsNivel.Estado = (int)TipoEstado.Inactivo;

            _unitOfWork.NivelDificultad.Update(existsNivel);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_DELETE;

            _fileLogger.Log("ws_congreso", "DeleteUser", "1", response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;

            _fileLogger.Log("ws_congreso", "DeleteNivelDificultad", "1", response, ex.Message);
        }

        return response;
    }
}

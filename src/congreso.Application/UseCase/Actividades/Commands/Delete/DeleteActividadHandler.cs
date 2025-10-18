using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;

namespace congreso.Application.UseCase.Actividades.Commands.Delete;

internal sealed class DeleteActividadHandler(IUnitOfWork unitOfWork, HandlerExecutor executor) : ICommandHandler<DeleteActividadCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;

    public async Task<BaseResponse<bool>> Handle(DeleteActividadCommand command, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(command, () => DeleteActividadAsync(command, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<bool>> DeleteActividadAsync(DeleteActividadCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var actividad = await _unitOfWork.Actividad.GetByIdAsync(command.ActividadId);

            if (actividad == null)
            {
                response.IsSuccess = false;
                response.Message = "Actividad no encontrada.";
                return response;
            }

            // Perform soft delete
            actividad.Estado = (int)TipoEstado.Inactivo;
            _unitOfWork.Actividad.Update(actividad);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            response.IsSuccess = true;
            response.Message = "Actividad eliminada exitosamente.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }

        return response;
    }
}
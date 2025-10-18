using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;

namespace congreso.Application.UseCase.Actividades.Commands.UpdateEstadoActividad;

internal sealed class UpdateEstadoActividadHandler(IUnitOfWork unitOfWork, HandlerExecutor executor) : ICommandHandler<UpdateEstadoActividadCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;

    public async Task<BaseResponse<bool>> Handle(UpdateEstadoActividadCommand command, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(command, () => UpdateEstadoActividadAsync(command, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<bool>> UpdateEstadoActividadAsync(UpdateEstadoActividadCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var actividad = await _unitOfWork.Actividad.GetByIdAsync(command.ActividadId);

            if (actividad is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            actividad.EstadoActividad = command.NewEstadoActividad;
            _unitOfWork.Actividad.Update(actividad);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_UPDATE;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }

        return response;
    }
}

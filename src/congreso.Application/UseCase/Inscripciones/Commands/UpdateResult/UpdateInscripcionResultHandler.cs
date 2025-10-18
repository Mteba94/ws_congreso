using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;

namespace congreso.Application.UseCase.Inscripciones.Commands.UpdateResult;

internal sealed class UpdateInscripcionResultHandler(IUnitOfWork unitOfWork, HandlerExecutor executor) : ICommandHandler<UpdateInscripcionResultCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;

    public async Task<BaseResponse<bool>> Handle(UpdateInscripcionResultCommand command, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(command, () => UpdateInscripcionResultAsync(command, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<bool>> UpdateInscripcionResultAsync(UpdateInscripcionResultCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var inscripcion = await _unitOfWork.Inscripcion.GetByIdAsync(command.InscripcionId);

            if (inscripcion is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            inscripcion.Puntaje = command.Puntaje;
            inscripcion.EsGanador = command.EsGanador;

            _unitOfWork.Inscripcion.Update(inscripcion);
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
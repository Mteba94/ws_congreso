using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.UseCase.Actividades.Commands.Create;

internal sealed class CreateActividadHandler(IUnitOfWork unitOfWork, HandlerExecutor executor) : ICommandHandler<CreateActividadCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;
    public async Task<BaseResponse<bool>> Handle(CreateActividadCommand command, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(command, () => CreateActividadAsync(command, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<bool>> CreateActividadAsync(CreateActividadCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();
        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            var actividad = command.Adapt<Actividad>();

            actividad.CuposDisponibles = command.CuposTotal;

            await _unitOfWork.Actividad.CreateAsync(actividad);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var objetivosActividad = command.ObjetivosActividad!
                .Select(objetivos => new ObjetivoActividad
                {
                    ActividadId = actividad.Id,
                    ObjetivoDesc = objetivos.ObjetoDesc!
                })
                .ToList();

            await _unitOfWork.ObjetivoActividad.RegistrarObjetivosActividad(objetivosActividad);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var actividadPonente = new ActividadPonente
                {
                    ActividadId = actividad.Id,
                    PonenteId = command.ActividadPonente.PonenteId
                };

            await _unitOfWork.ActividadPonente.CreateAsync(actividadPonente);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var materialesActividad = command.materialesActividad!
                .Select(materiales => new MaterialActividad
                {
                    ActividadId = actividad.Id,
                    MaterialDesc = materiales.MaterialDesc!
                })
                .ToList();

            await _unitOfWork.MaterialActividad.RegistrarMaterialesActividad(materialesActividad);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            transaction.Commit();

            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_SAVE;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }

        return response;
    }
}

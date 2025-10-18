using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.UseCase.Inscripciones.Commands.Create;

internal sealed class CreateInscripcionHandler(IUnitOfWork unitOfWork, HandlerExecutor executor) : ICommandHandler<CreateInscripcionCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;
    public async Task<BaseResponse<bool>> Handle(CreateInscripcionCommand command, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(command, () => CreateInscripcionAsync(command, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<bool>> CreateInscripcionAsync(CreateInscripcionCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();
        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            // 1. Buscar si ya existe una inscripción para este usuario y actividad.
            var inscripcionExistente = await _unitOfWork.Inscripcion.GetByActividadIdUserId(command.IdActividad, command.IdUsuario);

            // 2. Lógica de decisión: Reactivar, Fallar o Crear.
            if (inscripcionExistente is not null)
            {
                // CASO A: La inscripción existe.
                if (inscripcionExistente.Estado == (int)TipoEstado.Activo)
                {
                    // Ya está inscrito y activo, no se puede hacer nada más.
                    response.IsSuccess = false;
                    response.Message = "El usuario ya se encuentra inscrito en esta actividad.";
                    return response;
                }
                else // La inscripción existe pero está Inactiva (Estado == 0)
                {
                    // ¡NUEVA LÓGICA! Reactivamos la inscripción existente.
                    inscripcionExistente.Estado = (int)TipoEstado.Activo;
                    inscripcionExistente.FechaInscripcion = DateTime.UtcNow; // Actualizamos la fecha
                    _unitOfWork.Inscripcion.Update(inscripcionExistente);
                }
            }
            else
            {
                // CASO B: La inscripción no existe, es un nuevo registro.
                // 3. Validar el cupo ANTES de crear.
                var tieneCupo = await _unitOfWork.Inscripcion.ValidateQuota(command.IdActividad);
                if (!tieneCupo)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_NO_QUOTA;
                    return response;
                }

                // Creamos la nueva inscripción.
                var nuevaInscripcion = command.Adapt<Inscripcion>();
                nuevaInscripcion.FechaInscripcion = DateTime.UtcNow;
                nuevaInscripcion.Estado = (int)TipoEstado.Activo; // Nace como activa
                await _unitOfWork.Inscripcion.CreateAsync(nuevaInscripcion);
            }

            // 4. Actualizar el cupo de la actividad (se ejecuta tanto para creación como para reactivación).
            var actividad = await _unitOfWork.Actividad.GetByIdAsync(command.IdActividad);
            if (actividad is null)
            {
                // Esto sería un error de datos inconsistentes, la transacción hará rollback.
                transaction.Rollback();
                response.IsSuccess = false;
                response.Message = "La actividad asociada no fue encontrada.";
                return response;
            }

            actividad.CuposDisponibles -= 1;
            _unitOfWork.Actividad.Update(actividad);

            // 5. Guardar todos los cambios en la base de datos.
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            transaction.Commit();

            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_SAVE;
        }
        catch (Exception ex)
        {
            // Loguear la excepción 'ex' es una buena práctica aquí.
            transaction.Rollback();
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }

        return response;
    }
}

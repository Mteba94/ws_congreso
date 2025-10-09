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
            var inscripcion = command.Adapt<Inscripcion>();

            var validateRegister = await _unitOfWork.Inscripcion.validateRegistration(command.IdUsuario, command.IdActividad);

            if (validateRegister)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                return response;
            }

            var validateQuota = await _unitOfWork.Inscripcion.ValidateQuota(command.IdActividad);

            if (!validateQuota)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_NO_QUOTA;

                return response;
            }

            inscripcion.FechaInscripcion = DateTime.UtcNow;

            await _unitOfWork.Inscripcion.CreateAsync(inscripcion);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var actividad = await _unitOfWork.Actividad.GetByIdAsync(command.IdActividad);

            if(actividad == null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                return response;
            }

            actividad.CuposDisponibles = actividad.CuposDisponibles-1;

            _unitOfWork.Actividad.Update(actividad);
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

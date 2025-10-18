using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace congreso.Application.UseCase.Asistencias.Commands.MarkAttendance;

internal sealed class MarkAttendanceHandler(IUnitOfWork unitOfWork, HandlerExecutor executor) : ICommandHandler<MarkAttendanceCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    //private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly HandlerExecutor _executor = executor;

    public async Task<BaseResponse<bool>> Handle(MarkAttendanceCommand command, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(command, () => MarkAttendanceAsync(command, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<bool>> MarkAttendanceAsync(MarkAttendanceCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var user = await _unitOfWork.User.UserByEmailAsync(command.Email);

            // 2. Find Inscripcion for the UserId and ActividadId
            var inscripcion = await _unitOfWork.Inscripcion.GetByActividadIdUserId(command.ActividadId, user.Id);

            if (inscripcion == null)
            {
                response.IsSuccess = false;
                response.Message = "El usuario no está inscrito en esta actividad.";
                return response;
            }

            // 3. Check if attendance has already been marked for this Inscripcion for today
            var alreadyMarked = await _unitOfWork.Asistencia.HasAttendanceForInscripcion(inscripcion.Id);
            if (alreadyMarked)
            {
                response.IsSuccess = false;
                response.Message = "La asistencia para esta inscripción ya ha sido marcada hoy.";
                return response;
            }

            // 4. Create an Asistencia record
            var newAsistencia = new Asistencia
            {
                InscripcionId = inscripcion.Id,
                ActividadId = command.ActividadId,
                FechaRegistro = DateTime.UtcNow,
                Estado = (int)TipoEstado.Activo // Assuming active by default
            };

            await _unitOfWork.Asistencia.CreateAsync(newAsistencia);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            response.IsSuccess = true;
            response.Message = "Asistencia marcada exitosamente.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }

        return response;
    }
}
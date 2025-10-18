using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Asistencias;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using System.Linq;

namespace congreso.Application.UseCase.Asistencias.Queries.GetAllAttendanceDetails;

internal sealed class GetAllAttendanceDetailsHandler(IUnitOfWork unitOfWork, HandlerExecutor executor) : IQueryHandler<GetAllAttendanceDetailsQuery, IEnumerable<AttendanceDetailDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;

    public async Task<BaseResponse<IEnumerable<AttendanceDetailDto>>> Handle(GetAllAttendanceDetailsQuery query, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(query, () => GetAllAttendanceDetailsAsync(query, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<IEnumerable<AttendanceDetailDto>>> GetAllAttendanceDetailsAsync(GetAllAttendanceDetailsQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<AttendanceDetailDto>>();

        try
        {
            var allAttendances = await _unitOfWork.Asistencia.GetAllAsync();
            var allInscriptions = await _unitOfWork.Inscripcion.GetAllAsync();
            var allUsers = await _unitOfWork.User.GetAllAsync();
            var allActivities = await _unitOfWork.Actividad.GetAllAsync();
            var allTipoActividad = await _unitOfWork.TipoActividad.GetAllAsync();
            var allTipoParticipante = await _unitOfWork.TipoParticipante.GetAllAsync();
            var allSchools = await _unitOfWork.School.GetAllAsync();

            if (!allInscriptions.Any())
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var attendanceDetails = (from i in allInscriptions
                                     join u in allUsers on i.UserId equals u.Id
                                     join act in allActivities on i.ActividadId equals act.Id
                                     join ta in allTipoActividad on act.TipoActividadId equals ta.Id
                                     join tp in allTipoParticipante on u.TipoParticipanteId equals tp.Id into userTipoParticipante
                                     from tp in userTipoParticipante.DefaultIfEmpty()
                                     join s in allSchools on u.SchoolId equals s.Id into userSchool
                                     from s in userSchool.DefaultIfEmpty()
                                     join a in allAttendances on i.Id equals a.InscripcionId into attendanceGroup
                                     from a in attendanceGroup.DefaultIfEmpty() // Left Join with Attendances
                                     select new AttendanceDetailDto
                                     {
                                         Id = i.Id, // Use Inscription Id as the primary ID for this report item
                                         ParticipantName = u.Pnombre + " " + u.Snombre + " " + u.Papellido + " " + u.Sapellido,
                                         Email = u.Email,
                                         Activity = act.Titulo,
                                         ActivityType = ta.Nombre,
                                         CheckInTime = a != null ? a.FechaRegistro : (DateTime?)null,
                                         Status = a != null ? "Presente" : "Ausente",
                                         StudentType = tp?.Nombre ?? "Desconocido",
                                         Institution = s?.nombre ?? "N/A"
                                     }).ToList();

            response.IsSuccess = true;
            response.Data = attendanceDetails;
            response.TotalRecords = attendanceDetails.Count;
            response.Message = ReplyMessage.MESSAGE_QUERY;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }

        return response;
    }
}

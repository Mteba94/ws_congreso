using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.User;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;

namespace congreso.Application.UseCase.Users.Queries.GetUserAttendancePercentage;

internal sealed class GetUserAttendancePercentageHandler(IUnitOfWork unitOfWork, HandlerExecutor executor) : IQueryHandler<GetUserAttendancePercentageQuery, UserAttendancePercentageDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;

    public async Task<BaseResponse<UserAttendancePercentageDto>> Handle(GetUserAttendancePercentageQuery query, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(query, () => GetUserAttendancePercentageAsync(query, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<UserAttendancePercentageDto>> GetUserAttendancePercentageAsync(GetUserAttendancePercentageQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<UserAttendancePercentageDto>();

        try
        {
            // 1. Get all inscriptions for the user
            var userInscriptions = (await _unitOfWork.Inscripcion.GetAllAsync())
                                    .Where(i => i.UserId == query.UserId)
                                    .ToList();

            if (!userInscriptions.Any())
            {
                response.IsSuccess = true;
                response.Data = new UserAttendancePercentageDto { UserId = query.UserId, TotalActivitiesIniciado = 0, TotalActivitiesAttended = 0, AttendancePercentage = 0M };
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            // 2. Get all activities and filter for 'i' (Iniciado) state
            var allActivities = await _unitOfWork.Actividad.GetAllAsync();
            var allAttendances = await _unitOfWork.Asistencia.GetAllAsync();

            var activitiesIniciadoIds = allActivities
                                        .Where(a => a.EstadoActividad == ActividadEstado.Finalizado || a.EstadoActividad == ActividadEstado.Iniciado)
                                        .Select(a => a.Id)
                                        .ToHashSet();

            // 3. Filter user inscriptions to only include activities in 'i' state
            var enrolledIniciadoActivities = userInscriptions
                                            .Where(i => activitiesIniciadoIds.Contains(i.ActividadId))
                                            .ToList();

            int totalActivitiesIniciado = enrolledIniciadoActivities.Count();

            // 4. Count attended activities among those in 'i' state
            int totalActivitiesAttended = enrolledIniciadoActivities
                                            .Count(i => allAttendances.Any(a => a.InscripcionId == i.Id));

            // 5. Calculate percentage
            decimal attendancePercentage = (totalActivitiesIniciado > 0)
                                            ? (decimal)totalActivitiesAttended / totalActivitiesIniciado * 100M
                                            : 0M;

            response.IsSuccess = true;
            response.Data = new UserAttendancePercentageDto
            {
                UserId = query.UserId,
                TotalActivitiesIniciado = totalActivitiesIniciado,
                TotalActivitiesAttended = totalActivitiesAttended,
                AttendancePercentage = Math.Round(attendancePercentage, 2)
            };
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

using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Dashboard;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;

namespace congreso.Application.UseCase.Dashboard.Queries.GetSpecificDashboardMetrics;

internal sealed class GetSpecificDashboardMetricsHandler(IUnitOfWork unitOfWork, HandlerExecutor executor) : IQueryHandler<GetSpecificDashboardMetricsQuery, SpecificDashboardMetricsDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;

    public async Task<BaseResponse<SpecificDashboardMetricsDto>> Handle(GetSpecificDashboardMetricsQuery query, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(query, () => GetSpecificDashboardMetricsAsync(query, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<SpecificDashboardMetricsDto>> GetSpecificDashboardMetricsAsync(GetSpecificDashboardMetricsQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<SpecificDashboardMetricsDto>();

        try
        {
            // Total Attendance
            var allAttendances = await _unitOfWork.Asistencia.GetAllAsync();
            int totalAttendance = allAttendances.Count();

            // Completion Rate (Total Asistencia / Total Inscripcion)
            var allInscriptions = await _unitOfWork.Inscripcion.GetAllAsync();
            int totalInscriptions = allInscriptions.Count();
            decimal completionRate = (totalInscriptions > 0)
                                        ? (decimal)totalAttendance / totalInscriptions * 100M
                                        : 0M;

            // Active Sessions (Activities with EstadoActividad == 'i')
            var allActivities = await _unitOfWork.Actividad.GetAllAsync();
            int activeSessions = allActivities.Count(a => a.EstadoActividad == "i");

            // Average Participation (Total Inscriptions for active activities / Number of active activities)
            var inscriptionsForActiveActivities = allInscriptions
                                                    .Where(i => allActivities.Any(a => a.Id == i.ActividadId && a.EstadoActividad == "i"))
                                                    .Count();
            decimal avgParticipation = (activeSessions > 0)
                                        ? (decimal)inscriptionsForActiveActivities / activeSessions
                                        : 0M;

            response.IsSuccess = true;
            response.Data = new SpecificDashboardMetricsDto
            {
                TotalAttendance = totalAttendance,
                CompletionRate = Math.Round(completionRate, 2),
                ActiveSessions = activeSessions,
                AvgParticipation = Math.Round(avgParticipation, 2)
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

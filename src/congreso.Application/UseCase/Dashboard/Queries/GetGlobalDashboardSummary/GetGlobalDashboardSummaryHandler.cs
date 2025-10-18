using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Dashboard;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;

namespace congreso.Application.UseCase.Dashboard.Queries.GetGlobalDashboardSummary;

internal sealed class GetGlobalDashboardSummaryHandler(IUnitOfWork unitOfWork, HandlerExecutor executor) : IQueryHandler<GetGlobalDashboardSummaryQuery, GlobalDashboardSummaryDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;

    public async Task<BaseResponse<GlobalDashboardSummaryDto>> Handle(GetGlobalDashboardSummaryQuery query, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(query, () => GetGlobalDashboardSummaryAsync(query, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<GlobalDashboardSummaryDto>> GetGlobalDashboardSummaryAsync(GetGlobalDashboardSummaryQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<GlobalDashboardSummaryDto>();

        try
        {
            DateTime? dateCutoff = ParseDateRangeFilter(query.DateRangeFilter);

            // Total Users (not filtered by date range, as it's usually a global count)
            var totalUsers = (await _unitOfWork.User.GetAllAsync()).Count();

            // Active Events
            var allActivities = await _unitOfWork.Actividad.GetAllAsync();
            var filteredActivities = dateCutoff.HasValue
                ? allActivities.Where(a => a.FechaActividad >= dateCutoff.Value)
                : allActivities;
            var activeEvents = filteredActivities
                                .Count(a => a.EstadoActividad == "i" || a.EstadoActividad == "p");

            // Certificates Issued
            var allDiplomas = await _unitOfWork.Diploma.GetAllAsync();
            var filteredDiplomas = dateCutoff.HasValue
                ? allDiplomas.Where(d => d.FechaEmision >= dateCutoff.Value)
                : allDiplomas;
            var certificatesIssued = filteredDiplomas.Count();

            // Average Attendance
            var allInscriptions = await _unitOfWork.Inscripcion.GetAllAsync();
            var filteredInscriptions = dateCutoff.HasValue
                ? allInscriptions.Where(i => i.FechaInscripcion >= dateCutoff.Value)
                : allInscriptions;
            var totalInscriptions = filteredInscriptions.Count();

            var allAttendances = await _unitOfWork.Asistencia.GetAllAsync();
            var filteredAttendances = dateCutoff.HasValue
                ? allAttendances.Where(a => a.FechaRegistro >= dateCutoff.Value)
                : allAttendances;
            var totalAttendances = filteredAttendances.Count();

            decimal averageAttendancePercentage = (totalInscriptions > 0)
                                                    ? (decimal)totalAttendances / totalInscriptions * 100M
                                                    : 0M;

            response.IsSuccess = true;
            response.Data = new GlobalDashboardSummaryDto
            {
                TotalUsers = totalUsers,
                ActiveEvents = activeEvents,
                AverageAttendancePercentage = Math.Round(averageAttendancePercentage, 2),
                CertificatesIssued = certificatesIssued
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

    private DateTime? ParseDateRangeFilter(string? dateRangeFilter)
    {
        if (string.IsNullOrWhiteSpace(dateRangeFilter))
        {
            return null;
        }

        DateTime now = DateTime.UtcNow;
        return dateRangeFilter.ToLower() switch
        {
            "24h" => now.AddHours(-24),
            "7d" => now.AddDays(-7),
            "30d" => now.AddDays(-30),
            "90d" => now.AddDays(-90),
            _ => null, // Invalid filter, no date cutoff
        };
    }
}

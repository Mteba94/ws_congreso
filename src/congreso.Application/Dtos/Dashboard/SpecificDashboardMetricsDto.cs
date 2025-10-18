namespace congreso.Application.Dtos.Dashboard;

public class SpecificDashboardMetricsDto
{
    public int TotalAttendance { get; set; }
    public decimal CompletionRate { get; set; }
    public int ActiveSessions { get; set; }
    public decimal AvgParticipation { get; set; }
}

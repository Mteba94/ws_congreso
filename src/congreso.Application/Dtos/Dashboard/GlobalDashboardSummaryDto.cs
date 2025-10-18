namespace congreso.Application.Dtos.Dashboard;

public class GlobalDashboardSummaryDto
{
    public int TotalUsers { get; set; }
    public int ActiveEvents { get; set; }
    public decimal AverageAttendancePercentage { get; set; }
    public int CertificatesIssued { get; set; }
}

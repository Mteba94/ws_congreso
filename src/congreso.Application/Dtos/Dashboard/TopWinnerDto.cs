namespace congreso.Application.Dtos.Dashboard;

public class TopWinnerDto
{
    public int ActividadId { get; set; }
    public string ActividadTitulo { get; set; } = null!;
    public int? WinnerUserId { get; set; }
    public string? WinnerUserName { get; set; }
    public int? WinnerScore { get; set; }
}

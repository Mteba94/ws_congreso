namespace congreso.Application.Dtos.Asistencias;

public class AttendanceDetailDto
{
    public int Id { get; set; }
    public string ParticipantName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Activity { get; set; } = null!;
    public string ActivityType { get; set; } = null!;
    public DateTime? CheckInTime { get; set; }
    public string Status { get; set; } = null!; // e.g., "Presente", "Ausente"
    public string StudentType { get; set; } = null!;
    public string Institution { get; set; } = null!;
}

namespace congreso.Application.Dtos.User;

public class UserAttendancePercentageDto
{
    public int UserId { get; set; }
    public int TotalActivitiesIniciado { get; set; }
    public int TotalActivitiesAttended { get; set; }
    public decimal AttendancePercentage { get; set; }
}

using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.Asistencias.Commands.MarkAttendance;

public sealed class MarkAttendanceCommand : ICommand<bool>
{
    public int ActividadId { get; set; }
    public string Email { get; set; } = null!;
    // UserId will be implicitly obtained from the current user's context
}
using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.Asistencias.Commands.MarkAttendanceByQrCode;

public sealed record MarkAttendanceByQrCodeCommand(string QrCodeContent, int ActividadId) : ICommand<bool>;

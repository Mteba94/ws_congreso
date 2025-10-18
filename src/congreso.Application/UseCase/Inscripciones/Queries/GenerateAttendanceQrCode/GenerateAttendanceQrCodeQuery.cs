using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.Inscripciones.Queries.GenerateAttendanceQrCode;

public sealed class GenerateAttendanceQrCodeQuery : IQuery<string>
{
    public int ActividadId { get; set; }
}
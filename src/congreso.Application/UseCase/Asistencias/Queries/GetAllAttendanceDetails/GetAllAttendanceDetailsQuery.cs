using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Asistencias;

namespace congreso.Application.UseCase.Asistencias.Queries.GetAllAttendanceDetails;

public sealed record GetAllAttendanceDetailsQuery : IQuery<IEnumerable<AttendanceDetailDto>>;

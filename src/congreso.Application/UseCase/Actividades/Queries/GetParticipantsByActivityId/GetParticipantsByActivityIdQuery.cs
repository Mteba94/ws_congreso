using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Actividades;

namespace congreso.Application.UseCase.Actividades.Queries.GetParticipantsByActivityId;

public sealed record GetParticipantsByActivityIdQuery(int ActividadId) : IQuery<IEnumerable<ParticipantByActivityDto>>;

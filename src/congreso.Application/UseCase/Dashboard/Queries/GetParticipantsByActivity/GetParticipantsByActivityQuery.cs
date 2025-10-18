using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Dashboard;

namespace congreso.Application.UseCase.Dashboard.Queries.GetParticipantsByActivity;

public sealed record GetParticipantsByActivityQuery : IQuery<IEnumerable<ParticipantsByActivityDto>>;

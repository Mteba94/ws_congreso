using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Participantes;

namespace congreso.Application.UseCase.Participantes.Queries.GetAll;

public sealed class GetAllParticipanteQuery : BaseFilters, IQuery<IEnumerable<ParticipantesResponseDto>>
{

}

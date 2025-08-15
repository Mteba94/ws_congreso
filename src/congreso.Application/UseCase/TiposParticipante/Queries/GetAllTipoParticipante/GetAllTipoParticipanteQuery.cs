using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.TipoParticipante;

namespace congreso.Application.UseCase.TiposParticipante.Queries.GetAllTipoParticipante;

public sealed class GetAllTipoParticipanteQuery : BaseFilters, IQuery<IEnumerable<TipoParticipanteResponseDTO>>
{

}

using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.TipoParticipante;

namespace congreso.Application.UseCase.TiposParticipante.Queries.GetById;

public sealed class GetByIdTipoParticipanteQuery : IQuery<TipoParticipanteByIdResponseDTO>
{
    public int tipoParticipanteId { get; set; }
}

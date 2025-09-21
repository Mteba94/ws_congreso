using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Participantes;

namespace congreso.Application.UseCase.Participantes.Queries.GetById;

public sealed class GetByIdParticipanteQuery : IQuery<ParticipanteByIdResponseDto>
{
    public int ParticipanteId { get; set; }
}

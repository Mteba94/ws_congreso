using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.PonenteTags;

namespace congreso.Application.UseCase.PonenteTags.Queries.GetByIdPonente;

public sealed class GetByIdPonentePonenteTagQuery : IQuery<IEnumerable<PonenteTagsByIdResponseDto>>
{
    public int PonenteId { get; set; }
}

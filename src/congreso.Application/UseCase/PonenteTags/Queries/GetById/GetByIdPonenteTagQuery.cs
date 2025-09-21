using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.PonenteTags;

namespace congreso.Application.UseCase.PonenteTags.Queries.GetById;

public sealed class GetByIdPonenteTagQuery : IQuery<PonenteTagsByIdResponseDto>
{
    public int PonenteTagsId { get; set; }
}

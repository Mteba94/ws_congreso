using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Tags;

namespace congreso.Application.UseCase.Tags.Queries.GetById;

public sealed class GetByIdTagQuery : IQuery<TagByIdResponseDto>
{
    public int TagId { get; set; }
}

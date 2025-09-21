using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Commons;

namespace congreso.Application.UseCase.Tags.Queries.GetSelect;

public sealed class GetSelectTagQuery : IQuery<IEnumerable<SelectResponseDto>>
{
}

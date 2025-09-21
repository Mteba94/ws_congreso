using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Tags;

namespace congreso.Application.UseCase.Tags.Queries.GetAll;

public sealed class GetAllTagQuery : BaseFilters, IQuery<IEnumerable<TagsResponseDto>>
{
}

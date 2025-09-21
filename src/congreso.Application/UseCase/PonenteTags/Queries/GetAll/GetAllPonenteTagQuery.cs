using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.PonenteTags;

namespace congreso.Application.UseCase.PonenteTags.Queries.GetAll;

public sealed class GetAllPonenteTagQuery : BaseFilters, IQuery<IEnumerable<PonenteTagsResponseDto>>
{
}

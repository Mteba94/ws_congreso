using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Ponentes;

namespace congreso.Application.UseCase.Ponentes.Queries.GetAll;

public sealed class GetAllPonenteQuery : BaseFilters, IQuery<IEnumerable<PonenteResponseDto>>
{
}

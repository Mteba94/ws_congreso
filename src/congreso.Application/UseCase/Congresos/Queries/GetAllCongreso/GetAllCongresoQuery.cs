using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Congreso;

namespace congreso.Application.UseCase.Congresos.Queries.GetAllCongreso;

public sealed class GetAllCongresoQuery : BaseFilters, IQuery<IEnumerable<CongresoResponseDto>>
{
}

using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Actividades;

namespace congreso.Application.UseCase.Actividades.Queries.GetAll;

public sealed class GetAllActividadesQuery : BaseFilters, IQuery<IEnumerable<ActividadResponseDto>>
{
}

using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.MaterialesActividad;

namespace congreso.Application.UseCase.MaterialesActividad.Queries.GetAll;

public sealed class GetAllMaterialActividadQuery : BaseFilters, IQuery<IEnumerable<MaterialActividadResposeDTO>>
{
}

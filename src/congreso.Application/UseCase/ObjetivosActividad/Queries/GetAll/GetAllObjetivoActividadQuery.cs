using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.ObjetivosActividad;

namespace congreso.Application.UseCase.ObjetivosActividad.Queries.GetAll;

public sealed class GetAllObjetivoActividadQuery : BaseFilters, IQuery<IEnumerable<ObjetivosActividadResponseDto>>
{
}

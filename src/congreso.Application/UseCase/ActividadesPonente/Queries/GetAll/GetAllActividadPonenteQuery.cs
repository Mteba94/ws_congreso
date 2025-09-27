using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.ActividadesPonente;

namespace congreso.Application.UseCase.ActividadesPonente.Queries.GetAll;

public sealed class GetAllActividadPonenteQuery : BaseFilters, IQuery<IEnumerable<ActividadPonenteResponseDto>>
{
}

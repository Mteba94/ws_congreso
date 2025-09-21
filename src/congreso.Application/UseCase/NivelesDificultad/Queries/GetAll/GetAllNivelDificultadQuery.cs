using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.NivelesDificultad;

namespace congreso.Application.UseCase.NivelesDificultad.Queries.GetAll;

public sealed class GetAllNivelDificultadQuery : BaseFilters, IQuery<IEnumerable<NivelDificultdadResponseDto>>
{
}

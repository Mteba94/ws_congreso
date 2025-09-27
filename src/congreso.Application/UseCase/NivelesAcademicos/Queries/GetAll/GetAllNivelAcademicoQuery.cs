using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.NivelesAcademicos;

namespace congreso.Application.UseCase.NivelesAcademicos.Queries.GetAll;

public class GetAllNivelAcademicoQuery : BaseFilters, IQuery<IEnumerable<NivelAcademicoResponseDto>>
{
}

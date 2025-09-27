using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.School;

namespace congreso.Application.UseCase.Schools.Queries.GetAll;

public sealed class GetAllSchoolQuery : BaseFilters, IQuery<IEnumerable<SchoolResponseDto>>
{
}

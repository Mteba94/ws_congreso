using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.School;

namespace congreso.Application.UseCase.Schools.Queries.GetById;

public sealed class GetByIdSchoolQuery : IQuery<SchoolByIdResponseDto>
{
    public int schoolId { get; set; }
}

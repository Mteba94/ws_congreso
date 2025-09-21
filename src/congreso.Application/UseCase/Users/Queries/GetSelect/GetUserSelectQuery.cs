using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Commons;

namespace congreso.Application.UseCase.Users.Queries.GetSelect;

public sealed class GetUserSelectQuery : IQuery<IEnumerable<SelectResponseDto>>
{
}

using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.User;

namespace congreso.Application.UseCase.Users.Queries.GetById;

public sealed class GetByIdUserQuery : IQuery<UserByIdResponseDto>
{
    public int UserId { get; set; }
}

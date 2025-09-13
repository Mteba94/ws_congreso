using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.UserRoles;

namespace congreso.Application.UseCase.UserRoles.Queries.GetById;

public sealed class GetUserRoleByIdQuery : IQuery<UserRoleByIdResponseDto>
{
    public int UserRoleId { get; set; }
}

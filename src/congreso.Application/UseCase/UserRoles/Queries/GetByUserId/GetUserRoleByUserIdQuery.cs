using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Roles;

namespace congreso.Application.UseCase.UserRoles.Queries.GetByUserId;

public sealed class GetUserRoleByUserIdQuery : IQuery<RoleByIdResponseDto>
{
    public int UserId { get; set; }
}

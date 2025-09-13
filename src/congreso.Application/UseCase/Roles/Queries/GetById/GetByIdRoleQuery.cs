using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Roles;

namespace congreso.Application.UseCase.Roles.Queries.GetById;

public sealed class GetByIdRoleQuery : IQuery<RoleByIdResponseDto>
{
    public int RoleId { get; set; }
}

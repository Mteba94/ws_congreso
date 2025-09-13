using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Permisos;

namespace congreso.Application.UseCase.Permisos.Queries.GetPermissionsByRoleId;

public sealed class GetPermissionsByRoleIdQuery : IQuery<IEnumerable<PermissionsByRoleResponseDto>>
{
    public int? RoleId { get; set; }
}
using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Roles;

namespace congreso.Application.UseCase.Roles.Queries.GetAll;

public sealed class GetAllRoleQuery : BaseFilters, IQuery<IEnumerable<RoleResponseDto>>
{
}

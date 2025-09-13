using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.UserRoles;

namespace congreso.Application.UseCase.UserRoles.Queries.GetAll;

public sealed class GetAllUserRoleQuery : BaseFilters, IQuery<IEnumerable<UserRoleResponseDto>>
{
}


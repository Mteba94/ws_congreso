using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.UserRoles.Commands.Update;

public sealed class UpdateUserRoleCommand : ICommand<bool>
{
    public int UserRoleId { get; init; }
    public int UserId { get; init; }
    public int RoleId { get; init; }
    public string? State { get; init; }
}

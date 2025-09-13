using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.UserRoles.Commands.Create;

public class CreateUserRoleCommand : ICommand<bool>
{
    public int UserId { get; init; }
    public int RoleId { get; init; }
    public string? State { get; init; }
}

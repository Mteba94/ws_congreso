using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.UserRoles.Commands.Delete;

public sealed class DeleteUserRoleCommand : ICommand<bool>
{
    public int UserRoleId { get; set; }
}

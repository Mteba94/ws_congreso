using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.Roles.Commands.Delete;

public class DeleteRoleCommand : ICommand<bool>
{
    public int RoleId { get; set; }
}

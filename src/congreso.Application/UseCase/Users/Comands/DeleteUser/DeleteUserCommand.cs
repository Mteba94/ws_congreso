using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.Users.Comands.DeleteUser;

public sealed class DeleteUserCommand : ICommand<bool>
{
    public int UserId { get; set; }
}

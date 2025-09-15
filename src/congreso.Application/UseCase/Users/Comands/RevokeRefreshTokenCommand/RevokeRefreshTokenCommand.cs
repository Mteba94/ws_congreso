using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.Users.Comands.RevokeRefreshTokenCommand;

public sealed class RevokeRefreshTokenCommand : ICommand<bool>
{
    public int UserId { get; set; }
}

using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.Users.Comands.LoginRefreshTokenCommand;

public class LoginRefreshTokenCommand : ICommand<string>
{
    public string? RefreshToken { get; set; }
}

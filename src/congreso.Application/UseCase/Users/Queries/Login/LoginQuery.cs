using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.Users.Queries.Login;

public class LoginQuery : IQuery<string>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.Users.Queries.LoginAdmin;

public class LoginAdminQuery : IQuery<string>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

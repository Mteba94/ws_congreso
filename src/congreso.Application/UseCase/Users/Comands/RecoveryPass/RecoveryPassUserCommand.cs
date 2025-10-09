using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.Users.Comands.RecoveryPass;

public sealed class RecoveryPassUserCommand : ICommand<bool>
{
    public string Email { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public string Codigo { get; set; } = null!;
    public string Purpose { get; set; } = null!;
}

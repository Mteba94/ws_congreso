using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.Users.Comands.UpdateUser;

public sealed class UpdateUserCommand : ICommand<bool>
{
    public int UserId { get; set; }
    public string Pnombre { get; set; } = null!;
    public string? Snombre { get; set; }
    public string Papellido { get; set; } = null!;
    public string? Sapellido { get; set; }
    public string Email { get; set; } = null!;
    public string? Telefono { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public int TipoIdentificacionId { get; set; }
    public string? NumeroIdentificacion { get; set; }

}

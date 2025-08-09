using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.Users.Comands.CreateUser;

public sealed class CreateUserCommand : ICommand<bool>
{
    public string Pnombre { get; set; } = null!;
    public string? Snombre { get; set; }
    public string Papellido { get; set; } = null!;
    public string? Sapellido { get; set; }
    public int TipoParticipanteId { get; set; }
    public string Email { get; set; } = null!;
    public string? Telefono { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public int TipoIdentificacionId { get; set; }
    public string? NumeroIdentificacion { get; set; }
    public int NivelAcademicoId { get; set; }
    public int? Semestre { get; set; }
    public string Password { get; set; } = null!;
}

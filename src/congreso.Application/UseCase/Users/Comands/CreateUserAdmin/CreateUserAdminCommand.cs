namespace congreso.Application.UseCase.Users.Comands.CreateUserAdmin;

public sealed class CreateUserAdminCommand
{
    public string Pnombre { get; set; } = null!;
    public string? Snombre { get; set; }
    public string Papellido { get; set; } = null!;
    public string? Sapellido { get; set; }
    public string Email { get; set; } = null!;
    public string? Telefono { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    public int TipoIdentificacionId { get; set; }
    public string? NumeroIdentificacion { get; set; }
    public string Password { get; set; } = null!;
}

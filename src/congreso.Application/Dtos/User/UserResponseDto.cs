namespace congreso.Application.Dtos.User;

public class UserResponseDto
{
    public int UserId { get; set; }
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
    public string? NivelAcademico { get; set; }
    public int Semestre { get; set; }
    public int Estado { get; set; }
    public string? EstadoDescripcion { get; set; }
}

public class UserByIdResponseDto
{
    public int UserId { get; set; }
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
    public string? NivelAcademico { get; set; }
    public int Semestre { get; set; }
}

public class  TFAUserResponseDTO
{
    //clase para validacion de Two Factor Auth
    public string Email { get; set; } = null!;
    public string codigo { get; set; } = null!;
}
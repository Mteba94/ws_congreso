namespace congreso.Application.Dtos.Participantes;

public class ParticipantesResponseDto
{
    public int ParticipanteId { get; set; }
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

public class ParticipanteByIdResponseDto
{
    public int ParticipanteId { get; set; }
    public string Pnombre { get; set; } = null!;
    public string? Snombre { get; set; }
    public string Papellido { get; set; } = null!;
    public string? Sapellido { get; set; }
    public string? Imagen { get; set; }
    public int TipoParticipanteId { get; set; }
    public string Email { get; set; } = null!;
    public string? Telefono { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public int TipoIdentificacionId { get; set; }
    public string? NumeroIdentificacion { get; set; }
    public int? SchoolId { get; set; }
    public string? NivelAcademico { get; set; }
    public int Semestre { get; set; }
}

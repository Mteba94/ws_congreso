namespace congreso.Application.Dtos.Inscripciones;

public class InscripcionesResponseDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ActividadId { get; set; }
    public DateTime FechaInscripcion { get; set; }
    public int? Puntaje { get; set; }
    public bool? EsGanador { get; set; }
}

public class InscripcionesByUserDTO
{
    public int InscripcionId { get; set; }
    public int ActividadId { get; set; }
    public int? Puntaje { get; set; }
    public bool? EsGanador { get; set; }
}

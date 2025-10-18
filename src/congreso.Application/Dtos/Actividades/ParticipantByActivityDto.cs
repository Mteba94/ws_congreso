namespace congreso.Application.Dtos.Actividades;

public class ParticipantByActivityDto
{
    public int InscripcionId { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; } = null!;
    public string UserEmail { get; set; } = null!;
    public DateTime FechaInscripcion { get; set; }
    public int? Puntaje { get; set; }
    public bool? EsGanador { get; set; }
}

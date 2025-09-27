namespace congreso.Application.Dtos.ActividadesPonente;

public class ActividadPonenteResponseDto
{
    public int ActividadPonenteId { get; set; }
    public int ActividadId { get; set; }
    public int PonenteId { get; set; }
    public int Estado {  get; set; }
    public string? EstadoDescripcion { get; set; }
}

namespace congreso.Application.Dtos.ObjetivosActividad;

public class ObjetivosActividadResponseDto
{
    public int ObjetivoId { get; set; }
    public int ActividadId { get; set; }
    public string ObjetivoDescripcion { get; set; } = null!;
    public int Estado {  get; set; }
    public string? EstadoDescripcion { get; set; }
}

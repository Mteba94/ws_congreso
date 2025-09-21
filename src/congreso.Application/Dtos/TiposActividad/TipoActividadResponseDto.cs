namespace congreso.Application.Dtos.TiposActividad;

public class TipoActividadResponseDto
{
    public int TipoActividadId { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Descipcion { get; set; }
    public int Estado { get; set; }
    public string? EstadoDescripcion { get; set; }
}

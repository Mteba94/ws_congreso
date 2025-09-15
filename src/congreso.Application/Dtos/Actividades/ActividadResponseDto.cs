namespace congreso.Application.Dtos.Actividades;

public class ActividadResponseDto
{
    public int ActividadId { get; set; }
    public int CongresoId { get; set; }
    public string Titulo { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public int TipoActividadId { get; set; }
    public DateTime FechaActividad { get; set; }
    public DateTime HoraInicio { get; set; }
    public DateTime HoraFin { get; set; }
    public int CuposDisponibles { get; set; }
    public int CuposTotales { get; set; }
    public string? Ubicacion { get; set; }
    public string? RequisitosPrevios { get; set; }
    public int NivelDificultadId { get; set; }
    public int Estado { get; set; }
    public string? EstadoDescripcion { get; set; }
}

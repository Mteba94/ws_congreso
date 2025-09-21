namespace congreso.Application.Dtos.NivelesDificultad;

public class NivelDificultdadResponseDto
{
    public int NivelId { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
    public int Estado { get; set; }
    public string? EstadoDescripcion { get; set; }
}

public class NivelDificultdadByIdResponseDto
{
    public int NivelId { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
}

namespace congreso.Application.Dtos.NivelesAcademicos;

public class NivelAcademicoResponseDto
{
    public int NivelId { get; set; }
    public string nombreNivel { get; set; } = null!;
    public string? descripcionNivel { get; set; }
    public int Estado { get; set; }
    public string? EstadoDescripcion { get; set; }

}

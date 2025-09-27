namespace congreso.Application.Dtos.School;

public class SchoolResponseDto
{
    public int SchoolId { get; set; }
    public string SchoolName { get; set; } = null!;
    public int Estado {  get; set; }
    public string? EstadoDescripcion { get; set; }

}

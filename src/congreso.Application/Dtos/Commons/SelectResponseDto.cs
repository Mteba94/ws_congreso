namespace congreso.Application.Dtos.Commons;

public record SelectResponseDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
}

namespace congreso.Application.Dtos.Commons;

public record SelectResponseDto
{
    public int Id { get; init; }
    public string? Nombre { get; init; }
    public string? Descripcion { get; init; }
}

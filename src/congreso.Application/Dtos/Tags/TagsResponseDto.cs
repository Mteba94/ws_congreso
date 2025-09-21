namespace congreso.Application.Dtos.Tags;

public class TagsResponseDto
{
    public int TagId { get; set; }
    public string Nombre { get; set; } = null!;
    public int Estado { get; set; }
    public string? EstadoDescripcion { get; set; }
}

public class TagByIdResponseDto
{
    public int TagId { get; set; }
    public string Nombre { get; set; } = null!;
}

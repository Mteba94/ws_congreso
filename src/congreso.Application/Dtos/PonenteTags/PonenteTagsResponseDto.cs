namespace congreso.Application.Dtos.PonenteTags;

public class PonenteTagsResponseDto
{
    public int PonenteTagId { get; set; }
    public int TagId { get; set; }
    public int PonenteId { get; set; }
    public int Estado { get; set; }
    public string? EstadoDescription { get; set; }
}

public class PonenteTagsByIdResponseDto
{
    public int PonenteTagId { get; set; }
    public int TagId { get; set; }
    public int PonenteId { get; set; }
}

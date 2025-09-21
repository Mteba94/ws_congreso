namespace congreso.Application.Dtos.Ponentes;

public class PonenteResponseDto
{
    public int PonenteId {  get; set; }
    public string NombrePonente { get; set; } = null!;
    public string ApellidoPonente { get; set; } = null!;
    public string? TituloPonente { get; set; }
    public string? EmpresaPonente { get; set; }
    public string? BioPonente { get; set; }
    public string? imagePonente { get; set; }
    public int Estado { get; set; }
    public string? EstadoDescripcion { get; set; }
}

public class PonenteByIdResponseDto
{
    public int PonenteId { get; set; }
    public string NombrePonente { get; set; } = null!;
    public string ApellidoPonente { get; set; } = null!;
    public string? TituloPonente { get; set; }
    public string? EmpresaPonente { get; set; }
    public string? BioPonente { get; set; }
    public string? imagePonente { get; set; }
}

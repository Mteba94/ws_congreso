namespace congreso.Application.Dtos.TipoIdentificacion;

public class TipoIdentificacionResponseDTO
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
    public int Estado { get; set; }
    public string? EstadoDescipcion { get; set; }
}

public class TipoIdentificacionByIdResponseDTO
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
}

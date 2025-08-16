namespace congreso.Application.Dtos.TipoParticipante;

public class TipoParticipanteResponseDTO
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
    public int Estado { get; set; }
    public string? EstadoDescipcion { get; set; }
}

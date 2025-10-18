namespace congreso.Application.Dtos.MaterialesActividad;

public class MaterialActividadResposeDTO
{
    public int MaterialId { get; set; }
    public int ActividadId { get; set; }
    public string MaterialDesc { get; set; } = null!;
    public int Estado {  get; set; }
    public string? EstadoDescripcion { get; set; }
}

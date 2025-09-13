namespace congreso.Domain.Entities;

public class Role : CatalogoEntity
{
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
}

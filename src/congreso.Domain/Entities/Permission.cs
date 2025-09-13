namespace congreso.Domain.Entities;

public class Permission : BaseEntity
{
    public string Nombre { get; init; } = null!;
    public string? Descripcion { get; init; }
    public string Slug { get; set; } = null!;
    public int MenuId { get; set; }
}

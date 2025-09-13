namespace congreso.Application.Dtos.Roles;

public class RoleResponseDto
{
    public int RoleId { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
    public int? Estado { get; set; }
    public string? EstadoDescripcion { get; set; }
}

public class RoleByIdResponseDto
{
    public int RoleId { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
    public int? Estado { get; set; }
}

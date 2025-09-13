namespace congreso.Application.Dtos.UserRoles;

public class UserRoleResponseDto
{
    public int UserRoleId { get; set; }
    public string User { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string? Estado { get; set; } = null!;
    public string? EstadoDescripcion { get; set; }
}

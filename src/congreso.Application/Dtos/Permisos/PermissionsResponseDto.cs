namespace congreso.Application.Dtos.Permisos;

public class PermissionsResponseDto
{
    public int PermissionId { get; set; }
    public string PermissionName { get; set; } = null!;
    public string PermissionDescription { get; set; } = null!;
    public bool Selected { get; set; }
}

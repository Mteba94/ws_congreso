using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.Roles.Commands.Create;

public sealed class CreateRoleCommand : ICommand<bool>
{
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
    public int? Estado { get; set; }
    public ICollection<PermissionRequestDto> Permisos { get; set; } = null!;
    public ICollection<MenuRequestDto> Menus { get; set; } = null!;
}

public class PermissionRequestDto
{
    public int permisoId { get; set; }
    public string permisoNombre { get; set; } = null!;
    public string permisoDescripcion { get; set; } = null!;
    public bool Seleccionado { get; set; }
}

public class MenuRequestDto
{
    public int MenuId { get; set; }
}

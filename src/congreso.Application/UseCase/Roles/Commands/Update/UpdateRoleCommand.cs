using congreso.Application.Abstractions.Messaging;
using congreso.Application.UseCase.Roles.Commands.Create;

namespace congreso.Application.UseCase.Roles.Commands.Update;

public sealed class UpdateRoleCommand : ICommand<bool>
{
    public int RoleId { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
    public int? Estado { get; set; }
    public ICollection<PermissionUpdateDto> Permisos { get; set; } = null!;
    public ICollection<MenuUpdateDto> Menus { get; set; } = null!;
}

public class PermissionUpdateDto
{
    public int permisoId { get; set; }
    public string permisoNombre { get; set; } = null!;
    public string permisoDescripcion { get; set; } = null!;
    public bool Seleccionado { get; set; }
}

public class MenuUpdateDto
{
    public int MenuId { get; set; }
}

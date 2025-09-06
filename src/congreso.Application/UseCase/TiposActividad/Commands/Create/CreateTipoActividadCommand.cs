using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.TiposActividad.Commands.Create;

public sealed class CreateTipoActividadCommand : ICommand<bool>
{
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
}

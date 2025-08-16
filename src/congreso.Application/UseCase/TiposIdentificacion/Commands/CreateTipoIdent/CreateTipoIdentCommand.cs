using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.TiposIdentificacion.Commands.CreateTipoIdent;

public sealed class CreateTipoIdentCommand : ICommand<bool>
{
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
}

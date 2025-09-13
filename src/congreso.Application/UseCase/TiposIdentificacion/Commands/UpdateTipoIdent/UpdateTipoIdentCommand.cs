using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.TiposIdentificacion.Commands.UpdateTipoIdent;

public sealed class UpdateTipoIdentCommand : ICommand<bool>
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
}

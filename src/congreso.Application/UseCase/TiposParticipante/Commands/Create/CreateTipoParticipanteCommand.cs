using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.TiposParticipante.Commands.Create;

public sealed class CreateTipoParticipanteCommand : ICommand<bool>
{
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
}

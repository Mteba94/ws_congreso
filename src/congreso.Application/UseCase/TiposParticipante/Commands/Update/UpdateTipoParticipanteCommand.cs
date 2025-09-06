using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.TiposParticipante.Commands.Update;

public sealed class UpdateTipoParticipanteCommand : ICommand<bool>
{
    public int tipoParticipanteId { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
}

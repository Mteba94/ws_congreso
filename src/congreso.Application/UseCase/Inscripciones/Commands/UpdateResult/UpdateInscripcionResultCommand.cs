using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.Inscripciones.Commands.UpdateResult;

public sealed class UpdateInscripcionResultCommand : ICommand<bool>
{
    public int InscripcionId { get; set; }
    public int? Puntaje { get; set; }
    public bool? EsGanador { get; set; }
}
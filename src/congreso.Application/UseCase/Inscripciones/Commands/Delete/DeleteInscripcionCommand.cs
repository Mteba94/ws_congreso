using congreso.Application.Abstractions.Messaging;
using System.Windows.Input;

namespace congreso.Application.UseCase.Inscripciones.Commands.Delete;

public sealed class DeleteInscripcionCommand : ICommand<bool>
{
    public int InscripcionId { get; set; }
}

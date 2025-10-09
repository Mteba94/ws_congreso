using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.Inscripciones.Commands.Create;

public sealed class CreateInscripcionCommand : ICommand<bool>
{
    public int IdUsuario { get; set; }
    public int IdActividad { get; set; }
}

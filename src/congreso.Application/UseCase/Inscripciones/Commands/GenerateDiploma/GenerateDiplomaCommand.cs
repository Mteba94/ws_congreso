using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.Inscripciones.Commands.GenerateDiploma;

public sealed class GenerateDiplomaCommand : ICommand<string>
{
    public int InscripcionId { get; set; }
    public string? NombrePersonalizado { get; set; }
}
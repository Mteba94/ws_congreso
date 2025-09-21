using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.NivelesDificultad.Commands.Create;

public sealed class CreateNivelDificultadCommand : ICommand<bool>
{
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
}

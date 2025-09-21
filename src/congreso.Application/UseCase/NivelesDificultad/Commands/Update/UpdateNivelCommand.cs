using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.NivelesDificultad.Commands.Update;

public sealed class UpdateNivelCommand : ICommand<bool>
{
    public int nivelId { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
}

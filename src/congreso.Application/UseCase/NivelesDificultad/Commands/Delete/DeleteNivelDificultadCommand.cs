using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.NivelesDificultad.Commands.Delete;

public sealed class DeleteNivelDificultadCommand : ICommand<bool>
{
    public int NivelId { get; set; }
}

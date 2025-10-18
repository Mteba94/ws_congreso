using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.Actividades.Commands.Delete;

public sealed class DeleteActividadCommand : ICommand<bool>
{
    public int ActividadId { get; set; }
}
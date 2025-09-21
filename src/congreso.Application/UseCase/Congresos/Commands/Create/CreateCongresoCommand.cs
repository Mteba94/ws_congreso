using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.Congresos.Commands.Create;

public sealed class CreateCongresoCommand : ICommand<bool>
{
    public string Nombre { get; set; } = null!;
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
}

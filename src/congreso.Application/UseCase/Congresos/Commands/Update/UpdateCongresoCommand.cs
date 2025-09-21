using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.Congresos.Commands.Update;

public sealed class UpdateCongresoCommand : ICommand<bool>
{
    public int congresoId { get; set; }
    public string Nombre { get; set; } = null!;
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
}

using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.Actividades.Commands.UpdateEstadoActividad;

public sealed record UpdateEstadoActividadCommand(int ActividadId, string NewEstadoActividad) : ICommand<bool>;

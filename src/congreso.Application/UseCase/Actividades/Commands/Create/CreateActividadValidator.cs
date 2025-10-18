using congreso.Application.Interfaces.Services;
using FluentValidation;

namespace congreso.Application.UseCase.Actividades.Commands.Create;

public class CreateActividadValidator : AbstractValidator<CreateActividadCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateActividadValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(command => command)
            .MustAsync(BeWithinCongresoSchedule)
            .WithMessage("La fecha y hora de la actividad deben estar dentro del rango de fechas del congreso.");

        // Aquí puedes añadir otras reglas más simples
        RuleFor(c => c.Titulo)
            .NotEmpty().WithMessage("El título no puede estar vacío.")
            .MaximumLength(200).WithMessage("El título no puede exceder los 200 caracteres.");

        RuleFor(c => c.CuposTotal)
            .GreaterThan(0).WithMessage("Los cupos totales deben ser mayores a cero.");
    }

    /// <summary>
    /// Método de validación asíncrono que verifica si la fecha y hora de la actividad
    /// están dentro del período de su congreso asociado.
    /// </summary>
    private async Task<bool> BeWithinCongresoSchedule(CreateActividadCommand command, CancellationToken cancellationToken)
    {
        // 1. Obtener el congreso desde la base de datos.
        var congreso = await _unitOfWork.Congreso.GetByIdAsync(command.CongresoId);

        if (congreso is null)
        {
            // Si el congreso no existe, la actividad no puede ser válida.
            // Puedes añadir un RuleFor(c => c.CongresoId) específico para este caso si quieres un mensaje más claro.
            return false;
        }

        // 2. Construir los DateTime de inicio y fin de la actividad.
        // Combinamos la fecha de la actividad con la hora de inicio y fin.
        var actividadStartDateTime = command.Fecha.Date
            .Add(command.HoraInicio.ToTimeSpan()); // Usando .ToTimeSpan() en .NET 6+

        var actividadEndDateTime = command.Fecha.Date
            .Add(command.HoraFin.ToTimeSpan());

        // 3. Realizar la comparación.
        // La actividad es válida si su inicio es después del inicio del congreso
        // y su fin es antes del fin del congreso.
        return actividadStartDateTime >= congreso.FechaInicio &&
               actividadEndDateTime <= congreso.FechaFin;
    }
}

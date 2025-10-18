using congreso.Application.UseCase.Actividades.Commands.Update;
using FluentValidation;

namespace congreso.Application.UseCase.Actividades.Commands.Update;

public class UpdateActividadValidator : AbstractValidator<UpdateActividadCommand>
{
    public UpdateActividadValidator()
    {
        RuleFor(x => x.ActividadId)
            .GreaterThan(0).WithMessage("El Id de la actividad debe ser mayor a 0.");

        RuleFor(x => x.Titulo)
            .NotEmpty().WithMessage("El título no puede estar vacío.")
            .MaximumLength(100).WithMessage("El título no puede exceder los 100 caracteres.");

        RuleFor(x => x.Descripcion)
            .NotEmpty().WithMessage("La descripción no puede estar vacía.");

        RuleFor(x => x.TipoActividadId)
            .GreaterThan(0).WithMessage("El Id del tipo de actividad debe ser mayor a 0.");

        RuleFor(x => x.FechaActividad)
            .NotEmpty().WithMessage("La fecha de la actividad no puede estar vacía.");

        RuleFor(x => x.HoraInicio)
            .NotEmpty().WithMessage("La hora de inicio no puede estar vacía.");

        RuleFor(x => x.HoraFin)
            .NotEmpty().WithMessage("La hora de fin no puede estar vacía.")
            .GreaterThan(x => x.HoraInicio).WithMessage("La hora de fin debe ser posterior a la hora de inicio.");

        RuleFor(x => x.CuposTotales)
            .GreaterThan(0).WithMessage("Los cupos totales deben ser mayores a 0.");

        RuleFor(x => x.NivelDificultadId)
            .GreaterThan(0).WithMessage("El Id del nivel de dificultad debe ser mayor a 0.");

        RuleFor(x => x.Orden)
            .GreaterThanOrEqualTo(0).WithMessage("El orden no puede ser negativo.");
    }
}
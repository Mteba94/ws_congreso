using congreso.Application.UseCase.Actividades.Commands.Delete;
using FluentValidation;

namespace congreso.Application.UseCase.Actividades.Commands.Delete;

public class DeleteActividadValidator : AbstractValidator<DeleteActividadCommand>
{
    public DeleteActividadValidator()
    {
        RuleFor(x => x.ActividadId)
            .GreaterThan(0).WithMessage("El Id de la actividad debe ser mayor a 0.");
    }
}
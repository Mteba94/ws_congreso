using congreso.Application.UseCase.Inscripciones.Commands.UpdateResult;
using FluentValidation;

namespace congreso.Application.UseCase.Inscripciones.Commands.UpdateResult;

public class UpdateInscripcionResultValidator : AbstractValidator<UpdateInscripcionResultCommand>
{
    public UpdateInscripcionResultValidator()
    {
        RuleFor(x => x.InscripcionId)
            .GreaterThan(0).WithMessage("El Id de la inscripción debe ser mayor a 0.");

        // Optional: Add validation for Puntaje if there's a specific range or condition
        // RuleFor(x => x.Puntaje)
        //     .InclusiveBetween(0, 100).WithMessage("El puntaje debe estar entre 0 y 100.");
    }
}
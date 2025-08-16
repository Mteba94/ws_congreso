using congreso.Application.Interfaces.Services;
using FluentValidation;

namespace congreso.Application.UseCase.TiposIdentificacion.Commands.CreateTipoIdent;

public class CreateTipoIdentValidator : AbstractValidator<CreateTipoIdentCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateTipoIdentValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.Nombre)
            .NotNull().WithMessage("El nombre es obligatorio.")
            .NotEmpty().WithMessage("El nombre no puede estar vacío.")
            .MaximumLength(50).WithMessage("El nombre no puede exceder los 50 caracteres.");
    }
}

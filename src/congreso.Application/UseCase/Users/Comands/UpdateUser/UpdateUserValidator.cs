using congreso.Application.Interfaces.Services;
using FluentValidation;

namespace congreso.Application.UseCase.Users.Comands.UpdateUser;

public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    public UpdateUserValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.Pnombre)
            .NotNull().WithMessage("El primer nombre es obligatorio.")
            .NotEmpty().WithMessage("El primer nombre no puede estar vacío.")
            .MaximumLength(50).WithMessage("El primer nombre no puede exceder los 50 caracteres.");

        RuleFor(x => x.Snombre)
            .MaximumLength(50).WithMessage("El segundo nombre no puede exceder los 50 caracteres.");

        RuleFor(x => x.Papellido)
            .NotNull().WithMessage("El primer apellido es obligatorio.")
            .NotEmpty().WithMessage("El primer apellido no puede estar vacío.")
            .MaximumLength(50).WithMessage("El primer apellido no puede exceder los 50 caracteres.");

        RuleFor(x => x.Sapellido)
            .MaximumLength(50).WithMessage("El segundo apellido no puede exceder los 50 caracteres.");

        RuleFor(x => x.TipoParticipanteId)
            .GreaterThan(0).WithMessage("El tipo de participante es obligatorio.");

        RuleFor(x => x.Email)
            .NotNull().WithMessage("El correo electrónico es obligatorio.")
            .NotEmpty().WithMessage("El correo electrónico no puede estar vacío.")
            .EmailAddress().WithMessage("El correo electrónico no es válido.")
            .MaximumLength(100).WithMessage("El correo electrónico no puede exceder los 100 caracteres.");
            //.MustAsync(async (email, cancellation) => await IsEmailUnique(email)).WithMessage("El correo electrónico ya está registrado.");

        RuleFor(x => x.NumeroIdentificacion)
            .NotEmpty().WithMessage("El número de identificación es obligatorio.")
            .When(x => IsOver18(x.FechaNacimiento));
            //.MustAsync(async (identificacion, cancellation) => await uniqueIdentificacion(identificacion!)).WithMessage("El número de identificación ya está registrado.");
    }

    private async Task<bool> IsEmailUnique(string email)
    {
        var user = await _unitOfWork.User.UserByEmailAsync(email);
        return user == null;
    }

    private bool IsOver18(DateTime fechaNacimiento)
    {
        var today = DateTime.Today;
        var age = today.Year - fechaNacimiento.Year;

        // Restamos un año si el cumpleaños aún no ha pasado este año
        if (fechaNacimiento.Date > today.AddYears(-age))
        {
            age--;
        }

        return age >= 18;
    }

    private async Task<bool> uniqueIdentificacion(string identificacion)
    {
        var user = await _unitOfWork.User.UserByIdentificacion(identificacion);

        return user == null;
    }
}

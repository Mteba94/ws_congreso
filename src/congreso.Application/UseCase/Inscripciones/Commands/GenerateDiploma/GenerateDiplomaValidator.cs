using congreso.Application.Interfaces.Services;
using congreso.Application.UseCase.Inscripciones.Commands.GenerateDiploma;
using FluentValidation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace congreso.Application.UseCase.Inscripciones.Commands.GenerateDiploma;

public class GenerateDiplomaValidator : AbstractValidator<GenerateDiplomaCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public GenerateDiplomaValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.InscripcionId)
            .GreaterThan(0).WithMessage("El Id de la inscripción debe ser mayor a 0.")
            .MustAsync(InscripcionExists).WithMessage("La inscripción especificada no existe.");

        When(x => !string.IsNullOrWhiteSpace(x.NombrePersonalizado), () =>
        {
            RuleFor(x => x.NombrePersonalizado)
                .MustAsync(BeValidCustomizedName).WithMessage("El nombre personalizado debe contener el primer nombre y el apellido del usuario.");
        });
    }

    private async Task<bool> InscripcionExists(int inscripcionId, CancellationToken cancellationToken)
    {
        var inscripcion = await _unitOfWork.Inscripcion.GetByIdAsync(inscripcionId);
        return inscripcion != null;
    }

    private async Task<bool> BeValidCustomizedName(GenerateDiplomaCommand command, string nombrePersonalizado, CancellationToken cancellationToken)
    {
        var inscripcion = await _unitOfWork.Inscripcion.GetByIdAsync(command.InscripcionId);
        if (inscripcion == null) return false; // Should be caught by InscripcionExists rule

        var user = await _unitOfWork.User.GetByIdAsync(inscripcion.UserId);
        if (user == null) return false; // User not found

        // Assuming user.Nombre contains first name and user.Apellido contains last name
        // Or user.NombreCompleto contains the full name
        // For this example, let's assume user.Nombre is the full name or first name, and user.Apellido is the last name.
        // You might need to adjust this based on your actual User entity properties.

        string userFullName = $"{user.Pnombre} {user.Papellido}"; // Adjust based on your User entity

        // Check if the customized name contains at least the first name and last name
        // This is a basic check and can be made more robust if needed.
        return nombrePersonalizado.Contains(user.Pnombre, StringComparison.OrdinalIgnoreCase) &&
               nombrePersonalizado.Contains(user.Papellido, StringComparison.OrdinalIgnoreCase);
    }
}
using congreso.Application.Interfaces.Services;
using FluentValidation;

namespace congreso.Application.UseCase.Actividades.Commands.UpdateEstadoActividad;

public class UpdateEstadoActividadValidator : AbstractValidator<UpdateEstadoActividadCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateEstadoActividadValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.ActividadId)
            .GreaterThan(0).WithMessage("El Id de la actividad debe ser mayor a 0.")
            .MustAsync(async (idActividad, cancellation) =>
            {
                var actividad = await _unitOfWork.Actividad.GetByIdAsync(idActividad);
                return actividad != null;
            })
            .WithMessage("La actividad especificada no existe.");

        RuleFor(x => x.NewEstadoActividad)
            .NotEmpty().WithMessage("El nuevo estado de la actividad no puede ser vacío.")
            .Must(BeAValidEstadoActividad).WithMessage("El estado de actividad no es válido. Los valores permitidos son 'p' (Pendiente), 'i' (Iniciado), 'f' (Finalizado).");
    }

    private bool BeAValidEstadoActividad(string estado)
    {
        return estado == "p" || estado == "i" || estado == "f";
    }
}

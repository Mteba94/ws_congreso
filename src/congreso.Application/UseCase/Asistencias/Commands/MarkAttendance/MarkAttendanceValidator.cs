using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using FluentValidation;

namespace congreso.Application.UseCase.Asistencias.Commands.MarkAttendance;

public class MarkAttendanceValidator : AbstractValidator<MarkAttendanceCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public MarkAttendanceValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.ActividadId)
            .GreaterThan(0).WithMessage("El Id de la actividad debe ser mayor a 0.")
            .MustAsync(async (idActividad, cancellation) =>
            {
                var actividad = await _unitOfWork.Actividad.GetByIdAsync(idActividad);
                return actividad != null && actividad.EstadoActividad == ActividadEstado.Iniciado;
            })
            .WithMessage("No se puede registrar asistencia a una actividad que no ha iniciado.");
    }
}
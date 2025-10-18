using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using FluentValidation;

namespace congreso.Application.UseCase.Inscripciones.Commands.Create;

public class CreateInscripcionValidator : AbstractValidator<CreateInscripcionCommand>
{
    private readonly IUnitOfWork _unitOfWork;

            public CreateInscripcionValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
    
            RuleFor(x => x.IdActividad)
                .MustAsync(async (idActividad, cancellation) =>
                {
                    var actividad = await _unitOfWork.Actividad.GetByIdAsync(idActividad);
                    return actividad == null || actividad.EstadoActividad != ActividadEstado.Finalizado;
                })
                .WithMessage("No es posible inscribirse a esta actividad porque ha finalizado.");
        }
    
        private async Task<bool> validateQuota(int actividadId)
        {
            var cupo = await _unitOfWork.Inscripcion.ValidateQuota(actividadId);
    
            return cupo;
        }
    }

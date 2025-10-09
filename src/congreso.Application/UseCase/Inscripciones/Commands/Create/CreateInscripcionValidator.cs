using congreso.Application.Interfaces.Services;
using FluentValidation;

namespace congreso.Application.UseCase.Inscripciones.Commands.Create;

public class CreateInscripcionValidator : AbstractValidator<CreateInscripcionCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateInscripcionValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;


    }

    private async Task<bool> validateQuota(int actividadId)
    {
        var cupo = await _unitOfWork.Inscripcion.ValidateQuota(actividadId);

        return cupo;
    }
}

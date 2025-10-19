using congreso.Application.Interfaces.Services;
using FluentValidation;
using System.Text.RegularExpressions;

namespace congreso.Application.UseCase.Asistencias.Commands.MarkAttendanceByQrCode;

public class MarkAttendanceByQrCodeValidator : AbstractValidator<MarkAttendanceByQrCodeCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public MarkAttendanceByQrCodeValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        //RuleFor(x => x.ActividadId)
        //    .GreaterThan(0).WithMessage("El Id de la actividad debe ser mayor a 0.")
        //    .MustAsync(async (idActividad, cancellation) =>
        //    {
        //        var actividad = await _unitOfWork.Actividad.GetByIdAsync(idActividad);
        //        return actividad != null;
        //    })
        //    .WithMessage("La actividad especificada no existe.");

        RuleFor(x => x.QrCodeContent)
            .NotEmpty().WithMessage("El contenido del código QR no puede ser vacío.")
            .Must(BeAValidQrCodeContent).WithMessage("El formato del código QR es inválido. Se espera 'user:ID,email:EMAIL'.");
    }

    private bool BeAValidQrCodeContent(string qrCodeContent)
    {
        return Regex.IsMatch(qrCodeContent, @"^user:(\d+),email:([^@]+@[^\.]+\.[^\.]+)$");
    }
}

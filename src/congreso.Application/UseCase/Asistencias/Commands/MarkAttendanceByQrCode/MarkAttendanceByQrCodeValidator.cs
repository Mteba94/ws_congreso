using FluentValidation;
using System.Text.RegularExpressions;

namespace congreso.Application.UseCase.Asistencias.Commands.MarkAttendanceByQrCode;

public class MarkAttendanceByQrCodeValidator : AbstractValidator<MarkAttendanceByQrCodeCommand>
{
    public MarkAttendanceByQrCodeValidator()
    {
        RuleFor(x => x.QrCodeContent)
            .NotEmpty().WithMessage("El contenido del código QR no puede ser vacío.")
            .Must(BeAValidQrCodeContent).WithMessage("El formato del código QR es inválido.");
    }

    private bool BeAValidQrCodeContent(string qrCodeContent)
    {
        return Regex.IsMatch(qrCodeContent, @"^inscription:(\d+)$");
    }
}

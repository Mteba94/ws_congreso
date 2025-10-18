using congreso.Application.Interfaces.Services;
using QRCoder;
using System.Drawing;
using System.IO;

namespace congreso.Infrastructure.Services;

public class QrGeneratorService : IQRGeneratorService
{
    public string GenerateQrCodeAsBase64(string content)
    {
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
        PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
        byte[] qrCodeAsPngBytes = qrCode.GetGraphic(20); // 20 pixels per module
        return Convert.ToBase64String(qrCodeAsPngBytes);
    }
}

using congreso.Application.Interfaces.Services;
using QRCoder;
using System.IO;
using System.Threading.Tasks;

namespace congreso.Infrastructure.Services;

public class QrCodeService : IQrCodeService
{
    public Task<string> GenerateQrCodeAsBase64Async(string data)
    {
        using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
        using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q))
        using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
        {
            byte[] qrCodeBytes = qrCode.GetGraphic(20);
            return Task.FromResult(Convert.ToBase64String(qrCodeBytes));
        }
    }
}
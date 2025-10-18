namespace congreso.Application.Interfaces.Services;

public interface IQRGeneratorService
{
    string GenerateQrCodeAsBase64(string content);
}

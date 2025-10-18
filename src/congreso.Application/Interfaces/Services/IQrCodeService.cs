using System.Threading.Tasks;

namespace congreso.Application.Interfaces.Services;

public interface IQrCodeService
{
    Task<string> GenerateQrCodeAsBase64Async(string data);
}
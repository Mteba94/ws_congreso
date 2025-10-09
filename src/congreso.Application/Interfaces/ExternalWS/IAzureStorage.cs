using Microsoft.AspNetCore.Http;

namespace congreso.Application.Interfaces.ExternalWS;

public interface IAzureStorage
{
    Task<string> SaveFile(string container, IFormFile file);
    Task<string> EditarFile(string container, IFormFile file, string route);
    Task RemoveFile(string route, string container);
}
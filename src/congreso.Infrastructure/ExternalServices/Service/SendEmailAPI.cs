using congreso.Application.Interfaces.ExternalWS;
using logging.Interface;
using System.Text;
using System.Text.Json;

namespace congreso.Infrastructure.ExternalServices.Service;

public class SendEmailAPI(HttpClient httpClient, IFileLogger fileLogger) : ISendEmailAPI
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly IFileLogger _fileLogger = fileLogger;

    public async Task<string> GetDataAsync(string endpoint)
    {
        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<T> PostDataAsync<T>(string endpoint, object data)
    {
        var jsonContent = new StringContent(
                JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json"
            );

        _fileLogger.Log("ws_congreso", "SendEmail", "0", data);

        HttpResponseMessage response = null;
        try
        {
            response = await _httpClient.PostAsync(endpoint, jsonContent);

            response.EnsureSuccessStatusCode();

            _fileLogger.Log("ws_congreso", "SendEmail", "1", response);
        }
        catch (HttpRequestException ex)
        {
            if (response != null)
            {
                string errorContent = await response.Content.ReadAsStringAsync();

                _fileLogger.Log("ws_congreso", "SendEmail", "1", errorContent, ex.Message);

                //throw new Exception($"La petición falló con el código {response.StatusCode}. Detalles: {errorContent}", ex);
            }
            else
            {
                // Si la respuesta es null, fue un error de conexión
                //throw new Exception($"La petición falló sin una respuesta. Detalles: {ex.Message}", ex);

                _fileLogger.Log("ws_congreso", "SendEmail", "1", "", ex.Message);
            }
        }

        var responseData = await response!.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseData)!;
    }
}

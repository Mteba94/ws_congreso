namespace congreso.Application.Interfaces.ExternalWS;

public interface ISendEmailAPI
{
    Task<string> GetDataAsync(string endpoint);
    Task<T> PostDataAsync<T>(string endpoint, object data);
}

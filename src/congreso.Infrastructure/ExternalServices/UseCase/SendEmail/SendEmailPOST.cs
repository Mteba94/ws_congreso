using congreso.Application.Interfaces.ExternalWS;

namespace congreso.Infrastructure.ExternalServices.UseCase.SendEmail;

public class SendEmailPOST
{
    private readonly ISendEmailAPI _sendEmailAPI;

    public SendEmailPOST(ISendEmailAPI sendEmailAPI)
    {
        _sendEmailAPI = sendEmailAPI;
    }

    public async Task EnviaCorreo()
    {
        try
        {
            var endpoint = "SendEmail";

            object peticion = new
            {
                plantilla = "plantillaConfirmacion.html",
                to = "tebalandonis@gmail.com",
                subject = "CONFIRMACION DE CORREO 4",
                body = new Dictionary<string, string>
                {
                    { "0", "Melvin Tebalan" }
                },
                attachments = "plantillaConfirmacion.html"
                 
            };

            var data = await _sendEmailAPI.PostDataAsync<string>(endpoint, peticion);
        }
        catch (Exception ex)
        {
            // Manejo de excepciones
            Console.WriteLine($"Error al enviar el correo: {ex.Message}");
        }
    }
}

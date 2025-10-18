namespace congreso.Application.Dtos.Inscripciones;

public class GenerateQrCodeResponseDto
{
    public int InscripcionId { get; set; }
    public string QrCodeBase64Image { get; set; } = null!;
    public string QrCodeContent { get; set; } = null!;
}

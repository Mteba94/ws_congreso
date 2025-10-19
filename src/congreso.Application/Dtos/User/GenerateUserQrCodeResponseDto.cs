namespace congreso.Application.Dtos.User;

public class GenerateUserQrCodeResponseDto
{
    public int UserId { get; set; }
    public string QrCodeBase64Image { get; set; } = null!;
    public string QrCodeContent { get; set; } = null!;
}

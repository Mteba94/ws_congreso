namespace congreso.Application.Dtos.User;

public class UserSummaryDto
{
    public int UserId { get; set; }
    public string UserName { get; set; } = null!;
    public int InscriptionsCount { get; set; }
    public int CertificatesCount { get; set; }
}

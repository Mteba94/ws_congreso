using System.Threading.Tasks;

namespace congreso.Application.Interfaces.Services;

public class PdfGenerationResult
{
    public string Base64Content { get; set; } = null!;
    public string FilePath { get; set; } = null!;
}

public interface IPdfGeneratorService
{
    Task<PdfGenerationResult> GenerateDiplomaPdfAsync(string participantName, string activityTitle, DateTime issueDate, string uniqueCode, string? customizedName);
}
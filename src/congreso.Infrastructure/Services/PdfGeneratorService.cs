using congreso.Application.Interfaces.Services;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace congreso.Infrastructure.Services;

public class PdfGeneratorService : IPdfGeneratorService
{
    public async Task<PdfGenerationResult> GenerateDiplomaPdfAsync(string participantName, string activityTitle, DateTime issueDate, string uniqueCode, string? customizedName)
    {
        string diplomaBaseDirectory = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "diplomas");

        if (!Directory.Exists(diplomaBaseDirectory))
        {
            Directory.CreateDirectory(diplomaBaseDirectory);
        }

        string fileName = $"Diploma_{uniqueCode}.pdf";
        string physicalFilePath = System.IO.Path.Combine(diplomaBaseDirectory, fileName);
        string webAccessiblePath = $"/diplomas/{fileName}"; // Path for web access

        // Create bold font
        PdfFont boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

        using (var writer = new PdfWriter(physicalFilePath))
        using (var pdf = new PdfDocument(writer))
        {
            var document = new Document(pdf, PageSize.A4);
            document.SetMargins(72, 72, 72, 72);

            // Header
            document.Add(new Paragraph(new Text("Diploma de Participación").SetFont(boldFont))
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(24)
                .SetMarginBottom(20));

            // Content
            document.Add(new Paragraph()
                .Add(new Text("Se otorga el presente diploma a: ").SetFontSize(14))
                .Add(new Text(customizedName ?? participantName).SetFont(boldFont).SetFontSize(16))
                .SetMarginBottom(10));

            document.Add(new Paragraph()
                .Add(new Text("Por su valiosa participación en la actividad: ").SetFontSize(14))
                .Add(new Text(activityTitle).SetFont(boldFont).SetFontSize(16))
                .SetMarginBottom(10));

            document.Add(new Paragraph()
                .Add(new Text("Emitido el: ").SetFontSize(12))
                .Add(new Text(issueDate.ToShortDateString()).SetFontSize(12))
                .SetMarginBottom(10));

            document.Add(new Paragraph()
                .Add(new Text("Código Único: ").SetFontSize(10))
                .Add(new Text(uniqueCode).SetFontSize(10))
                .SetMarginBottom(50));

            // Footer
            document.Add(new Paragraph()
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(10)
                .Add(new Text("Página "))
                .Add(new Text(pdf.GetPageNumber(pdf.GetLastPage()).ToString()))
                .Add(new Text(" de "))
                .Add(new Text(pdf.GetNumberOfPages().ToString())));
            document.Close();
        }

        // Read the generated file into a byte array
        byte[] pdfBytes = await File.ReadAllBytesAsync(physicalFilePath);

        // Convert to Base64
        string base64Content = Convert.ToBase64String(pdfBytes);

        return new PdfGenerationResult
        {
            Base64Content = base64Content,
            FilePath = webAccessiblePath
        };
    }
}
namespace OutSystems.HtmlToPdfGenerator.Tests;

using System.Threading.Tasks;
using Xunit;


public class PdfGeneratorTests
{
    private readonly PdfGenerator _pdfGenerator;

    public PdfGeneratorTests()
    {
        _pdfGenerator = new PdfGenerator();
    }

    [Fact]
    public async Task GeneratePdf_ValidUrl_ReturnsPdfBytes()
    {
        // Arrange
        string url = "https://example.com";
        PaperOrientation orientation = PaperOrientation.Portrait;
        PaperSize size = PaperSize.A4;
        HtmlSource source = HtmlSource.Url;

        // Act
        byte[] pdfBytes = await _pdfGenerator.GeneratePdf(url, source, orientation, size);

        // Assert
        Assert.NotNull(pdfBytes);
        Assert.True(pdfBytes.Length > 0);
    }

    [Fact]
    public async Task GeneratePdf_ValidHtml_ReturnsPdfBytes()
    {
        // Arrange
        string html = "<html><body><h1>Hello, World!</h1></body></html>";
        PaperOrientation orientation = PaperOrientation.Landscape;
        PaperSize size = PaperSize.Letter;
        HtmlSource source = HtmlSource.HtmlString;

        // Act
        byte[] pdfBytes = await _pdfGenerator.GeneratePdf(html, source, orientation, size);

        // Assert
        Assert.NotNull(pdfBytes);
        Assert.True(pdfBytes.Length > 0);
    }
}

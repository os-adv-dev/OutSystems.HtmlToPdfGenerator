using OutSystems.HtmlToPdfGenerator;

namespace TestConsole;

internal class Program
{
    static async Task Main(string[] args)
    {
        PaperOrientation orientation = PaperOrientation.Portrait;
        PaperSize size = PaperSize.A4;
        HtmlSource source = HtmlSource.HtmlString;

        var pdfGenerator = new PdfGenerator();

        //var htmlOrUrl = "https://www.puppeteersharp.com/api/index.html";


        var htmlOrUrl = File.ReadAllText("SampleReport.html");

        byte[] pdfBytes = await pdfGenerator.GeneratePdf(htmlOrUrl, source, orientation, size);

        File.WriteAllBytes("C:\\test.pdf", pdfBytes);
    }
}

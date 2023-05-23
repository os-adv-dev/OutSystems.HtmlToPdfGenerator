using HeadlessChromium.Puppeteer.Lambda.Dotnet;
using Microsoft.Extensions.Logging;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace OutSystems.HtmlToPdfGenerator;

public enum HtmlSource
{
    Url,
    HtmlString
}

public enum PaperOrientation
{
    Portrait,
    Landscape
}

public enum PaperSize
{
    A4,
    Letter
}

public class PdfGenerator
{
    public async Task<byte[]> GeneratePdf(string urlOrHtml, HtmlSource source, PaperOrientation orientation, PaperSize size)
    {
        // Replace if you don't want to use HeadlessChromiumPuppeteerLauncher
        //using var browserLauncherFetcher = new BrowserFetcher();
        //await browserLauncherFetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
        //var browser = await Puppeteer.LaunchAsync(new LaunchOptions
        //{
        //    Headless = true,
        //    ExecutablePath = ""
        //});


        var browserLauncherFetcher = new HeadlessChromiumPuppeteerLauncher(new LoggerFactory());
        var browser = await browserLauncherFetcher.LaunchAsync();
        using var page = await browser.NewPageAsync();

        switch (source)
        {
            case HtmlSource.Url:
                await page.GoToAsync(urlOrHtml);
                break;
            case HtmlSource.HtmlString:
                await page.SetContentAsync(urlOrHtml);
                break;
            default:
                break;
        }

        var pdfOptions = new PdfOptions
        {
            Format = size == PaperSize.A4 ? PaperFormat.A4 : PaperFormat.Letter,
            Landscape = orientation == PaperOrientation.Landscape
        };

        var pdfData = await page.PdfStreamAsync(pdfOptions);

        return ReadFully(pdfData);
    }

    private static byte[] ReadFully(Stream input)
    {
        using var memoryStream = new MemoryStream();
        input.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }
}


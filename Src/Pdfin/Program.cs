using PuppeteerSharp;
using HandlebarsDotNet;
using Moq;
using AutoFixture;

namespace Pdfin;


class Program
{
    static async Task Main()
    {
        var commercialInvoiceDto = MapAShipmentRecordToACommercialInvoiceDto();
        var browser = GetBrowser();
        var template = Handlebars.Compile(File.ReadAllText("Templates\\CommercialInvoice.hbs"));
        var html = template(commercialInvoiceDto);
        var page = await browser.NewPageAsync();
        await page.SetContentAsync(html);
        await page.PdfAsync($"{Directory.GetCurrentDirectory()}\\out\\{commercialInvoiceDto.Title}.pdf");
    }

    public static IBrowser GetBrowser()
    {
        var browserFetcher = new BrowserFetcher();
        browserFetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision).Wait();
        var browser = Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = true
        }).Result;
        return browser;
    }

    public static CommercialInvoiceDto MapAShipmentRecordToACommercialInvoiceDto()
    {
        var shipment = MockAShipmentRecord();
        var commercialInvoiceDto = new CommercialInvoiceDto
        {
            Title = shipment.Company,
            InvoiceNumber = shipment.InvoiceNumber,
            InvoiceDate = shipment.InvoiceDate,
            ShipmentDate = shipment.ShipmentDate,
            Shipper = shipment.Shipper,
            Consignee = shipment.Consignee,
            NotifyParty = shipment.NotifyParty,
            Items = shipment.Items?.Select(item => new Item
            {
                Commodity = item.Commodity,
                Description = item.Description,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                Amount = item.Amount,
                HgCode = item.HgCode,
                HgValue = item.HgValue,
                HgUnit = item.HgUnit,
                Control = item.Control
            }).ToList()
        };
        return commercialInvoiceDto;
    }

    public static Shipment MockAShipmentRecord()
    {
        var fixture = new Fixture();
        var mock = fixture.Create<Shipment>();
        return mock;
    }
}

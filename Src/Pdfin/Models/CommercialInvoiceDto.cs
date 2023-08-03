namespace Pdfin;

public record CommercialInvoiceDto
{
    public string? Title { get; init; }
    public string? InvoiceNumber { get; init; }
    public string? InvoiceDate { get; init; }
    public string? ShipmentDate { get; init; }
    public string? Shipper { get; init; }
    public string? Consignee { get; init; }
    public string? NotifyParty { get; init; }
    public List<Item>? Items { get; init; }

}
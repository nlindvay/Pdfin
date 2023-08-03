namespace Pdfin;

public record Item
{
    public string? Commodity { get; set; }
    public string? Description { get; init; }
    public string? Quantity { get; init; }
    public string? UnitPrice { get; init; }
    public string? Amount { get; init; }
    public string? HgCode { get; init; }
    public string? HgValue { get; init; }
    public string? HgUnit { get; init; }
    public string? Control { get; init; }
}
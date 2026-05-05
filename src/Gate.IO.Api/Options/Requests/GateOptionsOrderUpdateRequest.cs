namespace Gate.IO.Api.Options;

/// <summary>
/// Options order update request
/// </summary>
public record GateOptionsOrderUpdateRequest
{
    public string Contract { get; set; }
    public decimal Price { get; set; }
    public long Size { get; set; }
}

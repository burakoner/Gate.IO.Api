namespace Gate.IO.Api.Options;

/// <summary>
/// Options order request
/// </summary>
public record GateOptionsOrderRequest
{
    public string Contract { get; set; }
    public long Size { get; set; }
    public long? Iceberg { get; set; }
    public decimal? Price { get; set; }
    public bool? Close { get; set; }
    public bool? ReduceOnly { get; set; }
    public bool? Mmp { get; set; }
    public GateOptionsTimeInForce? TimeInForce { get; set; }
    public string ClientOrderId { get; set; }
}

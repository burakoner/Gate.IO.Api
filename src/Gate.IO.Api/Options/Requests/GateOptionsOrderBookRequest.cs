namespace Gate.IO.Api.Options;

/// <summary>
/// Options order book request
/// </summary>
public record GateOptionsOrderBookRequest
{
    public string Contract { get; set; }
    public decimal? Interval { get; set; }
    public int? Limit { get; set; }
    public bool? WithId { get; set; }
}

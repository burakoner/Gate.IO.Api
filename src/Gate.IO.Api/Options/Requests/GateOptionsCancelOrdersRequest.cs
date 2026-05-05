namespace Gate.IO.Api.Options;

/// <summary>
/// Options cancel orders request
/// </summary>
public record GateOptionsCancelOrdersRequest
{
    public string Underlying { get; set; }
    public string Contract { get; set; }
    public GateOptionsOrderSide? Side { get; set; }
}

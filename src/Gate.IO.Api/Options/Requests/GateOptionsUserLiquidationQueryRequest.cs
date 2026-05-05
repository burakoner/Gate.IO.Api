namespace Gate.IO.Api.Options;

/// <summary>
/// Options user liquidation query request
/// </summary>
public record GateOptionsUserLiquidationQueryRequest
{
    public string Underlying { get; set; }
    public string Contract { get; set; }
}

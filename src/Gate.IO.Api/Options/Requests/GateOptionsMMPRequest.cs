namespace Gate.IO.Api.Options;

/// <summary>
/// Options MMP settings request
/// </summary>
public record GateOptionsMMPRequest
{
    public string Underlying { get; set; }
    public int Window { get; set; }
    public int FrozenPeriod { get; set; }
    public decimal QuantityLimit { get; set; }
    public decimal DeltaLimit { get; set; }
}

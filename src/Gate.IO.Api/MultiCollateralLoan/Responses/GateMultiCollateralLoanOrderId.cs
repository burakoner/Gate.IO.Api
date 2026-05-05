namespace Gate.IO.Api.MultiCollateralLoan;

/// <summary>
/// Multi-collateral loan order ID response
/// </summary>
public record GateMultiCollateralLoanOrderId
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("order_id")]
    public long OrderId { get; set; }
}

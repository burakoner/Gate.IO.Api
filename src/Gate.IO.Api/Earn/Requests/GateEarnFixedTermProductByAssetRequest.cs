namespace Gate.IO.Api.Earn;

/// <summary>
/// Fixed-term Earn product by asset request
/// </summary>
public record GateEarnFixedTermProductByAssetRequest
{
    /// <summary>
    /// Currency name
    /// </summary>
    public string Asset { get; set; }

    /// <summary>
    /// Product type
    /// </summary>
    public GateEarnFixedTermProductType? Type { get; set; }
}

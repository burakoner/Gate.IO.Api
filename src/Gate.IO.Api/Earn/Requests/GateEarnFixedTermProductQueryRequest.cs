namespace Gate.IO.Api.Earn;

/// <summary>
/// Fixed-term Earn product query request
/// </summary>
public record GateEarnFixedTermProductQueryRequest
{
    /// <summary>
    /// Currency
    /// </summary>
    public string Asset { get; set; }

    /// <summary>
    /// Product type
    /// </summary>
    public GateEarnFixedTermProductType? Type { get; set; }

    /// <summary>
    /// Page number
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Page size
    /// </summary>
    public int Limit { get; set; } = 100;
}

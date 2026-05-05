namespace Gate.IO.Api.EarnUni;

/// <summary>
/// EarnUni lending information update request
/// </summary>
public record GateEarnUniLendUpdateRequest
{
    /// <summary>
    /// Currency name
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Minimum interest rate
    /// </summary>
    public decimal? MinimumRate { get; set; }
}

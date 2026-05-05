namespace Gate.IO.Api.EarnUni;

/// <summary>
/// EarnUni annualized trend chart query request
/// </summary>
public record GateEarnUniChartQueryRequest
{
    /// <summary>
    /// Currency name
    /// </summary>
    public string Asset { get; set; }

    /// <summary>
    /// Start time
    /// </summary>
    public DateTime From { get; set; }

    /// <summary>
    /// End time
    /// </summary>
    public DateTime To { get; set; }
}

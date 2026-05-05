namespace Gate.IO.Api.Otc;

/// <summary>
/// OTC stablecoin order list request
/// </summary>
public record GateOtcStableCoinOrderListRequest
{
    /// <summary>
    /// Number of records per page
    /// </summary>
    public int? PageSize { get; set; }

    /// <summary>
    /// Page number
    /// </summary>
    public int? PageNumber { get; set; }

    /// <summary>
    /// Order currency
    /// </summary>
    public string CoinName { get; set; }

    /// <summary>
    /// Start time
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// End time
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// Status: PROCESSING, DONE, or FAILED
    /// </summary>
    public string Status { get; set; }
}

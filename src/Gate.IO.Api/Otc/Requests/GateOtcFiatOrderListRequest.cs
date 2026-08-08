namespace Gate.IO.Api.Otc;

/// <summary>
/// OTC fiat order list request
/// </summary>
public record GateOtcFiatOrderListRequest
{
    /// <summary>
    /// BUY or SELL
    /// </summary>
    public GateOtcOrderType? Type { get; set; }

    /// <summary>
    /// Fiat currency
    /// </summary>
    public string FiatCurrency { get; set; }

    /// <summary>
    /// Digital currency
    /// </summary>
    public string CryptoCurrency { get; set; }

    /// <summary>
    /// Start time
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// End time
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// Order status: DONE, CANCEL, PROCESSING, or DISBURSED
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Page number
    /// </summary>
    public int? PageNumber { get; set; }

    /// <summary>
    /// Number of items per page
    /// </summary>
    public int? PageSize { get; set; }
}

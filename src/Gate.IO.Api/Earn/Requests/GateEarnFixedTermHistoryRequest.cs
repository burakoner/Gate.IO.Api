namespace Gate.IO.Api.Earn;

/// <summary>
/// Fixed-term Earn history query request
/// </summary>
public record GateEarnFixedTermHistoryRequest
{
    /// <summary>
    /// Product ID
    /// </summary>
    public long? ProductId { get; set; }

    /// <summary>
    /// Order ID
    /// </summary>
    public long? OrderId { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    public string Asset { get; set; }

    /// <summary>
    /// History type
    /// </summary>
    public GateEarnFixedTermHistoryType Type { get; set; }

    /// <summary>
    /// Page number
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Page size
    /// </summary>
    public int Limit { get; set; } = 100;

    /// <summary>
    /// Start timestamp
    /// </summary>
    public DateTime? StartAt { get; set; }

    /// <summary>
    /// End timestamp
    /// </summary>
    public DateTime? EndAt { get; set; }

    /// <summary>
    /// Sub-business type
    /// </summary>
    public int? SubBusiness { get; set; }

    /// <summary>
    /// Business filter JSON
    /// </summary>
    public string BusinessFilter { get; set; }
}

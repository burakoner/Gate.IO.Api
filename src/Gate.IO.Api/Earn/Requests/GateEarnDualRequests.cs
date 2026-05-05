namespace Gate.IO.Api.Earn;

/// <summary>
/// Dual investment product query request
/// </summary>
public record GateEarnDualPlanQueryRequest
{
    /// <summary>
    /// Financial project ID
    /// </summary>
    public long? PlanId { get; set; }

    /// <summary>
    /// Investment token
    /// </summary>
    public string Coin { get; set; }

    /// <summary>
    /// Product type
    /// </summary>
    public GateEarnDualOptionType? Type { get; set; }

    /// <summary>
    /// Settlement currency
    /// </summary>
    public string QuoteCurrency { get; set; }

    /// <summary>
    /// Sort field
    /// </summary>
    public GateEarnDualPlanSort? Sort { get; set; }

    /// <summary>
    /// Page number
    /// </summary>
    public int? Page { get; set; }

    /// <summary>
    /// Items per page
    /// </summary>
    public int? PageSize { get; set; }
}

/// <summary>
/// Dual investment order query request
/// </summary>
public record GateEarnDualOrderQueryRequest
{
    /// <summary>
    /// Start settlement time
    /// </summary>
    public DateTime? From { get; set; }

    /// <summary>
    /// End settlement time
    /// </summary>
    public DateTime? To { get; set; }

    /// <summary>
    /// Product type
    /// </summary>
    public GateEarnDualOptionType? Type { get; set; }

    /// <summary>
    /// Order status
    /// </summary>
    public GateEarnDualOrderQueryStatus? Status { get; set; }

    /// <summary>
    /// Investment token
    /// </summary>
    public string Coin { get; set; }

    /// <summary>
    /// Page number
    /// </summary>
    public int? Page { get; set; }

    /// <summary>
    /// Maximum number of records returned in a single list
    /// </summary>
    public int? Limit { get; set; }
}

/// <summary>
/// Dual investment order request
/// </summary>
public record GateEarnDualOrderRequest
{
    /// <summary>
    /// Product ID
    /// </summary>
    public long PlanId { get; set; }

    /// <summary>
    /// Subscription amount
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Custom order information
    /// </summary>
    public string Text { get; set; }
}

/// <summary>
/// Dual investment early redemption request
/// </summary>
public record GateEarnDualRefundRequest
{
    /// <summary>
    /// Order ID
    /// </summary>
    public long OrderId { get; set; }

    /// <summary>
    /// Request ID returned by order-refund-preview
    /// </summary>
    public string RequestId { get; set; }
}

/// <summary>
/// Dual investment reinvest update request
/// </summary>
public record GateEarnDualReinvestUpdateRequest
{
    /// <summary>
    /// Order ID
    /// </summary>
    public long? OrderId { get; set; }

    /// <summary>
    /// Reinvest status. 0: off, 1: on
    /// </summary>
    public int? Status { get; set; }

    /// <summary>
    /// Effective duration in seconds
    /// </summary>
    public long? EffectiveTimeDuration { get; set; }
}

/// <summary>
/// Dual investment recommendation request
/// </summary>
public record GateEarnDualRecommendationRequest
{
    /// <summary>
    /// Sort mode
    /// </summary>
    public GateEarnDualRecommendationMode? Mode { get; set; }

    /// <summary>
    /// Investment token
    /// </summary>
    public string Coin { get; set; }

    /// <summary>
    /// Product type
    /// </summary>
    public GateEarnDualOptionType? Type { get; set; }

    /// <summary>
    /// Project IDs to exclude
    /// </summary>
    public IEnumerable<long> HistoryProductIds { get; set; }
}

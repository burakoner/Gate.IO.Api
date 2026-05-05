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

/// <summary>
/// Fixed-term Earn subscription order query request
/// </summary>
public record GateEarnFixedTermLendQueryRequest
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
    /// Order type
    /// </summary>
    public GateEarnFixedTermOrderType OrderType { get; set; }

    /// <summary>
    /// Page number
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Page size
    /// </summary>
    public int Limit { get; set; } = 100;

    /// <summary>
    /// Sub-business type
    /// </summary>
    public int? SubBusiness { get; set; }

    /// <summary>
    /// Business filter JSON
    /// </summary>
    public string BusinessFilter { get; set; }
}

/// <summary>
/// Fixed-term Earn subscription request
/// </summary>
public record GateEarnFixedTermLendRequest
{
    /// <summary>
    /// Product ID
    /// </summary>
    public long ProductId { get; set; }

    /// <summary>
    /// Subscription amount
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Annual interest rate
    /// </summary>
    public decimal? YearRate { get; set; }

    /// <summary>
    /// Auto-renewal status
    /// </summary>
    public int? ReinvestStatus { get; set; }

    /// <summary>
    /// Redemption payout account type
    /// </summary>
    public int? RedeemAccountType { get; set; }

    /// <summary>
    /// Interest rate boost coupon ID
    /// </summary>
    public long? FinancialRateId { get; set; }

    /// <summary>
    /// Sub-business type
    /// </summary>
    public int? SubBusiness { get; set; }
}

/// <summary>
/// Fixed-term Earn early redemption request
/// </summary>
public record GateEarnFixedTermPreRedeemRequest
{
    /// <summary>
    /// Order ID
    /// </summary>
    public long OrderId { get; set; }
}

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

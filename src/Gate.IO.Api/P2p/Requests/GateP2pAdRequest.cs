namespace Gate.IO.Api.P2p;

/// <summary>
/// Publish or edit ad request
/// </summary>
public record GateP2pAdRequest
{
    /// <summary>
    /// Cryptocurrency symbol
    /// </summary>
    public string CurrencyType { get; set; }

    /// <summary>
    /// Fiat currency
    /// </summary>
    public string ExchangeType { get; set; }

    /// <summary>
    /// Ad operation type
    /// </summary>
    public GateP2pAdOperationType Type { get; set; }

    /// <summary>
    /// Per-unit price in fixed-price mode
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Ad amount priced in CurrencyType
    /// </summary>
    public decimal Number { get; set; }

    /// <summary>
    /// Payment types, comma-separated
    /// </summary>
    public string PayType { get; set; }

    /// <summary>
    /// JSON map of payment type to payment method ID
    /// </summary>
    public string PayTypeJson { get; set; }

    /// <summary>
    /// Price type
    /// </summary>
    public int? RateFixed { get; set; }

    /// <summary>
    /// Advertisement ID when editing
    /// </summary>
    public long? OrderId { get; set; }

    /// <summary>
    /// Minimum trade amount in ExchangeType
    /// </summary>
    public decimal MinAmount { get; set; }

    /// <summary>
    /// Maximum amount per trade in ExchangeType
    /// </summary>
    public decimal MaxAmount { get; set; }

    /// <summary>
    /// Minimum counterparty VIP level
    /// </summary>
    public int? TierLimit { get; set; }

    /// <summary>
    /// Minimum counterparty verification level
    /// </summary>
    public int? VerifiedLimit { get; set; }

    /// <summary>
    /// Minimum counterparty account age in days
    /// </summary>
    public int? RegistrationTimeLimit { get; set; }

    /// <summary>
    /// Whether trading with the advertiser is restricted
    /// </summary>
    public int? AdvertisersLimit { get; set; }

    /// <summary>
    /// Payment timeout in minutes
    /// </summary>
    public int? ExpireMinutes { get; set; }

    /// <summary>
    /// Ad trading terms shown to the taker
    /// </summary>
    public string TradeTips { get; set; }

    /// <summary>
    /// Auto-reply message after order creation
    /// </summary>
    public string AutoReply { get; set; }

    /// <summary>
    /// Minimum completed orders for counterparty
    /// </summary>
    public int? MinCompletedLimit { get; set; }

    /// <summary>
    /// Maximum completed orders for counterparty
    /// </summary>
    public int? MaxCompletedLimit { get; set; }

    /// <summary>
    /// Counterparty minimum 30-day completion rate
    /// </summary>
    public decimal? CompletedRateLimit { get; set; }

    /// <summary>
    /// KYC nationality restriction
    /// </summary>
    public int? UserCountryLimit { get; set; }

    /// <summary>
    /// Maximum concurrent orders allowed for the counterparty
    /// </summary>
    public int? UserOrderLimit { get; set; }

    /// <summary>
    /// Floating price reference
    /// </summary>
    public int? RateReferenceId { get; set; }

    /// <summary>
    /// Absolute floating offset ratio
    /// </summary>
    public decimal? RateOffset { get; set; }

    /// <summary>
    /// Floating direction
    /// </summary>
    public int? FloatTrend { get; set; }

    /// <summary>
    /// Team payee UID
    /// </summary>
    public long? TeamPaymentUserId { get; set; }
}

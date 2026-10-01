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
    /// Payment types enabled for the advertisement, comma-separated.
    /// Use pay_type values from the current user's payment method list, not payment method IDs.
    /// </summary>
    public string PayType { get; set; }

    /// <summary>
    /// Optional JSON string mapping enabled payment types to the current user's specific payment method IDs.
    /// For PayType="bank,swift", an example is {"bank":"10001","swift":"10002"}.
    /// Each key must be listed in PayType; omitting this field remains supported.
    /// </summary>
    public string PayTypeJson { get; set; }

    /// <summary>
    /// Price type
    /// </summary>
    public int? RateFixed { get; set; }

    /// <summary>
    /// Required advertisement ID when editing. Omit or leave empty when publishing a new advertisement.
    /// </summary>
    public string OrderId { get; set; }

    /// <summary>
    /// Minimum cryptocurrency quantity per order. Required for cryptocurrency quantity limits.
    /// </summary>
    public decimal? MinAmount { get; set; }

    /// <summary>
    /// Maximum cryptocurrency quantity per order. Required for cryptocurrency quantity limits.
    /// </summary>
    public decimal? MaxAmount { get; set; }

    /// <summary>
    /// Trading-limit unit. Defaults to cryptocurrency quantity for a new advertisement.
    /// Cannot be changed when editing an existing advertisement; fiat-limit edits must keep Fiat.
    /// </summary>
    public GateP2pAdLimitBasis? LimitBasis { get; set; }

    /// <summary>
    /// Minimum fiat amount per order. Required when <see cref="LimitBasis"/> is <see cref="GateP2pAdLimitBasis.Fiat"/>.
    /// </summary>
    public decimal? FiatMinAmount { get; set; }

    /// <summary>
    /// Maximum fiat amount per order. Required when <see cref="LimitBasis"/> is <see cref="GateP2pAdLimitBasis.Fiat"/>.
    /// Must not exceed the total fiat value of the advertisement; checked locally only for explicit fixed pricing.
    /// </summary>
    public decimal? FiatMaxAmount { get; set; }

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
    /// Whether trading with Polymarket users is restricted
    /// </summary>
    public bool? PolymarketRestricted { get; set; }

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
    public string UserCountryLimit { get; set; }

    /// <summary>
    /// Maximum concurrent orders allowed for the counterparty
    /// </summary>
    public int? UserOrderLimit { get; set; }

    /// <summary>
    /// Floating price reference
    /// </summary>
    public int? RateReferenceId { get; set; }

    /// <summary>
    /// Absolute floating offset ratio. For example, 0.5 means 0.5 percent.
    /// </summary>
    public decimal? RateOffset { get; set; }

    /// <summary>
    /// Floating direction
    /// </summary>
    public int? FloatTrend { get; set; }

    /// <summary>
    /// Team payee UID
    /// </summary>
    public string TeamPaymentUserId { get; set; }
}

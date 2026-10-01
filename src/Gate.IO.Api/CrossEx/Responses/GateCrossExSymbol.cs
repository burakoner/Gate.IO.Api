namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx trading pair information
/// </summary>
public record GateCrossExSymbol
{
    /// <summary>
    /// Gets or sets the Symbol.
    /// </summary>
    [JsonProperty("symbol", Required = Required.Always)]
    public string Symbol { get; set; }

    /// <summary>
    /// Gets or sets the Exchange Type.
    /// </summary>
    [JsonProperty("exchange_type", Required = Required.Always)]
    public string ExchangeType { get; set; }

    /// <summary>
    /// Gets or sets the Business Type.
    /// </summary>
    [JsonProperty("business_type", Required = Required.Always)]
    public string BusinessType { get; set; }

    /// <summary>
    /// Gets or sets the State.
    /// </summary>
    [JsonProperty("state", Required = Required.Always)]
    public string State { get; set; }

    /// <summary>
    /// Gets or sets the Minimum Size.
    /// </summary>
    [JsonProperty("min_size", Required = Required.Always)]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal MinimumSize { get; set; }

    /// <summary>
    /// Gets or sets the Minimum Notional.
    /// </summary>
    [JsonProperty("min_notional", Required = Required.Always)]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal MinimumNotional { get; set; }

    /// <summary>
    /// Gets or sets the Lot Size.
    /// </summary>
    [JsonProperty("lot_size", Required = Required.Always)]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal LotSize { get; set; }

    /// <summary>
    /// Gets or sets the Tick Size.
    /// </summary>
    [JsonProperty("tick_size", Required = Required.Always)]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal TickSize { get; set; }

    /// <summary>
    /// Gets or sets the Maximum Number Of Orders.
    /// </summary>
    [JsonProperty("max_num_orders", Required = Required.Always)]
    [JsonConverter(typeof(GateFuturesOrderIdConverter))]
    public long MaximumNumberOfOrders { get; set; }

    /// <summary>
    /// Maximum market order quantity. The required key permits null for compatibility with captured venue responses.
    /// </summary>
    [JsonProperty("max_market_size", Required = Required.AllowNull)]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal? MaximumMarketSize { get; set; }

    /// <summary>
    /// Gets or sets the Maximum Limit Size.
    /// </summary>
    [JsonProperty("max_limit_size", Required = Required.Always)]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal MaximumLimitSize { get; set; }

    /// <summary>
    /// Deprecated contract multiplier. Quantities are used uniformly; legacy null values remain supported.
    /// </summary>
    [JsonProperty("contract_size", Required = Required.AllowNull)]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal? ContractSize { get; set; }

    /// <summary>
    /// Gets or sets the Liquidation Fee.
    /// </summary>
    [JsonProperty("liquidation_fee", Required = Required.Always)]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal LiquidationFee { get; set; }

    /// <summary>
    /// Legacy default leverage, retained for compatibility. Not present in the current Symbol schema.
    /// </summary>
    [JsonProperty("default_leverage")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal? DefaultLeverage { get; set; }

    /// <summary>
    /// Unix timestamp in milliseconds. Zero means not delisted; missing/null response values are rejected.
    /// </summary>
    [JsonProperty("delist_time", Required = Required.Always)]
    [JsonConverter(typeof(GateFuturesOrderIdConverter))]
    public long? DelistTime { get; set; }

    /// <summary>
    /// Whether RPI order placement is supported. Null means the venue did not supply this optional flag.
    /// </summary>
    [JsonProperty("support_rpi")]
    [JsonConverter(typeof(GateCrossExBooleanStringConverter))]
    public bool? SupportsRpi { get; set; }

    /// <summary>
    /// Whether cross-margin order placement is supported. Null is unknown, not false or permission to trade.
    /// The documented wire values are strings "true" and "false".
    /// </summary>
    [JsonProperty("support_cross")]
    [JsonConverter(typeof(GateCrossExBooleanStringConverter))]
    public bool? SupportsCross { get; set; }
}

namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx order request
/// </summary>
public record GateCrossExOrderRequest
{
    /// <summary>
    /// Client-defined order ID, shorter than 64 characters, using a-z, 0-9, hyphen and underscore only.
    /// </summary>
    [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateCrossExActionValueConverter))]
    public string Text { get; set; }

    /// <summary>
    /// Trading pair identifier in Exchange_Business_Base_Counter form. Examples: KRAKEN_FUTURE_ADA_USD,
    /// HYPERLIQUID_FUTURE_ADA_USDC, DERIBIT_FUTURE_ADA_USDC and LIGHTER_FUTURE_ADA_USDC.
    /// Kraken, Hyperliquid and Lighter support futures only; Bybit and Deribit do not support margin pairs.
    /// </summary>
    [JsonProperty("symbol", Required = Required.Always)]
    [JsonConverter(typeof(GateCrossExActionValueConverter))]
    public string Symbol { get; set; }

    /// <summary>
    /// Order side
    /// </summary>
    [JsonProperty("side", Required = Required.Always)]
    [JsonConverter(typeof(GateCrossExInstructionEnumConverter))]
    public GateCrossExOrderSide Side { get; set; }

    /// <summary>
    /// LIMIT/MARKET. Null omits the field; the server defaults to LIMIT.
    /// </summary>
    [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateCrossExInstructionEnumConverter))]
    public GateCrossExOrderType? Type { get; set; }

    /// <summary>
    /// GTC/IOC/FOK/POC/RPI. Null omits the field; the server defaults to GTC. POC/RPI are not valid for market orders.
    /// </summary>
    [JsonProperty("time_in_force", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateCrossExInstructionEnumConverter))]
    public GateCrossExTimeInForce? TimeInForce { get; set; }

    /// <summary>
    /// Positive base currency order quantity; required unless this is a spot or margin market buy.
    /// </summary>
    [JsonProperty("qty", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Limit order price; required for limit orders (including when Type is omitted).
    /// </summary>
    [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal? Price { get; set; }

    /// <summary>
    /// Positive quote currency order quantity; required for spot and margin market buy orders.
    /// </summary>
    [JsonProperty("quote_qty", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal? QuoteQuantity { get; set; }

    /// <summary>
    /// Reduce-only flag
    /// </summary>
    [JsonProperty("reduce_only", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateCrossExBooleanStringConverter))]
    public bool? ReduceOnly { get; set; }

    /// <summary>
    /// NONE/LONG/SHORT. Margin orders require an explicit LONG/SHORT. Otherwise null omits the field;
    /// the server defaults to NONE for one-way positions. No position mode is inferred.
    /// </summary>
    [JsonProperty("position_side", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateCrossExPositionSideConverter))]
    public GateCrossExPositionSide? PositionSide { get; set; }
}

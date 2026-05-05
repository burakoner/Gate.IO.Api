namespace Gate.IO.Api.TradFi;

/// <summary>
/// Represents a TradFi user order stream update.
/// </summary>
public record GateTradFiStreamOrder
{
    /// <summary>
    /// Order unique ID.
    /// </summary>
    [JsonProperty("order_id")]
    public long OrderId { get; set; }

    /// <summary>
    /// Gate user unique ID.
    /// </summary>
    [JsonProperty("gate_uid")]
    public long GateUserId { get; set; }

    /// <summary>
    /// TradFi symbol.
    /// </summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    /// <summary>
    /// Trade direction.
    /// </summary>
    [JsonProperty("side")]
    public GateTradFiOrderSide Side { get; set; }

    /// <summary>
    /// Order quantity.
    /// </summary>
    [JsonProperty("volume")]
    public decimal Volume { get; set; }

    /// <summary>
    /// Filled quantity.
    /// </summary>
    [JsonProperty("fill_volume")]
    public decimal FillVolume { get; set; }

    /// <summary>
    /// Order price.
    /// </summary>
    [JsonProperty("price")]
    public decimal Price { get; set; }

    /// <summary>
    /// Take profit price.
    /// </summary>
    [JsonProperty("price_tp")]
    public decimal TakeProfitPrice { get; set; }

    /// <summary>
    /// Stop loss price.
    /// </summary>
    [JsonProperty("price_sl")]
    public decimal StopLossPrice { get; set; }

    /// <summary>
    /// Whether the order is completed.
    /// </summary>
    [JsonProperty("finished"), JsonConverter(typeof(BooleanConverter))]
    public bool Finished { get; set; }

    /// <summary>
    /// Order completion method.
    /// </summary>
    [JsonProperty("finished_as")]
    public string FinishedAs { get; set; }

    /// <summary>
    /// Order creation time.
    /// </summary>
    [JsonProperty("time_setup")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime SetupTime { get; set; }

    /// <summary>
    /// Order status update timestamp.
    /// </summary>
    [JsonProperty("timestamp")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Order operation type.
    /// </summary>
    [JsonProperty("order_opt_type")]
    public GateTradFiStreamOrderOperationType OperationType { get; set; }
}

namespace Gate.IO.Api.TradFi;

/// <summary>
/// Represents a TradFi user position stream update.
/// </summary>
public record GateTradFiStreamPosition
{
    /// <summary>
    /// Position unique ID.
    /// </summary>
    [JsonProperty("position_id")]
    public long PositionId { get; set; }

    /// <summary>
    /// Gate user unique ID.
    /// </summary>
    [JsonProperty("gate_uid")]
    public long GateUserId { get; set; }

    /// <summary>
    /// Position side.
    /// </summary>
    [JsonProperty("side")]
    public GateTradFiStreamPositionSide Side { get; set; }

    /// <summary>
    /// TradFi symbol.
    /// </summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    /// <summary>
    /// Position quantity.
    /// </summary>
    [JsonProperty("volume")]
    public decimal Volume { get; set; }

    /// <summary>
    /// Opening price.
    /// </summary>
    [JsonProperty("price_open")]
    public decimal OpenPrice { get; set; }

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
    /// Position creation time.
    /// </summary>
    [JsonProperty("time_create")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }
}

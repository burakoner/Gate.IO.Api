namespace Gate.IO.Api.TradFi;

/// <summary>
/// Represents a TradFi user balance stream update.
/// </summary>
public record GateTradFiStreamBalance
{
    /// <summary>
    /// Balance deal ID.
    /// </summary>
    [JsonProperty("deal_id")]
    public long DealId { get; set; }

    /// <summary>
    /// Gate user unique ID.
    /// </summary>
    [JsonProperty("gate_uid")]
    public long GateUserId { get; set; }

    /// <summary>
    /// Balance change amount.
    /// </summary>
    [JsonProperty("change")]
    public decimal Change { get; set; }

    /// <summary>
    /// Balance change comment.
    /// </summary>
    [JsonProperty("comment")]
    public string Comment { get; set; }

    /// <summary>
    /// Balance change timestamp.
    /// </summary>
    [JsonProperty("timestamp")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Timestamp { get; set; }
}

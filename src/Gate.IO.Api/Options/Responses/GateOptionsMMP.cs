namespace Gate.IO.Api.Options;

/// <summary>
/// GateOptionsMMP
/// </summary>
public record GateOptionsMMP
{
    /// <summary>
    /// Underlying
    /// </summary>
    [JsonProperty("underlying")]
    public string Underlying { get; set; }

    /// <summary>
    /// Time window (milliseconds), between 1-5000, 0 means disable MMP
    /// </summary>
    [JsonProperty("window")]
    public int Window { get; set; }

    /// <summary>
    /// Freeze duration (milliseconds), 0 means always frozen, need to call reset API to unfreeze
    /// </summary>
    [JsonProperty("frozen_period")]
    public int FrozenPeriod { get; set; }

    /// <summary>
    /// Trading volume upper limit (positive number, up to 2 decimal places)
    /// </summary>
    [JsonProperty("qty_limit")]
    public decimal QuantityLimit { get; set; }

    /// <summary>
    /// Upper limit of net delta value (positive number, up to 2 decimal places)
    /// </summary>
    [JsonProperty("delta_limit")]
    public decimal DeltaLimit { get; set; }

    /// <summary>
    /// Trigger freeze time (milliseconds), 0 means no freeze is triggered
    /// </summary>
    [JsonProperty("trigger_time_ms")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime TriggerTime{ get; set; }

    /// <summary>
    /// Unfreeze time (milliseconds). If the freeze duration is not configured, there will be no unfreeze time after the freeze is triggered
    /// </summary>
    [JsonProperty("frozen_until_ms")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime FrozenUntil{ get; set; }
}
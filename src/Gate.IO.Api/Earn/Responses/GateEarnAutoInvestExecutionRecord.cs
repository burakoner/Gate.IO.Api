namespace Gate.IO.Api.Earn;

/// <summary>
/// Auto invest plan execution record
/// </summary>
public record GateEarnAutoInvestExecutionRecord
{
    /// <summary>
    /// Record ID
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; }

    /// <summary>
    /// Source currency
    /// </summary>
    [JsonProperty("money")]
    public string Money { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("asset")]
    public string Asset { get; set; }

    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// Plan ID
    /// </summary>
    [JsonProperty("plan_id")]
    public long PlanId { get; set; }

    /// <summary>
    /// Plan version
    /// </summary>
    [JsonProperty("plan_version")]
    public long PlanVersion { get; set; }

    /// <summary>
    /// Investment amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Investment time
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Update time
    /// </summary>
    [JsonProperty("update_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }

    /// <summary>
    /// Status enum
    /// </summary>
    [JsonProperty("status_type")]
    public long StatusType { get; set; }

    /// <summary>
    /// Direction
    /// </summary>
    [JsonProperty("side")]
    public long Side { get; set; }

    /// <summary>
    /// Status description
    /// </summary>
    [JsonProperty("status_message")]
    public string StatusMessage { get; set; }

    /// <summary>
    /// Details
    /// </summary>
    [JsonProperty("detail")]
    public string Detail { get; set; }
}

namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx transfer record
/// </summary>
public record GateCrossExTransferRecord
{
    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the Text.
    /// </summary>
    [JsonProperty("text")]
    public string Text { get; set; }

    /// <summary>
    /// Gets or sets the From Account Type.
    /// </summary>
    [JsonProperty("from_account_type")]
    public string FromAccountType { get; set; }

    /// <summary>
    /// Gets or sets the To Account Type.
    /// </summary>
    [JsonProperty("to_account_type")]
    public string ToAccountType { get; set; }

    /// <summary>
    /// Gets or sets the Coin.
    /// </summary>
    [JsonProperty("coin")]
    public string Coin { get; set; }

    /// <summary>
    /// Gets or sets the Amount.
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the Actual Receive.
    /// </summary>
    [JsonProperty("actual_receive")]
    public decimal? ActualReceive { get; set; }

    /// <summary>
    /// Gets or sets the Status.
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }

    /// <summary>
    /// Gets or sets the Fail Reason.
    /// </summary>
    [JsonProperty("fail_reason")]
    public string FailReason { get; set; }

    /// <summary>
    /// Gets or sets the Create Time.
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// Gets or sets the Update Time.
    /// </summary>
    [JsonProperty("update_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? UpdateTime { get; set; }
}

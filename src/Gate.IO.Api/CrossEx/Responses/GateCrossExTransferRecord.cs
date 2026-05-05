namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx transfer record
/// </summary>
public record GateCrossExTransferRecord
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("from_account_type")]
    public string FromAccountType { get; set; }

    [JsonProperty("to_account_type")]
    public string ToAccountType { get; set; }

    [JsonProperty("coin")]
    public string Coin { get; set; }

    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    [JsonProperty("actual_receive")]
    public decimal? ActualReceive { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("fail_reason")]
    public string FailReason { get; set; }

    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? CreateTime { get; set; }

    [JsonProperty("update_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? UpdateTime { get; set; }
}

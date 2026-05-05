namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx account book record
/// </summary>
public record GateCrossExAccountBookRecord
{
    [JsonProperty("id")]
    public long? Id { get; set; }

    [JsonProperty("user_id")]
    public long? UserId { get; set; }

    [JsonProperty("business_id")]
    public long? BusinessId { get; set; }

    [JsonProperty("statement_type")]
    public string StatementType { get; set; }

    [JsonProperty("exchange_type")]
    public string ExchangeType { get; set; }

    [JsonProperty("coin")]
    public string Coin { get; set; }

    [JsonProperty("change")]
    public decimal? Change { get; set; }

    [JsonProperty("balance")]
    public decimal? Balance { get; set; }

    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? CreateTime { get; set; }
}

namespace Gate.IO.Api.Models.RestApi.Margin;

public class CrossMarginAccountBook
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("currency")]
    public string Currency { get; set; }

    [JsonProperty("change")]
    public decimal Change { get; set; }

    [JsonProperty("balance")]
    public decimal Balance { get; set; }

    [JsonProperty("time"), JsonConverter(typeof(MarginTransactionTypeConverter))]
    public MarginTransactionType Type { get; set; }
}

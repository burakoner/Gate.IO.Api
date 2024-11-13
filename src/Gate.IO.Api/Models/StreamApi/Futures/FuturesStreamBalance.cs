namespace Gate.IO.Api.Models.StreamApi.Futures;

public class FuturesStreamBalance
{
    [JsonProperty("time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("time_ms")]
    public long TimeInMilliseconds { get; set; }

    [JsonProperty("type")]
    public GateFuturesBalanceChangeType Type { get; set; }

    [JsonProperty("user")]
    public long UserId { get; set; }

    [JsonProperty("currency")]
    public string Currency { get; set; }

    [JsonProperty("change")]
    public decimal Change { get; set; }

    [JsonProperty("balance")]
    public decimal Balance { get; set; }

    [JsonProperty("text")]
    public string Comment { get; set; }
}

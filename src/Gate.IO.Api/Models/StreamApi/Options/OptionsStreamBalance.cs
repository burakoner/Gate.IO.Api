namespace Gate.IO.Api.Models.StreamApi.Options;

public record OptionsStreamBalance
{
    [JsonProperty("balance")]
    public decimal Balance { get; set; }

    [JsonProperty("change")]
    public decimal Change { get; set; }

    [JsonProperty("text")]
    public string Comment { get; set; }

    [JsonProperty("type")]
    public GateOptionsBalanceChangeType Type { get; set; }

    [JsonProperty("user")]
    public long UserId { get; set; }

    [JsonProperty("time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("time_ms")]
    public long TimeInMilliseconds { get; set; }
}
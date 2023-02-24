namespace Gate.IO.Api.Models.StreamApi.Options;

public class OptionsStreamPosition
{
    [JsonProperty("entry_price")]
    public decimal EntryPrice { get; set; }

    [JsonProperty("realised_pnl")]
    public decimal RealisedPnl { get; set; }

    [JsonProperty("size")]
    public long Size { get; set; }

    [JsonProperty("contract")]
    public string Contract { get; set; }

    [JsonProperty("user")]
    public int UserId { get; set; }

    [JsonProperty("time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("time_ms")]
    public long TimeMilliseconds { get; set; }
}
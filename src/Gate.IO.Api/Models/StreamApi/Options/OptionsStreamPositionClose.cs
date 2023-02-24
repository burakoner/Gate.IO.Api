namespace Gate.IO.Api.Models.StreamApi.Options;

public class OptionsStreamPositionClose
{
    [JsonProperty("contract")]
    public string Contract { get; set; }

    [JsonProperty("pnl")]
    public decimal PNL { get; set; }

    [JsonProperty("settle_size")]
    public long SettleSize { get; set; }
    
    [JsonProperty("side"), JsonConverter(typeof(OptionsSideConverter))]
    public OptionsSide Side { get; set; }

    [JsonProperty("text")]
    public string ClientOrderId { get; set; }

    [JsonProperty("underlying")]
    public string Underlying { get; set; }

    [JsonProperty("user")]
    public int UserId { get; set; }

    [JsonProperty("time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("time_ms")]
    public long TimeMilliseconds { get; set; }
}
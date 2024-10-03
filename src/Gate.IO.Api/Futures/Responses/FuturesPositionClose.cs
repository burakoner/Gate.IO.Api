namespace Gate.IO.Api.Futures;

public class FuturesPositionClose
{
    [JsonProperty("time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("pnl")]
    public decimal PNL { get; set; }

    [JsonProperty("side"), JsonConverter(typeof(FuturesPositionSideConverter))]
    public FuturesPositionSide Side { get; set; }

    [JsonProperty("contract")]
    public string Contract { get; set; }

    [JsonProperty("text")]
    public string ClientOrderId { get; set; }

    [JsonProperty("user")]
    public int UserId { get; set; }
}
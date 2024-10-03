namespace Gate.IO.Api.Futures;

public class FuturesPositionClose
{
    [JsonProperty("time")]
    public DateTime Time { get; set; }

    [JsonProperty("pnl")]
    public decimal PNL { get; set; }

    [JsonProperty("side")]
    public FuturesPositionSide Side { get; set; }

    [JsonProperty("contract")]
    public string Contract { get; set; }

    [JsonProperty("text")]
    public string ClientOrderId { get; set; }

    [JsonProperty("user")]
    public int UserId { get; set; }
}
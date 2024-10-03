namespace Gate.IO.Api.Futures;

public class DeliverySettlement
{
    [JsonProperty("time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("contract")]
    public string Contract { get; set; }

    [JsonProperty("size")]
    public long Size { get; set; }

    [JsonProperty("leverage")]
    public int Leverage { get; set; }

    [JsonProperty("margin")]
    public decimal Margin { get; set; }

    [JsonProperty("entry_price")]
    public decimal EntryPrice { get; set; }

    [JsonProperty("settle_price")]
    public decimal SettlePrice { get; set; }

    [JsonProperty("profit")]
    public decimal Profit { get; set; }

    [JsonProperty("fee")]
    public decimal Fee { get; set; }
}

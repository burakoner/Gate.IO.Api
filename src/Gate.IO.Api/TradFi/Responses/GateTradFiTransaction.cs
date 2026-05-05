namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi transaction
/// </summary>
public record GateTradFiTransaction
{
    [JsonProperty("asset")]
    public string Asset { get; set; }

    [JsonProperty("type"), JsonConverter(typeof(MapConverter))]
    public GateTradFiTransactionType Type { get; set; }

    [JsonProperty("type_desc")]
    public string TypeDescription { get; set; }

    [JsonProperty("change")]
    public decimal Change { get; set; }

    [JsonProperty("balance")]
    public decimal Balance { get; set; }

    [JsonProperty("time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}

namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi transaction page
/// </summary>
public record GateTradFiTransactionList
{
    [JsonProperty("total")]
    public int Total { get; set; }

    [JsonProperty("total_page")]
    public int TotalPage { get; set; }

    [JsonProperty("list")]
    public List<GateTradFiTransaction> List { get; set; } = [];
}

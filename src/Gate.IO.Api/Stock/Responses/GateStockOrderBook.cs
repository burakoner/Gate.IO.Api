namespace Gate.IO.Api.Stock;

/// <summary>
/// Stock order book
/// </summary>
public record GateStockOrderBook
{
    private List<GateStockOrderBookEntry> bids = [];
    private List<GateStockOrderBookEntry> asks = [];

    /// <summary>Gets or sets the symbol.</summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; }
    /// <summary>Gets or sets bid entries.</summary>
    [JsonProperty("bids")]
    public List<GateStockOrderBookEntry> Bids
    {
        get => bids;
        set => bids = value ?? [];
    }
    /// <summary>Gets or sets ask entries.</summary>
    [JsonProperty("asks")]
    public List<GateStockOrderBookEntry> Asks
    {
        get => asks;
        set => asks = value ?? [];
    }
}

/// <summary>
/// Stock order book entry
/// </summary>
public record GateStockOrderBookEntry
{
    /// <summary>Gets or sets the price.</summary>
    [JsonProperty("p")]
    public decimal Price { get; set; }
    /// <summary>Gets or sets whether the current user has an order at this price.</summary>
    [JsonProperty("user_order")]
    public bool IsUserOrder { get; set; }
}

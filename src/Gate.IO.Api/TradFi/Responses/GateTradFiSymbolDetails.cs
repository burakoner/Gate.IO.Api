namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi trading symbol details
/// </summary>
public record GateTradFiSymbolDetails
{
    /// <summary>
    /// Gets or sets the Symbol.
    /// </summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    /// <summary>
    /// Gets or sets the Description.
    /// </summary>
    [JsonProperty("symbol_desc")]
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the Category Name.
    /// </summary>
    [JsonProperty("category_name")]
    public string CategoryName { get; set; }

    /// <summary>
    /// Gets or sets the Contract Volume.
    /// </summary>
    [JsonProperty("contract_volume")]
    public decimal ContractVolume { get; set; }

    /// <summary>
    /// Gets or sets the Settlement Currency.
    /// </summary>
    [JsonProperty("settlement_currency")]
    public string SettlementCurrency { get; set; }

    /// <summary>
    /// Gets or sets the Max Order Volume.
    /// </summary>
    [JsonProperty("max_order_volume")]
    public decimal MaxOrderVolume { get; set; }

    /// <summary>
    /// Gets or sets the Min Order Volume.
    /// </summary>
    [JsonProperty("min_order_volume")]
    public decimal MinOrderVolume { get; set; }

    /// <summary>
    /// Gets or sets the Leverage.
    /// </summary>
    [JsonProperty("leverage")]
    public int Leverage { get; set; }

    /// <summary>
    /// Gets or sets the Price Precision.
    /// </summary>
    [JsonProperty("price_precision")]
    public int PricePrecision { get; set; }

    /// <summary>
    /// Gets or sets the Stop Loss Price Level.
    /// </summary>
    [JsonProperty("price_sl_level")]
    public decimal StopLossPriceLevel { get; set; }

    /// <summary>
    /// Gets or sets the Swap Cost Type.
    /// </summary>
    [JsonProperty("swap_cost_type")]
    public string SwapCostType { get; set; }

    /// <summary>
    /// Gets or sets the Buy Swap Cost Rate.
    /// </summary>
    [JsonProperty("buy_swap_cost_rate")]
    public decimal BuySwapCostRate { get; set; }

    /// <summary>
    /// Gets or sets the Sell Swap Cost Rate.
    /// </summary>
    [JsonProperty("sell_swap_cost_rate")]
    public decimal SellSwapCostRate { get; set; }

    /// <summary>
    /// Gets or sets the Swap Cost3 Day.
    /// </summary>
    [JsonProperty("swap_cost_3day")]
    public string SwapCost3Day { get; set; }

    /// <summary>
    /// Gets or sets the Trade Timezone.
    /// </summary>
    [JsonProperty("trade_timezone")]
    public string TradeTimezone { get; set; }

    /// <summary>
    /// Gets or sets the Trade Mode.
    /// </summary>
    [JsonProperty("trade_mode"), JsonConverter(typeof(MapConverter))]
    public GateTradFiTradeMode TradeMode { get; set; }

    /// <summary>
    /// Gets or sets the Icon Link.
    /// </summary>
    [JsonProperty("icon_link")]
    public string IconLink { get; set; }
}

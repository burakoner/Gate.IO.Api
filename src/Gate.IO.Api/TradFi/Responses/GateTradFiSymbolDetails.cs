namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi trading symbol details
/// </summary>
public record GateTradFiSymbolDetails
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("symbol_desc")]
    public string Description { get; set; }

    [JsonProperty("category_name")]
    public string CategoryName { get; set; }

    [JsonProperty("contract_volume")]
    public decimal ContractVolume { get; set; }

    [JsonProperty("settlement_currency")]
    public string SettlementCurrency { get; set; }

    [JsonProperty("max_order_volume")]
    public decimal MaxOrderVolume { get; set; }

    [JsonProperty("min_order_volume")]
    public decimal MinOrderVolume { get; set; }

    [JsonProperty("leverage")]
    public int Leverage { get; set; }

    [JsonProperty("price_precision")]
    public int PricePrecision { get; set; }

    [JsonProperty("price_sl_level")]
    public decimal StopLossPriceLevel { get; set; }

    [JsonProperty("swap_cost_type")]
    public string SwapCostType { get; set; }

    [JsonProperty("buy_swap_cost_rate")]
    public decimal BuySwapCostRate { get; set; }

    [JsonProperty("sell_swap_cost_rate")]
    public decimal SellSwapCostRate { get; set; }

    [JsonProperty("swap_cost_3day")]
    public string SwapCost3Day { get; set; }

    [JsonProperty("trade_timezone")]
    public string TradeTimezone { get; set; }

    [JsonProperty("trade_mode"), JsonConverter(typeof(MapConverter))]
    public GateTradFiTradeMode TradeMode { get; set; }

    [JsonProperty("icon_link")]
    public string IconLink { get; set; }
}

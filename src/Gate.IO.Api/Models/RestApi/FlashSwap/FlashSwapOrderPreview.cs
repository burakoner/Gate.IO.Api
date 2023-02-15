namespace Gate.IO.Api.Models.RestApi.FlashSwap;

public  class FlashSwapOrderPreview
{
    [JsonProperty("preview_id")]
    public long PreviewId { get;  set; }

    [JsonProperty("sell_currency")]
    public string SellCurrency { get; set; }

    [JsonProperty("sell_amount")]
    public decimal? SellAmount { get; set; }

    [JsonProperty("buy_currency")]
    public string BuyCurrency { get; set; }

    [JsonProperty("buy_amount")]
    public decimal? BuyAmount { get; set; }

    [JsonProperty("price")]
    public decimal Price { get; set; }
}
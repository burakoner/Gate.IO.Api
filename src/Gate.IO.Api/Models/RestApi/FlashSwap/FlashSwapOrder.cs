namespace Gate.IO.Api.Models.RestApi.FlashSwap;

public  class FlashSwapOrder
{
    [JsonProperty("id")]
    public long OrderId { get;  set; }

    [JsonProperty("create_time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get;   set; }

    [JsonProperty("update_time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdateTime { get;  set; }

    [JsonProperty("user_id")]
    public long UserId { get; set; }

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

    [JsonProperty("status"), JsonConverter(typeof(SwapOrderStatusConverter))]
    public SwapOrderStatus Status { get; set; }
}
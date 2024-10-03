using Gate.IO.Api.Spot;

namespace Gate.IO.Api.Models.StreamApi.Spot;

public  class SpotStreamOrderUpdate
{
    [JsonProperty("id")]
    public long OrderId { get; set; }

    [JsonProperty("user")]
    public long UserId { get; set; }

    [JsonProperty("text")]
    public string ClientOrderId { get; set; }

    [JsonProperty("create_time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }
    
    [JsonProperty("create_time_ms")]
    public long CreateTimeInMilliseconds { get; set; }

    [JsonProperty("update_time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdateTime { get; set; }

    [JsonProperty("update_time_ms")]
    public long UpdateTimeInMilliseconds { get; set; }

    [JsonProperty("event")]
    public GateSpotOrderUpdateEvent Event { get; set; }

    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    [JsonProperty("type")]
    public GateSpotOrderType Type { get; set; }

    [JsonProperty("account")]
    public GateSpotAccountType Account { get; set; }

    [JsonProperty("side")]
    public GateSpotOrderSide Side { get; set; }

    [JsonProperty("amount")]
    public decimal? Quantity { get; set; }

    [JsonProperty("price")]
    public decimal? Price { get; set; }

    [JsonProperty("time_in_force")]
    public GateSpotTimeInForce TimeInForce { get; set; }

    [JsonProperty("left")]
    public decimal Left { get; set; }

    [JsonProperty("filled_total")]
    public decimal FilledTotal { get; set; }

    [JsonProperty("avg_deal_price")]
    public decimal? AverageDealPrice { get; set; }

    [JsonProperty("fee")]
    public decimal Fee { get; set; }

    [JsonProperty("fee_currency")]
    public string FeeCurrency { get; set; }

    [JsonProperty("point_fee")]
    public decimal? PointFee { get; set; }

    [JsonProperty("gt_fee")]
    public decimal? GtFee { get; set; }

    [JsonProperty("gt_discount")]
    public bool? GtDiscount { get; set; }

    [JsonProperty("rebated_fee")]
    public decimal RebatedFee { get; set; }

    [JsonProperty("rebated_fee_currency")]
    public string RebatedFeeCurrency { get; set; }
}
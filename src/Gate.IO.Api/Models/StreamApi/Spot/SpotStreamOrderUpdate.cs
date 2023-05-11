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
    
    [JsonProperty("update_time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdateTime { get; set; }

    [JsonProperty("create_time_ms")]
    public long CreateTimeInMilliseconds { get; set; }

    [JsonProperty("update_time_ms")]
    public long UpdateTimeInMilliseconds { get; set; }

    [JsonProperty("event"), JsonConverter(typeof(OrderUpdateEventConverter))]
    public SpotOrderUpdateEvent Event { get; set; }

    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    [JsonProperty("type"), JsonConverter(typeof(SpotOrderTypeConverter))]
    public SpotOrderType Type { get; set; }

    [JsonProperty("account"), JsonConverter(typeof(AccountTypeConverter))]
    public AccountType Account { get; set; }

    [JsonProperty("price")]
    public decimal? Price { get; set; }

    [JsonProperty("time_in_force"), JsonConverter(typeof(SpotTimeInForceConverter))]
    public SpotTimeInForce TimeInForce { get; set; }

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
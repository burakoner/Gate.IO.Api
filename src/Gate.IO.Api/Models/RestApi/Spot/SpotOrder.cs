namespace Gate.IO.Api.Models.RestApi.Spot;

/// <summary>
/// Spot order details
/// </summary>
public  class SpotOrder: SpotOrderRequest
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("id")]
    public long OrderId { get;  set; }

    /// <summary>
    /// Creation time of order
    /// </summary>
    [JsonProperty("create_time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get;   set; }

    /// <summary>
    /// Last modification time of order
    /// </summary>
    [JsonProperty("update_time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdateTime { get;  set; }

    /// <summary>
    /// Creation time of order (in milliseconds)
    /// </summary>
    [JsonProperty("create_time_ms")]
    public long CreateTimeInMilliseconds { get;  set; }

    /// <summary>
    /// Last modification time of order (in milliseconds)
    /// </summary>
    [JsonProperty("update_time_ms")]
    public long UpdateTimeInMilliseconds { get;  set; }

    /// <summary>
    /// Order status
    /// </summary>
    [JsonProperty("status"), JsonConverter(typeof(SpotOrderStatusConverter))]
    public SpotOrderStatus? Status { get; set; }

    /// <summary>
    /// Amount left to fill
    /// </summary>
    [JsonProperty("left")]
    public decimal Left { get;  set; }

    /// <summary>
    /// Total filled in quote currency. Deprecated in favor of &#x60;filled_total&#x60;
    /// </summary>
    [JsonProperty("fill_price")]
    public decimal? FillPrice { get;  set; }

    /// <summary>
    /// Total filled in quote currency
    /// </summary>
    [JsonProperty("filled_total")]
    public decimal FilledTotal { get;  set; }

    /// <summary>
    /// Average fill price
    /// </summary>
    [JsonProperty("avg_deal_price")]
    public decimal? AverageDealPrice { get;  set; }

    /// <summary>
    /// Fee deducted
    /// </summary>
    [JsonProperty("fee")]
    public decimal Fee { get;  set; }

    /// <summary>
    /// Fee currency unit
    /// </summary>
    [JsonProperty("fee_currency")]
    public string FeeCurrency { get;  set; }

    /// <summary>
    /// Points used to deduct fee
    /// </summary>
    [JsonProperty("point_fee")]
    public decimal? PointFee { get;  set; }

    /// <summary>
    /// GT used to deduct fee
    /// </summary>
    [JsonProperty("gt_fee")]
    public decimal? GtFee { get;  set; }

    /// <summary>
    /// GT used to deduct maker fee
    /// </summary>
    [JsonProperty("gt_maker_fee")]
    public decimal? GtMakerFee { get;  set; }

    /// <summary>
    /// GT used to deduct taker fee
    /// </summary>
    [JsonProperty("gt_taker_fee")]
    public decimal? GtTakerFee { get; set; }

    /// <summary>
    /// Whether GT fee discount is used
    /// </summary>
    [JsonProperty("gt_discount")]
    public bool? GtDiscount { get; set; }

    /// <summary>
    /// Rebated fee
    /// </summary>
    [JsonProperty("rebated_fee")]
    public decimal RebatedFee { get; set; }

    /// <summary>
    /// Rebated fee currency unit
    /// </summary>
    [JsonProperty("rebated_fee_currency")]
    public string RebatedFeeCurrency { get; set; }
}

public class SpotBatchOrder : SpotOrder
{
    [JsonProperty("succeeded")]
    public bool Succeeded { get; set; }

    [JsonProperty("label")]
    public string Label { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }
}

public class SpotOpenOrders
{
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    [JsonProperty("total")]
    public int Total { get; set; }

    [JsonProperty("orders")]
    public IEnumerable<SpotOrder> Orders { get; set; } = new List<SpotOrder>();
}
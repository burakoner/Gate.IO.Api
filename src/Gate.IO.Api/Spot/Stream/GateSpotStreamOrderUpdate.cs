namespace Gate.IO.Api.Spot;

/// <summary>
/// Represents the Gate Spot Stream Order Update.
/// </summary>
public  class GateSpotStreamOrderUpdate
{
    /// <summary>
    /// Gets or sets the Order ID.
    /// </summary>
    [JsonProperty("id")]
    public long OrderId { get; set; }

    /// <summary>
    /// Gets or sets the User ID.
    /// </summary>
    [JsonProperty("user")]
    public long UserId { get; set; }

    /// <summary>
    /// Gets or sets the Client Order ID.
    /// </summary>
    [JsonProperty("text")]
    public string ClientOrderId { get; set; }

    /// <summary>
    /// Gets or sets the Create Time.
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }
    
    /// <summary>
    /// Gets or sets the Create Time In Milliseconds.
    /// </summary>
    [JsonProperty("create_time_ms")]
    public long CreateTimeInMilliseconds { get; set; }

    /// <summary>
    /// Gets or sets the Update Time.
    /// </summary>
    [JsonProperty("update_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// Gets or sets the Update Time In Milliseconds.
    /// </summary>
    [JsonProperty("update_time_ms")]
    public long UpdateTimeInMilliseconds { get; set; }

    /// <summary>
    /// Gets or sets the Event.
    /// </summary>
    [JsonProperty("event")]
    public GateSpotOrderUpdateEvent Event { get; set; }

    /// <summary>
    /// Gets or sets the Symbol.
    /// </summary>
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    /// <summary>
    /// Gets or sets the Type.
    /// </summary>
    [JsonProperty("type")]
    public GateSpotOrderType Type { get; set; }

    /// <summary>
    /// Gets or sets the order status.
    /// </summary>
    [JsonProperty("status")]
    public GateSpotOrderStatus? Status { get; set; }

    /// <summary>
    /// Gets or sets the Account.
    /// </summary>
    [JsonProperty("account"), JsonConverter(typeof(MapConverter))]
    public GateSpotAccountType Account { get; set; }

    /// <summary>
    /// Gets or sets the Side.
    /// </summary>
    [JsonProperty("side")]
    public GateSpotOrderSide Side { get; set; }

    /// <summary>
    /// Gets or sets the Quantity.
    /// </summary>
    [JsonProperty("amount")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Gets or sets the Price.
    /// </summary>
    [JsonProperty("price")]
    public decimal? Price { get; set; }

    /// <summary>
    /// Gets or sets the Time In Force.
    /// </summary>
    [JsonProperty("time_in_force"), JsonConverter(typeof(MapConverter))]
    public GateSpotTimeInForce TimeInForce { get; set; }

    /// <summary>
    /// Gets or sets the Left.
    /// </summary>
    [JsonProperty("left")]
    public decimal Left { get; set; }

    /// <summary>
    /// Gets or sets the Filled Total.
    /// </summary>
    [JsonProperty("filled_total")]
    public decimal FilledTotal { get; set; }

    /// <summary>
    /// Gets or sets the filled amount.
    /// </summary>
    [JsonProperty("filled_amount")]
    public decimal? FilledAmount { get; set; }

    /// <summary>
    /// Gets or sets the Average Deal Price.
    /// </summary>
    [JsonProperty("avg_deal_price")]
    public decimal? AverageDealPrice { get; set; }

    /// <summary>
    /// Gets or sets the Fee.
    /// </summary>
    [JsonProperty("fee")]
    public decimal Fee { get; set; }

    /// <summary>
    /// Gets or sets the Fee Currency.
    /// </summary>
    [JsonProperty("fee_currency")]
    public string FeeCurrency { get; set; }

    /// <summary>
    /// Gets or sets the Point Fee.
    /// </summary>
    [JsonProperty("point_fee")]
    public decimal? PointFee { get; set; }

    /// <summary>
    /// Gets or sets the GT Fee.
    /// </summary>
    [JsonProperty("gt_fee")]
    public decimal? GtFee { get; set; }

    /// <summary>
    /// Gets or sets the GT Discount.
    /// </summary>
    [JsonProperty("gt_discount")]
    public bool? GtDiscount { get; set; }

    /// <summary>
    /// Gets or sets whether auto borrow was enabled.
    /// </summary>
    [JsonProperty("auto_borrow")]
    public bool? AutoBorrow { get; set; }

    /// <summary>
    /// Gets or sets whether auto repayment was enabled.
    /// </summary>
    [JsonProperty("auto_repay")]
    public bool? AutoRepay { get; set; }

    /// <summary>
    /// Gets or sets the Rebated Fee.
    /// </summary>
    [JsonProperty("rebated_fee")]
    public decimal RebatedFee { get; set; }

    /// <summary>
    /// Gets or sets the Rebated Fee Currency.
    /// </summary>
    [JsonProperty("rebated_fee_currency")]
    public string RebatedFeeCurrency { get; set; }

    /// <summary>
    /// Gets or sets the self-trade prevention group ID.
    /// </summary>
    [JsonProperty("stp_id")]
    public long? SelfTradePreventionId { get; set; }

    /// <summary>
    /// Gets or sets the self-trade prevention action.
    /// </summary>
    [JsonProperty("stp_act"), JsonConverter(typeof(MapConverter))]
    public GateSpotSelfTradeAction? SelfTradeAction { get; set; }

    /// <summary>
    /// Gets or sets the finish reason.
    /// </summary>
    [JsonProperty("finish_as"), JsonConverter(typeof(MapConverter))]
    public GateSpotFinishAs? FinishAs { get; set; }

    /// <summary>
    /// Gets or sets Gate business information.
    /// </summary>
    [JsonProperty("biz_info")]
    public string BusinessInfo { get; set; }

    /// <summary>
    /// Gets or sets the amend text.
    /// </summary>
    [JsonProperty("amend_text")]
    public string AmendText { get; set; }

    /// <summary>
    /// Gets or sets the maximum price deviation for market orders.
    /// </summary>
    [JsonProperty("slippage")]
    public decimal? Slippage { get; set; }
}

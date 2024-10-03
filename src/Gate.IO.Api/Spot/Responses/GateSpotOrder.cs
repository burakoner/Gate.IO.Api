﻿namespace Gate.IO.Api.Spot;

/// <summary>
/// Spot order details
/// </summary>
public class GateSpotOrder
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("id")]
    public long OrderId { get; set; }
    
    /// <summary>
    /// User defined information. If not empty, must follow the rules below:  
    /// 1. prefixed with t-
    /// 2. no longer than 28 bytes without t- prefix
    /// 3. can only include 0-9, A-Z, a-z, underscore(_), hyphen(-) or dot(.)
    /// </summary>
    [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
    public string ClientOrderId { get; set; }

    /// <summary>
    /// The custom data that the user remarked when amending the order
    /// </summary>
    [JsonProperty("amend_text")]
    public string AmendText { get; set; }

    /// <summary>
    /// Creation time of order
    /// </summary>
    [JsonProperty("create_time")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Last modification time of order
    /// </summary>
    [JsonProperty("update_time")]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// Creation time of order (in milliseconds)
    /// </summary>
    [JsonProperty("create_time_ms")]
    public long CreateTimeInMilliseconds { get; set; }

    /// <summary>
    /// Last modification time of order (in milliseconds)
    /// </summary>
    [JsonProperty("update_time_ms")]
    public long UpdateTimeInMilliseconds { get; set; }

    /// <summary>
    /// Order status
    /// </summary>
    [JsonProperty("status")]
    public GateSpotOrderStatus Status { get; set; }
    
    /// <summary>
    /// Currency pair
    /// </summary>
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }
    
    /// <summary>
    /// Order Type
    /// </summary>
    [JsonProperty("type")]
    public GateSpotOrderType Type { get; set; }
    
    /// <summary>
    /// Account type. spot - use spot account; margin - use margin account; cross_margin - use cross margin account. Portfolio margin account must set to &#x60;cross-margin&#x60; 
    /// </summary>
    [JsonProperty("account")]
    public GateSpotAccountType Account { get; set; }
    
    /// <summary>
    /// Order side
    /// </summary>
    [JsonProperty("side")]
    public GateSpotOrderSide Side { get; set; }
    
    /// <summary>
    /// When "type" is limit, it refers to base currency.  For instance, &#x60;BTC_USDT&#x60; means &#x60;BTC&#x60;  When &#x60;type&#x60; is &#x60;market&#x60;, it refers to different currency according to &#x60;side&#x60;  - &#x60;side&#x60; : &#x60;buy&#x60; means quote currency, &#x60;BTC_USDT&#x60; means &#x60;USDT&#x60; - &#x60;side&#x60; : &#x60;sell&#x60; means base currency，&#x60;BTC_USDT&#x60; means &#x60;BTC&#x60; 
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Price can't be empty when "type" is limit
    /// </summary>
    [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? Price { get; set; }
    /// <summary>
    /// Time in force  - gtc: GoodTillCancelled - ioc: ImmediateOrCancelled, taker only - poc: PendingOrCancelled, makes a post-only order that always enjoys a maker fee - fok: FillOrKill, fill either completely or none Only &#x60;ioc&#x60; and &#x60;fok&#x60; are supported when &#x60;type&#x60;&#x3D;&#x60;market&#x60;
    /// </summary>
    [JsonProperty("time_in_force")]
    public GateSpotTimeInForce TimeInForce { get; set; }
    
    /// <summary>
    /// Amount to display for the iceberg order. Null or 0 for normal orders. Set to -1 to hide the order completely
    /// </summary>
    [JsonProperty("iceberg", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? Iceberg { get; set; }
    
    /// <summary>
    /// Used in margin or cross margin trading to allow automatic loan of insufficient amount if balance is not enough.
    /// </summary>
    [JsonProperty("auto_borrow", NullValueHandling = NullValueHandling.Ignore)]
    public bool? AutoBorrow { get; set; }

    /// <summary>
    /// Enable or disable automatic repayment for automatic borrow loan generated by cross margin order. Default is disabled. Note that:  1. This field is only effective for cross margin orders. Margin account does not support setting auto repayment for orders. 2. &#x60;auto_borrow&#x60; and &#x60;auto_repay&#x60; cannot be both set to true in one order.
    /// </summary>
    [JsonProperty("auto_repay", NullValueHandling = NullValueHandling.Ignore)]
    public bool? AutoRepay { get; set; }

    /// <summary>
    /// Amount left to fill
    /// </summary>
    [JsonProperty("left")]
    public decimal Left { get; set; }

    /// <summary>
    /// Amount traded to fill
    /// </summary>
    [JsonProperty("filled_amount")]
    public decimal FilledAmount { get; set; }

    /// <summary>
    /// Total filled in quote currency. Deprecated in favor of &#x60;filled_total&#x60;
    /// </summary>
    [JsonProperty("fill_price")]
    public decimal? FillPrice { get; set; }

    /// <summary>
    /// Total filled in quote currency
    /// </summary>
    [JsonProperty("filled_total")]
    public decimal FilledTotal { get; set; }

    /// <summary>
    /// Average fill price
    /// </summary>
    [JsonProperty("avg_deal_price")]
    public decimal? AverageDealPrice { get; set; }

    /// <summary>
    /// Fee deducted
    /// </summary>
    [JsonProperty("fee")]
    public decimal Fee { get; set; }

    /// <summary>
    /// Fee currency unit
    /// </summary>
    [JsonProperty("fee_currency")]
    public string FeeCurrency { get; set; }

    /// <summary>
    /// Points used to deduct fee
    /// </summary>
    [JsonProperty("point_fee")]
    public decimal? PointFee { get; set; }

    /// <summary>
    /// GT used to deduct fee
    /// </summary>
    [JsonProperty("gt_fee")]
    public decimal? GtFee { get; set; }

    /// <summary>
    /// GT used to deduct maker fee
    /// </summary>
    [JsonProperty("gt_maker_fee")]
    public decimal? GtMakerFee { get; set; }

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
    
    /// <summary>
    /// Orders between users in the same stp_id group are not allowed to be self-traded
    /// </summary>
    [JsonProperty("stp_id")]
    public long? SelfTradingPreventionActionId { get; set; }
    
    /// <summary>
    /// Self-Trading Prevention Action. Users can use this field to set self-trade prevetion strategies
    /// </summary>
    [JsonProperty("stp_act")]
    public GateSpotSelfTradeAction? SelfTradingPreventionAction { get; set; }

    /// <summary>
    /// Order completion statuses include:
    /// </summary>
    [JsonProperty("finish_as")]
    public GateSpotFinishAs? FiniashAs { get; set; }

    /// <summary>
    /// Processing Mode
    /// </summary>
    [JsonProperty("action_mode")]
    public GateSpotActionMode? ActionMode { get; set; }
}
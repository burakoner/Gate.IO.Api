namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesOrder
/// </summary>
public class GateFuturesOrder
{
    /// <summary>
    /// Futures order ID
    /// </summary>
    [JsonProperty("id")]
    public long OrderId { get; set; }

    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("user")]
    public int UserId { get; set; }
    
    /// <summary>
    /// Creation time of order
    /// </summary>
    [JsonProperty("create_time")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Order finished time. Not returned if order is open
    /// </summary>
    [JsonProperty("finish_time")]
    public DateTime? FinishTime { get; set; }
    
    /// <summary>
    /// How the order was finished.  - filled: all filled - cancelled: manually cancelled - liquidated: cancelled because of liquidation - ioc: time in force is &#x60;IOC&#x60;, finish immediately - auto_deleveraged: finished by ADL - reduce_only: cancelled because of increasing position while &#x60;reduce-only&#x60; set- position_closed: cancelled because of position close 
    /// </summary>
    [JsonProperty("finish_as")]
    public GateFuturesOrderFinishAs? FinishAs { get; set; }
    
    /// <summary>
    /// Order status  - &#x60;open&#x60;: waiting to be traded - &#x60;finished&#x60;: finished
    /// </summary>
    [JsonProperty("status")]
    public GateFuturesOrderStatus Status { get; set; }

    /// <summary>
    /// Futures contract
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// Order size. Specify positive number to make a bid, and negative number to ask
    /// </summary>
    [JsonProperty("size")]
    public long Size { get; set; }

    /// <summary>
    /// Display size for iceberg order. 0 for non-iceberg. Note that you will have to pay the taker fee for the hidden size
    /// </summary>
    [JsonProperty("iceberg")]
    public long Iceberg { get; set; }

        /// <summary>
    /// Order price. 0 for market order with &#x60;tif&#x60; set as &#x60;ioc&#x60;
    /// </summary>
    [JsonProperty("price")]
    public decimal? Price { get; set; }
    
    /// <summary>
    /// Set as true to close the position, with size set to 0
    /// </summary>
    [JsonProperty("close")]
    public bool Close { get; set; }

    /// <summary>
    /// Is the order to close position
    /// </summary>
    [JsonProperty("is_close")]
    public bool IsClose { get; set; }
    
    /// <summary>
    /// Is the order reduce-only
    /// </summary>
    [JsonProperty("is_reduce_only")]
    public bool IsReduceOnly { get; set; }
    
    /// <summary>
    /// Is the order for liquidation
    /// </summary>
    [JsonProperty("is_liq")]
    public bool IsLiquidation { get; set; }
    
    /// <summary>
    /// Time in force  - gtc: GoodTillCancelled - ioc: ImmediateOrCancelled, taker only - poc: PendingOrCancelled, makes a post-only order that always enjoys a maker fee - fok: FillOrKill, fill either completely or none
    /// </summary>
    [JsonProperty("tif")]
    public GateFuturesTimeInForce TimeInForce { get; set; }

    /// <summary>
    /// Size left to be traded
    /// </summary>
    [JsonProperty("left")]
    public long Left { get; set; }

    /// <summary>
    /// Fill price of the order
    /// </summary>
    [JsonProperty("fill_price")]
    public decimal? FillPrice { get; set; }
    
    /// <summary>
    /// User defined information. If not empty, must follow the rules below:  
    /// 1. prefixed with t-
    /// 2. no longer than 28 bytes without t- prefix
    /// 3. can only include 0-9, A-Z, a-z, underscore(_), hyphen(-) or dot(.)
    /// </summary>
    [JsonProperty("text")]
    public string ClientOrderId { get; set; }

    /// <summary>
    /// Taker fee
    /// </summary>
    [JsonProperty("tkfr")]
    public decimal TakerFee { get; set; }

    /// <summary>
    /// Maker fee
    /// </summary>
    [JsonProperty("mkfr")]
    public decimal MakerFee { get; set; }

    /// <summary>
    /// Reference user ID
    /// </summary>
    [JsonProperty("refu")]
    public long ReferenceUserId { get; set; }

    /// <summary>
    /// Set side to close dual-mode position. close_long closes the long side; while close_short the short one. Note size also needs to be set to 0
    /// </summary>
    [JsonProperty("auto_size")]
    public GateFuturesOrderAutoSize? AutoSize { get; set; }

    /// <summary>
    /// Orders between users in the same stp_id group are not allowed to be self-traded
    /// 1. If the stp_id of two orders being matched is non-zero and equal, they will not be executed. Instead, the corresponding strategy will be executed based on the stp_act of the taker.
    /// 2. stp_id returns 0 by default for orders that have not been set for STP group
    /// </summary>
    [JsonProperty("stp_id")]
    public long? SelfTradeActionId { get; set; }

    /// <summary>
    /// Self-Trading Prevention Action. Users can use this field to set self-trade prevetion strategies
    /// 
    /// 1. After users join the STP Group, he can pass stp_act to limit the user's self-trade prevetion strategy. If stp_act is not passed, the default is cn strategy。
    /// 2. When the user does not join the STP group, an error will be returned when passing the stp_act parameter。
    /// 3. If the user did not use 'stp_act' when placing the order, 'stp_act' will return '-'
    /// 
    /// - cn: Cancel newest, Cancel new orders and keep old ones
    /// - co: Cancel oldest, Cancel old orders and keep new ones
    /// - cb: Cancel both, Both old and new orders will be cancelled
    /// </summary>
    [JsonProperty("stp_act")]
    public GateFuturesSelfTradeAction? SelfTradeAction { get; set; }

    /// <summary>
    /// The custom data that the user remarked when amending the order
    /// </summary>
    [JsonProperty("amend_text")]
    public string AmendText { get; set; }

    /// <summary>
    /// Additional information
    /// </summary>
    [JsonProperty("biz_info")]
    public string BusinessInfo { get; set; }
}

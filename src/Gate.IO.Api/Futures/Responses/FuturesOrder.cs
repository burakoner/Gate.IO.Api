namespace Gate.IO.Api.Futures;

public class FuturesOrder
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
    /// Futures contract
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

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
    /// Size left to be traded
    /// </summary>
    [JsonProperty("left")]
    public long Left { get; set; }

    /// <summary>
    /// Order price. 0 for market order with &#x60;tif&#x60; set as &#x60;ioc&#x60;
    /// </summary>
    [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? Price { get; set; }

    /// <summary>
    /// Fill price of the order
    /// </summary>
    [JsonProperty("fill_price")]
    public decimal? FillPrice { get; set; }

    /// <summary>
    /// Maker fee
    /// </summary>
    [JsonProperty("mkfr")]
    public decimal MakerFee { get; set; }

    /// <summary>
    /// Taker fee
    /// </summary>
    [JsonProperty("tkfr")]
    public decimal TakerFee { get; set; }

    /// <summary>
    /// Reference user ID
    /// </summary>
    [JsonProperty("refu")]
    public long ReferenceUserId { get; set; }

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
    [JsonProperty("tif", NullValueHandling = NullValueHandling.Ignore)]
    public FuturesTimeInForce? TimeInForce { get; set; }

    /// <summary>
    /// User defined information. If not empty, must follow the rules below:  
    /// 1. prefixed with t-
    /// 2. no longer than 28 bytes without t- prefix
    /// 3. can only include 0-9, A-Z, a-z, underscore(_), hyphen(-) or dot(.)
    /// </summary>
    [JsonProperty("text")]
    public string ClientOrderId { get; set; }

    /// <summary>
    /// Order status  - &#x60;open&#x60;: waiting to be traded - &#x60;finished&#x60;: finished
    /// </summary>
    [JsonProperty("status")]
    public FuturesOrderStatus Status { get; set; }

    /// <summary>
    /// How the order was finished.  - filled: all filled - cancelled: manually cancelled - liquidated: cancelled because of liquidation - ioc: time in force is &#x60;IOC&#x60;, finish immediately - auto_deleveraged: finished by ADL - reduce_only: cancelled because of increasing position while &#x60;reduce-only&#x60; set- position_closed: cancelled because of position close 
    /// </summary>
    [JsonProperty("finish_as")]
    public FuturesOrderFinishType? FinishedAs { get; set; }
}

public class FuturesBatchOrder : FuturesOrder
{
    [JsonProperty("succeeded")]
    public bool Succeeded { get; set; }

    [JsonProperty("label")]
    public string Label { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }
}
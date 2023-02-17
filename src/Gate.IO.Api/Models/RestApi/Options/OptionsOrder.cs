namespace Gate.IO.Api.Models.RestApi.Options;

public class OptionsOrder
{
    /// <summary>
    /// Options order ID
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
    [JsonProperty("create_time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Order finished time. Not returned if order is open
    /// </summary>
    [JsonProperty("finish_time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? FinishTime { get; set; }

    /// <summary>
    /// How the order was finished.
    /// </summary>
    [JsonProperty("finish_as"), JsonConverter(typeof(OptionsOrderFinishTypeConverter))]
    public OptionsOrderFinishType? FinishedAs { get; set; }

    [JsonProperty("status"), JsonConverter(typeof(OptionsOrderStatusConverter))]
    public OptionsOrderStatus Status { get; set; }

    [JsonProperty("tif"), JsonConverter(typeof(OptionsTimeInForceConverter))]
    public OptionsTimeInForce TimeInForce{ get; set; }

    /// <summary>
    /// Contract name
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
    /// Order price. 0 for market order with &#x60;tif&#x60; set as &#x60;ioc&#x60; (USDT)
    /// </summary>
    [JsonProperty("price")]
    public decimal? Price { get; set; }

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
    public bool IsLiquidated { get; set; }

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
    /// User defined information. If not empty, must follow the rules below:  1. prefixed with &#x60;t-&#x60; 2. no longer than 28 bytes without &#x60;t-&#x60; prefix 3. can only include 0-9, A-Z, a-z, underscore(_), hyphen(-) or dot(.) Besides user defined information, reserved contents are listed below, denoting how the order is created:  - web: from web - api: from API - app: from mobile phones - auto_deleveraging: from ADL - liquidation: from liquidation - insurance: from insurance 
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
    public int? RefUserId { get; set; }

    /// <summary>
    /// Referrer rebate
    /// </summary>
    [JsonProperty("refr")]
    public decimal? RefRebate { get; set; }
}
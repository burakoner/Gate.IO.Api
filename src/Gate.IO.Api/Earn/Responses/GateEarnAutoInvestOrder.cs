namespace Gate.IO.Api.Earn;

/// <summary>
/// Auto invest order item
/// </summary>
public record GateEarnAutoInvestOrder
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; }

    /// <summary>
    /// Size
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Plan ID
    /// </summary>
    [JsonProperty("plan_id")]
    public long PlanId { get; set; }

    /// <summary>
    /// Direction
    /// </summary>
    [JsonProperty("side")]
    public long Side { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("asset")]
    public string Asset { get; set; }

    /// <summary>
    /// Record ID
    /// </summary>
    [JsonProperty("record_id")]
    public long RecordId { get; set; }

    /// <summary>
    /// Total amount
    /// </summary>
    [JsonProperty("total_money")]
    public decimal TotalMoney { get; set; }

    /// <summary>
    /// Currency pair
    /// </summary>
    [JsonProperty("market")]
    public string Market { get; set; }

    /// <summary>
    /// Price
    /// </summary>
    [JsonProperty("price")]
    public decimal Price { get; set; }

    /// <summary>
    /// Creation time
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Total
    /// </summary>
    [JsonProperty("total")]
    public decimal Total { get; set; }

    /// <summary>
    /// Fund flow direction
    /// </summary>
    [JsonProperty("fund_flow")]
    public string FundFlow { get; set; }

    /// <summary>
    /// Error code
    /// </summary>
    [JsonProperty("error_code")]
    public long ErrorCode { get; set; }

    /// <summary>
    /// Error message
    /// </summary>
    [JsonProperty("error_msg")]
    public string ErrorMessage { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public long Status { get; set; }
}

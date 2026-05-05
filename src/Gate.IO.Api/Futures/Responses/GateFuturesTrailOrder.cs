namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures trail order
/// </summary>
public record GateFuturesTrailOrder
{
    /// <summary>
    /// Gets or sets the Order ID.
    /// </summary>
    [JsonProperty("id")]
    public long OrderId { get; set; }

    /// <summary>
    /// Gets or sets the User ID.
    /// </summary>
    [JsonProperty("user_id")]
    public long? UserId { get; set; }

    /// <summary>
    /// Gets or sets the User.
    /// </summary>
    [JsonProperty("user")]
    public long? User { get; set; }

    /// <summary>
    /// Gets or sets the Contract.
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// Gets or sets the Settlement.
    /// </summary>
    [JsonProperty("settle")]
    public string Settlement { get; set; }

    /// <summary>
    /// Gets or sets the Amount.
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the Is Greater Than Or Equal.
    /// </summary>
    [JsonProperty("is_gte")]
    public bool IsGreaterThanOrEqual { get; set; }

    /// <summary>
    /// Gets or sets the Activation Price.
    /// </summary>
    [JsonProperty("activation_price")]
    public decimal? ActivationPrice { get; set; }

    /// <summary>
    /// Gets or sets the Price Type.
    /// </summary>
    [JsonProperty("price_type")]
    public GateFuturesTrailPriceType PriceType { get; set; }

    /// <summary>
    /// Gets or sets the Price Offset.
    /// </summary>
    [JsonProperty("price_offset")]
    public string PriceOffset { get; set; }

    /// <summary>
    /// Gets or sets the Client Order ID.
    /// </summary>
    [JsonProperty("text")]
    public string ClientOrderId { get; set; }

    /// <summary>
    /// Gets or sets the Reduce Only.
    /// </summary>
    [JsonProperty("reduce_only")]
    public bool ReduceOnly { get; set; }

    /// <summary>
    /// Gets or sets the Position Related.
    /// </summary>
    [JsonProperty("position_related")]
    public bool PositionRelated { get; set; }

    /// <summary>
    /// Gets or sets the Created At.
    /// </summary>
    [JsonProperty("created_at")]
    public long? CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the Activated At.
    /// </summary>
    [JsonProperty("activated_at")]
    public long? ActivatedAt { get; set; }

    /// <summary>
    /// Gets or sets the Finished At.
    /// </summary>
    [JsonProperty("finished_at")]
    public long? FinishedAt { get; set; }

    /// <summary>
    /// Gets or sets the Create Time.
    /// </summary>
    [JsonProperty("create_time")]
    public long? CreateTime { get; set; }

    /// <summary>
    /// Gets or sets the Active Time.
    /// </summary>
    [JsonProperty("active_time")]
    public long? ActiveTime { get; set; }

    /// <summary>
    /// Gets or sets the Finish Time.
    /// </summary>
    [JsonProperty("finish_time")]
    public long? FinishTime { get; set; }

    /// <summary>
    /// Gets or sets the Reason.
    /// </summary>
    [JsonProperty("reason")]
    public string Reason { get; set; }

    /// <summary>
    /// Gets or sets the Sub Order Client Order ID.
    /// </summary>
    [JsonProperty("suborder_text")]
    public string SubOrderClientOrderId { get; set; }

    /// <summary>
    /// Gets or sets the Is Dual Mode.
    /// </summary>
    [JsonProperty("is_dual_mode")]
    public bool? IsDualMode { get; set; }

    /// <summary>
    /// Gets or sets the Trigger Price.
    /// </summary>
    [JsonProperty("trigger_price")]
    public decimal? TriggerPrice { get; set; }

    /// <summary>
    /// Gets or sets the Sub Order ID.
    /// </summary>
    [JsonProperty("suborder_id")]
    public long? SubOrderId { get; set; }

    /// <summary>
    /// Gets or sets the Side Label.
    /// </summary>
    [JsonProperty("side_label")]
    public string SideLabel { get; set; }

    /// <summary>
    /// Gets or sets the Original Status.
    /// </summary>
    [JsonProperty("original_status")]
    public int? OriginalStatus { get; set; }

    /// <summary>
    /// Gets or sets the Status.
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }

    /// <summary>
    /// Gets or sets the Position Side Output.
    /// </summary>
    [JsonProperty("position_side_output")]
    public string PositionSideOutput { get; set; }

    /// <summary>
    /// Gets or sets the Updated At.
    /// </summary>
    [JsonProperty("updated_at")]
    public long? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the Extremum Price.
    /// </summary>
    [JsonProperty("extremum_price")]
    public decimal? ExtremumPrice { get; set; }

    /// <summary>
    /// Gets or sets the Status Code.
    /// </summary>
    [JsonProperty("status_code")]
    public string StatusCode { get; set; }

    /// <summary>
    /// Gets or sets the Created At Precise.
    /// </summary>
    [JsonProperty("created_at_precise")]
    public string CreatedAtPrecise { get; set; }

    /// <summary>
    /// Gets or sets the Finished At Precise.
    /// </summary>
    [JsonProperty("finished_at_precise")]
    public string FinishedAtPrecise { get; set; }

    /// <summary>
    /// Gets or sets the Activated At Precise.
    /// </summary>
    [JsonProperty("activated_at_precise")]
    public string ActivatedAtPrecise { get; set; }

    /// <summary>
    /// Gets or sets the Status Label.
    /// </summary>
    [JsonProperty("status_label")]
    public string StatusLabel { get; set; }

    /// <summary>
    /// Gets or sets the Position Margin Mode.
    /// </summary>
    [JsonProperty("pos_margin_mode")]
    public string PositionMarginMode { get; set; }

    /// <summary>
    /// Gets or sets the Position Mode.
    /// </summary>
    [JsonProperty("position_mode")]
    public string PositionMode { get; set; }

    /// <summary>
    /// Gets or sets the Error Label.
    /// </summary>
    [JsonProperty("error_label")]
    public string ErrorLabel { get; set; }

    /// <summary>
    /// Gets or sets the Leverage.
    /// </summary>
    [JsonProperty("leverage")]
    public string Leverage { get; set; }
}

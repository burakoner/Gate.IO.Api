namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures trail order
/// </summary>
public record GateFuturesTrailOrder
{
    [JsonProperty("id")]
    public long OrderId { get; set; }

    [JsonProperty("user_id")]
    public long? UserId { get; set; }

    [JsonProperty("user")]
    public long? User { get; set; }

    [JsonProperty("contract")]
    public string Contract { get; set; }

    [JsonProperty("settle")]
    public string Settlement { get; set; }

    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    [JsonProperty("is_gte")]
    public bool IsGreaterThanOrEqual { get; set; }

    [JsonProperty("activation_price")]
    public decimal? ActivationPrice { get; set; }

    [JsonProperty("price_type")]
    public GateFuturesTrailPriceType PriceType { get; set; }

    [JsonProperty("price_offset")]
    public string PriceOffset { get; set; }

    [JsonProperty("text")]
    public string ClientOrderId { get; set; }

    [JsonProperty("reduce_only")]
    public bool ReduceOnly { get; set; }

    [JsonProperty("position_related")]
    public bool PositionRelated { get; set; }

    [JsonProperty("created_at")]
    public long? CreatedAt { get; set; }

    [JsonProperty("activated_at")]
    public long? ActivatedAt { get; set; }

    [JsonProperty("finished_at")]
    public long? FinishedAt { get; set; }

    [JsonProperty("create_time")]
    public long? CreateTime { get; set; }

    [JsonProperty("active_time")]
    public long? ActiveTime { get; set; }

    [JsonProperty("finish_time")]
    public long? FinishTime { get; set; }

    [JsonProperty("reason")]
    public string Reason { get; set; }

    [JsonProperty("suborder_text")]
    public string SubOrderClientOrderId { get; set; }

    [JsonProperty("is_dual_mode")]
    public bool? IsDualMode { get; set; }

    [JsonProperty("trigger_price")]
    public decimal? TriggerPrice { get; set; }

    [JsonProperty("suborder_id")]
    public long? SubOrderId { get; set; }

    [JsonProperty("side_label")]
    public string SideLabel { get; set; }

    [JsonProperty("original_status")]
    public int? OriginalStatus { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("position_side_output")]
    public string PositionSideOutput { get; set; }

    [JsonProperty("updated_at")]
    public long? UpdatedAt { get; set; }

    [JsonProperty("extremum_price")]
    public decimal? ExtremumPrice { get; set; }

    [JsonProperty("status_code")]
    public string StatusCode { get; set; }

    [JsonProperty("created_at_precise")]
    public string CreatedAtPrecise { get; set; }

    [JsonProperty("finished_at_precise")]
    public string FinishedAtPrecise { get; set; }

    [JsonProperty("activated_at_precise")]
    public string ActivatedAtPrecise { get; set; }

    [JsonProperty("status_label")]
    public string StatusLabel { get; set; }

    [JsonProperty("pos_margin_mode")]
    public string PositionMarginMode { get; set; }

    [JsonProperty("position_mode")]
    public string PositionMode { get; set; }

    [JsonProperty("error_label")]
    public string ErrorLabel { get; set; }

    [JsonProperty("leverage")]
    public string Leverage { get; set; }
}

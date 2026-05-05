namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures trail order change-log item
/// </summary>
public record GateFuturesTrailOrderChange
{
    /// <summary>
    /// Gets or sets the Updated At.
    /// </summary>
    [JsonProperty("updated_at")]
    public long UpdatedAt { get; set; }

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
    public decimal ActivationPrice { get; set; }

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
    /// Gets or sets the Is Create.
    /// </summary>
    [JsonProperty("is_create")]
    public bool IsCreate { get; set; }
}

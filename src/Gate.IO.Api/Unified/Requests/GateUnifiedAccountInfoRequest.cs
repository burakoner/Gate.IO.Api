namespace Gate.IO.Api.Unified;

/// <summary>
/// Unified account information query request
/// </summary>
public record GateUnifiedAccountInfoRequest
{
    /// <summary>
    /// Query by specified currency name
    /// </summary>
    [JsonProperty("currency", NullValueHandling = NullValueHandling.Ignore)]
    public string Currency { get; set; }

    /// <summary>
    /// Sub-account user ID
    /// </summary>
    [JsonProperty("sub_uid", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateFuturesOrderIdConverter))]
    public long? SubAccountId { get; set; }
}

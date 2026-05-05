namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi user
/// </summary>
public record GateTradFiUser
{
    [JsonProperty("status")]
    public GateTradFiAccountStatus Status { get; set; }

    [JsonProperty("leverage")]
    public int Leverage { get; set; }

    [JsonProperty("mt5_uid")]
    public long Mt5Uid { get; set; }
}

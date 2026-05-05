namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi MT5 account information
/// </summary>
public record GateTradFiMt5Account
{
    [JsonProperty("mt5_uid")]
    public long Mt5Uid { get; set; }

    [JsonProperty("leverage")]
    public int Leverage { get; set; }

    [JsonProperty("stop_out_level")]
    public string StopOutLevel { get; set; }

    [JsonProperty("status")]
    public GateTradFiAccountStatus Status { get; set; }
}

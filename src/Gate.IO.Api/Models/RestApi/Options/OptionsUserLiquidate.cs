namespace Gate.IO.Api.Models.RestApi.Options;

public class OptionsUserLiquidate
{
    [JsonProperty("time")]
    public DateTime Time { get; set; }

    /// <summary>
    /// PNL
    /// </summary>
    [JsonProperty("pnl")]
    public decimal PNL { get; set; }

    /// <summary>
    /// settlement size
    /// </summary>
    [JsonProperty("settle_size")]
    public decimal SettleSize { get; set; }

    [JsonProperty("side")]
    public OptionsSide Side { get; set; }

    /// <summary>
    /// Options contract name
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// Text of close order
    /// </summary>
    [JsonProperty("text")]
    public string Comment { get; set; }

}
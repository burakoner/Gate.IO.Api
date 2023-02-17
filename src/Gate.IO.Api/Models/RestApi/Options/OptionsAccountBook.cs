namespace Gate.IO.Api.Models.RestApi.Options;

public class OptionsAccountBook
{
    /// <summary>
    /// Change time
    /// </summary>
    [JsonProperty("time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Amount changed (USDT)
    /// </summary>
    [JsonProperty("change")]
    public decimal Change { get; set; }

    /// <summary>
    /// Account total balance after change (USDT)
    /// </summary>
    [JsonProperty("balance")]
    public decimal Balance { get; set; }

    /// <summary>
    /// Changing Type: - dnw: Deposit &amp; Withdraw - prem: Trading premium - fee: Trading fee - refr: Referrer rebate - point_dnw: POINT Deposit &amp; Withdraw - point_fee: POINT Trading fee - point_refr: POINT Referrer rebate
    /// </summary>
    [JsonProperty("type"), JsonConverter(typeof(OptionsAccountBookType))]
    public OptionsAccountBookType Type { get; set; }

    /// <summary>
    /// custom text
    /// </summary>
    [JsonProperty("text")]
    public string Comment { get; set; }
}
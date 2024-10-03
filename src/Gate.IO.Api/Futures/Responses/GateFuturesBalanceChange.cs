namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesBalanceChange
/// </summary>
public class GateFuturesBalanceChange
{
    /// <summary>
    /// Change time
    /// </summary>
    [JsonProperty("time")]
    public DateTime Time { get; set; }
    
    /// <summary>
    /// Change amount
    /// </summary>
    [JsonProperty("change")]
    public decimal Change { get; set; }
    
    /// <summary>
    /// Balance after change
    /// </summary>
    [JsonProperty("balance")]
    public decimal Balance { get; set; }

    /// <summary>
    /// Changing Type：
    /// </summary>
    [JsonProperty("type")]
    public GateFuturesBalanceType Type { get; set; }

    /// <summary>
    /// Comment
    /// </summary>
    [JsonProperty("text")]
    public string Comment { get; set; }

    /// <summary>
    /// Futures contract, the field is only available for data after 2023-10-30.
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// trade id
    /// </summary>
    [JsonProperty("text")]
    public long TradeId { get; set; }
}

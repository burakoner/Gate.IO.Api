namespace Gate.IO.Api.Models.RestApi.Wallet;

public class MarginBalanceHistory
{
    /// <summary>
    /// Balance change record ID
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Balance changed timestamp
    /// </summary>
    [JsonProperty("time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// The timestamp of the change (in milliseconds)
    /// </summary>
    [JsonProperty("time_ms")]
    public decimal TimeMilliseconds { get; set; }

    /// <summary>
    /// Currency changed
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Account currency pair
    /// </summary>
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    /// <summary>
    /// Amount changed. Positive value means transferring in, while negative out
    /// </summary>
    [JsonProperty("change")]
    public decimal Change { get; set; }

    /// <summary>
    /// Balance after change
    /// </summary>
    [JsonProperty("balance")]
    public decimal Balance { get; set; }
}

namespace Gate.IO.Api.Options;

/// <summary>
/// GateOptionsUnderlying
/// </summary>
public class GateOptionsUnderlying
{
    /// <summary>
    /// Underlying Name
    /// </summary>
    [JsonProperty("name")]
    public string Underlying { get; set; }

    /// <summary>
    /// Spot index price (quote currency)
    /// </summary>
    [JsonProperty("index_price")]
    public decimal IndexPrice { get; set; }
}

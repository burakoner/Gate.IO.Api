namespace Gate.IO.Api.Futures;

/// <summary>
/// FuturesIndexConstituents
/// </summary>
public class GateFuturesIndexConstituents
{
    /// <summary>
    /// Index name
    /// </summary>
    [JsonProperty("index")]
    public string Index { get; set; }

    /// <summary>
    /// Constituents
    /// </summary>
    [JsonProperty("constituents")]
    public List<GateFuturesIndexConstituent> Constituents { get; set; }=[];
}

/// <summary>
/// IndexConstituent
/// </summary>
public class GateFuturesIndexConstituent
{
    /// <summary>
    /// Exchange
    /// </summary>
    [JsonProperty("exchange")]
    public string Exchange { get; set; }

    /// <summary>
    /// Symbol list
    /// </summary>
    [JsonProperty("symbols")]
    public List<string> Symbols { get; set; }=[];
}

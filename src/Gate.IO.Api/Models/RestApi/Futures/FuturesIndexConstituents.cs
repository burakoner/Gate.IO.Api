namespace Gate.IO.Api.Models.RestApi.Futures;

public class FuturesIndexConstituents
{
    /// <summary>
    /// Index name
    /// </summary>
    [JsonProperty("index")]
    public string Index { get;  set; }

    /// <summary>
    /// Constituents
    /// </summary>
    [JsonProperty("constituents")]
    public IEnumerable<IndexConstituent> Constituents { get;  set; }
}

public class IndexConstituent
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
    public IEnumerable<string> Symbols { get; set; }
}

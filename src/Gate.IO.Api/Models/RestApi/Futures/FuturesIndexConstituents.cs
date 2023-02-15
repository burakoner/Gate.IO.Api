namespace Gate.IO.Api.Models.RestApi.Futures;

public class FuturesIndexConstituents
{
    /// <summary>
    /// Index name
    /// </summary>
    [JsonProperty("index")]
    public string Index { get; private set; }

    /// <summary>
    /// Constituents
    /// </summary>
    [JsonProperty("constituents")]
    public IEnumerable<IndexConstituent> Constituents { get; private set; }
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

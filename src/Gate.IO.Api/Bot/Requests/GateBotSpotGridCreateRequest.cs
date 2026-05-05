namespace Gate.IO.Api.Bot;

/// <summary>
/// Spot grid creation request
/// </summary>
public record GateBotSpotGridCreateRequest : GateBotRequestHeaders
{
    /// <summary>
    /// Gets or sets the Market.
    /// </summary>
    public string Market { get; set; }

    /// <summary>
    /// Gets or sets the Create Parameters.
    /// </summary>
    public GateBotSpotGridCreateParameters CreateParameters { get; set; }
}

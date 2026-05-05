namespace Gate.IO.Api.Bot;

/// <summary>
/// Spot martingale creation request
/// </summary>
public record GateBotSpotMartingaleCreateRequest : GateBotRequestHeaders
{
    /// <summary>
    /// Gets or sets the Market.
    /// </summary>
    public string Market { get; set; }

    /// <summary>
    /// Gets or sets the Create Parameters.
    /// </summary>
    public GateBotSpotMartingaleCreateParameters CreateParameters { get; set; }
}

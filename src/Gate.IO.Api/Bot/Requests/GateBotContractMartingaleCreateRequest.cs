namespace Gate.IO.Api.Bot;

/// <summary>
/// Contract martingale creation request
/// </summary>
public record GateBotContractMartingaleCreateRequest : GateBotRequestHeaders
{
    /// <summary>
    /// Gets or sets the Market.
    /// </summary>
    public string Market { get; set; }

    /// <summary>
    /// Gets or sets the Create Parameters.
    /// </summary>
    public GateBotContractMartingaleCreateParameters CreateParameters { get; set; }
}

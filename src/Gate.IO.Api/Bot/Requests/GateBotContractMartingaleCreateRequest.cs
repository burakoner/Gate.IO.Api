namespace Gate.IO.Api.Bot;

/// <summary>
/// Contract martingale creation request
/// </summary>
public record GateBotContractMartingaleCreateRequest : GateBotRequestHeaders
{
    public string Market { get; set; }

    public GateBotContractMartingaleCreateParameters CreateParameters { get; set; }
}

namespace Gate.IO.Api.Options;

/// <summary>
/// Options contract query request
/// </summary>
public record GateOptionsContractQueryRequest
{
    public string Underlying { get; set; }
    public long? Expiration { get; set; }
}

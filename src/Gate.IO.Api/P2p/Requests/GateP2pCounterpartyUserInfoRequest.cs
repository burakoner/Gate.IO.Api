namespace Gate.IO.Api.P2p;

/// <summary>
/// Counterparty user info request
/// </summary>
public record GateP2pCounterpartyUserInfoRequest
{
    /// <summary>
    /// Counterparty encrypted UID
    /// </summary>
    public string BusinessUserId { get; set; }
}

namespace Gate.IO.Api.Wallet;

/// <summary>
/// Wallet sub-account balance query request
/// </summary>
public record GateWalletSubAccountBalanceQueryRequest
{
    /// <summary>
    /// Sub-account user IDs
    /// </summary>
    public List<long> SubAccounts { get; set; }

    /// <summary>
    /// Page number
    /// </summary>
    public int? Page { get; set; }

    /// <summary>
    /// Maximum number of records returned
    /// </summary>
    public int? Limit { get; set; }
}

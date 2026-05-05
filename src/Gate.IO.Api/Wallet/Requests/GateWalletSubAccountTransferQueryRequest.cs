namespace Gate.IO.Api.Wallet;

/// <summary>
/// Wallet main-sub account transfer records query request
/// </summary>
public record GateWalletSubAccountTransferQueryRequest
{
    /// <summary>
    /// Sub-account user IDs
    /// </summary>
    public List<long> SubAccounts { get; set; }

    /// <summary>
    /// Start time for querying records
    /// </summary>
    public DateTime? From { get; set; }

    /// <summary>
    /// End timestamp for the query
    /// </summary>
    public DateTime? To { get; set; }

    /// <summary>
    /// Maximum number of records returned in a single list
    /// </summary>
    public int? Limit { get; set; } = 100;

    /// <summary>
    /// List offset, starting from 0
    /// </summary>
    public int? Offset { get; set; } = 0;
}

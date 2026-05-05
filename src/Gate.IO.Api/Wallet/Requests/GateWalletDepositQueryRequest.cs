namespace Gate.IO.Api.Wallet;

/// <summary>
/// Wallet deposit records query request
/// </summary>
public record GateWalletDepositQueryRequest
{
    /// <summary>
    /// Specify the currency. If not specified, returns all currencies
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Start time for querying records
    /// </summary>
    public DateTime? From { get; set; }

    /// <summary>
    /// End timestamp for the query
    /// </summary>
    public DateTime? To { get; set; }

    /// <summary>
    /// Maximum number of entries returned in the list
    /// </summary>
    public int? Limit { get; set; } = 100;

    /// <summary>
    /// List offset, starting from 0
    /// </summary>
    public int? Offset { get; set; } = 0;
}

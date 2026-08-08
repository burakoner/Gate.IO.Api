namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx account book query request
/// </summary>
public record GateCrossExAccountBookQueryRequest
{
    /// <summary>
    /// Page number
    /// </summary>
    public int? Page { get; set; }

    /// <summary>
    /// Maximum records, max 1000
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    public string Coin { get; set; }

    /// <summary>
    /// Bill entry type. Supported values are TRANSACTION, TRADING_FEE, FUNDING_FEE, LIQUIDATION_FEE, TRANSFER_IN,
    /// TRANSFER_OUT, BANKRUPT_COMPENSATION, AUTO_REPAY, INTEREST_ISOLATED, ACCOUNT_MODE_CHANGE, KRAKEN_CONVERSION, and OTHER.
    /// </summary>
    public string StatementType { get; set; }

    /// <summary>
    /// Start time
    /// </summary>
    public DateTime? From { get; set; }

    /// <summary>
    /// End time
    /// </summary>
    public DateTime? To { get; set; }
}

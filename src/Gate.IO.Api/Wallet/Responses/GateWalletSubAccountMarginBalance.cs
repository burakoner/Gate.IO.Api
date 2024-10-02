namespace Gate.IO.Api.Wallet;

/// <summary>
/// Sub Account Margin Balance
/// </summary>
public class GateWalletSubAccountMarginBalance
{
    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("uid")]
    public long UserId { get; set; }

    /// <summary>
    /// Margin account balances
    /// </summary>
    [JsonProperty("available")]
    public List<GateWalletSubAccountMarginBalanceAvailable> Available { get; set; } = [];
}

/// <summary>
/// Sub Account Margin Balance Available
/// </summary>
public class GateWalletSubAccountMarginBalanceAvailable
{
    /// <summary>
    /// Currency pair
    /// </summary>
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    /// <summary>
    /// Whether account is locked
    /// </summary>
    [JsonProperty("locked")]
    public bool Locked { get; set; }

    /// <summary>
    /// Current risk rate of margin account
    /// </summary>
    [JsonProperty("risk")]
    public decimal Risk { get; set; }

    /// <summary>
    /// Account currency details
    /// </summary>
    [JsonProperty("base")]
    public GateWalletSubAccountMarginBalanceAvailableItem Base { get; set; }

    /// <summary>
    /// Account currency details
    /// </summary>
    [JsonProperty("quote")]
    public GateWalletSubAccountMarginBalanceAvailableItem Quote { get; set; }
}

/// <summary>
/// Sub Account Margin Balance Available Item
/// </summary>
public class GateWalletSubAccountMarginBalanceAvailableItem
{
    /// <summary>
    /// Currency name
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Amount suitable for margin trading.
    /// </summary>
    [JsonProperty("available")]
    public decimal Available { get; set; }

    /// <summary>
    /// Locked amount, used in margin trading
    /// </summary>
    [JsonProperty("locked")]
    public bool Locked { get; set; }

    /// <summary>
    /// Borrowed amount
    /// </summary>
    [JsonProperty("borrowed")]
    public decimal Borrowed { get; set; }

    /// <summary>
    /// Unpaid interests
    /// </summary>
    [JsonProperty("interest")]
    public decimal Interest { get; set; }
}

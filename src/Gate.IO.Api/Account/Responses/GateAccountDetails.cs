namespace Gate.IO.Api.Account;

/// <summary>
/// GateAccountDetails
/// </summary>
public record GateAccountDetails
{
    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// IP whitelist
    /// </summary>
    [JsonProperty("ip_whitelist")]
    public List<string> IpWhitelist { get; set; } = [];

    /// <summary>
    /// CurrencyPair whitelisting
    /// </summary>
    [JsonProperty("currency_pairs")]
    public List<string> CurrencyPairs { get; set; } = [];

    /// <summary>
    /// CurrencyPair whitelisting
    /// </summary>
    [JsonIgnore]
    public List<string> Symbols
    {
        get => CurrencyPairs;
        set => CurrencyPairs = value;
    }

    /// <summary>
    /// User VIP level
    /// </summary>
    [JsonProperty("tier")]
    public long Tier { get; set; }

    /// <summary>
    /// API Key detail
    /// </summary>
    [JsonProperty("key")]
    public GateAccountDetailsKey ApiKey { get; set; } = null!;

    /// <summary>
    /// User role
    /// </summary>
    [JsonProperty("copy_trading_role")]
    public GateAccountCopyTradingRole CopyTradingRole { get; set; }
}

/// <summary>
/// GateAccountDetailsKey
/// </summary>
public record GateAccountDetailsKey
{
    /// <summary>
    /// Mode
    /// </summary>
    [JsonProperty("mode")]
    public GateAccountApiKeyMode Mode { get; set; }
}

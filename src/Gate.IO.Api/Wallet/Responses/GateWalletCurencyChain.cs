namespace Gate.IO.Api.Wallet;

/// <summary>
/// Wallet Currency Chain
/// </summary>
public class GateWalletCurencyChain
{
    /// <summary>
    /// Chain name
    /// </summary>
    [JsonProperty("chain")]
    public string Chain { get; set; }

    /// <summary>
    /// Chain name in Chinese
    /// </summary>
    [JsonProperty("name_cn")]
    public string ChineseName { get; set; }

    /// <summary>
    /// Chain name in English
    /// </summary>
    [JsonProperty("name_en")]
    public string EnglishName { get; set; }

    /// <summary>
    /// Smart contract address for the currency; if no address is available, it will be an empty string
    /// </summary>
    [JsonProperty("contract_address")]
    public string ContractAddress { get; set; }

    /// <summary>
    /// If it is disabled. 0 means NOT being disabled
    /// </summary>
    [JsonProperty("is_disabled")]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// Is deposit disabled. 0 means not
    /// </summary>
    [JsonProperty("is_deposit_disabled")]
    public bool IsDepositDisabled { get; set; }

    /// <summary>
    /// Is withdrawal disabled. 0 means not
    /// </summary>
    [JsonProperty("is_withdraw_disabled")]
    public bool IsWithdrawalDisabled { get; set; }

    /// <summary>
    /// Withdrawal precision
    /// </summary>
    [JsonProperty("decimal")]
    public int Decimal { get; set; }
}

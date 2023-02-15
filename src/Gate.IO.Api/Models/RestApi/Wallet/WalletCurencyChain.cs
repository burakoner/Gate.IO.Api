namespace Gate.IO.Api.Models.RestApi.Wallet;

public class WalletCurencyChain
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
}

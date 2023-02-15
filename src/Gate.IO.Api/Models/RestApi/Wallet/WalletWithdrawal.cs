namespace Gate.IO.Api.Models.RestApi.Wallet;

public class WalletWithdrawal
{
    [JsonProperty("currency")]
    public string Currency { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("name_cn")]
    public string ChineseName { get; set; }
    
    [JsonProperty("deposit")]
    public decimal Deposit { get; set; }
    
    [JsonProperty("withdraw_percent")]
    public decimal WithdrawPercent { get; set; }
    
    [JsonProperty("withdraw_fix")]
    public decimal WithdrawFix { get; set; }
    
    [JsonProperty("withdraw_day_limit")]
    public decimal WithdrawDayLimit { get; set; }
    
    [JsonProperty("withdraw_day_limit_remain")]
    public decimal WithdrawDayLimitRemain { get; set; }
    
    [JsonProperty("withdraw_amount_mini")]
    public decimal WithdrawAmountMini { get; set; }
    
    [JsonProperty("withdraw_eachtime_limit")]
    public decimal WithdrawEachTimeLimit { get; set; }
    
    [JsonProperty("withdraw_fix_on_chains")]
    public Dictionary<string, decimal> WithdrawFixOnChains { get; set; }
}

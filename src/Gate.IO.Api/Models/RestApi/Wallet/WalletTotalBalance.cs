namespace Gate.IO.Api.Models.RestApi.Wallet;

public class WalletTotalBalance
{
    [JsonProperty("total")]
    public WalletTotalBalanceItem Total { get; set; }

    [JsonProperty("details")]
    public WalletTotalBalanceDetails Details { get; set; }
}

public class WalletTotalBalanceItem
{
    [JsonProperty("amount")]
    public decimal Amount { get; set; }
    
    [JsonProperty("currency")]
    public string Currency { get; set; }
}

public class WalletTotalBalanceDetails
{
    [JsonProperty("cross_margin")]
    public WalletTotalBalanceItem CrossMargin { get; set; }

    [JsonProperty("spot")]
    public WalletTotalBalanceItem Spot { get; set; }

    [JsonProperty("finance")]
    public WalletTotalBalanceItem Finance { get; set; }

    [JsonProperty("margin")]
    public WalletTotalBalanceItem Margin { get; set; }

    [JsonProperty("quant")]
    public WalletTotalBalanceItem Quant { get; set; }

    [JsonProperty("futures")]
    public WalletTotalBalanceItem Futures { get; set; }

    [JsonProperty("delivery")]
    public WalletTotalBalanceItem Delivery { get; set; }

    [JsonProperty("warrant")]
    public WalletTotalBalanceItem Warrant { get; set; }

    [JsonProperty("cbbc")]
    public WalletTotalBalanceItem Cbbc { get; set; }
}
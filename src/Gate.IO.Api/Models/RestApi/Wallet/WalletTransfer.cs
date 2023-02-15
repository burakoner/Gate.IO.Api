namespace Gate.IO.Api.Models.RestApi.Wallet;

public class WalletTransfer
{
    [JsonProperty("currency")]
    public string Currency { get; set; }
        
    [JsonProperty("sub_account")]
    public long SubAccountId { get; set; }

    [JsonProperty("direction"), JsonConverter(typeof(TransferDirectionConverter))]
    public TransferDirection Direction { get; set; }

    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    [JsonProperty("sub_account_type"), JsonConverter(typeof(AccountType3Converter))]
    public AccountType3 SubAccountType { get; set; }
}
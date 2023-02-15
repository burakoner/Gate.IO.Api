namespace Gate.IO.Api.Models.RestApi.Wallet;

public class SubAccountTransfer
{
    [JsonProperty("currency")]
    public string Currency { get; set; }

    [JsonProperty("sub_account_from")]
    public long SenderSubAccountId { get; set; }

    [JsonProperty("sub_account_from_type"), JsonConverter(typeof(AccountType3Converter))]
    public AccountType3 SenderSubAccountType { get; set; }

    [JsonProperty("sub_account_to")]
    public long RecipientSubAccountId { get; set; }

    [JsonProperty("sub_account_type"), JsonConverter(typeof(AccountType3Converter))]
    public AccountType3 RecipientSubAccountType { get; set; }

    [JsonProperty("amount")]
    public decimal Amount { get; set; }
}
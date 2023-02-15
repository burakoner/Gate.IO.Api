namespace Gate.IO.Api.Models.RestApi.Wallet;

public class WalletTransaction
{
    [JsonProperty("id")]
    public long WithdrawalId { get; set; }

    [JsonProperty("timestamp"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Timestamp { get; set; }

    [JsonProperty("withdraw_order_id")]
    public string WithdrawalOrderId { get; set; }
    
    [JsonProperty("currency")]
    public string Currency { get; set; }
    
    [JsonProperty("chain")]
    public string Chain { get; set; }
    
    [JsonProperty("address")]
    public string Address { get; set; }
    
    [JsonProperty("memo")]
    public string Memo { get; set; }
    
    [JsonProperty("txid")]
    public string TransactionId { get; set; }

    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    [JsonProperty("fee")]
    public decimal Fee { get; set; }

    [JsonProperty("status"), JsonConverter(typeof(WithdrawalStatusConverter))]
    public WithdrawalStatus Status { get; set; }
}

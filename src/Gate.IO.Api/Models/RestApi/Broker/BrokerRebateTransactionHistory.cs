namespace Gate.IO.Api.Models.RestApi.Broker;

public class BrokerRebateTransactionHistory
{
    [JsonProperty("total")]
    public long Total { get; set; }

    [JsonProperty("list")]
    public List<RebateTransactionHistoryRecord> List { get; set; }=[];
}

public class RebateTransactionHistoryRecord
{
    [JsonProperty("transaction_time")]
    public DateTime TransactionTime { get; set; }

    [JsonProperty("user_id")]
    public int UserId { get; set; }

    [JsonProperty("group_name")]
    public string GroupName { get; set; }

    [JsonProperty("fee")]
    public decimal Fee { get; set; }

    [JsonProperty("fee_asset")]
    public string FeeAsset { get; set; }

    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    [JsonProperty("amount")]
    public decimal Amount { get; set; }
    
    [JsonProperty("amount_asset")]
    public string AmountAsset { get; set; }
    
    [JsonProperty("source")]
    public string Source { get; set; }

}
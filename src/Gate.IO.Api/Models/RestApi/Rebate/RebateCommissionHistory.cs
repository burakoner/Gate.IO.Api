namespace Gate.IO.Api.Models.RestApi.Rebate;

public class RebateCommissionHistory
{
    [JsonProperty("total")]
    public long Total { get; set; }

    [JsonProperty("list")]
    public IEnumerable<RebateCommissionHistoryRecord> List { get; set; }
}

public class RebateCommissionHistoryRecord
{
    [JsonProperty("commission_time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime TransactionTime { get; set; }

    [JsonProperty("user_id")]
    public int UserId { get; set; }

    [JsonProperty("group_name")]
    public string GroupName { get; set; }

    [JsonProperty("commission_amount")]
    public decimal CommissionAmount { get; set; }

    [JsonProperty("commission_asset")]
    public string CommissionAsset { get; set; }

    [JsonProperty("source")]
    public string Source { get; set; }
}
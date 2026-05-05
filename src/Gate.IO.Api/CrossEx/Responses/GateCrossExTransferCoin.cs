namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx supported transfer currency
/// </summary>
public record GateCrossExTransferCoin
{
    [JsonProperty("coin")]
    public string Coin { get; set; }

    [JsonProperty("min_trans_amount")]
    public decimal MinimumTransferAmount { get; set; }

    [JsonProperty("est_fee")]
    public decimal EstimatedFee { get; set; }

    [JsonProperty("precision")]
    public int Precision { get; set; }

    [JsonProperty("is_disabled")]
    public int IsDisabled { get; set; }
}

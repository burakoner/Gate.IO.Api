namespace Gate.IO.Api.Models.RestApi.Wallet;

public class SubAccountFuturesBalance
{
    [JsonProperty("uid")]
    public long UserId { get; set; }

    [JsonProperty("available")]
    public Dictionary<string, decimal> Available { get; set; }
}
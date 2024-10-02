namespace Gate.IO.Api.Models.RestApi.FlashSwap;

public class GateSwapCurrency
{
    [JsonProperty("currency")]
    public string Symbol { get; set; }
    
    [JsonProperty("min_amount")]
    public decimal MinimumAmount { get; set; }
    
    [JsonProperty("max_amount")]
    public decimal MaximumAmount { get; set; }

    [JsonProperty("swappable")]
    public IEnumerable<string> Swappable { get; set; }
}

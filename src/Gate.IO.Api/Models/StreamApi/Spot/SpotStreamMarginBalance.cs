namespace Gate.IO.Api.Models.StreamApi.Spot;

public  class SpotStreamMarginBalance
{
    [JsonProperty("timestamp")]
    public DateTime Time { get; set; }

    [JsonProperty("timestamp_ms")]
    public long TimeInMilliseconds { get; set; }

    [JsonProperty("user")]
    public int UserId { get; set; }

    [JsonProperty("currency")]
    public string Currency { get; set; }
    
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    [JsonProperty("change")]
    public decimal Change { get; set; }
    
    [JsonProperty("available")]
    public decimal Available { get; set; }

    [JsonProperty("freeze")]
    public decimal Freeze { get; set; }

    [JsonProperty("borrowed")]
    public decimal Borrowed { get; set; }

    [JsonProperty("interest")]
    public decimal Interest { get; set; }
}

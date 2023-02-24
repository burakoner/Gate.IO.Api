namespace Gate.IO.Api.Models.StreamApi.Spot;

public  class SpotStreamFundingBalance
{
    [JsonProperty("timestamp"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("timestamp_ms")]
    public long TimeInMilliseconds { get; set; }

    [JsonProperty("user")]
    public int UserId { get; set; }

    [JsonProperty("currency")]
    public string Currency { get; set; }
    
    [JsonProperty("change")]
    public decimal Change { get; set; }
    
    [JsonProperty("freeze")]
    public decimal Freeze { get; set; }

    [JsonProperty("lent")]
    public decimal Lent { get; set; }
}

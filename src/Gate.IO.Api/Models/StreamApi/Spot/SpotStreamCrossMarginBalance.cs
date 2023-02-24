namespace Gate.IO.Api.Models.StreamApi.Spot;

public  class SpotStreamCrossMarginBalance
{
    [JsonProperty("timestamp"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Timestamp { get; set; }

    [JsonProperty("timestamp_ms")]
    public long TimeInMilliseconds { get; set; }

    [JsonProperty("user")]
    public int UserId { get; set; }

    [JsonProperty("currency")]
    public string Currency { get; set; }

    [JsonProperty("change")]
    public decimal Change { get; set; }

    [JsonProperty("total")]
    public decimal Total { get; set; }

    [JsonProperty("available")]
    public decimal Available { get; set; }
}

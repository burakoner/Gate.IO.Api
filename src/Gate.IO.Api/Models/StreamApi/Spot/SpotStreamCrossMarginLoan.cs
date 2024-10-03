namespace Gate.IO.Api.Models.StreamApi.Spot;

public  class SpotStreamCrossMarginLoan
{
    [JsonProperty("timestamp")]
    public DateTime Timestamp { get; set; }

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

    [JsonProperty("borrowed")]
    public decimal Borrowed { get; set; }

    [JsonProperty("interest")]
    public decimal Interest { get; set; }
}

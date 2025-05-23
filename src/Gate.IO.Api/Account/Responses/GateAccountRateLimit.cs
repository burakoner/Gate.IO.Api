namespace Gate.IO.Api.Account;

public record GateAccountRateLimit
{
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("tier")]
    public int Tier { get; set; }

    [JsonProperty("ratio")]
    public decimal Ratio { get; set; }

    [JsonProperty("main_ratio")]
    public decimal MainRatio { get; set; }

    [JsonProperty("updated_at")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdatedAt { get; set; }
}

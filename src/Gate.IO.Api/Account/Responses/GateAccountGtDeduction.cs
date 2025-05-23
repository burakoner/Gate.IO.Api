namespace Gate.IO.Api.Account;

public record GateAccountGtDeduction
{
    [JsonProperty("enabled")]
    public bool Enabled { get; set; }
}

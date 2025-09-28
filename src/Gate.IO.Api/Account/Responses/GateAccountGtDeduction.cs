namespace Gate.IO.Api.Account;

/// <summary>
/// Gate Account GT Deduction
/// </summary>
public record GateAccountGtDeduction
{
    /// <summary>
    /// Is Enabled
    /// </summary>
    [JsonProperty("enabled")]
    public bool Enabled { get; set; }
}

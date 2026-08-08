namespace Gate.IO.Api.Otc;

/// <summary>
/// OTC bank card creation result
/// </summary>
public record GateOtcBankCreateResult
{
    /// <summary>
    /// Bank card ID
    /// </summary>
    [JsonProperty("bank_id")]
    public int BankId { get; set; }

    /// <summary>
    /// Review status
    /// </summary>
    [JsonProperty("status")]
    public int Status { get; set; }
}

namespace Gate.IO.Api.Otc;

/// <summary>
/// OTC bank card creation result
/// </summary>
public record GateOtcBankCreateResult
{
    /// <summary>
    /// Bank card ID
    /// </summary>
    [JsonProperty("bank_id", Required = Required.Always)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public long BankId { get; set; }

    /// <summary>
    /// Raw review status, including unknown values. A successful submission is not review approval.
    /// </summary>
    [JsonProperty("status", Required = Required.Always)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public int Status { get; set; }

    /// <summary>Root acknowledgement code, populated by the client. Not part of a saved data-only snapshot.</summary>
    [JsonIgnore]
    public int Code { get; internal set; }

    /// <summary>Root acknowledgement message, populated by the client. Not part of a saved data-only snapshot.</summary>
    [JsonIgnore]
    public string Message { get; internal set; }

    /// <summary>Optional root integer timestamp with unspecified unit. Not converted or saved in a data-only snapshot.</summary>
    [JsonIgnore]
    public long? Timestamp { get; internal set; }
}

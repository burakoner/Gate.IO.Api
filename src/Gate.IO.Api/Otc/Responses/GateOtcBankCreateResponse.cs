namespace Gate.IO.Api.Otc;

/// <summary>Complete current bank submission envelope; not approval of the bank card.</summary>
[JsonConverter(typeof(GateOtcBankJsonConverter))]
public record GateOtcBankCreateResponse
{
    /// <summary>Business code; zero is successful submission.</summary>
    [JsonProperty("code", Required = Required.Always)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public int Code { get; set; }

    /// <summary>Server message.</summary>
    [JsonProperty("message", Required = Required.Always)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public string Message { get; set; }

    /// <summary>Required bank identity and raw review status.</summary>
    [JsonProperty("data", Required = Required.Always)]
    public GateOtcBankCreateResult Data { get; set; }

    /// <summary>Optional integer timestamp. The documentation specifies no unit.</summary>
    [JsonProperty("timestamp", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public long? Timestamp { get; set; }
}

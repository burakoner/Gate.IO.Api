namespace Gate.IO.Api.Otc;

/// <summary>Pre-upload credential acknowledgement, not proof of file upload or business submission.</summary>
public record GateOtcUploadPreUploadResponse
{
    /// <summary>Business code; the client reports nonzero codes as errors, including with HTTP 200.</summary>
    [JsonProperty("code", Required = Required.Always)]
    [JsonConverter(typeof(GateOtcUploadValueConverter))]
    public int Code { get; set; }

    /// <summary>Server acknowledgement message.</summary>
    [JsonProperty("message", Required = Required.Always)]
    [JsonConverter(typeof(GateOtcUploadValueConverter))]
    public string Message { get; set; }

    /// <summary>Sensitive direct-upload credentials. Do not log or automatically forward to another host.</summary>
    [JsonProperty("data", Required = Required.Always)]
    public GateOtcUploadPreUploadData Data { get; set; }

    /// <summary>Server timestamp, decoded from exact integer Unix seconds into UTC.</summary>
    [JsonProperty("timestamp", Required = Required.Always)]
    [JsonConverter(typeof(GateOtcUploadValueConverter))]
    public DateTime Timestamp { get; set; }
}

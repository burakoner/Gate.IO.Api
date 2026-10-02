namespace Gate.IO.Api.Otc;

/// <summary>Local multipart file input. This object is not an extra wire form field.</summary>
public record GateOtcFileUpload
{
    /// <summary>Raw file bytes; saved JSON encodes these as Base64. No file is read from disk.</summary>
    [JsonProperty("content", Required = Required.Always)]
    public byte[] Content { get; set; }

    /// <summary>File name without a path or header-control characters.</summary>
    [JsonProperty("file_name", Required = Required.Always)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public string FileName { get; set; }

    /// <summary>Optional plaintext MIME header; omission uses application/octet-stream, without inspecting bytes.</summary>
    [JsonProperty("content_type", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public string ContentType { get; set; }
}

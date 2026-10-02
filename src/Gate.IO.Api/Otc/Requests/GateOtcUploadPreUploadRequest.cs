namespace Gate.IO.Api.Otc;

/// <summary>Requests temporary S3 POST credentials; contains no file content and uploads nothing.</summary>
public record GateOtcUploadPreUploadRequest
{
    /// <summary>Required supported MIME type, sent as its documented base64 representation.</summary>
    [JsonProperty("content_type", Required = Required.Always)]
    [JsonConverter(typeof(GateOtcUploadInstructionConverter))]
    public GateOtcUploadContentType ContentType { get; set; }

    /// <summary>Optional business scene. Omission is preserved; the server defaults to general.</summary>
    [JsonProperty("scene", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateOtcUploadInstructionConverter))]
    public GateOtcUploadScene? Scene { get; set; }
}

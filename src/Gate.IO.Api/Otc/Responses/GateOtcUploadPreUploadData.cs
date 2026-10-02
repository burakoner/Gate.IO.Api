namespace Gate.IO.Api.Otc;

/// <summary>Temporary S3 POST Policy. Credential issuance does not upload the file.</summary>
public record GateOtcUploadPreUploadData
{
    /// <summary>Base64 temporary object path. Pass unchanged to bank/create or order/paid; do not decode.</summary>
    [JsonProperty("file_key", Required = Required.Always)]
    [JsonConverter(typeof(GateOtcUploadValueConverter))]
    public string FileKey { get; set; }

    /// <summary>S3 POST URL. Returned as-is; this client never follows it or forwards Gate authentication.</summary>
    [JsonProperty("url", Required = Required.Always)]
    [JsonConverter(typeof(GateOtcUploadValueConverter))]
    public string Url { get; set; }

    /// <summary>
    /// Case-sensitive S3 form fields, including future string fields. Send all pairs unchanged;
    /// the file part must be last. Policy limits the upload to 1..10485760 bytes. Contains secrets: do not log.
    /// </summary>
    [JsonProperty("fields", Required = Required.Always)]
    [JsonConverter(typeof(GateOtcUploadFieldsConverter))]
    public Dictionary<string, string> Fields { get; set; }

    /// <summary>Returned validity in seconds (currently 5400); not a hardcoded lifetime or automatic refresh timer.</summary>
    [JsonProperty("expires_in", Required = Required.Always)]
    [JsonConverter(typeof(GateOtcUploadValueConverter))]
    public int ExpiresIn { get; set; }
}

namespace Gate.IO.Api.Otc;

/// <summary>
/// Enterprise OTC bank card supplement request
/// </summary>
[JsonConverter(typeof(GateOtcBankJsonConverter))]
public record GateOtcBankEnterpriseSupplementRequest
{
    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("uid", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public string UserId { get; set; }

    /// <summary>
    /// Bank card ID
    /// </summary>
    [JsonProperty("bank_id", Required = Required.Always)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public string BankId { get; set; }

    /// <summary>
    /// Optional Base64 certificate content, decoded into a real file part. Excludes CertificateUpload.
    /// </summary>
    [JsonProperty("certificate", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public string Certificate { get; set; }

    /// <summary>Local raw certificate file input; not an extra HTTP field.</summary>
    [JsonProperty("certificate_upload", NullValueHandling = NullValueHandling.Ignore)]
    public GateOtcFileUpload CertificateUpload { get; set; }

    /// <summary>
    /// Optional Base64 shareholder register content, decoded into a real file part. Excludes ShareHoldersUpload.
    /// </summary>
    [JsonProperty("share_holders", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public string ShareHolders { get; set; }

    /// <summary>Local raw shareholder register input; not an extra HTTP field.</summary>
    [JsonProperty("share_holders_upload", NullValueHandling = NullValueHandling.Ignore)]
    public GateOtcFileUpload ShareHoldersUpload { get; set; }

    /// <summary>
    /// Optional Base64 passport content, decoded into a real file part. Excludes PassportUpload.
    /// </summary>
    [JsonProperty("passport", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public string Passport { get; set; }

    /// <summary>Local raw passport input; not an extra HTTP field.</summary>
    [JsonProperty("passport_upload", NullValueHandling = NullValueHandling.Ignore)]
    public GateOtcFileUpload PassportUpload { get; set; }

    /// <summary>
    /// Optional Base64 ownership structure content, decoded into a real file part. Excludes ShareHoldingStructureUpload.
    /// </summary>
    [JsonProperty("share_holding_structure", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public string ShareHoldingStructure { get; set; }

    /// <summary>Local raw ownership structure input; not an extra HTTP field.</summary>
    [JsonProperty("share_holding_structure_upload", NullValueHandling = NullValueHandling.Ignore)]
    public GateOtcFileUpload ShareHoldingStructureUpload { get; set; }

    /// <summary>
    /// Optional Base64 proof-of-funds content, decoded into a real file part. Excludes FundsStatementUpload.
    /// </summary>
    [JsonProperty("funds_statement", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public string FundsStatement { get; set; }

    /// <summary>Local raw proof-of-funds input; not an extra HTTP field.</summary>
    [JsonProperty("funds_statement_upload", NullValueHandling = NullValueHandling.Ignore)]
    public GateOtcFileUpload FundsStatementUpload { get; set; }

    /// <summary>
    /// Optional Base64 additional material, decoded into a real file part. Excludes AdditionalUpload.
    /// </summary>
    [JsonProperty("additional", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public string Additional { get; set; }

    /// <summary>Local raw additional material input; not an extra HTTP field.</summary>
    [JsonProperty("additional_upload", NullValueHandling = NullValueHandling.Ignore)]
    public GateOtcFileUpload AdditionalUpload { get; set; }

    /// <summary>
    /// Optional JSON text sent unchanged. Pre-upload items require plaintext object keys and MIME types.
    /// Follow the enterprise checklist; no automatic decoding or JSON shape inference occurs.
    /// </summary>
    [JsonProperty("relationship_proof", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public string RelationshipProof { get; set; }
}

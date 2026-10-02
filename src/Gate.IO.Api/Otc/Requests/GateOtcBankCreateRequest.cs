namespace Gate.IO.Api.Otc;

/// <summary>
/// OTC bank card creation request
/// </summary>
[JsonConverter(typeof(GateOtcBankJsonConverter))]
public record GateOtcBankCreateRequest
{
    /// <summary>
    /// Bank account name
    /// </summary>
    [JsonProperty("bank_account_name", Required = Required.Always)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public string BankAccountName { get; set; }

    /// <summary>
    /// Bank name
    /// </summary>
    [JsonProperty("bank_name", Required = Required.Always)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public string BankName { get; set; }

    /// <summary>
    /// Bank country
    /// </summary>
    [JsonProperty("bank_country", Required = Required.Always)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public string BankCountry { get; set; }

    /// <summary>
    /// Bank address
    /// </summary>
    [JsonProperty("bank_address", Required = Required.Always)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public string BankAddress { get; set; }

    /// <summary>
    /// IBAN number
    /// </summary>
    [JsonProperty("iban", Required = Required.Always)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public string Iban { get; set; }

    /// <summary>
    /// SWIFT code
    /// </summary>
    [JsonProperty("swift", Required = Required.Always)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public string Swift { get; set; }

    /// <summary>
    /// Remittance routing number
    /// </summary>
    [JsonProperty("remittance_line_number", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public string RemittanceLineNumber { get; set; }

    /// <summary>
    /// Correspondent bank name
    /// </summary>
    [JsonProperty("agent_bank_name", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public string AgentBankName { get; set; }

    /// <summary>
    /// Correspondent bank SWIFT code
    /// </summary>
    [JsonProperty("agent_bank_swift", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public string AgentBankSwift { get; set; }

    /// <summary>
    /// Legacy Base64 account-opening proof content, decoded into a real file part named documentation_file.
    /// Choose this, DocumentationUpload, or DocumentationFileKey; no text placeholder or data URL is accepted.
    /// </summary>
    [JsonProperty("documentation_file", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public string DocumentationFile { get; set; }

    /// <summary>Local raw file/metadata alternative; serialized as a local DTO field, never an extra HTTP form field.</summary>
    [JsonProperty("documentation_upload", NullValueHandling = NullValueHandling.Ignore)]
    public GateOtcFileUpload DocumentationUpload { get; set; }

    /// <summary>Pre-upload object key, plaintext or Base64. Sent unchanged; excludes either direct file input.</summary>
    [JsonProperty("documentation_file_key", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public string DocumentationFileKey { get; set; }

    /// <summary>Required with DocumentationFileKey. Plaintext MIME or Base64 is sent unchanged.</summary>
    [JsonProperty("file_type", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateOtcBankValueConverter))]
    public string FileType { get; set; }
}

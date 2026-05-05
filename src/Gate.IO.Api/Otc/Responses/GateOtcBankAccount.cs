namespace Gate.IO.Api.Otc;

/// <summary>
/// OTC bank account
/// </summary>
public record GateOtcBankAccount
{
    /// <summary>
    /// Bank ID
    /// </summary>
    [JsonProperty("id")]
    public long? Id { get; set; }

    /// <summary>
    /// Bank account name
    /// </summary>
    [JsonProperty("bank_account_name")]
    public string BankAccountName { get; set; }

    /// <summary>
    /// Bank name
    /// </summary>
    [JsonProperty("bank_name")]
    public string BankName { get; set; }

    /// <summary>
    /// Bank country
    /// </summary>
    [JsonProperty("bank_country")]
    public string BankCountry { get; set; }

    /// <summary>
    /// Bank address
    /// </summary>
    [JsonProperty("bank_address")]
    public string BankAddress { get; set; }

    /// <summary>
    /// Bank code
    /// </summary>
    [JsonProperty("bank_code")]
    public string BankCode { get; set; }

    /// <summary>
    /// Branch code
    /// </summary>
    [JsonProperty("branch_code")]
    public string BranchCode { get; set; }

    /// <summary>
    /// IBAN number
    /// </summary>
    [JsonProperty("iban")]
    public string Iban { get; set; }

    /// <summary>
    /// SWIFT code
    /// </summary>
    [JsonProperty("swift")]
    public string Swift { get; set; }

    /// <summary>
    /// Remittance routing number
    /// </summary>
    [JsonProperty("remittance_line_number")]
    public string RemittanceLineNumber { get; set; }

    /// <summary>
    /// Correspondent bank name
    /// </summary>
    [JsonProperty("agent_bank_name")]
    public string AgentBankName { get; set; }

    /// <summary>
    /// Correspondent bank SWIFT code
    /// </summary>
    [JsonProperty("agent_bank_swift")]
    public string AgentBankSwift { get; set; }

    /// <summary>
    /// Submission time
    /// </summary>
    [JsonProperty("submit_time")]
    public DateTime? SubmitTime { get; set; }

    /// <summary>
    /// Update time
    /// </summary>
    [JsonProperty("update_time")]
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }

    /// <summary>
    /// Document file type
    /// </summary>
    [JsonProperty("documentation_file_type")]
    public string DocumentationFileType { get; set; }

    /// <summary>
    /// Remark
    /// </summary>
    [JsonProperty("memo")]
    public string Memo { get; set; }

    /// <summary>
    /// Whether it is the default bank card
    /// </summary>
    [JsonProperty("is_default")]
    public int? IsDefault { get; set; }

    /// <summary>
    /// Bank ID
    /// </summary>
    [JsonProperty("bank_id")]
    public long? BankId { get; set; }

    /// <summary>
    /// Document file URL
    /// </summary>
    [JsonProperty("documentation_file_key_url")]
    public string DocumentationFileKeyUrl { get; set; }

    /// <summary>
    /// Message returned when bank information is unavailable
    /// </summary>
    [JsonProperty("message")]
    public string Message { get; set; }

    /// <summary>
    /// Action URL returned when bank information is unavailable
    /// </summary>
    [JsonProperty("url")]
    public string Url { get; set; }

    /// <summary>
    /// Display flag returned when bank information is unavailable
    /// </summary>
    [JsonProperty("show")]
    public int? Show { get; set; }
}

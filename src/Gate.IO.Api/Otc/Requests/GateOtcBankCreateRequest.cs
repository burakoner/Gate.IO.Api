namespace Gate.IO.Api.Otc;

/// <summary>
/// OTC bank card creation request
/// </summary>
public record GateOtcBankCreateRequest
{
    /// <summary>
    /// Bank account name
    /// </summary>
    public string BankAccountName { get; set; }

    /// <summary>
    /// Bank name
    /// </summary>
    public string BankName { get; set; }

    /// <summary>
    /// Bank country
    /// </summary>
    public string BankCountry { get; set; }

    /// <summary>
    /// Bank address
    /// </summary>
    public string BankAddress { get; set; }

    /// <summary>
    /// IBAN number
    /// </summary>
    public string Iban { get; set; }

    /// <summary>
    /// SWIFT code
    /// </summary>
    public string Swift { get; set; }

    /// <summary>
    /// Remittance routing number
    /// </summary>
    public string RemittanceLineNumber { get; set; }

    /// <summary>
    /// Correspondent bank name
    /// </summary>
    public string AgentBankName { get; set; }

    /// <summary>
    /// Correspondent bank SWIFT code
    /// </summary>
    public string AgentBankSwift { get; set; }

    /// <summary>
    /// Base64 account-opening proof content. Supported formats are jpg, jpeg, png, and pdf; maximum file size is 10 MB.
    /// </summary>
    public string DocumentationFile { get; set; }
}

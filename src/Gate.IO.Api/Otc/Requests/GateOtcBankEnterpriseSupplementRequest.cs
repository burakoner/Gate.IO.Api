namespace Gate.IO.Api.Otc;

/// <summary>
/// Enterprise OTC bank card supplement request
/// </summary>
public record GateOtcBankEnterpriseSupplementRequest
{
    /// <summary>
    /// User ID
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// Bank card ID
    /// </summary>
    public string BankId { get; set; }

    /// <summary>
    /// Base64 business license or registration certificate file content
    /// </summary>
    public string Certificate { get; set; }

    /// <summary>
    /// Base64 register-of-shareholders file content
    /// </summary>
    public string ShareHolders { get; set; }

    /// <summary>
    /// Base64 legal representative or shareholder passport file content
    /// </summary>
    public string Passport { get; set; }

    /// <summary>
    /// Base64 ownership structure chart file content
    /// </summary>
    public string ShareHoldingStructure { get; set; }

    /// <summary>
    /// Base64 proof-of-funds file content
    /// </summary>
    public string FundsStatement { get; set; }

    /// <summary>
    /// Base64 additional supplementary material file content
    /// </summary>
    public string Additional { get; set; }

    /// <summary>
    /// Optional relationship proof as JSON text
    /// </summary>
    public string RelationshipProof { get; set; }
}

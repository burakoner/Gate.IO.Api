namespace Gate.IO.Api.Otc;

/// <summary>
/// Personal OTC bank card supplement request
/// </summary>
public record GateOtcBankPersonalSupplementRequest
{
    /// <summary>
    /// Bank card ID
    /// </summary>
    public string BankId { get; set; }

    /// <summary>
    /// Base64 ID document front-side file content
    /// </summary>
    public string IdDocumentFront { get; set; }

    /// <summary>
    /// Base64 ID document back-side file content
    /// </summary>
    public string IdDocumentBack { get; set; }

    /// <summary>
    /// Base64 proof-of-address file content
    /// </summary>
    public string AddressProof { get; set; }

    /// <summary>
    /// Optional relationship proof as JSON text
    /// </summary>
    public string RelationshipProof { get; set; }
}

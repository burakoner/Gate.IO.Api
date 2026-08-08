namespace Gate.IO.Api.Otc;

/// <summary>
/// OTC bank card ID request
/// </summary>
public record GateOtcBankIdRequest
{
    /// <summary>
    /// Bank card ID
    /// </summary>
    public string BankId { get; set; }
}

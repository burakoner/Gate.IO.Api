namespace Gate.IO.Api.Otc;

/// <summary>
/// Explicit remittance name for a fiat order. Eligibility depends on the account type;
/// omission leaves the choice unspecified rather than selecting a name locally.
/// </summary>
public enum GateOtcReceiveType : byte
{
    /// <summary>Corporate user: remit in the company's name.</summary>
    [Map("YOU")]
    Company = 1,

    /// <summary>Corporate or individual user: remit in Gate's name.</summary>
    [Map("GATE")]
    Gate = 2,

    /// <summary>Corporate user: remit in the recipient's name.</summary>
    [Map("RECIPIENT")]
    Recipient = 3,

    /// <summary>Individual user: remit in the user's own name.</summary>
    [Map("PERSON")]
    Person = 4,
}

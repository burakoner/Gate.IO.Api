namespace Gate.IO.Api.Enums;

public enum MarginLoanStatus
{
    /// <summary>
    /// not fully loaned
    /// </summary>
    [EnumMember(Value = "open")]
    Open,

    /// <summary>
    /// All loaned out for lending loan; Loaned in for borrowing side
    /// </summary>
    [EnumMember(Value = "loaned")]
    Loaned,

    /// <summary>
    /// loan is finished, either being all repaid or cancelled by the lender
    /// </summary>
    [EnumMember(Value = "finished")]
    Finished,

    /// <summary>
    /// Automatically repaid by the system
    /// </summary>
    [EnumMember(Value = "auto_repaid")]
    AutoRepaid,
}
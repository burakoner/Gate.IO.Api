namespace Gate.IO.Api.Enums;

public enum MarginLoanStatus
{
    /// <summary>
    /// not fully loaned
    /// </summary>
    [Map("open")]
    Open,

    /// <summary>
    /// All loaned out for lending loan; Loaned in for borrowing side
    /// </summary>
    [Map("loaned")]
    Loaned,

    /// <summary>
    /// loan is finished, either being all repaid or cancelled by the lender
    /// </summary>
    [Map("finished")]
    Finished,

    /// <summary>
    /// Automatically repaid by the system
    /// </summary>
    [Map("auto_repaid")]
    AutoRepaid,
}
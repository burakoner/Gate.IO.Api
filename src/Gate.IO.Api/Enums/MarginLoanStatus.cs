namespace Gate.IO.Api.Enums;

public enum MarginLoanStatus
{
    /// <summary>
    /// not fully loaned
    /// </summary>
    [Label("open")]
    Open,

    /// <summary>
    /// All loaned out for lending loan; Loaned in for borrowing side
    /// </summary>
    [Label("loaned")]
    Loaned,

    /// <summary>
    /// loan is finished, either being all repaid or cancelled by the lender
    /// </summary>
    [Label("finished")]
    Finished,

    /// <summary>
    /// Automatically repaid by the system
    /// </summary>
    [Label("auto_repaid")]
    AutoRepaid,
}
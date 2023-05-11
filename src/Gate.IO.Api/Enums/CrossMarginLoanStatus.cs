namespace Gate.IO.Api.Enums;

public enum CrossMarginLoanStatus
{
    [Label("1")]
    Failed,

    [Label("2")]
    Borrowed,

    [Label("3")]
    Repaid,
}
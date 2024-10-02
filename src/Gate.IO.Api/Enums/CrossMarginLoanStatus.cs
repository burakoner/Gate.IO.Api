namespace Gate.IO.Api.Enums;

public enum CrossMarginLoanStatus
{
    [Map("1")]
    Failed,

    [Map("2")]
    Borrowed,

    [Map("3")]
    Repaid,
}
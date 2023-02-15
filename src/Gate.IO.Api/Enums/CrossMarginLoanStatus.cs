namespace Gate.IO.Api.Enums;

public enum CrossMarginLoanStatus
{
    [EnumMember(Value = "1")]
    Failed,

    [EnumMember(Value = "2")]
    Borrowed,

    [EnumMember(Value = "3")]
    Repaid,
}
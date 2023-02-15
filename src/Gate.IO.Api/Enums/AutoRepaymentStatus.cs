namespace Gate.IO.Api.Enums;

public enum AutoRepaymentStatus
{
    [EnumMember(Value = "on")]
    Enabled,

    [EnumMember(Value = "off")]
    Disabled,
}
namespace Gate.IO.Api.Enums;

public enum OptionsTimeInForce
{
    /// <summary>
    /// GoodTillCancelled
    /// </summary>
    [EnumMember(Value = "gtc")]
    GoodTillCancelled,

    /// <summary>
    /// ImmediateOrCancelled, taker only
    /// </summary>
    [EnumMember(Value = "ioc")]
    ImmediateOrCancel,

    /// <summary>
    /// PendingOrCancelled, makes a post-only order that always enjoys a maker fee
    /// </summary>
    [EnumMember(Value = "poc")]
    PendingOrCancelled,
}
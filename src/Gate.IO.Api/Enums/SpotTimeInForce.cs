namespace Gate.IO.Api.Enums;

/// <summary>
/// Spot Order Time-In-Force
/// </summary>
public enum SpotTimeInForce
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
    /// FillOrKill, fill either completely or none Only. ioc and fok are supported when type=market
    /// </summary>
    [EnumMember(Value = "fok")]
    FillOrKill,

    /// <summary>
    /// PendingOrCancelled, makes a post-only order that always enjoys a maker fee
    /// </summary>
    [EnumMember(Value = "poc")]
    PendingOrCancelled,
}
namespace Gate.IO.Api.Enums;

/// <summary>
/// Spot Order Time-In-Force
/// </summary>
public enum SpotOrderTimeInForce
{
    /// <summary>
    /// GoodTillCancelled
    /// </summary>
    [EnumMember(Value = "gtc")]
    GoodTillCancelled = 1,

    /// <summary>
    /// ImmediateOrCancelled, taker only
    /// </summary>
    [EnumMember(Value = "ioc")]
    ImmediateOrCancel = 2,

    /// <summary>
    /// FillOrKill, fill either completely or none Only. ioc and fok are supported when type=market
    /// </summary>
    [EnumMember(Value = "fok")]
    FillOrKill = 3,

    /// <summary>
    /// PendingOrCancelled, makes a post-only order that always enjoys a maker fee
    /// </summary>
    [EnumMember(Value = "poc")]
    PendingOrCancelled = 4,
}

public enum SpotPriceOrderTimeInForce
{
    /// <summary>
    /// GoodTillCancelled
    /// </summary>
    [EnumMember(Value = "gtc")]
    GoodTillCancelled = 1,

    /// <summary>
    /// ImmediateOrCancelled, taker only
    /// </summary>
    [EnumMember(Value = "ioc")]
    ImmediateOrCancel = 2,
}
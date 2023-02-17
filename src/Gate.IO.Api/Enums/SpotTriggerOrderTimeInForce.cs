namespace Gate.IO.Api.Enums;

public enum SpotTriggerOrderTimeInForce
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
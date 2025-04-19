namespace Gate.IO.Api.Options;

/// <summary>
/// GateOptionsType
/// </summary>
public enum GateOptionsType : byte
{
    /// <summary>
    /// Call
    /// </summary>
    [Map("C")]
    Call = 1,

    /// <summary>
    /// Put
    /// </summary>
    [Map("P")]
    Put = 2,
}

namespace Gate.IO.Api.Bot;

/// <summary>
/// Bot recommendation scene
/// </summary>
public enum GateBotDiscoverScene : byte
{
    /// <summary>
    /// Represents the Top One value.
    /// </summary>
    [Map("top1")]
    TopOne = 1,

    /// <summary>
    /// Represents the Bundle value.
    /// </summary>
    [Map("bundle")]
    Bundle = 2,

    /// <summary>
    /// Represents the Filter value.
    /// </summary>
    [Map("filter")]
    Filter = 3,

    /// <summary>
    /// Represents the Refresh value.
    /// </summary>
    [Map("refresh")]
    Refresh = 4,
}

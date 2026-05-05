namespace Gate.IO.Api.Bot;

/// <summary>
/// Bot recommendation scene
/// </summary>
public enum GateBotDiscoverScene : byte
{
    [Map("top1")]
    TopOne = 1,

    [Map("bundle")]
    Bundle = 2,

    [Map("filter")]
    Filter = 3,

    [Map("refresh")]
    Refresh = 4,
}

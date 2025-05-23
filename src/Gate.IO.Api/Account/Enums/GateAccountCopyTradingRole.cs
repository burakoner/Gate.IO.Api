namespace Gate.IO.Api.Account;

/// <summary>
/// GateAccountCopyTradingRole
/// </summary>
public enum GateAccountCopyTradingRole : byte
{
    /// <summary>
    /// Ordinary User
    /// </summary>
    [Map("0")]
    OrdinaryUser = 0,

    /// <summary>
    /// Order Leader
    /// </summary>
    [Map("1")]
    OrderLeader = 1,

    /// <summary>
    /// Follower
    /// </summary>
    [Map("2")]
    Follower = 2,

    /// <summary>
    /// Order Leader And Follower
    /// </summary>
    [Map("3")]
    OrderLeaderAndFollower = 3,
}
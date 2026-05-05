namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi account status
/// </summary>
public enum GateTradFiAccountStatus
{
    /// <summary>
    /// Not opened
    /// </summary>
    NotOpened = 1,

    /// <summary>
    /// Pending review
    /// </summary>
    PendingReview = 2,

    /// <summary>
    /// Active or opened
    /// </summary>
    Active = 3,
}

namespace Gate.IO.Api.Announcements;

/// <summary>
/// Announcement summary channel type.
/// </summary>
public enum GateAnnouncementType
{
    /// <summary>
    /// Listing announcements.
    /// </summary>
    [Map("announcement.summary_listing")]
    Listing = 1,

    /// <summary>
    /// Delisting announcements.
    /// </summary>
    [Map("announcement.summary_delisting")]
    Delisting = 2,

    /// <summary>
    /// Fee announcements.
    /// </summary>
    [Map("announcement.summary_fee")]
    Fee = 3,

    /// <summary>
    /// ETF announcements.
    /// </summary>
    [Map("announcement.summary_etf")]
    Etf = 4,

    /// <summary>
    /// Deposit and withdrawal announcements.
    /// </summary>
    [Map("announcement.summary_deposit_withdrawal")]
    DepositWithdrawal = 5,

    /// <summary>
    /// Rename announcements.
    /// </summary>
    [Map("announcement.summary_rename")]
    Rename = 6,

    /// <summary>
    /// Precision announcements.
    /// </summary>
    [Map("announcement.summary_precision")]
    Precision = 7,

    /// <summary>
    /// Engine upgrade announcements.
    /// </summary>
    [Map("announcement.summary_engine_upgrade")]
    EngineUpgrade = 8,
}

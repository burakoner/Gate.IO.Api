namespace Gate.IO.Api.Announcements;

/// <summary>
/// Announcement language.
/// </summary>
public enum GateAnnouncementLanguage
{
    /// <summary>
    /// Simplified Chinese announcement.
    /// </summary>
    [Map("cn")]
    Chinese = 1,

    /// <summary>
    /// English announcement.
    /// </summary>
    [Map("en")]
    English = 2,
}

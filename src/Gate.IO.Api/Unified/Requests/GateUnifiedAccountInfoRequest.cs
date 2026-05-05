namespace Gate.IO.Api.Unified;

/// <summary>
/// Unified account information query request
/// </summary>
public record GateUnifiedAccountInfoRequest
{
    /// <summary>
    /// Query by specified currency name
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Sub-account user ID
    /// </summary>
    public long? SubAccountId { get; set; }
}

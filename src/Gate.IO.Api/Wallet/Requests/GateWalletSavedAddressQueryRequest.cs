namespace Gate.IO.Api.Wallet;

/// <summary>
/// Wallet saved-address query request
/// </summary>
public record GateWalletSavedAddressQueryRequest
{
    /// <summary>
    /// Currency
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Chain name
    /// </summary>
    public string Chain { get; set; }

    /// <summary>
    /// Whether to return verified addresses. Null applies no verification filter.
    /// </summary>
    public bool? Verified { get; set; }

    /// <summary>
    /// Maximum number returned, up to 100
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// Page number
    /// </summary>
    public int? Page { get; set; }
}

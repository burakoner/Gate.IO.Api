namespace Gate.IO.Api.Earn;

/// <summary>
/// Fixed-term Earn product type
/// </summary>
public enum GateEarnFixedTermProductType : byte
{
    /// <summary>
    /// All products
    /// </summary>
    All = 0,

    /// <summary>
    /// Regular product
    /// </summary>
    Regular = 1,

    /// <summary>
    /// VIP product
    /// </summary>
    Vip = 2,
}

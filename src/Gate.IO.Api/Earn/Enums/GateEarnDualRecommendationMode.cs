namespace Gate.IO.Api.Earn;

/// <summary>
/// Dual investment recommendation mode
/// </summary>
public enum GateEarnDualRecommendationMode : byte
{
    /// <summary>
    /// Normal recommendation
    /// </summary>
    [Map("normal")]
    Normal = 1,

    /// <summary>
    /// Curated picks
    /// </summary>
    [Map("senior")]
    Senior = 2,

    /// <summary>
    /// APY ascending
    /// </summary>
    [Map("apy_up")]
    ApyAscending = 3,

    /// <summary>
    /// Target price descending
    /// </summary>
    [Map("ep_down")]
    ExercisePriceDescending = 4,

    /// <summary>
    /// Target price ascending
    /// </summary>
    [Map("ep_up")]
    ExercisePriceAscending = 5,

    /// <summary>
    /// Maturity time descending
    /// </summary>
    [Map("dt_down")]
    DeliveryTimeDescending = 6,

    /// <summary>
    /// Maturity time ascending
    /// </summary>
    [Map("dt_up")]
    DeliveryTimeAscending = 7,
}

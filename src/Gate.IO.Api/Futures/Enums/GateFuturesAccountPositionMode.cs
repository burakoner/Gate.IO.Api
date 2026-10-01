namespace Gate.IO.Api.Futures;

/// <summary>Account holding mode, distinct from the direction of an individual position.</summary>
public enum GateFuturesAccountPositionMode : byte
{
    /// <summary>One-way position mode.</summary>
    [Map("single")] Single = 1,
    /// <summary>Hedge position mode.</summary>
    [Map("dual")] Dual = 2,
    /// <summary>Split position mode.</summary>
    [Map("dual_plus")] DualPlus = 3,
}

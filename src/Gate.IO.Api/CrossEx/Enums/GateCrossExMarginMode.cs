namespace Gate.IO.Api.CrossEx;

/// <summary>Explicit requested futures margin mode. Zero is undefined, not a default financial instruction.</summary>
public enum GateCrossExMarginMode
{
    /// <summary>Cross margin.</summary>
    [Map("CROSS")]
    Cross = 1,

    /// <summary>Isolated margin.</summary>
    [Map("ISOLATED")]
    Isolated = 2,
}

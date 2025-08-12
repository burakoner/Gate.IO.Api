namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures contract status
/// </summary>
public enum GateFuturesContractStatus : byte
{
    /// <summary>
    /// Pre-launch status, before the contract is available for trading
    /// </summary>
    [Map("prelaunch")]
    Prelaunch = 1,

    /// <summary>
    /// Trading status, when the contract is actively traded
    /// </summary>
    [Map("trading")]
    Trading = 2,

    /// <summary>
    /// Delisting status, when the contract is in the process of being removed from trading
    /// </summary>
    [Map("delisting")]
    Delisting = 3,

    /// <summary>
    /// Delisted status, when the contract has been completely removed from trading
    /// </summary>
    [Map("delisted")]
    Delisted = 4,

    /// <summary>
    /// Circuit breaker status, indicating a temporary halt in trading due to extreme market conditions
    /// </summary>
    [Map("circuit_breaker")]
    CircuitBreaker = 5
}
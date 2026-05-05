namespace Gate.IO.Api.Otc;

/// <summary>
/// OTC quote direction
/// </summary>
public enum GateOtcQuoteSide : byte
{
    /// <summary>
    /// User inputs the payment amount
    /// </summary>
    [Map("PAY")]
    Pay = 1,

    /// <summary>
    /// User inputs the received amount
    /// </summary>
    [Map("GET")]
    Get = 2,
}

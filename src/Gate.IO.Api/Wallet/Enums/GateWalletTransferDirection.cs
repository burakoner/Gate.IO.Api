﻿namespace Gate.IO.Api.Wallet;

/// <summary>
/// Transfer Direction
/// </summary>
public enum GateWalletTransferDirection : byte
{
    /// <summary>
    /// From
    /// </summary>
    [Map("from")]
    From = 1,

    /// <summary>
    /// To
    /// </summary>
    [Map("to")]
    To = 2,
}

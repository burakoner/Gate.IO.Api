namespace Gate.IO.Api.Futures;

/// <summary>Which side of the book supplies the BBO price; independent of signed order quantity.</summary>
public enum GateFuturesBboDirection : byte
{
    /// <summary>Fetch the ask side.</summary>
    [Map("buy")] Buy = 1,
    /// <summary>Fetch the bid side.</summary>
    [Map("sell")] Sell = 2,
}

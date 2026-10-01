namespace Gate.IO.Api;

/// <summary>
/// Gate.IO API Client Options
/// </summary>
public class GateRestApiClientOptions : RestApiClientOptions
{
    /// <summary>
    /// Receive Window
    /// </summary>
    public TimeSpan? ReceiveWindow { get; set; }

    /// <summary>
    /// Auto Timestamp
    /// </summary>
    public bool AutoTimestamp { get; set; }

    /// <summary>
    /// Timestamp Recalculation Interval
    /// </summary>
    public TimeSpan TimestampRecalculationInterval { get; set; }

    /// <summary>
    /// Enable the Stock lead-trading context using the stock_copy request header.
    /// Defaults to false; fund transfers and transaction history are excluded.
    /// </summary>
    /// <remarks>
    /// Captured when the client is constructed. Use separate clients for personal and lead trading;
    /// changing this option later does not change an existing client's trading context.
    /// </remarks>
    public bool StockLeadTrading { get; set; }

    /// <summary>
    /// Enable TradFi CFD lead trading using the cfd_copy request header. Defaults to false.
    /// User activation and both fund transfer operations are excluded.
    /// </summary>
    /// <remarks>
    /// Captured at construction. Use separate clients for personal and lead trading;
    /// later option changes cannot change an existing client's trading context.
    /// </remarks>
    public bool TradFiLeadTrading { get; set; }

    /// <summary>
    /// Gate.IO API Client Options
    /// </summary>
    public GateRestApiClientOptions() : this(null)
    {
    }

    /// <summary>
    /// Gate.IO API Client Options
    /// </summary>
    /// <param name="credentials">Credentials</param>
    public GateRestApiClientOptions(ApiCredentials credentials)
    {
        // API Credentials
        ApiCredentials = credentials;

        // Api Addresses
        BaseAddress = GateConstants.Default.RestApiAddress;

        // Receive Window
        // ReceiveWindow = TimeSpan.FromSeconds(15);

        // Auto Timestamp
        AutoTimestamp = true;
        TimestampRecalculationInterval = TimeSpan.FromHours(1);

        // Http Options
        HttpOptions = new HttpOptions
        {
            UserAgent = RestApiConstants.USER_AGENT,
            AcceptMimeType = RestApiConstants.JSON_CONTENT_HEADER,
            RequestTimeout = TimeSpan.FromSeconds(30),
            EncodeQueryString = true,
        };
    }

}

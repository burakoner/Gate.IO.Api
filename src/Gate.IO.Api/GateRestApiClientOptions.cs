namespace Gate.IO.Api;

public class GateRestApiClientOptions : RestApiClientOptions
{
    // Receive Window
    public TimeSpan ReceiveWindow { get; set; }

    // Auto Timestamp
    public bool AutoTimestamp { get; set; }
    public TimeSpan TimestampRecalculationInterval { get; set; }

    public GateRestApiClientOptions() : this(null)
    {
    }

    public GateRestApiClientOptions(ApiCredentials credentials)
    {
        // API Credentials
        ApiCredentials = credentials;

        // Api Addresses
        BaseAddress = GateApiAddresses.Default.RestClientAddress;

        // Rate Limiters
        RateLimiters = new List<IRateLimiter>
        {
            new RateLimiter()
                .AddPartialEndpointLimit("/api/", 1200, TimeSpan.FromMinutes(1))
                .AddPartialEndpointLimit("/sapi/", 12000, TimeSpan.FromMinutes(1))
                .AddEndpointLimit("/api/v3/order", 50, TimeSpan.FromSeconds(10), HttpMethod.Post, true)
        };

        // Receive Window
        ReceiveWindow = TimeSpan.FromSeconds(5);

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
            BodyFormat = RestBodyFormat.Json,
        };

        // Request Body
        RequestBodyParameterKey = "BODY";
    }
}
namespace Gate.IO.Api;

public class GateRestApiClientOptions : RestApiClientOptions
{
    // Receive Window
    public TimeSpan ReceiveWindow { get; set; }

    // Auto Timestamp
    public bool AutoTimestamp { get; set; }
    public TimeSpan TimestampRecalculationInterval { get; set; }

    // Broker Id
    public string BrokerId { get; set; }

    public GateRestApiClientOptions() : this(null)
    {
    }

    public GateRestApiClientOptions(ApiCredentials credentials)
    {
        // API Credentials
        ApiCredentials = credentials;

        // Api Addresses
        BaseAddress = GateApiAddresses.Default.RestApiAddress;

        // Rate Limiters
        RateLimiters = new List<IRateLimiter>
        {
            new RateLimiter()
                // General
                .AddTotalRateLimit(900, TimeSpan.FromSeconds(1))

                // Spot
                .AddEndpointLimit("/api/v4/spot/orders", 10, TimeSpan.FromSeconds(1), HttpMethod.Post)
                .AddEndpointLimit("/api/v4/spot/orders", 5000, TimeSpan.FromSeconds(1), HttpMethod.Delete)
                .AddEndpointLimit("/api/v4/spot/batch_orders", 10, TimeSpan.FromSeconds(1), HttpMethod.Post)
                .AddEndpointLimit("/api/v4/spot/batch_orders", 5000, TimeSpan.FromSeconds(1), HttpMethod.Delete)
                
                // Perpetual 
                .AddPartialEndpointLimit("/futures/", 300, TimeSpan.FromSeconds(1), null)
                .AddEndpointLimit("/api/v4/futures/btc/orders", 100, TimeSpan.FromSeconds(1), HttpMethod.Post)
                .AddEndpointLimit("/api/v4/futures/btc/batch_orders", 100, TimeSpan.FromSeconds(1), HttpMethod.Post)
                .AddEndpointLimit("/api/v4/futures/btc/orders", 400, TimeSpan.FromSeconds(1), HttpMethod.Delete)
                .AddEndpointLimit("/api/v4/futures/btc/batch_orders", 400, TimeSpan.FromSeconds(1), HttpMethod.Delete)
                .AddEndpointLimit("/api/v4/futures/usd/orders", 100, TimeSpan.FromSeconds(1), HttpMethod.Post)
                .AddEndpointLimit("/api/v4/futures/usd/batch_orders", 100, TimeSpan.FromSeconds(1), HttpMethod.Post)
                .AddEndpointLimit("/api/v4/futures/usd/orders", 400, TimeSpan.FromSeconds(1), HttpMethod.Delete)
                .AddEndpointLimit("/api/v4/futures/usd/batch_orders", 400, TimeSpan.FromSeconds(1), HttpMethod.Delete)
                .AddEndpointLimit("/api/v4/futures/usdt/orders", 100, TimeSpan.FromSeconds(1), HttpMethod.Post)
                .AddEndpointLimit("/api/v4/futures/usdt/batch_orders", 100, TimeSpan.FromSeconds(1), HttpMethod.Post)
                .AddEndpointLimit("/api/v4/futures/usdt/orders", 400, TimeSpan.FromSeconds(1), HttpMethod.Delete)
                .AddEndpointLimit("/api/v4/futures/usdt/batch_orders", 400, TimeSpan.FromSeconds(1), HttpMethod.Delete)

                // Delivery 
                .AddPartialEndpointLimit("/delivery/", 300, TimeSpan.FromSeconds(1), null)
                .AddEndpointLimit("/api/v4/delivery/btc/orders", 100, TimeSpan.FromSeconds(1), HttpMethod.Post)
                .AddEndpointLimit("/api/v4/delivery/btc/batch_orders", 100, TimeSpan.FromSeconds(1), HttpMethod.Post)
                .AddEndpointLimit("/api/v4/delivery/btc/orders", 400, TimeSpan.FromSeconds(1), HttpMethod.Delete)
                .AddEndpointLimit("/api/v4/delivery/btc/batch_orders", 400, TimeSpan.FromSeconds(1), HttpMethod.Delete)
                .AddEndpointLimit("/api/v4/delivery/usdt/orders", 100, TimeSpan.FromSeconds(1), HttpMethod.Post)
                .AddEndpointLimit("/api/v4/delivery/usdt/batch_orders", 100, TimeSpan.FromSeconds(1), HttpMethod.Post)
                .AddEndpointLimit("/api/v4/delivery/usdt/orders", 400, TimeSpan.FromSeconds(1), HttpMethod.Delete)
                .AddEndpointLimit("/api/v4/delivery/usdt/batch_orders", 400, TimeSpan.FromSeconds(1), HttpMethod.Delete)

                // Swap 
                .AddPartialEndpointLimit("/flash_swap/", 300, TimeSpan.FromSeconds(1), null)
                .AddEndpointLimit("/api/v4/flash_swap/orders", 100, TimeSpan.FromSeconds(1), HttpMethod.Post)
                .AddEndpointLimit("/api/v4/flash_swap/batch_orders", 100, TimeSpan.FromSeconds(1), HttpMethod.Post)
                .AddEndpointLimit("/api/v4/flash_swap/orders", 400, TimeSpan.FromSeconds(1), HttpMethod.Delete)
                .AddEndpointLimit("/api/v4/flash_swap/batch_orders", 400, TimeSpan.FromSeconds(1), HttpMethod.Delete)
                
                // Wallet 
                .AddPartialEndpointLimit("/wallet/", 200, TimeSpan.FromSeconds(1), null)

                // Withdrawal 
                .AddEndpointLimit("/api/v4/withdrawals", 1, TimeSpan.FromSeconds(3), HttpMethod.Post)
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

        // Broker Id
        BrokerId = "phalchatha";
    }

}
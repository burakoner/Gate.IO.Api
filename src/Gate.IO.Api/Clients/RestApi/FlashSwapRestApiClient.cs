using Gate.IO.Api.Models.RestApi.FlashSwap;

namespace Gate.IO.Api.Clients.RestApi;

public class FlashSwapRestApiClient : RestApiClient
{
    // Api
    protected const string api = "api";
    protected const string version = "4";
    protected const string swap = "flash_swap";

    // Endpoints
    private const string currenciesEndpoint = "currencies";
    private const string ordersEndpoint = "orders";
    private const string ordersPreviewEndpoint = "orders/preview";

    // Internal
    internal Log Log { get => this.log; }
    internal TimeSyncState TimeSyncState = new("Gate.IO FlashSwap RestApi");

    // Root Client
    internal GateRestApiClient RootClient { get; }
    internal CultureInfo CI { get { return RootClient.CI; } }
    public GateRestApiClientOptions ClientOptions { get { return RootClient.ClientOptions; } }

    internal FlashSwapRestApiClient(GateRestApiClient root) : base("Gate.IO FlashSwap RestApi", root.ClientOptions)
    {
        RootClient = root;

        RequestBodyFormat = RestRequestBodyFormat.Json;
        ArraySerialization = ArraySerialization.MultipleValues;

        Thread.CurrentThread.CurrentCulture = CI;
        Thread.CurrentThread.CurrentUICulture = CI;
    }

    #region Override Methods
    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
        => new GateAuthenticationProvider(credentials);

    protected override CallError ParseErrorResponse(JToken error)
        => RootClient.ParseErrorResponse(error);

    protected override Task<RestCallResult<DateTime>> GetServerTimestampAsync()
        => RootClient.Spot.GetServerTimeAsync();

    protected override TimeSyncInfo GetTimeSyncInfo()
        => new(log, ClientOptions.AutoTimestamp, ClientOptions.TimestampRecalculationInterval, TimeSyncState);

    public override TimeSpan GetTimeOffset()
        => TimeSyncState.TimeOffset;
    #endregion

    #region Internal Methods
    internal async Task<RestCallResult<T>> SendRequestInternal<T>(
        Uri uri,
        HttpMethod method,
        CancellationToken cancellationToken,
        bool signed = false,
        Dictionary<string, object> queryParameters = null,
        Dictionary<string, object> bodyParameters = null,
        Dictionary<string, string> headerParameters = null,
        ArraySerialization? arraySerialization = null,
        JsonSerializer deserializer = null,
        bool ignoreRatelimit = false,
        int requestWeight = 1) where T : class
    {
        Thread.CurrentThread.CurrentCulture = CI;
        Thread.CurrentThread.CurrentUICulture = CI;
        return await SendRequestAsync<T>(uri, method, cancellationToken, signed, queryParameters, bodyParameters, headerParameters, arraySerialization, deserializer, ignoreRatelimit, requestWeight).ConfigureAwait(false);
    }
    #endregion

    #region List all supported currencies in flash swap
    public async Task<RestCallResult<IEnumerable<FlashSwapCurrency>>> GetAllCurrenciesAsync(CancellationToken ct = default)
    {
        return await SendRequestInternal<IEnumerable<FlashSwapCurrency>>(RootClient.GetUrl(api, version, swap, currenciesEndpoint), HttpMethod.Get, ct).ConfigureAwait(false);
    }
    #endregion

    #region Create a flash swap order
    public async Task<RestCallResult<FlashSwapOrderResponse>> PlaceOrderAsync(
        string buyCurrency,
        string sellCurrency, 
        decimal? buyAmount = null,
        decimal? sellAmount = null,
        long? previewId = null,
        CancellationToken ct = default)
        => await PlaceOrderAsync(new FlashSwapOrderRequest
        {
            BuyCurrency = buyCurrency,
            BuyAmount = buyAmount,
            SellCurrency = sellCurrency,
            SellAmount = sellAmount,
            PreviewId = previewId,
        }, ct).ConfigureAwait(false);

    public async Task<RestCallResult<FlashSwapOrderResponse>> PlaceOrderAsync(FlashSwapOrderRequest request, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "sell_currency", request.SellCurrency },
            { "buy_currency", request.BuyCurrency },
        };
        parameters.AddOptionalParameter("preview_id", request.PreviewId);
        parameters.AddOptionalParameter("sell_amount", request.SellAmount);
        parameters.AddOptionalParameter("buy_amount", request.BuyAmount);

        return await SendRequestInternal<FlashSwapOrderResponse>(RootClient.GetUrl(api, version, swap, ordersEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region List all flash swap orders
    public async Task<RestCallResult<IEnumerable<FlashSwapOrderResponse>>> GetOrdersAsync(
        SwapOrderStatus status,
        string buyCurrency,
        string sellCurrency,
        int page = 1,
        int limit = 100,
        bool reverse = true,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "page", page },
            { "limit", limit },
            { "reverse", reverse },
        };
        parameters.AddOptionalParameter("status", JsonConvert.SerializeObject(status, new SwapOrderStatusConverter(false)));
        parameters.AddOptionalParameter("sell_currency", sellCurrency);
        parameters.AddOptionalParameter("buy_currency", buyCurrency);

        return await SendRequestInternal<IEnumerable<FlashSwapOrderResponse>>(RootClient.GetUrl(api, version, swap, ordersEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Get a single order
    public async Task<RestCallResult<FlashSwapOrderResponse>> GetOrderAsync(long orderId, CancellationToken ct = default)
    {
        return await SendRequestInternal<FlashSwapOrderResponse>(RootClient.GetUrl(api, version, swap, ordersEndpoint.AppendPath(orderId.ToString())), HttpMethod.Get, ct, true).ConfigureAwait(false);
    }
    #endregion

    #region Initiate a flash swap order preview
    public async Task<RestCallResult<FlashSwapOrderPreview>> PreviewOrderAsync(
        string buyCurrency,
        string sellCurrency,
        decimal? buyAmount = null,
        decimal? sellAmount = null,
        long? previewId = null,
        CancellationToken ct = default)
        => await PreviewOrderAsync(new FlashSwapOrderRequest
        {
            BuyCurrency = buyCurrency,
            BuyAmount = buyAmount,
            SellCurrency = sellCurrency,
            SellAmount = sellAmount,
            PreviewId = previewId,
        }, ct).ConfigureAwait(false);

    public async Task<RestCallResult<FlashSwapOrderPreview>> PreviewOrderAsync(FlashSwapOrderRequest request, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "sell_currency", request.SellCurrency },
            { "buy_currency", request.BuyCurrency },
        };
        parameters.AddOptionalParameter("preview_id", request.PreviewId);
        parameters.AddOptionalParameter("sell_amount", request.SellAmount);
        parameters.AddOptionalParameter("buy_amount", request.BuyAmount);

        return await SendRequestInternal<FlashSwapOrderPreview>(RootClient.GetUrl(api, version, swap, ordersPreviewEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }
    #endregion

}
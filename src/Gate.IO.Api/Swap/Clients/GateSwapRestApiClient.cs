namespace Gate.IO.Api.Swap;

/// <summary>
/// Gate.IO Flash Swap REST API Client
/// </summary>
public class GateSwapRestApiClient
{
    // Api
    private const string api = "api";
    private const string version = "4";
    private const string section = "flash_swap";

    // Endpoints
    private const string currencyPairsEndpoint = "currency_pairs";
    private const string ordersEndpoint = "orders";
    private const string ordersPreviewEndpoint = "orders/preview";

    // Root Client
    internal GateRestApiClient _ { get; }

    // Constructor
    internal GateSwapRestApiClient(GateRestApiClient root) => _ = root;

    /// <summary>
    /// List All Supported Currency Pairs In Flash Swap
    /// BTC_GT represents selling BTC and buying GT. The limits for each currency may vary across different currency pairs.
    /// It is not necessary that two currencies that can be swapped instantaneously can be exchanged with each other. For example, it is possible to sell BTC and buy GT, but it does not necessarily mean that GT can be sold to buy BTC.
    /// </summary>
    /// <param name="currency">Retrieve data of the specified currency</param>
    /// <param name="page">Page number</param>
    /// <param name="limit">Maximum response items. Default: 100, minimum: 1, Maximum: 1000</param>
    /// <param name="ct">CancellationToken</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateSwapMarket>>> GetMarketsAsync(string currency = null, int page = 1, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "page", page },
            { "limit", limit },
        };
        parameters.AddOptionalParameter("currency", currency);

        return _.SendRequestInternal<List<GateSwapMarket>>(_.GetUrl(api, version, section, currencyPairsEndpoint), HttpMethod.Get, ct, queryParameters: parameters);
    }

    /// <summary>
    /// Create a flash swap order
    /// Initiate a flash swap preview in advance because order creation requires a preview result
    /// </summary>
    /// <param name="previewId">Preview result ID</param>
    /// <param name="sellCurrency">The name of the asset being sold, as obtained from the "GET /flash_swap/currency_pairs" API, which retrieves a list of supported flash swap currency pairs.</param>
    /// <param name="sellAmount">Amount to sell (based on the preview result)</param>
    /// <param name="buyCurrency">The name of the asset being purchased, as obtained from the "GET /flash_swap/currency_pairs" API, which provides a list of supported flash swap currency pairs.</param>
    /// <param name="buyAmount">Amount to buy (based on the preview result)</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateSwapOrder>> PlaceOrderAsync(
        long previewId,
        string sellCurrency,
        decimal sellAmount,
        string buyCurrency,
        decimal buyAmount,
        CancellationToken ct = default)
        => PlaceOrderAsync(new GateSwapOrderRequest
        {
            BuyCurrency = buyCurrency,
            BuyAmount = buyAmount,
            SellCurrency = sellCurrency,
            SellAmount = sellAmount,
            PreviewId = previewId,
        }, ct);

    /// <summary>
    /// Create a flash swap order
    /// Initiate a flash swap preview in advance because order creation requires a preview result
    /// </summary>
    /// <param name="request">Order Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateSwapOrder>> PlaceOrderAsync(GateSwapOrderRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalString("preview_id", request.PreviewId);
        parameters.Add("sell_currency", request.SellCurrency);
        parameters.AddOptionalString("sell_amount", request.SellAmount);
        parameters.Add("buy_currency", request.BuyCurrency);
        parameters.AddOptionalString("buy_amount", request.BuyAmount);

        return _.SendRequestInternal<GateSwapOrder>(_.GetUrl(api, version, section, ordersEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// List all flash swap orders
    /// </summary>
    /// <param name="status">Flash swap order status</param>
    /// <param name="sellCurrency">Currency to sell which can be retrieved from supported currency list API GET /flash_swap/currencies</param>
    /// <param name="buyCurrency">Currency to buy which can be retrieved from supported currency list API GET /flash_swap/currencies</param>
    /// <param name="page">Page number</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="reverse">If results are sorted by id in reverse order. Default to true</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateSwapOrder>>> GetOrdersAsync(
        GateSwapOrderStatus? status = null,
        string sellCurrency = null,
        string buyCurrency = null,
        int page = 1,
        int limit = 100,
        bool reverse = true,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "page", page },
            { "limit", limit },
            { "reverse", reverse.ToString().ToLower() },
        };
        parameters.AddOptionalEnum("status", status);
        parameters.AddOptional("sell_currency", sellCurrency);
        parameters.AddOptional("buy_currency", buyCurrency);

        return _.SendRequestInternal<List<GateSwapOrder>>(_.GetUrl(api, version, section, ordersEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Get a single order
    /// </summary>
    /// <param name="orderId">Flash swap order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateSwapOrder>> GetOrderAsync(long orderId, CancellationToken ct = default)
    {
        return _.SendRequestInternal<GateSwapOrder>(_.GetUrl(api, version, section, ordersEndpoint.AppendPath(orderId.ToString())), HttpMethod.Get, ct, true);
    }

    /// <summary>
    /// Initiate a flash swap order preview
    /// </summary>
    /// <param name="sellCurrency">The name of the asset being sold, as obtained from the "GET /flash_swap/currency_pairs" API, which retrieves a list of supported flash swap currency pairs.</param>
    /// <param name="sellAmount">Amount to sell.</param>
    /// <param name="buyCurrency">The name of the asset being purchased, as obtained from the "GET /flash_swap/currency_pairs" API, which provides a list of supported flash swap currency pairs.</param>
    /// <param name="buyAmount">Amount to buy.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateSwapOrderPreview>> PreviewOrderAsync(
        string sellCurrency,
        decimal sellAmount,
        string buyCurrency,
        decimal buyAmount,
        CancellationToken ct = default)
        => PreviewOrderAsync(new GateSwapOrderRequest
        {
            SellCurrency = sellCurrency,
            SellAmount = sellAmount,
            BuyCurrency = buyCurrency,
            BuyAmount = buyAmount,
        }, ct);

    /// <summary>
    /// Initiate a flash swap order preview
    /// </summary>
    /// <param name="request">Preview Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public Task<RestCallResult<GateSwapOrderPreview>> PreviewOrderAsync(GateSwapOrderRequest request, CancellationToken ct = default)
    {
        if (request.PreviewId.HasValue) 
            throw new ArgumentException("PreviewId must be null for preview endpoint", nameof(request.PreviewId));

        var parameters = new ParameterCollection();
        parameters.Add("sell_currency", request.SellCurrency);
        parameters.AddOptionalString("sell_amount", request.SellAmount);
        parameters.Add("buy_currency", request.BuyCurrency);
        parameters.AddOptionalString("buy_amount", request.BuyAmount);

        return _.SendRequestInternal<GateSwapOrderPreview>(_.GetUrl(api, version, section, ordersPreviewEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }
}
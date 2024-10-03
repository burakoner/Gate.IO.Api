namespace Gate.IO.Api.Swap;

/// <summary>
/// Gate.IO Flash Swap REST API Client
/// </summary>
public class GateSwapRestApiClient
{
    // Api
    private const string api = "api";
    private const string version = "4";
    private const string swap = "flash_swap";

    // Endpoints
    private const string currenciesEndpoint = "currencies";
    private const string currencyPairsEndpoint = "currency_pairs";
    private const string ordersEndpoint = "orders";
    private const string ordersPreviewEndpoint = "orders/preview";

    // Root Client
    internal GateRestApiClient Root { get; }

    internal GateSwapRestApiClient(GateRestApiClient root)
    {
        Root = root;
    }

    /// <summary>
    /// List all supported currencies in flash swap
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateSwapCurrency>>> GetCurrenciesAsync(CancellationToken ct = default)
    {
        return Root.SendRequestInternal<List<GateSwapCurrency>>(Root.GetUrl(api, version, swap, currenciesEndpoint), HttpMethod.Get, ct);
    }

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
    public Task<RestCallResult<List<GateSwapMarket>>> GetMarketsAsync(string currency = "", int page = 1, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "page", page },
            { "limit", limit },
        };
        parameters.AddOptionalParameter("currency", currency);

        return Root.SendRequestInternal<List<GateSwapMarket>>(Root.GetUrl(api, version, swap, currencyPairsEndpoint), HttpMethod.Get, ct, queryParameters: parameters);
    }

    /// <summary>
    /// Create a flash swap order
    /// Initiate a flash swap preview in advance because order creation requires a preview result
    /// </summary>
    /// <param name="previewId"></param>
    /// <param name="sellCurrency"></param>
    /// <param name="sellAmount"></param>
    /// <param name="buyCurrency"></param>
    /// <param name="buyAmount"></param>
    /// <param name="ct"></param>
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
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<RestCallResult<GateSwapOrder>> PlaceOrderAsync(GateSwapOrderRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalString("preview_id", request.PreviewId);
        parameters.Add("sell_currency", request.SellCurrency);
        parameters.AddOptionalString("sell_amount", request.SellAmount);
        parameters.Add("buy_currency", request.BuyCurrency);
        parameters.AddOptionalString("buy_amount", request.BuyAmount);

        return Root.SendRequestInternal<GateSwapOrder>(Root.GetUrl(api, version, swap, ordersEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// List all flash swap orders
    /// </summary>
    /// <param name="status"></param>
    /// <param name="sellCurrency"></param>
    /// <param name="buyCurrency"></param>
    /// <param name="page"></param>
    /// <param name="limit"></param>
    /// <param name="reverse"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<List<GateSwapOrder>>> GetOrdersAsync(
        GateSwapOrderStatus? status = null,
        string sellCurrency = "",
        string buyCurrency = "",
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

        return await Root.SendRequestInternal<List<GateSwapOrder>>(Root.GetUrl(api, version, swap, ordersEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Get a single order
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<GateSwapOrder>> GetOrderAsync(long orderId, CancellationToken ct = default)
    {
        return await Root.SendRequestInternal<GateSwapOrder>(Root.GetUrl(api, version, swap, ordersEndpoint.AppendPath(orderId.ToString())), HttpMethod.Get, ct, true).ConfigureAwait(false);
    }

    /// <summary>
    /// Initiate a flash swap order preview
    /// </summary>
    /// <param name="sellCurrency"></param>
    /// <param name="sellAmount"></param>
    /// <param name="buyCurrency"></param>
    /// <param name="buyAmount"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<GateSwapOrderPreview>> PreviewOrderAsync(
        string sellCurrency,
        decimal sellAmount,
        string buyCurrency,
        decimal buyAmount,
        CancellationToken ct = default)
        => await PreviewOrderAsync(new GateSwapOrderRequest
        {
            SellCurrency = sellCurrency,
            SellAmount = sellAmount,
            BuyCurrency = buyCurrency,
            BuyAmount = buyAmount,
        }, ct).ConfigureAwait(false);

    /// <summary>
    /// Initiate a flash swap order preview
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<RestCallResult<GateSwapOrderPreview>> PreviewOrderAsync(GateSwapOrderRequest request, CancellationToken ct = default)
    {
        if (request.PreviewId.HasValue) throw new ArgumentException("PreviewId must be null for preview endpoint", nameof(request.PreviewId));

        var parameters = new ParameterCollection();
        parameters.Add("sell_currency", request.SellCurrency);
        parameters.AddOptionalString("sell_amount", request.SellAmount);
        parameters.Add("buy_currency", request.BuyCurrency);
        parameters.AddOptionalString("buy_amount", request.BuyAmount);

        return await Root.SendRequestInternal<GateSwapOrderPreview>(Root.GetUrl(api, version, swap, ordersPreviewEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }
}
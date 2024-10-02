using Gate.IO.Api.Converters;
using Gate.IO.Api.Enums;
using Gate.IO.Api.Models.RestApi.FlashSwap;

namespace Gate.IO.Api.Clients.RestApi;

public class RestApiSwapClient
{
    // Api
    private const string api = "api";
    private const string version = "4";
    private const string swap = "flash_swap";

    // Endpoints
    private const string currenciesEndpoint = "currencies";
    private const string ordersEndpoint = "orders";
    private const string ordersPreviewEndpoint = "orders/preview";

    // Root Client
    internal GateRestApiClient Root { get; }

    internal RestApiSwapClient(GateRestApiClient root)
    {
        Root = root;
    }

    #region List all supported currencies in flash swap
    public async Task<RestCallResult<IEnumerable<GateSwapCurrency>>> GetAllCurrenciesAsync(CancellationToken ct = default)
    {
        return await Root.SendRequestInternal<IEnumerable<GateSwapCurrency>>(Root.GetUrl(api, version, swap, currenciesEndpoint), HttpMethod.Get, ct).ConfigureAwait(false);
    }
    #endregion

    #region Create a flash swap order
    public async Task<RestCallResult<GateSwapOrder>> PlaceOrderAsync(
        string buyCurrency,
        string sellCurrency, 
        decimal? buyAmount = null,
        decimal? sellAmount = null,
        long? previewId = null,
        CancellationToken ct = default)
        => await PlaceOrderAsync(new GateSwapOrderRequest
        {
            BuyCurrency = buyCurrency,
            BuyAmount = buyAmount,
            SellCurrency = sellCurrency,
            SellAmount = sellAmount,
            PreviewId = previewId,
        }, ct).ConfigureAwait(false);

    public async Task<RestCallResult<GateSwapOrder>> PlaceOrderAsync(GateSwapOrderRequest request, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "sell_currency", request.SellCurrency },
            { "buy_currency", request.BuyCurrency },
        };
        parameters.AddOptionalParameter("preview_id", request.PreviewId);
        parameters.AddOptionalParameter("sell_amount", request.SellAmount?.ToGateString());
        parameters.AddOptionalParameter("buy_amount", request.BuyAmount?.ToGateString());

        return await Root.SendRequestInternal<GateSwapOrder>(Root.GetUrl(api, version, swap, ordersEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region List all flash swap orders
    public async Task<RestCallResult<IEnumerable<GateSwapOrder>>> GetOrdersAsync(
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
            { "reverse", reverse.ToString().ToLower() },
        };
        parameters.AddOptionalParameter("status", JsonConvert.SerializeObject(status, new SwapOrderStatusConverter(false)));
        parameters.AddOptionalParameter("sell_currency", sellCurrency);
        parameters.AddOptionalParameter("buy_currency", buyCurrency);

        return await Root.SendRequestInternal<IEnumerable<GateSwapOrder>>(Root.GetUrl(api, version, swap, ordersEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Get a single order
    public async Task<RestCallResult<GateSwapOrder>> GetOrderAsync(long orderId, CancellationToken ct = default)
    {
        return await Root.SendRequestInternal<GateSwapOrder>(Root.GetUrl(api, version, swap, ordersEndpoint.AppendPath(orderId.ToString())), HttpMethod.Get, ct, true).ConfigureAwait(false);
    }
    #endregion

    #region Initiate a flash swap order preview
    public async Task<RestCallResult<GateSwapOrderPreview>> PreviewOrderAsync(
        string buyCurrency,
        string sellCurrency,
        decimal? buyAmount = null,
        decimal? sellAmount = null,
        long? previewId = null,
        CancellationToken ct = default)
        => await PreviewOrderAsync(new GateSwapOrderRequest
        {
            BuyCurrency = buyCurrency,
            BuyAmount = buyAmount,
            SellCurrency = sellCurrency,
            SellAmount = sellAmount,
            PreviewId = previewId,
        }, ct).ConfigureAwait(false);

    public async Task<RestCallResult<GateSwapOrderPreview>> PreviewOrderAsync(GateSwapOrderRequest request, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "sell_currency", request.SellCurrency },
            { "buy_currency", request.BuyCurrency },
        };
        parameters.AddOptionalParameter("preview_id", request.PreviewId);
        parameters.AddOptionalParameter("sell_amount", request.SellAmount?.ToGateString());
        parameters.AddOptionalParameter("buy_amount", request.BuyAmount?.ToGateString());

        return await Root.SendRequestInternal<GateSwapOrderPreview>(Root.GetUrl(api, version, swap, ordersPreviewEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }
    #endregion

}
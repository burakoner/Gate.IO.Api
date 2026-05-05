namespace Gate.IO.Api.CrossEx;

/// <summary>
/// Gate.IO CrossEx WebSocket API client.
/// </summary>
public class GateCrossExStreamApiClient
{
    // Internal
    internal GateWebSocketClient RootClient { get; }
    internal GateBaseStreamApiClient BaseClient { get; }
    internal GateWebSocketClientOptions ClientOptions { get; }
    internal string PublicAddress { get; }
    internal string PrivateAddress { get; }

    // Public channels
    private const string lastPriceChannel = "last_price";
    private const string indexPriceChannel = "index_price";
    private const string markPriceChannel = "mark_price";
    private const string orderBookUpdateChannel = "order_book_update";
    private const string tickerChannel = "ticker";
    private const string tradeChannel = "trade";
    private const string fundingRateChannel = "funding_rate";
    private const string openInterestChannel = "open_interest";

    // Private channels
    private const string orderChannel = "order";
    private const string assetChannel = "asset";
    private const string userTradesChannel = "usertrades";
    private const string positionChannel = "position";
    private const string marginPositionChannel = "margin_position";
    private const string marginInterestChannel = "margin_interest";

    // API channels
    private const string placeOrderChannel = "place_order";
    private const string cancelOrderChannel = "cancel_order";
    private const string updateOrderChannel = "update_order";
    private const string setLeverageChannel = "set_leverage";
    private const string setMarginLeverageChannel = "set_margin_leverage";
    private const string updateAccountsChannel = "update_accounts";
    private const string closePositionChannel = "close_position";

    internal GateCrossExStreamApiClient(GateWebSocketClient root)
    {
        RootClient = root;
        BaseClient = root.Base;
        ClientOptions = root.ClientOptions;
        PublicAddress = root.ClientOptions.StreamCrossExPublicAddress;
        PrivateAddress = root.ClientOptions.StreamCrossExPrivateAddress;
    }

    /// <summary>
    /// Executes the Unsubscribe operation.
    /// </summary>
    public async Task UnsubscribeAsync(int subscriptionId)
        => await BaseClient.UnsubscribeAsync(subscriptionId).ConfigureAwait(false);

    /// <summary>
    /// Executes the Unsubscribe operation.
    /// </summary>
    public async Task UnsubscribeAsync(WebSocketUpdateSubscription subscription)
        => await BaseClient.UnsubscribeAsync(subscription).ConfigureAwait(false);

    /// <summary>
    /// Executes the Unsubscribe All operation.
    /// </summary>
    public async Task UnsubscribeAllAsync()
        => await BaseClient.UnsubscribeAllAsync().ConfigureAwait(false);

    /// <summary>
    /// Logs in to the CrossEx private WebSocket connection. Private subscriptions and WebSocket API requests also authenticate their own connection automatically.
    /// </summary>
    public async Task<CallResult<GateCrossExStreamResponse<GateCrossExStreamLoginResult>>> LoginAsync()
    {
        var timestamp = DateTime.UtcNow.ConvertToSeconds();
        var payload = BaseClient.CreateCrossExLoginPayload(timestamp);
        return await BaseClient.CrossExQueryAsync<GateCrossExStreamResponse<GateCrossExStreamLoginResult>>(PrivateAddress, null, "login", payload).ConfigureAwait(false);
    }

    /// <summary>
    /// Subscribes to last trade price updates.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToLastPricesAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<GateCrossExStreamLastPrice>> onMessage, CancellationToken ct = default)
        => await SubscribePublicAsync(PublicAddress, lastPriceChannel, symbols, onMessage, ct).ConfigureAwait(false);

    /// <summary>
    /// Subscribes to index price updates.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToIndexPricesAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<GateCrossExStreamIndexPrice>> onMessage, CancellationToken ct = default)
        => await SubscribePublicAsync(PublicAddress, indexPriceChannel, symbols, onMessage, ct).ConfigureAwait(false);

    /// <summary>
    /// Subscribes to mark price updates.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToMarkPricesAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<GateCrossExStreamMarkPrice>> onMessage, CancellationToken ct = default)
        => await SubscribePublicAsync(PublicAddress, markPriceChannel, symbols, onMessage, ct).ConfigureAwait(false);

    /// <summary>
    /// Subscribes to full limited-level order book updates.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBooksAsync(IEnumerable<string> symbols, int level, Action<WebSocketDataEvent<GateCrossExStreamOrderBook>> onMessage, CancellationToken ct = default)
    {
        level.ValidateIntValues(nameof(level), 1, 5, 10, 20, 30, 50, 100);
        return await SubscribePublicAsync(PublicAddress, $"order_book_{level}", symbols, onMessage, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Subscribes to incremental order book updates.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<GateCrossExStreamOrderBookUpdate>> onMessage, CancellationToken ct = default)
        => await SubscribePublicAsync(PublicAddress, orderBookUpdateChannel, symbols, onMessage, ct).ConfigureAwait(false);

    /// <summary>
    /// Subscribes to ticker updates.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTickersAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<GateCrossExStreamTicker>> onMessage, CancellationToken ct = default)
        => await SubscribePublicAsync(PublicAddress, tickerChannel, symbols, onMessage, ct).ConfigureAwait(false);

    /// <summary>
    /// Subscribes to public trade updates.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTradesAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<GateCrossExStreamTrade>> onMessage, CancellationToken ct = default)
        => await SubscribePublicAsync(PublicAddress, tradeChannel, symbols, onMessage, ct).ConfigureAwait(false);

    /// <summary>
    /// Subscribes to kline updates.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToKlinesAsync(IEnumerable<string> symbols, GateCrossExKlineInterval interval, Action<WebSocketDataEvent<GateCrossExStreamKline>> onMessage, CancellationToken ct = default)
        => await SubscribePublicAsync(PublicAddress, $"kline_{MapConverter.GetString(interval)}", symbols, onMessage, ct).ConfigureAwait(false);

    /// <summary>
    /// Subscribes to futures funding rate updates.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToFundingRatesAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<GateCrossExStreamFundingRate>> onMessage, CancellationToken ct = default)
        => await SubscribePublicAsync(PublicAddress, fundingRateChannel, symbols, onMessage, ct).ConfigureAwait(false);

    /// <summary>
    /// Subscribes to futures open interest updates.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOpenInterestsAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<GateCrossExStreamOpenInterest>> onMessage, CancellationToken ct = default)
        => await SubscribePublicAsync(PublicAddress, openInterestChannel, symbols, onMessage, ct).ConfigureAwait(false);

    /// <summary>
    /// Subscribes to all user order updates.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserOrdersAsync(Action<WebSocketDataEvent<GateCrossExOrder>> onMessage, CancellationToken ct = default)
        => await SubscribeToUserOrdersAsync(["!all"], onMessage, ct).ConfigureAwait(false);

    /// <summary>
    /// Subscribes to user order updates.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserOrdersAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<GateCrossExOrder>> onMessage, CancellationToken ct = default)
        => await SubscribePrivateAsync(orderChannel, symbols, onMessage, ct).ConfigureAwait(false);

    /// <summary>
    /// Subscribes to all user asset updates.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserAssetsAsync(Action<WebSocketDataEvent<GateCrossExAccountAsset>> onMessage, CancellationToken ct = default)
        => await SubscribeToUserAssetsAsync(["!all"], onMessage, ct).ConfigureAwait(false);

    /// <summary>
    /// Subscribes to user asset updates.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserAssetsAsync(IEnumerable<string> coins, Action<WebSocketDataEvent<GateCrossExAccountAsset>> onMessage, CancellationToken ct = default)
        => await SubscribePrivateAsync(assetChannel, coins, onMessage, ct).ConfigureAwait(false);

    /// <summary>
    /// Subscribes to all user trade updates.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserTradesAsync(Action<WebSocketDataEvent<GateCrossExTrade>> onMessage, CancellationToken ct = default)
        => await SubscribeToUserTradesAsync(["!all"], onMessage, ct).ConfigureAwait(false);

    /// <summary>
    /// Subscribes to user trade updates.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserTradesAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<GateCrossExTrade>> onMessage, CancellationToken ct = default)
        => await SubscribePrivateAsync(userTradesChannel, symbols, onMessage, ct).ConfigureAwait(false);

    /// <summary>
    /// Subscribes to all user position updates.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserPositionsAsync(Action<WebSocketDataEvent<GateCrossExPosition>> onMessage, CancellationToken ct = default)
        => await SubscribeToUserPositionsAsync(["!all"], onMessage, ct).ConfigureAwait(false);

    /// <summary>
    /// Subscribes to user position updates.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserPositionsAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<GateCrossExPosition>> onMessage, CancellationToken ct = default)
        => await SubscribePrivateAsync(positionChannel, symbols, onMessage, ct).ConfigureAwait(false);

    /// <summary>
    /// Subscribes to all user margin position updates.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserMarginPositionsAsync(Action<WebSocketDataEvent<GateCrossExMarginPosition>> onMessage, CancellationToken ct = default)
        => await SubscribeToUserMarginPositionsAsync(["!all"], onMessage, ct).ConfigureAwait(false);

    /// <summary>
    /// Subscribes to user margin position updates.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserMarginPositionsAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<GateCrossExMarginPosition>> onMessage, CancellationToken ct = default)
        => await SubscribePrivateAsync(marginPositionChannel, symbols, onMessage, ct).ConfigureAwait(false);

    /// <summary>
    /// Subscribes to all user margin interest updates.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserMarginInterestsAsync(Action<WebSocketDataEvent<GateCrossExStreamMarginInterest>> onMessage, CancellationToken ct = default)
        => await SubscribeToUserMarginInterestsAsync(["!all"], onMessage, ct).ConfigureAwait(false);

    /// <summary>
    /// Subscribes to user margin interest updates.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserMarginInterestsAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<GateCrossExStreamMarginInterest>> onMessage, CancellationToken ct = default)
        => await SubscribePrivateAsync(marginInterestChannel, symbols, onMessage, ct).ConfigureAwait(false);

    /// <summary>
    /// Places a CrossEx order through WebSocket.
    /// </summary>
    public async Task<CallResult<GateCrossExStreamResponse<GateCrossExOrderActionResult>>> PlaceOrderAsync(GateCrossExOrderRequest request)
        => await CrossExApiAsync<GateCrossExOrderActionResult>(placeOrderChannel, CreateOrderPayload(request)).ConfigureAwait(false);

    /// <summary>
    /// Cancels a CrossEx order through WebSocket.
    /// </summary>
    public async Task<CallResult<GateCrossExStreamResponse<JToken>>> CancelOrderAsync(string orderId)
        => await CrossExApiAsync<JToken>(cancelOrderChannel, orderId).ConfigureAwait(false);

    /// <summary>
    /// Updates a CrossEx order through WebSocket.
    /// </summary>
    public async Task<CallResult<GateCrossExStreamResponse<GateCrossExOrderActionResult>>> UpdateOrderAsync(string orderId, GateCrossExOrderUpdateRequest request, string symbol = null)
        => await CrossExApiAsync<GateCrossExOrderActionResult>(updateOrderChannel, CreateOrderUpdatePayload(orderId, request, symbol)).ConfigureAwait(false);

    /// <summary>
    /// Sets futures leverage through WebSocket.
    /// </summary>
    public async Task<CallResult<GateCrossExStreamResponse<GateCrossExLeverageResult>>> SetLeverageAsync(GateCrossExLeverageRequest request)
        => await CrossExApiAsync<GateCrossExLeverageResult>(setLeverageChannel, CreateLeveragePayload(request)).ConfigureAwait(false);

    /// <summary>
    /// Sets margin leverage through WebSocket.
    /// </summary>
    public async Task<CallResult<GateCrossExStreamResponse<GateCrossExLeverageResult>>> SetMarginLeverageAsync(GateCrossExLeverageRequest request)
        => await CrossExApiAsync<GateCrossExLeverageResult>(setMarginLeverageChannel, CreateLeveragePayload(request)).ConfigureAwait(false);

    /// <summary>
    /// Updates CrossEx account settings through WebSocket.
    /// </summary>
    public async Task<CallResult<GateCrossExStreamResponse<GateCrossExAccountUpdateResult>>> UpdateAccountsAsync(GateCrossExAccountUpdateRequest request)
        => await CrossExApiAsync<GateCrossExAccountUpdateResult>(updateAccountsChannel, CreateAccountUpdatePayload(request)).ConfigureAwait(false);

    /// <summary>
    /// Closes a CrossEx position through WebSocket.
    /// </summary>
    public async Task<CallResult<GateCrossExStreamResponse<GateCrossExOrderActionResult>>> ClosePositionAsync(GateCrossExClosePositionRequest request)
        => await CrossExApiAsync<GateCrossExOrderActionResult>(closePositionChannel, CreateClosePositionPayload(request)).ConfigureAwait(false);

    private async Task<CallResult<WebSocketUpdateSubscription>> SubscribePublicAsync<T>(string url, string channel, IEnumerable<string> symbols, Action<WebSocketDataEvent<T>> onMessage, CancellationToken ct)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<T>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.CrossExSubscribeAsync(url, channel, symbols.ToList(), handler, ct).ConfigureAwait(false);
    }

    private async Task<CallResult<WebSocketUpdateSubscription>> SubscribePrivateAsync<T>(string channel, IEnumerable<string> payload, Action<WebSocketDataEvent<T>> onMessage, CancellationToken ct)
    {
        var handler = new Action<WebSocketDataEvent<GateCrossExStreamResponse<T>>>(data => onMessage(data.As(data.Data.Payload, data.Data.Channel)));
        return await BaseClient.CrossExSubscribeAsync(PrivateAddress, channel, payload.ToList(), true, handler, ct).ConfigureAwait(false);
    }

    private async Task<CallResult<GateCrossExStreamResponse<T>>> CrossExApiAsync<T>(string channel, object payload)
        => await BaseClient.CrossExQueryAsync<GateCrossExStreamResponse<T>>(PrivateAddress, channel, "api", payload, true).ConfigureAwait(false);

    private static Dictionary<string, object> CreateOrderPayload(GateCrossExOrderRequest request)
    {
        var payload = new Dictionary<string, object>
        {
            { "header", new Dictionary<string, string> { { "X-Gate-Channel-Id", GateConstants.Default.ChannelId } } },
            { "symbol", request.Symbol },
            { "side", MapConverter.GetString(request.Side) },
        };
        AddOptional(payload, "text", request.Text);
        AddOptional(payload, "type", request.Type.HasValue ? MapConverter.GetString(request.Type.Value) : null);
        AddOptional(payload, "time_in_force", request.TimeInForce.HasValue ? MapConverter.GetString(request.TimeInForce.Value) : null);
        AddOptionalString(payload, "qty", request.Quantity);
        AddOptionalString(payload, "price", request.Price);
        AddOptionalString(payload, "quote_qty", request.QuoteQuantity);
        AddOptional(payload, "reduce_only", request.ReduceOnly);
        AddOptional(payload, "position_side", request.PositionSide.HasValue ? MapConverter.GetString(request.PositionSide.Value) : null);
        return payload;
    }

    private static Dictionary<string, object> CreateOrderUpdatePayload(string orderId, GateCrossExOrderUpdateRequest request, string symbol)
    {
        var payload = new Dictionary<string, object> { { "order_id", orderId } };
        AddOptionalString(payload, "qty", request.Quantity);
        AddOptionalString(payload, "price", request.Price);
        AddOptional(payload, "symbol", symbol);
        return payload;
    }

    private static Dictionary<string, object> CreateLeveragePayload(GateCrossExLeverageRequest request)
        => new()
        {
            { "symbol", request.Symbol },
            { "leverage", request.Leverage.ToString(CultureInfo.InvariantCulture) },
        };

    private static Dictionary<string, object> CreateAccountUpdatePayload(GateCrossExAccountUpdateRequest request)
    {
        var payload = new Dictionary<string, object>();
        AddOptional(payload, "position_mode", request.PositionMode.HasValue ? MapConverter.GetString(request.PositionMode.Value) : null);
        AddOptional(payload, "account_mode", request.AccountMode.HasValue ? MapConverter.GetString(request.AccountMode.Value) : null);
        AddOptional(payload, "exchange_type", request.ExchangeType.HasValue ? MapConverter.GetString(request.ExchangeType.Value) : null);
        return payload;
    }

    private static Dictionary<string, object> CreateClosePositionPayload(GateCrossExClosePositionRequest request)
    {
        var payload = new Dictionary<string, object> { { "symbol", request.Symbol } };
        AddOptional(payload, "position_side", request.PositionSide.HasValue ? MapConverter.GetString(request.PositionSide.Value) : null);
        return payload;
    }

    private static void AddOptional(Dictionary<string, object> payload, string name, object value)
    {
        if (value != null)
            payload[name] = value;
    }

    private static void AddOptionalString(Dictionary<string, object> payload, string name, decimal? value)
    {
        if (value.HasValue)
            payload[name] = value.Value.ToString(CultureInfo.InvariantCulture);
    }
}

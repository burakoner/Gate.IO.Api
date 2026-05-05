namespace Gate.IO.Api.Spot;

/// <summary>
/// Represents the Stream API Spot Client.
/// </summary>
public class GateSpotStreamApiClient
{
    // Channels
    private const string spotPingChannel = "spot.ping";
    private const string spotTickersChannel = "spot.tickers";
    private const string spotTradesChannel = "spot.trades";
    private const string spotTradesV2Channel = "spot.trades_v2";
    private const string spotCandlesticksChannel = "spot.candlesticks";
    private const string spotBookTickerChannel = "spot.book_ticker";
    private const string spotOrderBookUpdateChannel = "spot.order_book_update";
    private const string spotOrderBookChannel = "spot.order_book";
    private const string spotOrderBookV2Channel = "spot.obu";
    private const string spotUserOrdersChannel = "spot.orders";
    private const string spotUserOrdersV2Channel = "spot.orders_v2";
    private const string spotUserTradesChannel = "spot.usertrades";
    private const string spotUserTradesV2Channel = "spot.usertrades_v2";
    private const string spotUserSpotBalancesChannel = "spot.balances";
    private const string spotUserMarginBalancesChannel = "spot.margin_balances";
    private const string spotUserFundingBalancesChannel = "spot.funding_balances";
    private const string spotUserCrossMarginBalancesChannel = "spot.cross_balances";
    private const string spotUserCrossMarginLoanChannel = "spot.cross_loan";
    private const string spotPriceOrdersChannel = "spot.priceorders";

    // Internal
    internal GateWebSocketClient RootClient { get; }
    internal GateBaseStreamApiClient BaseClient { get; }
    internal GateWebSocketClientOptions ClientOptions { get; }
    private string BaseAddress { get => ClientOptions.StreamSpotAddress; }

    internal GateSpotStreamApiClient(GateWebSocketClient root)
    {
        RootClient = root;
        BaseClient = root.Base;
        ClientOptions = root.ClientOptions;
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
    /// Executes the Ping operation.
    /// </summary>
    public async Task<CallResult<GateStreamLatency>> PingAsync()
        => await BaseClient.PingAsync(BaseAddress, spotPingChannel).ConfigureAwait(false);

    /// <summary>
    /// Executes the Subscribe To Tickers operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTickersAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<GateSpotStreamTicker>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<GateSpotStreamTicker>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, spotTickersChannel, symbols, false, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To Trades operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTradesAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<GateSpotStreamTrade>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<GateSpotStreamTrade>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, spotTradesChannel, symbols, false, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To Trades V2 operation.
    /// </summary>
    [Obsolete("Gate marks spot.trades_v2 as deprecated. Use SubscribeToTradesAsync instead.")]
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTradesV2Async(IEnumerable<string> symbols, Action<WebSocketDataEvent<GateSpotStreamTrade>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<GateSpotStreamTrade>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, spotTradesV2Channel, symbols, false, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To Candlesticks operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToCandlesticksAsync(string symbol, GateSpotCandlestickInterval interval, Action<WebSocketDataEvent<GateSpotStreamCandlestick>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(MapConverter.GetString(interval));
        payload.Add(symbol);

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<GateSpotStreamCandlestick>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, spotCandlesticksChannel, payload, false, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To Order Book Tickers operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBookTickersAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<GateSpotStreamBookTicker>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<GateSpotStreamBookTicker>>>(data => onMessage(data.As<GateSpotStreamBookTicker>(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, spotBookTickerChannel, symbols, false, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To Order Book Differences operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBookDifferencesAsync(string symbol, int interval, Action<WebSocketDataEvent<GateSpotStreamBookDifference>> onMessage, CancellationToken ct = default)
    {
        interval.ValidateIntValues(nameof(interval), 20, 100);

        var payload = new List<string>();
        payload.Add(symbol);
        payload.Add($"{interval}ms");

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<GateSpotStreamBookDifference>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, spotOrderBookUpdateChannel, payload, false, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To Order Book Snapshots operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBookSnapshotsAsync(string symbol, int interval, int level, Action<WebSocketDataEvent<GateSpotStreamBookSnapshot>> onMessage, CancellationToken ct = default)
    {
        level.ValidateIntValues(nameof(level), 5, 10, 20, 50, 100);
        interval.ValidateIntValues(nameof(interval), 100, 1000);

        var payload = new List<string>();
        payload.Add(symbol);
        payload.Add(level.ToString());
        payload.Add($"{interval}ms");

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<GateSpotStreamBookSnapshot>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, spotOrderBookChannel, payload, false, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To Order Book V2 operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBookV2Async(string symbol, int level, Action<WebSocketDataEvent<GateSpotStreamOrderBookV2Update>> onMessage, CancellationToken ct = default)
    {
        level.ValidateIntValues(nameof(level), 50, 400);

        var payload = new List<string> { $"ob.{symbol}.{level}" };

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<GateSpotStreamOrderBookV2Update>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, spotOrderBookV2Channel, payload, false, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To User Orders operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserOrdersAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<GateSpotStreamOrderUpdate>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateSpotStreamOrderUpdate>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, spotUserOrdersChannel, symbols, true, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To User Orders V2 operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserOrdersV2Async(IEnumerable<string> symbols, Action<WebSocketDataEvent<GateSpotStreamOrderUpdate>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateSpotStreamOrderUpdate>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, spotUserOrdersV2Channel, symbols, true, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To User Trades operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserTradesAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<GateSpotTradeHistory>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateSpotTradeHistory>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, spotUserTradesChannel, symbols, true, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To User Trades V2 operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserTradesV2Async(IEnumerable<string> symbols, Action<WebSocketDataEvent<GateSpotTradeHistory>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateSpotTradeHistory>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, spotUserTradesV2Channel, symbols, true, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To User Spot Balances operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserSpotBalancesAsync(Action<WebSocketDataEvent<GateSpotStreamUserBalance>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateSpotStreamUserBalance>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, spotUserSpotBalancesChannel, (object)null, true, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To User Margin Balances operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserMarginBalancesAsync(Action<WebSocketDataEvent<GateSpotStreamMarginBalance>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateSpotStreamMarginBalance>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, spotUserMarginBalancesChannel, (object)null, true, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To User Funding Balances operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserFundingBalancesAsync(Action<WebSocketDataEvent<GateSpotStreamFundingBalance>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateSpotStreamFundingBalance>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, spotUserFundingBalancesChannel, (object)null, true, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To User Cross Margin Balances operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserCrossMarginBalancesAsync(Action<WebSocketDataEvent<GateSpotStreamCrossMarginBalance>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateSpotStreamCrossMarginBalance>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, spotUserCrossMarginBalancesChannel, (object)null, true, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To User Cross Margin Loans operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserCrossMarginLoansAsync(Action<WebSocketDataEvent<GateSpotStreamCrossMarginLoan>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<GateSpotStreamCrossMarginLoan>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, spotUserCrossMarginLoanChannel, (object)null, true, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To Price Orders operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToPriceOrdersAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<GateSpotStreamPriceOrder>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<GateSpotStreamPriceOrder>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, spotPriceOrdersChannel, symbols, true, handler, ct).ConfigureAwait(false);
    }

}

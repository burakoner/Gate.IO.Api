using Gate.IO.Api.Models.StreamApi.Spot;

namespace Gate.IO.Api.Clients.StreamApi;

public class StreamApiSpotClient
{
    // Channels
    private const string spotPingChannel = "spot.ping";
    private const string spotTickersChannel = "spot.tickers";
    private const string spotTradesChannel = "spot.trades";
    private const string spotCandlesticksChannel = "spot.candlesticks";
    private const string spotBookTickerChannel = "spot.book_ticker";
    private const string spotOrderBookUpdateChannel = "spot.order_book_update";
    private const string spotOrderBookChannel = "spot.order_book";
    private const string spotUserOrdersChannel = "spot.orders";
    private const string spotUserTradesChannel = "spot.usertrades";
    private const string spotUserSpotBalancesChannel = "spot.balances";
    private const string spotUserMarginBalancesChannel = "spot.margin_balances";
    private const string spotUserFundingBalancesChannel = "spot.funding_balances";
    private const string spotUserCrossMarginBalancesChannel = "spot.cross_balances";
    private const string spotUserCrossMarginLoanChannel = "spot.cross_loan";

    // Internal
    internal GateWebSocketClient RootClient { get; }
    internal StreamApiBaseClient BaseClient { get; }
    internal GateWebSocketClientOptions ClientOptions { get; }
    private string BaseAddress { get => ClientOptions.StreamSpotAddress; }

    internal StreamApiSpotClient(GateWebSocketClient root)
    {
        RootClient = root;
        BaseClient = root.Base;
        ClientOptions = root.ClientOptions;
    }

    public async Task UnsubscribeAsync(int subscriptionId)
        => await BaseClient.UnsubscribeAsync(subscriptionId).ConfigureAwait(false);

    public async Task UnsubscribeAsync(WebSocketUpdateSubscription subscription)
        => await BaseClient.UnsubscribeAsync(subscription).ConfigureAwait(false);

    public async Task UnsubscribeAllAsync()
        => await BaseClient.UnsubscribeAllAsync().ConfigureAwait(false);

    public async Task<CallResult<GateStreamLatency>> PingAsync()
        => await BaseClient.PingAsync(BaseAddress, spotPingChannel).ConfigureAwait(false);

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTickersAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<SpotStreamTicker>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<SpotStreamTicker>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, spotTickersChannel, symbols, false, handler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTradesAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<SpotStreamTrade>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<SpotStreamTrade>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, spotTradesChannel, symbols, false, handler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToCandlesticksAsync(string symbol, GateSpotCandlestickInterval interval, Action<WebSocketDataEvent<SpotStreamCandlestick>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(MapConverter.GetString(interval));
        payload.Add(symbol);

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<SpotStreamCandlestick>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, spotCandlesticksChannel, payload, false, handler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBookTickersAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<SpotStreamBookTicker>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<SpotStreamBookTicker>>>(data => onMessage(data.As<SpotStreamBookTicker>(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, spotBookTickerChannel, symbols, false, handler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBookDifferencesAsync(string symbol, int interval, Action<WebSocketDataEvent<SpotStreamBookDifference>> onMessage, CancellationToken ct = default)
    {
        interval.ValidateIntValues(nameof(interval), 100, 1000);

        var payload = new List<string>();
        payload.Add(symbol);
        payload.Add($"{interval}ms");

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<SpotStreamBookDifference>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, spotOrderBookUpdateChannel, payload, false, handler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBookSnapshotsAsync(string symbol, int interval, int level, Action<WebSocketDataEvent<SpotStreamBookSnapshot>> onMessage, CancellationToken ct = default)
    {
        level.ValidateIntValues(nameof(level), 5, 10, 20, 50, 100);
        interval.ValidateIntValues(nameof(interval), 100, 1000);

        var payload = new List<string>();
        payload.Add(symbol);
        payload.Add(level.ToString());
        payload.Add($"{interval}ms");

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<SpotStreamBookSnapshot>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, spotOrderBookChannel, payload, false, handler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserOrdersAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<SpotStreamOrderUpdate>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<SpotStreamOrderUpdate>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, spotUserOrdersChannel, symbols, true, handler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserTradesAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<GateSpotTradeHistory>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateSpotTradeHistory>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, spotUserTradesChannel, symbols, true, handler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserSpotBalancesAsync(Action<WebSocketDataEvent<SpotStreamUserBalance>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<SpotStreamUserBalance>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, spotUserSpotBalancesChannel, Array.Empty<string>(), true, handler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserMarginBalancesAsync(Action<WebSocketDataEvent<SpotStreamMarginBalance>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<SpotStreamMarginBalance>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, spotUserMarginBalancesChannel, Array.Empty<string>(), true, handler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserFundingBalancesAsync(Action<WebSocketDataEvent<SpotStreamFundingBalance>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<SpotStreamFundingBalance>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, spotUserFundingBalancesChannel, Array.Empty<string>(), true, handler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserCrossMarginBalancesAsync(Action<WebSocketDataEvent<SpotStreamCrossMarginBalance>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<SpotStreamCrossMarginBalance>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, spotUserCrossMarginBalancesChannel, Array.Empty<string>(), true, handler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserCrossMarginLoansAsync(Action<WebSocketDataEvent<SpotStreamCrossMarginLoan>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<SpotStreamCrossMarginLoan>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, spotUserCrossMarginLoanChannel, Array.Empty<string>(), true, handler, ct).ConfigureAwait(false);
    }

}
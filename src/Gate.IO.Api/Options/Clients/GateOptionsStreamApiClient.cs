namespace Gate.IO.Api.Options;

/// <summary>
/// Represents the Stream API Options Client.
/// </summary>
public class GateOptionsStreamApiClient
{
    // Channels
    private const string optionsPingChannel = "options.ping";
    private const string optionsContractTickersChannel = "options.contract_tickers";
    private const string optionsUnderlyingTickersChannel = "options.ul_tickers";
    private const string optionsContractTradesChannel = "options.trades";
    private const string optionsUnderlyingTradesChannel = "options.ul_trades";
    private const string optionsUnderlyingPriceChannel = "options.ul_price";
    private const string optionsMarkPriceChannel = "options.mark_prices";
    private const string optionsSettlementsChannel = "options.settlements";
    private const string optionsContractsChannel = "options.contracts";
    private const string optionsContractCandlesticksChannel = "options.contract_candlesticks";
    private const string optionsUnderlyingCandlesticksChannel = "options.ul_candlesticks";
    private const string optionsOrderBookChannel = "options.order_book";
    private const string optionsOrderBookTickerChannel = "options.book_ticker";
    private const string optionsOrderBookUpdateChannel = "options.order_book_update";
    private const string optionsUserOrdersChannel = "options.orders";
    private const string optionsUserTradesChannel = "options.usertrades";
    private const string optionsUserLiquidatesChannel = "options.liquidates";
    private const string optionsUserSettlementsChannel = "options.user_settlements";
    private const string optionsUserPositionClosesChannel = "options.position_closes";
    private const string optionsUserBalancesChannel = "options.balances";
    private const string optionsUserPositionsChannel = "options.positions";

    // Internal
    internal GateWebSocketClient RootClient { get; }
    internal GateBaseStreamApiClient BaseClient { get; }
    internal GateWebSocketClientOptions ClientOptions { get; }
    private string BaseAddress { get => ClientOptions.StreamOptionsAddress; }

    internal GateOptionsStreamApiClient(GateWebSocketClient root)
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
        => await BaseClient.PingAsync(BaseAddress, optionsPingChannel).ConfigureAwait(false);

    /// <summary>
    /// Executes the Subscribe To Contract Tickers operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToContractTickersAsync(IEnumerable<string> contracts, Action<WebSocketDataEvent<GateOptionsContractTicker>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<GateOptionsContractTicker>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsContractTickersChannel, contracts, false, handler, ct).ConfigureAwait(false);
    }
    
    /// <summary>
    /// Executes the Subscribe To Underlying Tickers operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUnderlyingTickersAsync(IEnumerable<string> underlyings, Action<WebSocketDataEvent<GateOptionsUnderlyingTicker>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<GateOptionsUnderlyingTicker>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsUnderlyingTickersChannel, underlyings, false, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To Contract Trades operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToContractTradesAsync(IEnumerable<string> contracts, Action<WebSocketDataEvent<GateOptionsStreamContractTrade>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateOptionsStreamContractTrade>>>>(data => 
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsContractTradesChannel, contracts, false, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To Underlying Trades operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUnderlyingTradesAsync(IEnumerable<string> underlyings, Action<WebSocketDataEvent<GateOptionsStreamUnderlyingTrade>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateOptionsStreamUnderlyingTrade>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsUnderlyingTradesChannel, underlyings, false, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To Underlying Prices operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUnderlyingPricesAsync(IEnumerable<string> underlyings, Action<WebSocketDataEvent<GateOptionsStreamUnderlyingPrice>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<GateOptionsStreamUnderlyingPrice>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsUnderlyingPriceChannel, underlyings, false, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To Mark Prices operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToMarkPricesAsync(IEnumerable<string> contracts, Action<WebSocketDataEvent<GateOptionsStreamContractPrice>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<GateOptionsStreamContractPrice>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsMarkPriceChannel, contracts, false, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To Settlements operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToSettlementsAsync(IEnumerable<string> contracts, Action<WebSocketDataEvent<GateOptionsStreamSettlement>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<GateOptionsStreamSettlement>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsSettlementsChannel, contracts, false, handler, ct).ConfigureAwait(false);
    }
    
    /// <summary>
    /// Executes the Subscribe To Contracts operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToContractsAsync(IEnumerable<string> contracts, Action<WebSocketDataEvent<GateOptionsStreamContract>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<GateOptionsStreamContract>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsContractsChannel, contracts, false, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To Contract Candlesticks operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToContractCandlesticksAsync(string contract, GateOptionsCandlestickInterval interval, Action<WebSocketDataEvent<GateOptionsStreamCandlestick>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(MapConverter.GetString(interval));
        payload.Add(contract);

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateOptionsStreamCandlestick>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsContractCandlesticksChannel, payload, false, handler, ct).ConfigureAwait(false);
    }
    
    /// <summary>
    /// Executes the Subscribe To Underlying Candlesticks operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUnderlyingCandlesticksAsync(string underlying, GateOptionsCandlestickInterval interval, Action<WebSocketDataEvent<GateOptionsStreamCandlestick>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(MapConverter.GetString(interval));
        payload.Add(underlying);

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateOptionsStreamCandlestick>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsUnderlyingCandlesticksChannel, payload, false, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To Order Book Tickers operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBookTickersAsync(IEnumerable<string> contracts, Action<WebSocketDataEvent<GateOptionsStreamBookTicker>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<GateOptionsStreamBookTicker>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsOrderBookTickerChannel, contracts, false, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To Order Book Differences operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBookDifferencesAsync(string contract, int interval, Action<WebSocketDataEvent<GateOptionsStreamBookDifference>> onMessage, CancellationToken ct = default)
    {
        interval.ValidateIntValues(nameof(interval), 100, 1000);

        var payload = new List<string>();
        payload.Add(contract);
        payload.Add($"{interval}ms");

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<GateOptionsStreamBookDifference>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsOrderBookUpdateChannel, payload, false, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To Order Book Differences operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBookDifferencesAsync(string contract, int interval, int level, Action<WebSocketDataEvent<GateOptionsStreamBookDifference>> onMessage, CancellationToken ct = default)
    {
        interval.ValidateIntValues(nameof(interval), 100, 1000);
        level.ValidateIntValues(nameof(level), 5, 10, 20, 50);

        var payload = new List<string>();
        payload.Add(contract);
        payload.Add($"{interval}ms");
        payload.Add(level.ToString());

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<GateOptionsStreamBookDifference>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsOrderBookUpdateChannel, payload, false, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To Order Book Snapshots operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBookSnapshotsAsync(string contract, int level, Action<WebSocketDataEvent<GateOptionsStreamBookSnapshot>> onMessage, CancellationToken ct = default)
        => await SubscribeToOrderBookAsync(contract, level, onMessage, null, ct).ConfigureAwait(false);

    /// <summary>
    /// Executes the Subscribe To Order Book operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBookAsync(
        string contract,
        int level,
        Action<WebSocketDataEvent<GateOptionsStreamBookSnapshot>> onSnapshot,
        Action<WebSocketDataEvent<GateOptionsStreamOrderBookUpdate>> onUpdate,
        CancellationToken ct = default)
    {
        level.ValidateIntValues(nameof(level), 1, 5, 10, 20, 50);

        var payload = new List<string>();
        payload.Add(contract);
        payload.Add(level.ToString());
        payload.Add("0");

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<JToken>>>(data =>
        {
            if (data.Data.Data == null || data.Data.Data.Type == JTokenType.Null)
                return;

            if (data.Data.Data.Type == JTokenType.Object)
            {
                var snapshot = data.Data.Data.ToObject<GateOptionsStreamBookSnapshot>();
                if (snapshot != null)
                    onSnapshot?.Invoke(data.As(snapshot, data.Data.Channel));

                return;
            }

            if (data.Data.Data.Type != JTokenType.Array)
                return;

            foreach (var token in data.Data.Data)
            {
                var update = token.ToObject<GateOptionsStreamOrderBookUpdate>();
                if (update != null)
                    onUpdate?.Invoke(data.As(update, data.Data.Channel));
            }
        });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsOrderBookChannel, payload, false, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To User Orders operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserOrdersAsync(long userId, Action<WebSocketDataEvent<GateOptionsOrder>> onMessage, CancellationToken ct = default)
        => await SubscribeToUserOrdersAsync(userId, "!all", onMessage, ct).ConfigureAwait(false);
    /// <summary>
    /// Executes the Subscribe To User Orders operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserOrdersAsync(long userId, string contract, Action<WebSocketDataEvent<GateOptionsOrder>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(userId.ToString());
        payload.Add(contract);

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateOptionsOrder>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsUserOrdersChannel, payload, true, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To User Trades operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserTradesAsync(long userId, Action<WebSocketDataEvent<GateOptionsUserTrade>> onMessage, CancellationToken ct = default)
        => await SubscribeToUserTradesAsync(userId, "!all", onMessage, ct).ConfigureAwait(false);
    /// <summary>
    /// Executes the Subscribe To User Trades operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserTradesAsync(long userId, string contract, Action<WebSocketDataEvent<GateOptionsUserTrade>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(userId.ToString());
        payload.Add(contract);

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateOptionsUserTrade>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsUserTradesChannel, payload, true, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To User Liquidations operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserLiquidationsAsync(long userId, string contract, Action<WebSocketDataEvent<GateOptionsStreamUserLiquidation>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(userId.ToString());
        payload.Add(contract);

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateOptionsStreamUserLiquidation>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsUserLiquidatesChannel, payload, true, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To User Settlements operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserSettlementsAsync(long userId, string contract, Action<WebSocketDataEvent<GateOptionsStreamUserSettlement>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(userId.ToString());
        payload.Add(contract);

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateOptionsStreamUserSettlement>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsUserSettlementsChannel, payload, true, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To User Position Closes operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserPositionClosesAsync(long userId, string contract, Action<WebSocketDataEvent<GateOptionsStreamPositionClose>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(userId.ToString());
        payload.Add(contract);

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateOptionsStreamPositionClose>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsUserPositionClosesChannel, payload, true, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To User Balances operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserBalancesAsync(long userId, Action<WebSocketDataEvent<GateOptionsStreamBalance>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(userId.ToString());

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateOptionsStreamBalance>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsUserBalancesChannel, payload, true, handler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the Subscribe To User Positions operation.
    /// </summary>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserPositionsAsync(long userId, string contract, Action<WebSocketDataEvent<GateOptionsStreamPosition>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(userId.ToString());
        payload.Add(contract);

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateOptionsStreamPosition>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsUserPositionsChannel, payload, true, handler, ct).ConfigureAwait(false);
    }
}

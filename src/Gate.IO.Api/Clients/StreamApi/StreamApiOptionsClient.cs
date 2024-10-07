using Gate.IO.Api.Models.StreamApi.Options;
using Gate.IO.Api.Options;
using Gate.IO.Api.Options;

namespace Gate.IO.Api.Clients.StreamApi;

public class StreamApiOptionsClient
{
    // Channels
    private const string optionsPingChannel = "options.ping";
    private const string optionsContractTickersChannel = "options.contract_tickers";
    private const string optionsUnderlyingTickersChannel = "options.ul_tickers";
    private const string optionsContractTradesChannel = "options.trades";
    private const string optionsUnderlyingTradesChannel = "options.ul_trades";
    private const string optionsUnderlyingPriceChannel = "options.ul_price";
    private const string optionsMarkPriceChannel = "options.mark_price";
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
    internal StreamApiBaseClient BaseClient { get; }
    internal GateWebSocketClientOptions ClientOptions { get; }
    private string BaseAddress { get => ClientOptions.StreamOptionsAddress; }

    internal StreamApiOptionsClient(GateWebSocketClient root)
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
        => await BaseClient.PingAsync(BaseAddress, optionsPingChannel).ConfigureAwait(false);

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToContractTickersAsync(IEnumerable<string> contracts, Action<WebSocketDataEvent<GateOptionsContractTicker>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<GateOptionsContractTicker>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsContractTickersChannel, contracts, false, handler, ct).ConfigureAwait(false);
    }
    
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUnderlyingTickersAsync(IEnumerable<string> underlyings, Action<WebSocketDataEvent<GateOptionsUnderlyingTicker>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<GateOptionsUnderlyingTicker>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsUnderlyingTickersChannel, underlyings, false, handler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToContractTradesAsync(IEnumerable<string> contracts, Action<WebSocketDataEvent<OptionsStreamContractTrade>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<OptionsStreamContractTrade>>>>(data => 
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsContractTradesChannel, contracts, false, handler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUnderlyingTradesAsync(IEnumerable<string> underlyings, Action<WebSocketDataEvent<OptionsStreamUnderlyingTrade>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<OptionsStreamUnderlyingTrade>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsUnderlyingTradesChannel, underlyings, false, handler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUnderlyingPricesAsync(IEnumerable<string> underlyings, Action<WebSocketDataEvent<OptionsStreamUnderlyingPrice>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<OptionsStreamUnderlyingPrice>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsUnderlyingPriceChannel, underlyings, false, handler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToMarkPricesAsync(IEnumerable<string> contracts, Action<WebSocketDataEvent<OptionsStreamContractPrice>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<OptionsStreamContractPrice>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsMarkPriceChannel, contracts, false, handler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToSettlementsAsync(IEnumerable<string> contracts, Action<WebSocketDataEvent<OptionsStreamSettlement>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<OptionsStreamSettlement>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsSettlementsChannel, contracts, false, handler, ct).ConfigureAwait(false);
    }
    
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToContractsAsync(IEnumerable<string> contracts, Action<WebSocketDataEvent<OptionsStreamContract>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<OptionsStreamContract>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsContractsChannel, contracts, false, handler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToContractCandlesticksAsync(string contract, GateOptionsCandlestickInterval interval, Action<WebSocketDataEvent<OptionsStreamCandlestick>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(MapConverter.GetString(interval));
        payload.Add(contract);

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<OptionsStreamCandlestick>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsContractCandlesticksChannel, payload, false, handler, ct).ConfigureAwait(false);
    }
    
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUnderlyingCandlesticksAsync(string underlying, GateOptionsCandlestickInterval interval, Action<WebSocketDataEvent<OptionsStreamCandlestick>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(MapConverter.GetString(interval));
        payload.Add(underlying);

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<OptionsStreamCandlestick>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsUnderlyingCandlesticksChannel, payload, false, handler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBookTickersAsync(IEnumerable<string> contracts, Action<WebSocketDataEvent<OptionsStreamBookTicker>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<GateStreamResponse<OptionsStreamBookTicker>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsOrderBookTickerChannel, contracts, false, handler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBookDifferencesAsync(string contract, int interval, int level, Action<WebSocketDataEvent<OptionsStreamBookDifference>> onMessage, CancellationToken ct = default)
    {
        interval.ValidateIntValues(nameof(interval), 100, 1000);
        level.ValidateIntValues(nameof(level), 5, 10, 20, 50);

        var payload = new List<string>();
        payload.Add(contract);
        payload.Add($"{interval}ms");
        payload.Add(level.ToString());

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<OptionsStreamBookDifference>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsOrderBookUpdateChannel, payload, false, handler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBookSnapshotsAsync(string contract, int level, Action<WebSocketDataEvent<OptionsStreamBookSnapshot>> onMessage, CancellationToken ct = default)
    {
        level.ValidateIntValues(nameof(level), 1, 5, 10, 20, 50);

        var payload = new List<string>();
        payload.Add(contract);
        payload.Add(level.ToString());
        payload.Add("0");

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<OptionsStreamBookSnapshot>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsOrderBookChannel, payload, false, handler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserOrdersAsync(long userId, Action<WebSocketDataEvent<GateOptionsOrder>> onMessage, CancellationToken ct = default)
        => await SubscribeToUserOrdersAsync(userId, "!all", onMessage, ct).ConfigureAwait(false);
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserOrdersAsync(long userId, string contract, Action<WebSocketDataEvent<GateOptionsOrder>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(userId.ToString());
        payload.Add(contract);

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateOptionsOrder>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsUserOrdersChannel, payload, true, handler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserTradesAsync(long userId, Action<WebSocketDataEvent<GateOptionsUserTrade>> onMessage, CancellationToken ct = default)
        => await SubscribeToUserTradesAsync(userId, "!all", onMessage, ct).ConfigureAwait(false);
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserTradesAsync(long userId, string contract, Action<WebSocketDataEvent<GateOptionsUserTrade>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(userId.ToString());
        payload.Add(contract);

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<GateOptionsUserTrade>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsUserTradesChannel, payload, true, handler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserLiquidationsAsync(long userId, string contract, Action<WebSocketDataEvent<OptionsStreamUserLiquidation>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(userId.ToString());
        payload.Add(contract);

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<OptionsStreamUserLiquidation>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsUserLiquidatesChannel, payload, true, handler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserSettlementsAsync(long userId, string contract, Action<WebSocketDataEvent<OptionsStreamUserSettlement>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(userId.ToString());
        payload.Add(contract);

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<OptionsStreamUserSettlement>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsUserSettlementsChannel, payload, true, handler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserPositionClosesAsync(long userId, string contract, Action<WebSocketDataEvent<OptionsStreamPositionClose>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(userId.ToString());
        payload.Add(contract);

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<OptionsStreamPositionClose>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsUserPositionClosesChannel, payload, true, handler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserBalancesAsync(long userId, Action<WebSocketDataEvent<OptionsStreamBalance>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(userId.ToString());

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<OptionsStreamBalance>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsUserBalancesChannel, payload, true, handler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserPositionsAsync(long userId, string contract, Action<WebSocketDataEvent<OptionsStreamPosition>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(userId.ToString());
        payload.Add(contract);

        var handler = new Action<WebSocketDataEvent<GateStreamResponse<IEnumerable<OptionsStreamPosition>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(BaseAddress, optionsUserPositionsChannel, payload, true, handler, ct).ConfigureAwait(false);
    }
}
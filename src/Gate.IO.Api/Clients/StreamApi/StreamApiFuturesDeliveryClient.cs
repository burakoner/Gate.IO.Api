using Gate.IO.Api.Models.RestApi.Futures;
using Gate.IO.Api.Models.StreamApi;
using Gate.IO.Api.Models.StreamApi.Futures;

namespace Gate.IO.Api.Clients.StreamApi;

public class StreamApiFuturesDeliveryClient
{
    // Public
    public StreamApiFuturesDeliverySettleClient BTC { get; }
    public StreamApiFuturesDeliverySettleClient USDT { get; }

    // Internal
    internal GateStreamClient RootClient { get; }
    internal StreamApiBaseClient BaseClient { get; }
    internal GateStreamClientOptions ClientOptions { get; }

    // Channels
    private const string futuresPingChannel = "futures.ping";
    private const string futuresTickersChannel = "futures.tickers";
    private const string futuresTradesChannel = "futures.trades";
    private const string futuresOrderBookChannel = "futures.order_book";
    private const string futuresBookTickerChannel = "futures.book_ticker";
    private const string futuresOrderBookUpdateChannel = "futures.order_book_update";
    private const string futuresCandlesticksChannel = "futures.candlesticks";
    private const string futuresUserOrdersChannel = "futures.orders";
    private const string futuresUserTradesChannel = "futures.usertrades";
    private const string futuresUserLiquidatesChannel = "futures.liquidates";
    private const string futuresUserDeleveragesChannel = "futures.auto_deleverages";
    private const string futuresUserPositionClosesChannel = "futures.position_closes";
    private const string futuresUserBalancesChannel = "futures.balances";
    private const string futuresUserReduceRiskLimitsChannel = "futures.reduce_risk_limits";
    private const string futuresUserPositionsChannel = "futures.positions";
    private const string futuresUserAutoOrdersChannel = "futures.autoorders";

    internal StreamApiFuturesDeliveryClient(GateStreamClient root)
    {
        RootClient = root;
        BaseClient = root.Base;
        ClientOptions = root.ClientOptions;

        BTC = new StreamApiFuturesDeliverySettleClient(this, FuturesDeliverySettle.BTC);
        USDT = new StreamApiFuturesDeliverySettleClient(this, FuturesDeliverySettle.USDT);
    }
    internal async Task UnsubscribeAsync(int subscriptionId)
        => await BaseClient.UnsubscribeAsync(subscriptionId).ConfigureAwait(false);

    internal async Task UnsubscribeAsync(UpdateSubscription subscription)
        => await BaseClient.UnsubscribeAsync(subscription).ConfigureAwait(false);

    internal async Task UnsubscribeAllAsync()
        => await BaseClient.UnsubscribeAllAsync().ConfigureAwait(false);

    internal async Task<CallResult<GateStreamLatency>> PingAsync(FuturesDeliverySettle settle)
        => await BaseClient.PingAsync(ClientOptions.StreamDeliveryFuturesAddresses[settle], futuresPingChannel).ConfigureAwait(false);

    internal async Task<CallResult<UpdateSubscription>> SubscribeToTickersAsync(FuturesDeliverySettle settle, IEnumerable<string> contracts, Action<StreamDataEvent<FuturesStreamPerpetualTicker>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<StreamDataEvent<GateStreamResponse<IEnumerable<FuturesStreamPerpetualTicker>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(ClientOptions.StreamDeliveryFuturesAddresses[settle], futuresTickersChannel, contracts, false, handler, ct).ConfigureAwait(false);
    }

    /*
    internal async Task<CallResult<GateStreamResponse<GateStreamStatus>>> UnsubscribeFromTickersAsync(FuturesDeliverySettle settle, IEnumerable<string> contracts, Action<StreamDataEvent<GateStreamResponse<GateStreamStatus>>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<StreamDataEvent<GateStreamResponse<GateStreamStatus>>>(data => onMessage(data));
        return await BaseClient.BaseUnsubscribeAsync(ClientOptions.StreamDeliveryFuturesAddresses[settle], futuresTickersChannel, contracts, false, handler, ct).ConfigureAwait(false);
    }
    */

    internal async Task<CallResult<UpdateSubscription>> SubscribeToTradesAsync(FuturesDeliverySettle settle, IEnumerable<string> contracts, Action<StreamDataEvent<FuturesStreamTrade>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<StreamDataEvent<GateStreamResponse<IEnumerable<FuturesStreamTrade>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(ClientOptions.StreamDeliveryFuturesAddresses[settle], futuresTradesChannel, contracts, false, handler, ct).ConfigureAwait(false);
    }

    internal async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookTickersAsync(FuturesDeliverySettle settle, IEnumerable<string> contracts, Action<StreamDataEvent<FuturesStreamBookTicker>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<StreamDataEvent<GateStreamResponse<FuturesStreamBookTicker>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(ClientOptions.StreamDeliveryFuturesAddresses[settle], futuresBookTickerChannel, contracts, false, handler, ct).ConfigureAwait(false);
    }

    internal async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookDifferencesAsync(FuturesDeliverySettle settle, string contract, int frequency, int level, Action<StreamDataEvent<FuturesStreamBookDifference>> onMessage, CancellationToken ct = default)
    {
        level.ValidateIntValues(nameof(level), 5, 10, 20, 50, 100);
        frequency.ValidateIntValues(nameof(frequency), 100, 1000);

        var payload = new List<string>();
        payload.Add(contract);
        payload.Add($"{frequency}ms");
        payload.Add(level.ToString());

        var handler = new Action<StreamDataEvent<GateStreamResponse<FuturesStreamBookDifference>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(ClientOptions.StreamDeliveryFuturesAddresses[settle], futuresOrderBookUpdateChannel, payload, false, handler, ct).ConfigureAwait(false);
    }

    internal async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookSnapshotsAsync(FuturesDeliverySettle settle, string contract, /*int interval,*/ int limit, Action<StreamDataEvent<FuturesStreamBookSnapshot>> onMessage, CancellationToken ct = default)
    {
        limit.ValidateIntValues(nameof(limit), 1, 5, 10, 20, 50, 100);
        // interval.ValidateIntValues(nameof(interval), 100, 1000);

        var payload = new List<string>();
        payload.Add(contract);
        payload.Add(limit.ToString());
        payload.Add($"0");

        var handler = new Action<StreamDataEvent<GateStreamResponse<FuturesStreamBookSnapshot>>>(data => onMessage(data.As(data.Data.Data, data.Data.Channel)));
        return await BaseClient.BaseSubscribeAsync(ClientOptions.StreamDeliveryFuturesAddresses[settle], futuresOrderBookChannel, payload, false, handler, ct).ConfigureAwait(false);
    }

    internal async Task<CallResult<UpdateSubscription>> SubscribeToCandlesticksAsync(FuturesDeliverySettle settle, string prefix, string contract, FuturesCandlestickInterval interval, Action<StreamDataEvent<FuturesStreamCandlestick>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(JsonConvert.SerializeObject(interval, new FuturesCandlestickIntervalConverter(false)));
        payload.Add(prefix + contract);

        var handler = new Action<StreamDataEvent<GateStreamResponse<IEnumerable<FuturesStreamCandlestick>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(ClientOptions.StreamDeliveryFuturesAddresses[settle], futuresCandlesticksChannel, payload, false, handler, ct).ConfigureAwait(false);
    }

    internal async Task<CallResult<UpdateSubscription>> SubscribeToUserOrdersAsync(FuturesDeliverySettle settle, int userId, Action<StreamDataEvent<FuturesOrder>> onMessage, CancellationToken ct = default)
        => await SubscribeToUserOrdersAsync(settle, userId, "!all", onMessage, ct).ConfigureAwait(false);
    internal async Task<CallResult<UpdateSubscription>> SubscribeToUserOrdersAsync(FuturesDeliverySettle settle, int userId, string contract, Action<StreamDataEvent<FuturesOrder>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(userId.ToString());
        payload.Add(contract);

        var handler = new Action<StreamDataEvent<GateStreamResponse<IEnumerable<FuturesOrder>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(ClientOptions.StreamDeliveryFuturesAddresses[settle], futuresUserOrdersChannel, payload, true, handler, ct).ConfigureAwait(false);
    }

    internal async Task<CallResult<UpdateSubscription>> SubscribeToUserTradesAsync(FuturesDeliverySettle settle, int userId, Action<StreamDataEvent<FuturesUserTrade>> onMessage, CancellationToken ct = default)
        => await SubscribeToUserTradesAsync(settle, userId, "!all", onMessage, ct).ConfigureAwait(false);
    internal async Task<CallResult<UpdateSubscription>> SubscribeToUserTradesAsync(FuturesDeliverySettle settle, int userId, string contract, Action<StreamDataEvent<FuturesUserTrade>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(userId.ToString());
        payload.Add(contract);

        var handler = new Action<StreamDataEvent<GateStreamResponse<IEnumerable<FuturesUserTrade>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(ClientOptions.StreamDeliveryFuturesAddresses[settle], futuresUserTradesChannel, payload, true, handler, ct).ConfigureAwait(false);
    }

    internal async Task<CallResult<UpdateSubscription>> SubscribeToUserLiquidatesAsync(FuturesDeliverySettle settle, int userId, Action<StreamDataEvent<FuturesUserLiquidate>> onMessage, CancellationToken ct = default)
        => await SubscribeToUserLiquidatesAsync(settle, userId, "!all", onMessage, ct).ConfigureAwait(false);
    internal async Task<CallResult<UpdateSubscription>> SubscribeToUserLiquidatesAsync(FuturesDeliverySettle settle, int userId, string contract, Action<StreamDataEvent<FuturesUserLiquidate>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(userId.ToString());
        payload.Add(contract);

        var handler = new Action<StreamDataEvent<GateStreamResponse<IEnumerable<FuturesUserLiquidate>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(ClientOptions.StreamDeliveryFuturesAddresses[settle], futuresUserLiquidatesChannel, payload, true, handler, ct).ConfigureAwait(false);
    }

    internal async Task<CallResult<UpdateSubscription>> SubscribeToUserDeleveragesAsync(FuturesDeliverySettle settle, int userId, Action<StreamDataEvent<FuturesUserDeleverage>> onMessage, CancellationToken ct = default)
        => await SubscribeToUserDeleveragesAsync(settle, userId, "!all", onMessage, ct).ConfigureAwait(false);
    internal async Task<CallResult<UpdateSubscription>> SubscribeToUserDeleveragesAsync(FuturesDeliverySettle settle, int userId, string contract, Action<StreamDataEvent<FuturesUserDeleverage>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(userId.ToString());
        payload.Add(contract);

        var handler = new Action<StreamDataEvent<GateStreamResponse<IEnumerable<FuturesUserDeleverage>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(ClientOptions.StreamDeliveryFuturesAddresses[settle], futuresUserDeleveragesChannel, payload, true, handler, ct).ConfigureAwait(false);
    }

    internal async Task<CallResult<UpdateSubscription>> SubscribeToUserPositionClosesAsync(FuturesDeliverySettle settle, int userId, Action<StreamDataEvent<FuturesPositionClose>> onMessage, CancellationToken ct = default)
        => await SubscribeToUserPositionClosesAsync(settle, userId, "!all", onMessage, ct).ConfigureAwait(false);
    internal async Task<CallResult<UpdateSubscription>> SubscribeToUserPositionClosesAsync(FuturesDeliverySettle settle, int userId, string contract, Action<StreamDataEvent<FuturesPositionClose>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(userId.ToString());
        payload.Add(contract);

        var handler = new Action<StreamDataEvent<GateStreamResponse<IEnumerable<FuturesPositionClose>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(ClientOptions.StreamDeliveryFuturesAddresses[settle], futuresUserPositionClosesChannel, payload, true, handler, ct).ConfigureAwait(false);
    }

    internal async Task<CallResult<UpdateSubscription>> SubscribeToUserBalancesAsync(FuturesDeliverySettle settle, int userId, Action<StreamDataEvent<FuturesStreamBalance>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(userId.ToString());

        var handler = new Action<StreamDataEvent<GateStreamResponse<IEnumerable<FuturesStreamBalance>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(ClientOptions.StreamDeliveryFuturesAddresses[settle], futuresUserBalancesChannel, payload, true, handler, ct).ConfigureAwait(false);
    }

    internal async Task<CallResult<UpdateSubscription>> SubscribeToUserReduceRiskLimitsAsync(FuturesDeliverySettle settle, int userId, Action<StreamDataEvent<FuturesStreamReduceRiskLimit>> onMessage, CancellationToken ct = default)
    => await SubscribeToUserReduceRiskLimitsAsync(settle, userId, "!all", onMessage, ct).ConfigureAwait(false);
    internal async Task<CallResult<UpdateSubscription>> SubscribeToUserReduceRiskLimitsAsync(FuturesDeliverySettle settle, int userId, string contract, Action<StreamDataEvent<FuturesStreamReduceRiskLimit>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(userId.ToString());
        payload.Add(contract);

        var handler = new Action<StreamDataEvent<GateStreamResponse<IEnumerable<FuturesStreamReduceRiskLimit>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(ClientOptions.StreamDeliveryFuturesAddresses[settle], futuresUserReduceRiskLimitsChannel, payload, true, handler, ct).ConfigureAwait(false);
    }

    internal async Task<CallResult<UpdateSubscription>> SubscribeToUserPositionsAsync(FuturesDeliverySettle settle, int userId, Action<StreamDataEvent<FuturesPosition>> onMessage, CancellationToken ct = default)
    => await SubscribeToUserPositionsAsync(settle, userId, "!all", onMessage, ct).ConfigureAwait(false);
    internal async Task<CallResult<UpdateSubscription>> SubscribeToUserPositionsAsync(FuturesDeliverySettle settle, int userId, string contract, Action<StreamDataEvent<FuturesPosition>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(userId.ToString());
        payload.Add(contract);

        var handler = new Action<StreamDataEvent<GateStreamResponse<IEnumerable<FuturesPosition>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(ClientOptions.StreamDeliveryFuturesAddresses[settle], futuresUserPositionsChannel, payload, true, handler, ct).ConfigureAwait(false);
    }

    internal async Task<CallResult<UpdateSubscription>> SubscribeToUserAutoOrdersAsync(FuturesDeliverySettle settle, int userId, Action<StreamDataEvent<FuturesStreamAutoOrder>> onMessage, CancellationToken ct = default)
    => await SubscribeToUserAutoOrdersAsync(settle, userId, "!all", onMessage, ct).ConfigureAwait(false);
    internal async Task<CallResult<UpdateSubscription>> SubscribeToUserAutoOrdersAsync(FuturesDeliverySettle settle, int userId, string contract, Action<StreamDataEvent<FuturesStreamAutoOrder>> onMessage, CancellationToken ct = default)
    {
        var payload = new List<string>();
        payload.Add(userId.ToString());
        payload.Add(contract);

        var handler = new Action<StreamDataEvent<GateStreamResponse<IEnumerable<FuturesStreamAutoOrder>>>>(data =>
        { foreach (var row in data.Data.Data) onMessage(data.As(row, data.Data.Channel)); });
        return await BaseClient.BaseSubscribeAsync(ClientOptions.StreamDeliveryFuturesAddresses[settle], futuresUserAutoOrdersChannel, payload, true, handler, ct).ConfigureAwait(false);
    }

}
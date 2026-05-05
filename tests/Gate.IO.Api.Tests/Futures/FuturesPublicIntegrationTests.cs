using Gate.IO.Api.Futures;
using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.Futures;

[Trait("Category", "PublicIntegration")]
public class FuturesPublicIntegrationTests
{
    [Fact]
    public async Task Public_futures_endpoints_return_deserializable_data_when_live_tests_are_enabled()
    {
        if (!LiveTestSettings.Enabled)
            return;

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(45));
        var client = new GateRestApiClient();

        var contracts = await client.Futures.USDT.GetContractsAsync(limit: 1, ct: cts.Token);
        var contract = await client.Futures.USDT.GetContractAsync("BTC_USDT", cts.Token);
        var tickers = await client.Futures.USDT.GetTickersAsync("BTC_USDT", cts.Token);
        var orderBook = await client.Futures.USDT.GetOrderBookAsync("BTC_USDT", limit: 5, ct: cts.Token);
        var trades = await client.Futures.USDT.GetTradesAsync("BTC_USDT", limit: 1, ct: cts.Token);
        var candlesticks = await client.Futures.USDT.GetMarkPriceCandlesticksAsync("BTC_USDT", GateFuturesCandlestickInterval.OneMinute, limit: 1, ct: cts.Token);
        var fundingRates = await client.Futures.USDT.GetFundingRateHistoryAsync("BTC_USDT", limit: 1, ct: cts.Token);
        var batchFundingRates = await client.Futures.USDT.GetBatchFundingRateHistoryAsync(["BTC_USDT"], cts.Token);
        var insurance = await client.Futures.USDT.GetInsuranceHistoryAsync(limit: 1, ct: cts.Token);
        var stats = await client.Futures.USDT.GetStatsAsync("BTC_USDT", GateFuturesStatsInterval.OneHour, limit: 1, ct: cts.Token);
        var constituents = await client.Futures.USDT.GetIndexConstituentsAsync("BTC_USDT", cts.Token);
        var liquidations = await client.Futures.USDT.GetLiquidationsAsync("BTC_USDT", limit: 1, ct: cts.Token);
        var riskLimitTiers = await client.Futures.USDT.GetRiskLimitTiersAsync("BTC_USDT", limit: 1, ct: cts.Token);

        Assert.True(contracts.Success, contracts.Error?.ToString());
        Assert.NotEmpty(contracts.Data!);
        Assert.True(contract.Success, contract.Error?.ToString());
        Assert.Equal("BTC_USDT", contract.Data!.Contract);
        Assert.True(tickers.Success, tickers.Error?.ToString());
        Assert.Single(tickers.Data!);
        Assert.True(orderBook.Success, orderBook.Error?.ToString());
        Assert.NotEmpty(orderBook.Data!.Asks);
        Assert.True(trades.Success, trades.Error?.ToString());
        Assert.NotEmpty(trades.Data!);
        Assert.True(candlesticks.Success, candlesticks.Error?.ToString());
        Assert.NotEmpty(candlesticks.Data!);
        Assert.True(fundingRates.Success, fundingRates.Error?.ToString());
        Assert.NotEmpty(fundingRates.Data!);
        Assert.True(batchFundingRates.Success, batchFundingRates.Error?.ToString());
        Assert.NotEmpty(batchFundingRates.Data!);
        Assert.True(insurance.Success, insurance.Error?.ToString());
        Assert.NotEmpty(insurance.Data!);
        Assert.True(stats.Success, stats.Error?.ToString());
        Assert.NotEmpty(stats.Data!);
        Assert.True(constituents.Success, constituents.Error?.ToString());
        Assert.Equal("BTC_USDT", constituents.Data!.Index);
        Assert.True(liquidations.Success, liquidations.Error?.ToString());
        Assert.NotNull(liquidations.Data);
        Assert.True(riskLimitTiers.Success, riskLimitTiers.Error?.ToString());
        Assert.NotEmpty(riskLimitTiers.Data!);
    }

    [Fact]
    public async Task Public_futures_http_json_is_available_when_live_tests_are_enabled()
    {
        if (!LiveTestSettings.Enabled)
            return;

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(20));
        var json = await PublicHttpCapture.GetStringAsync("https://api.gateio.ws/api/v4/futures/usdt/contracts?limit=1", cts.Token);
        var token = JToken.Parse(json);

        Assert.Equal(JTokenType.Array, token.Type);
        Assert.NotEmpty(token);
    }
}

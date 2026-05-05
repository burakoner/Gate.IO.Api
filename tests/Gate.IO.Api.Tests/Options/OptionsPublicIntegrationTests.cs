using Gate.IO.Api.Options;
using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.Options;

[Trait("Category", "PublicIntegration")]
public class OptionsPublicIntegrationTests
{
    [Fact]
    public async Task Public_options_endpoints_return_deserializable_data_when_live_tests_are_enabled()
    {
        if (!LiveTestSettings.Enabled)
            return;

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(60));
        var client = new GateRestApiClient();

        var underlyings = await client.Options.GetUnderlyingsAsync(cts.Token);
        Assert.True(underlyings.Success, underlyings.Error?.ToString());
        Assert.NotEmpty(underlyings.Data!);

        var underlying = underlyings.Data!.FirstOrDefault(x => x.Underlying == "BTC_USDT")?.Underlying
            ?? underlyings.Data![0].Underlying;
        var expirations = await client.Options.GetExpirationsAsync(underlying, cts.Token);
        var contracts = await client.Options.GetContractsAsync(underlying, expirations.Data?.FirstOrDefault(), cts.Token);
        Assert.True(expirations.Success, expirations.Error?.ToString());
        Assert.True(contracts.Success, contracts.Error?.ToString());
        Assert.NotEmpty(contracts.Data!);

        var contract = contracts.Data![0].Name;
        var contractDetail = await client.Options.GetContractAsync(contract, cts.Token);
        var orderBook = await client.Options.GetOrderBookAsync(contract, limit: 5, ct: cts.Token);
        var tickers = await client.Options.GetContractTickersAsync(underlying, cts.Token);
        var underlyingTicker = await client.Options.GetUnderlyingTickersAsync(underlying, cts.Token);
        var candlesticks = await client.Options.GetCandlesticksAsync(contract, GateOptionsCandlestickInterval.OneMinute, limit: 1, ct: cts.Token);
        var underlyingCandlesticks = await client.Options.GetUnderlyingCandlesticksAsync(underlying, GateOptionsCandlestickInterval.OneMinute, limit: 1, ct: cts.Token);
        var trades = await client.Options.GetTradesAsync(new GateOptionsTradeQueryRequest { Limit = 1 }, cts.Token);

        Assert.True(contractDetail.Success, contractDetail.Error?.ToString());
        Assert.Equal(contract, contractDetail.Data!.Name);
        Assert.True(orderBook.Success, orderBook.Error?.ToString());
        Assert.NotNull(orderBook.Data);
        Assert.True(tickers.Success, tickers.Error?.ToString());
        Assert.NotEmpty(tickers.Data!);
        Assert.True(underlyingTicker.Success, underlyingTicker.Error?.ToString());
        Assert.NotNull(underlyingTicker.Data);
        Assert.True(candlesticks.Success, candlesticks.Error?.ToString());
        Assert.NotNull(candlesticks.Data);
        Assert.True(underlyingCandlesticks.Success, underlyingCandlesticks.Error?.ToString());
        Assert.NotNull(underlyingCandlesticks.Data);
        Assert.True(trades.Success, trades.Error?.ToString());
        Assert.NotNull(trades.Data);
    }

    [Fact]
    public async Task Public_options_http_json_is_available_when_live_tests_are_enabled()
    {
        if (!LiveTestSettings.Enabled)
            return;

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(20));
        var json = await PublicHttpCapture.GetStringAsync("https://api.gateio.ws/api/v4/options/underlyings", cts.Token);
        var token = JToken.Parse(json);

        Assert.Equal(JTokenType.Array, token.Type);
        Assert.NotEmpty(token);
    }
}

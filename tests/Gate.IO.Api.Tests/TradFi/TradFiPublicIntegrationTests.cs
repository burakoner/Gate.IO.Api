using Gate.IO.Api.Tests.Infrastructure;
using Gate.IO.Api.TradFi;

namespace Gate.IO.Api.Tests.TradFi;

[Trait("Category", "PublicIntegration")]
public class TradFiPublicIntegrationTests
{
    [Fact]
    public async Task Public_tradfi_endpoints_return_deserializable_data_when_live_tests_are_enabled()
    {
        if (!LiveTestSettings.Enabled)
            return;

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(45));
        var client = new GateRestApiClient();

        var categories = await client.TradFi.GetSymbolCategoriesAsync(cts.Token);
        var symbols = await client.TradFi.GetSymbolsAsync(cts.Token);
        var symbol = symbols.Data?.FirstOrDefault(x => x.Symbol == "EURUSD")?.Symbol
            ?? symbols.Data?.FirstOrDefault(x => x.Status == GateTradFiTradingStatus.Open)?.Symbol
            ?? symbols.Data?.FirstOrDefault()?.Symbol
            ?? "EURUSD";
        var ticker = await client.TradFi.GetTickerAsync(symbol, cts.Token);
        var candlesticks = await client.TradFi.GetCandlesticksAsync(symbol, GateTradFiKlineInterval.OneMinute, limit: 1, ct: cts.Token);

        Assert.True(categories.Success, categories.Error?.ToString());
        Assert.NotEmpty(categories.Data!);
        Assert.True(symbols.Success, symbols.Error?.ToString());
        Assert.NotEmpty(symbols.Data!);
        Assert.True(ticker.Success, ticker.Error?.ToString());
        Assert.NotNull(ticker.Data);
        Assert.True(candlesticks.Success, candlesticks.Error?.ToString());
        Assert.NotNull(candlesticks.Data);
    }

    [Fact]
    public async Task Public_tradfi_http_json_is_available_when_live_tests_are_enabled()
    {
        if (!LiveTestSettings.Enabled)
            return;

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(20));
        var json = await PublicHttpCapture.GetStringAsync("https://api.gateio.ws/api/v4/tradfi/symbols/categories", cts.Token);
        var token = JToken.Parse(json);

        Assert.Equal(JTokenType.Object, token.Type);
        Assert.NotNull(token["data"]?["list"]);
    }
}

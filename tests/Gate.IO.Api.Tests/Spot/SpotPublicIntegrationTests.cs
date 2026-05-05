using Gate.IO.Api.Spot;
using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.Spot;

[Trait("Category", "PublicIntegration")]
public class SpotPublicIntegrationTests
{
    [Fact]
    public async Task Public_spot_endpoints_return_deserializable_data_when_live_tests_are_enabled()
    {
        if (!LiveTestSettings.Enabled)
            return;

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        var client = new GateRestApiClient();

        var currency = await client.Spot.GetCurrencyAsync("GT", cts.Token);
        var market = await client.Spot.GetMarketAsync("BTC_USDT", cts.Token);
        var tickers = await client.Spot.GetTickersAsync("BTC_USDT", ct: cts.Token);
        var orderBook = await client.Spot.GetOrderBookAsync("BTC_USDT", limit: 5, ct: cts.Token);
        var trades = await client.Spot.GetTradesAsync("BTC_USDT", limit: 1, ct: cts.Token);
        var candles = await client.Spot.GetCandlesticksAsync("BTC_USDT", GateSpotCandlestickInterval.OneMinute, limit: 1, ct: cts.Token);
        var serverTime = await client.Spot.GetServerTimeAsync(cts.Token);
        var insurance = await client.Spot.GetInsuranceHistoryAsync("margin", "BTC", from: 1727054547, to: 1727054547, limit: 1, ct: cts.Token);

        Assert.True(currency.Success, currency.Error?.ToString());
        Assert.True(market.Success, market.Error?.ToString());
        Assert.True(tickers.Success, tickers.Error?.ToString());
        Assert.True(orderBook.Success, orderBook.Error?.ToString());
        Assert.True(trades.Success, trades.Error?.ToString());
        Assert.True(candles.Success, candles.Error?.ToString());
        Assert.True(serverTime.Success, serverTime.Error?.ToString());
        Assert.True(insurance.Success, insurance.Error?.ToString());
        Assert.Equal("GT", currency.Data!.Symbol);
        Assert.Equal("BTC_USDT", market.Data!.Symbol);
        Assert.NotEmpty(tickers.Data!);
        Assert.NotEmpty(orderBook.Data!.Asks);
        Assert.NotEmpty(trades.Data!);
        Assert.NotEmpty(candles.Data!);
        Assert.NotEqual(default, serverTime.Data);
        Assert.NotNull(insurance.Data);
    }

    [Fact]
    public async Task Public_spot_http_json_is_available_when_live_tests_are_enabled()
    {
        if (!LiveTestSettings.Enabled)
            return;

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(20));
        var json = await PublicHttpCapture.GetStringAsync("https://api.gateio.ws/api/v4/spot/tickers?currency_pair=BTC_USDT", cts.Token);
        var token = JToken.Parse(json);

        Assert.Equal(JTokenType.Array, token.Type);
        Assert.Equal("BTC_USDT", token[0]!["currency_pair"]!.ToString());
    }
}

using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.Margin;

[Trait("Category", "PublicIntegration")]
public class MarginPublicIntegrationTests
{
    [Fact]
    public async Task Public_margin_endpoints_return_deserializable_data_when_live_tests_are_enabled()
    {
        if (!LiveTestSettings.Enabled)
            return;

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        var client = new GateRestApiClient();

        var markets = await client.IsolatedMargin.GetMarketsAsync(cts.Token);
        var market = await client.IsolatedMargin.GetMarketsAsync("BTC_USDT", cts.Token);
        var tiers = await client.IsolatedMargin.GetCurrentLendingTiersAsync("BTC_USDT", cts.Token);

        Assert.True(markets.Success, markets.Error?.ToString());
        Assert.True(market.Success, market.Error?.ToString());
        Assert.True(tiers.Success, tiers.Error?.ToString());
        Assert.NotEmpty(markets.Data!);
        Assert.Equal("BTC_USDT", market.Data!.Symbol);
        Assert.NotEmpty(tiers.Data!);
    }

    [Fact]
    public async Task Public_margin_http_json_is_available_when_live_tests_are_enabled()
    {
        if (!LiveTestSettings.Enabled)
            return;

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(20));
        var json = await PublicHttpCapture.GetStringAsync("https://api.gateio.ws/api/v4/margin/uni/currency_pairs/BTC_USDT", cts.Token);
        var token = JToken.Parse(json);

        Assert.Equal(JTokenType.Object, token.Type);
        Assert.Equal("BTC_USDT", token["currency_pair"]!.ToString());
    }
}

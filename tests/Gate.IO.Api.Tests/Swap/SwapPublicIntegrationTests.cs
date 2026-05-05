using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.Swap;

[Trait("Category", "PublicIntegration")]
public class SwapPublicIntegrationTests
{
    [Fact]
    public async Task Public_flash_swap_endpoints_return_deserializable_data_when_live_tests_are_enabled()
    {
        if (!LiveTestSettings.Enabled)
            return;

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        var client = new GateRestApiClient();

        var markets = await client.FlashSwap.GetMarketsAsync("BTC", limit: 1, ct: cts.Token);

        Assert.True(markets.Success, markets.Error?.ToString());
        Assert.NotEmpty(markets.Data!);
        Assert.Equal("BTC", markets.Data![0].SellCurrency);
    }

    [Fact]
    public async Task Public_flash_swap_http_json_is_available_when_live_tests_are_enabled()
    {
        if (!LiveTestSettings.Enabled)
            return;

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(20));
        var json = await PublicHttpCapture.GetStringAsync("https://api.gateio.ws/api/v4/flash_swap/currency_pairs?currency=BTC&limit=1", cts.Token);
        var token = JToken.Parse(json);

        Assert.Equal(JTokenType.Array, token.Type);
        Assert.Equal("BTC", token[0]!["sell_currency"]!.ToString());
    }
}

using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.Alpha;

[Trait("Category", "PublicIntegration")]
public class AlphaPublicIntegrationTests
{
    [Fact]
    public async Task Public_alpha_endpoints_return_deserializable_data_when_live_tests_are_enabled()
    {
        if (!LiveTestSettings.Enabled)
            return;

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(20));
        var client = new GateRestApiClient();

        var currencies = await client.Alpha.GetCurrenciesAsync(limit: 1, ct: cts.Token);
        var tickers = await client.Alpha.GetTickersAsync(limit: 1, ct: cts.Token);
        var tokens = await client.Alpha.GetTokensAsync(page: 1, ct: cts.Token);

        Assert.True(currencies.Success, currencies.Error?.ToString());
        Assert.True(tickers.Success, tickers.Error?.ToString());
        Assert.True(tokens.Success, tokens.Error?.ToString());
        Assert.NotNull(currencies.Data);
        Assert.NotNull(tickers.Data);
        Assert.NotNull(tokens.Data);
    }

    [Fact]
    public async Task Public_alpha_http_json_is_available_when_live_tests_are_enabled()
    {
        if (!LiveTestSettings.Enabled)
            return;

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(20));
        var json = await PublicHttpCapture.GetStringAsync("https://api.gateio.ws/api/v4/alpha/currencies?limit=1", cts.Token);
        var token = JToken.Parse(json);

        Assert.Equal(JTokenType.Array, token.Type);
    }
}

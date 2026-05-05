using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.CrossEx;

[Trait("Category", "PublicIntegration")]
public class CrossExPublicIntegrationTests
{
    [Fact]
    public async Task Public_crossex_endpoints_return_deserializable_data_when_live_tests_are_enabled()
    {
        if (!LiveTestSettings.Enabled)
            return;

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        var client = new GateRestApiClient();

        var symbols = await client.CrossEx.GetSymbolsAsync(["BINANCE_FUTURE_ADA_USDT"], cts.Token);
        var riskLimits = await client.CrossEx.GetRiskLimitsAsync(["BINANCE_FUTURE_ADA_USDT"], cts.Token);
        var transferCoins = await client.CrossEx.GetTransferCoinsAsync("USDT", cts.Token);

        Assert.True(symbols.Success, symbols.Error?.ToString());
        Assert.NotEmpty(symbols.Data!);
        Assert.True(riskLimits.Success, riskLimits.Error?.ToString());
        Assert.NotEmpty(riskLimits.Data!);
        Assert.True(transferCoins.Success, transferCoins.Error?.ToString());
        Assert.NotEmpty(transferCoins.Data!);
    }

    [Fact]
    public async Task Public_crossex_http_json_is_available_when_live_tests_are_enabled()
    {
        if (!LiveTestSettings.Enabled)
            return;

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(20));
        var json = await PublicHttpCapture.GetStringAsync("https://api.gateio.ws/api/v4/crossex/transfers/coin?coin=USDT", cts.Token);
        var token = JToken.Parse(json);

        Assert.Equal(JTokenType.Array, token.Type);
        Assert.Equal("USDT", token[0]!["coin"]!.ToString());
    }
}

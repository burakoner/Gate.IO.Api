using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.EarnUni;

[Trait("Category", "PublicIntegration")]
public class EarnUniPublicIntegrationTests
{
    [Fact]
    public async Task Public_earn_uni_endpoints_return_deserializable_data_when_live_tests_are_enabled()
    {
        if (!LiveTestSettings.Enabled)
            return;

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        var client = new GateRestApiClient();

        var currencies = await client.EarnUni.GetCurrenciesAsync(cts.Token);
        var btc = await client.EarnUni.GetCurrencyAsync("BTC", cts.Token);

        Assert.True(currencies.Success, currencies.Error?.ToString());
        Assert.True(btc.Success, btc.Error?.ToString());
        Assert.NotEmpty(currencies.Data!);
        Assert.Contains(currencies.Data!, x => x.Currency == "BTC");
        Assert.Equal("BTC", btc.Data!.Currency);
    }

    [Fact]
    public async Task Public_earn_uni_http_json_is_available_when_live_tests_are_enabled()
    {
        if (!LiveTestSettings.Enabled)
            return;

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(20));
        var json = await PublicHttpCapture.GetStringAsync("https://api.gateio.ws/api/v4/earn/uni/currencies/BTC", cts.Token);
        var token = JToken.Parse(json);

        Assert.Equal(JTokenType.Object, token.Type);
        Assert.Equal("BTC", token["currency"]!.ToString());
    }
}

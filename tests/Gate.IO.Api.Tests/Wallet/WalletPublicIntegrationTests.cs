using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.Wallet;

[Trait("Category", "PublicIntegration")]
public class WalletPublicIntegrationTests
{
    [Fact]
    public async Task Public_wallet_currency_chains_endpoint_returns_deserializable_data_when_live_tests_are_enabled()
    {
        if (!LiveTestSettings.Enabled)
            return;

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(20));
        var client = new GateRestApiClient();

        var chains = await client.Wallet.GetCurrencyChainsAsync("GT", cts.Token);

        Assert.True(chains.Success, chains.Error?.ToString());
        Assert.NotNull(chains.Data);
        Assert.Contains(chains.Data, x => x.Chain == "GT");
    }

    [Fact]
    public async Task Public_wallet_currency_chains_http_json_is_available_when_live_tests_are_enabled()
    {
        if (!LiveTestSettings.Enabled)
            return;

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(20));
        var json = await PublicHttpCapture.GetStringAsync("https://api.gateio.ws/api/v4/wallet/currency_chains?currency=GT", cts.Token);
        var token = JToken.Parse(json);

        Assert.Equal(JTokenType.Array, token.Type);
    }
}

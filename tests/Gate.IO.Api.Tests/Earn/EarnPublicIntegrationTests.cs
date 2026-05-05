using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.Earn;

[Trait("Category", "PublicIntegration")]
public class EarnPublicIntegrationTests
{
    [Fact]
    public async Task Public_earn_endpoints_return_deserializable_data_when_live_tests_are_enabled()
    {
        if (!LiveTestSettings.Enabled)
            return;

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        var client = new GateRestApiClient();

        var dualPlans = await client.Earn.GetDualInvestmentPlansAsync(coin: "BTC", page: 1, pageSize: 1, ct: cts.Token);
        var products = await client.Earn.GetFixedTermProductsAsync(asset: "USDT", page: 1, limit: 1, ct: cts.Token);
        var productsByAsset = await client.Earn.GetFixedTermProductsByAssetAsync("USDT", ct: cts.Token);

        Assert.True(dualPlans.Success, dualPlans.Error?.ToString());
        Assert.True(products.Success, products.Error?.ToString());
        Assert.True(productsByAsset.Success, productsByAsset.Error?.ToString());
        Assert.NotEmpty(dualPlans.Data!);
        Assert.Contains(dualPlans.Data!, x => x.InvestCurrency == "BTC" || x.ExerciseCurrency == "BTC");
        Assert.NotEmpty(products.Data!.List);
        Assert.Equal("USDT", products.Data.List[0].Asset);
        Assert.NotEmpty(productsByAsset.Data!.List);
        Assert.All(productsByAsset.Data.List, x => Assert.Equal("USDT", x.Asset));
    }

    [Fact]
    public async Task Public_earn_http_json_is_available_when_live_tests_are_enabled()
    {
        if (!LiveTestSettings.Enabled)
            return;

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(20));
        var json = await PublicHttpCapture.GetStringAsync("https://api.gateio.ws/api/v4/earn/fixed-term/product?page=1&limit=1", cts.Token);
        var token = JToken.Parse(json);

        Assert.Equal(JTokenType.Object, token.Type);
        Assert.Equal(0, token["code"]!.Value<int>());
        Assert.Equal("USDT", token["data"]!["list"]![0]!["asset"]!.ToString());
    }
}

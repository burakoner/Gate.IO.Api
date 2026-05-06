using Gate.IO.Api.Unified;
using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.Unified;

[Trait("Category", "PublicIntegration")]
public class UnifiedPublicIntegrationTests
{
    [Fact]
    public async Task Public_unified_endpoints_return_deserializable_data_when_live_tests_are_enabled()
    {
        if (!LiveTestSettings.Enabled)
            return;

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        var client = new GateRestApiClient();

        var currencies = await client.Unified.GetCurrenciesAsync("BTC", cts.Token);
        var historicalRates = await client.Unified.GetHistoricalLendingRatesAsync("USDT", limit: 1, ct: cts.Token);
        var discountTiers = await client.Unified.GetCurrencyDiscountTiersAsync(cts.Token);
        var loanMarginTiers = await client.Unified.GetLoanMarginTiersAsync(cts.Token);
        var portfolio = await client.Unified.CalculatePortfolioAsync(new GateUnifiedPortfolioCalculatorRequest { SpotHedge = true }, cts.Token);

        Assert.True(currencies.Success, currencies.Error?.ToString());
        Assert.True(historicalRates.Success, historicalRates.Error?.ToString());
        Assert.True(discountTiers.Success, discountTiers.Error?.ToString());
        Assert.True(loanMarginTiers.Success, loanMarginTiers.Error?.ToString());
        Assert.True(portfolio.Success, portfolio.Error?.ToString());
        Assert.NotEmpty(currencies.Data!);
        Assert.NotEmpty(historicalRates.Data!.Rates);
        Assert.NotEmpty(discountTiers.Data!);
        Assert.NotEmpty(loanMarginTiers.Data!);
        Assert.Equal(0m, portfolio.Data!.TotalMaintenanceMargin);
    }

    [Fact]
    public async Task Public_unified_http_json_is_available_when_live_tests_are_enabled()
    {
        if (!LiveTestSettings.Enabled)
            return;

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(20));
        var json = await PublicHttpCapture.GetStringAsync("https://api.gateio.ws/api/v4/unified/currencies?currency=BTC", cts.Token);
        var token = JToken.Parse(json);

        Assert.Equal(JTokenType.Array, token.Type);
    }
}

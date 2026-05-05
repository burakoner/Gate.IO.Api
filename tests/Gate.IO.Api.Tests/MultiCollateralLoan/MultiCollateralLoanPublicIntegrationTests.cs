using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.MultiCollateralLoan;

[Trait("Category", "PublicIntegration")]
public class MultiCollateralLoanPublicIntegrationTests
{
    [Fact]
    public async Task Public_multi_collateral_loan_endpoints_return_deserializable_data_when_live_tests_are_enabled()
    {
        if (!LiveTestSettings.Enabled)
            return;

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        var client = new GateRestApiClient();

        var currencies = await client.MultiCollateralLoan.GetCurrenciesAsync(cts.Token);
        var ltv = await client.MultiCollateralLoan.GetLtvAsync(cts.Token);
        var fixedRates = await client.MultiCollateralLoan.GetFixedRatesAsync(cts.Token);
        var currentRates = await client.MultiCollateralLoan.GetCurrentRatesAsync(["BTC", "GT"], ct: cts.Token);

        Assert.True(currencies.Success, currencies.Error?.ToString());
        Assert.True(ltv.Success, ltv.Error?.ToString());
        Assert.True(fixedRates.Success, fixedRates.Error?.ToString());
        Assert.True(currentRates.Success, currentRates.Error?.ToString());
        Assert.NotEmpty(currencies.Data!.LoanCurrencies);
        Assert.NotEmpty(currencies.Data!.CollateralCurrencies);
        Assert.True(ltv.Data!.InitialLtv > 0m);
        Assert.NotEmpty(fixedRates.Data!);
        Assert.Equal(2, currentRates.Data!.Count);
    }

    [Fact]
    public async Task Public_multi_collateral_loan_http_json_is_available_when_live_tests_are_enabled()
    {
        if (!LiveTestSettings.Enabled)
            return;

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(20));
        var json = await PublicHttpCapture.GetStringAsync("https://api.gateio.ws/api/v4/loan/multi_collateral/ltv", cts.Token);
        var token = JToken.Parse(json);

        Assert.Equal(JTokenType.Object, token.Type);
        Assert.NotEmpty(token["init_ltv"]!.ToString());
    }
}

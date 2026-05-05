using Gate.IO.Api.EarnUni;
using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.EarnUni;

[Trait("Category", "Contract")]
public class EarnUniContractTests
{
    [Fact]
    public void Documented_public_currency_responses_deserialize()
    {
        var currencies = JsonFixture.Deserialize<List<GateEarnUniCurrency>>("Docs/EarnUni/currencies.success.json");
        var currency = JsonFixture.Deserialize<GateEarnUniCurrency>("Docs/EarnUni/currency.success.json");

        Assert.Single(currencies);
        Assert.Equal("AE", currencies[0].Currency);
        Assert.Equal(100m, currencies[0].MinimumLendAmount);
        Assert.Equal(200000000m, currencies[0].MaximumLendAmount);
        Assert.Equal(0.00057m, currencies[0].MaximumRate);
        Assert.Equal(0.000001m, currency.MinimumRate);
    }

    [Fact]
    public void Documented_private_lending_responses_deserialize()
    {
        var lends = JsonFixture.Deserialize<List<GateEarnUniLend>>("Docs/EarnUni/lends.success.json");
        var lendRecords = JsonFixture.Deserialize<List<GateEarnUniLendRecord>>("Docs/EarnUni/lend_records.success.json");
        var lendInterest = JsonFixture.Deserialize<GateEarnUniLendInterest>("Docs/EarnUni/lend_interest.success.json");
        var interestRecords = JsonFixture.Deserialize<List<GateEarnUniInterestRecord>>("Docs/EarnUni/interest_records.success.json");
        var interestStatus = JsonFixture.Deserialize<GateEarnUniCurrencyInterest>("Docs/EarnUni/interest_status.success.json");
        var chart = JsonFixture.Deserialize<List<GateEarnUniChartPoint>>("Docs/EarnUni/chart.success.json");
        var estimatedRates = JsonFixture.Deserialize<List<GateEarnUniEstimatedRate>>("Docs/EarnUni/estimated_rates.success.json");

        Assert.Single(lends);
        Assert.Equal("BTC", lends[0].Currency);
        Assert.Equal(20.999992m, lends[0].CurrentAmount);
        Assert.Equal(GateEarnUniInterestStatus.Dividend, lends[0].InterestStatus);
        Assert.Equal(0m, lends[0].ReinvestLeftAmount);
        Assert.NotEqual(default, lends[0].CreateTime);
        Assert.Single(lendRecords);
        Assert.Equal(GateEarnUniLendOperationType.Lend, lendRecords[0].Type);
        Assert.Equal(0.2m, lendRecords[0].LastWalletAmount);
        Assert.Equal(123.345m, lendInterest.Interest);
        Assert.Single(interestRecords);
        Assert.Equal(GateEarnUniInterestRecordStatus.Success, interestRecords[0].Status);
        Assert.Equal(0.0005m, interestRecords[0].ActualRate);
        Assert.Equal(GateEarnUniInterestStatus.Dividend, interestStatus.InterestStatus);
        Assert.Equal(0.01m, chart[0].Value);
        Assert.NotEqual(default, chart[0].Time);
        Assert.Equal(0.0226m, estimatedRates[0].EstimatedRate);
    }

    [Fact]
    public void Captured_live_public_earn_uni_responses_deserialize()
    {
        var currencies = JsonFixture.Deserialize<List<GateEarnUniCurrency>>("Live/EarnUni/currencies.json");
        var btc = JsonFixture.Deserialize<GateEarnUniCurrency>("Live/EarnUni/currency.BTC.json");

        Assert.NotEmpty(currencies);
        Assert.Contains(currencies, x => x.Currency == "BTC");
        Assert.Equal("BTC", btc.Currency);
        Assert.True(btc.MinimumLendAmount > 0m);
        Assert.True(btc.MaximumLendAmount > 0m);
        Assert.True(btc.MaximumRate >= btc.MinimumRate);
    }
}

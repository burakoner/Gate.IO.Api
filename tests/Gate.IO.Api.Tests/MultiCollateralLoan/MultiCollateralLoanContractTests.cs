using Gate.IO.Api.MultiCollateralLoan;
using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.MultiCollateralLoan;

[Trait("Category", "Contract")]
public class MultiCollateralLoanContractTests
{
    [Fact]
    public void Documented_order_responses_deserialize()
    {
        var orders = JsonFixture.Deserialize<List<GateMultiCollateralLoanOrder>>("Docs/MultiCollateralLoan/orders.success.json");
        var order = JsonFixture.Deserialize<GateMultiCollateralLoanOrder>("Docs/MultiCollateralLoan/order.success.json");
        var orderId = JsonFixture.Deserialize<GateMultiCollateralLoanOrderId>("Docs/MultiCollateralLoan/order_id.success.json");

        Assert.Single(orders);
        Assert.Equal(10005578, order.OrderId);
        Assert.Equal(GateMultiCollateralLoanOrderType.Fixed, order.OrderType);
        Assert.Equal(GateMultiCollateralLoanFixedType.SevenDays, order.FixedType);
        Assert.Equal(0.00001m, order.FixedRate);
        Assert.Equal(GateMultiCollateralLoanOrderStatus.Lent, order.Status);
        Assert.Equal(0.0001004349664281m, order.CurrentLtv);
        Assert.Equal(106.491212982m, order.TotalLeftRepayUsdt);
        Assert.Equal(10.6491m, order.BorrowCurrencies[0].IndexPrice);
        Assert.Equal(9.4m, order.CollateralCurrencies[0].LeftCollateral);
        Assert.NotEqual(default, order.ExpireTime);
        Assert.NotEqual(default, order.BorrowTime);
        Assert.Equal(10005578, orderId.OrderId);
    }

    [Fact]
    public void Documented_repayment_collateral_and_quota_responses_deserialize()
    {
        var repayments = JsonFixture.Deserialize<List<GateMultiCollateralLoanRepaymentRecord>>("Docs/MultiCollateralLoan/repayment_records.success.json");
        var repaymentResult = JsonFixture.Deserialize<GateMultiCollateralLoanRepaymentResult>("Docs/MultiCollateralLoan/repayment_result.success.json");
        var collateralRecords = JsonFixture.Deserialize<List<GateMultiCollateralLoanCollateralRecord>>("Docs/MultiCollateralLoan/collateral_records.success.json");
        var collateralResult = JsonFixture.Deserialize<GateMultiCollateralLoanCollateralAdjustmentResult>("Docs/MultiCollateralLoan/collateral_adjustment.success.json");
        var quotas = JsonFixture.Deserialize<List<GateMultiCollateralLoanCurrencyQuota>>("Docs/MultiCollateralLoan/currency_quota.success.json");

        Assert.Single(repayments);
        Assert.Equal(10005679, repayments[0].OrderId);
        Assert.Equal(0.2141m, repayments[0].InitialLtv);
        Assert.Equal(102.91873134m, repayments[0].BorrowCurrencies[0].AfterAmountUsdt);
        Assert.Equal(0.000983m, repayments[0].RepaidCurrencies[0].RepaidPrincipal);
        Assert.Equal(0.000017m, repayments[0].TotalInterestList[0].Amount);
        Assert.Equal(0m, repayments[0].LeftRepayInterestList[0].AfterAmount);
        Assert.NotEqual(default, repayments[0].RepayTime);
        Assert.False(repaymentResult.RepaidCurrencies[0].Succeeded);
        Assert.Equal("INVALID_PARAM_VALUE", repaymentResult.RepaidCurrencies[0].Label);
        Assert.True(repaymentResult.RepaidCurrencies[1].Succeeded);
        Assert.Single(collateralRecords);
        Assert.Equal(10000452, collateralRecords[0].RecordId);
        Assert.Equal(0.00019672777810740000m, collateralRecords[0].AfterLtv);
        Assert.Equal(1006m, collateralRecords[0].CollateralCurrencies[0].AfterAmountUsdt);
        Assert.NotEqual(default, collateralRecords[0].OperateTime);
        Assert.True(collateralResult.CollateralCurrencies[0].Succeeded);
        Assert.Equal(0.5m, collateralResult.CollateralCurrencies[0].Amount);
        Assert.Single(quotas);
        Assert.Equal(35306.1m, quotas[0].IndexPrice);
        Assert.Equal(2768152.4958445218723677m, quotas[0].LeftQuota);
        Assert.Null(quotas[0].LeftQuotaFixed);
    }

    [Fact]
    public void Documented_public_market_responses_deserialize()
    {
        var currencies = JsonFixture.Deserialize<GateMultiCollateralLoanCurrencies>("Docs/MultiCollateralLoan/currencies.success.json");
        var ltv = JsonFixture.Deserialize<GateMultiCollateralLoanLtv>("Docs/MultiCollateralLoan/ltv.success.json");
        var fixedRates = JsonFixture.Deserialize<List<GateMultiCollateralLoanFixedRate>>("Docs/MultiCollateralLoan/fixed_rate.success.json");
        var currentRates = JsonFixture.Deserialize<List<GateMultiCollateralLoanCurrentRate>>("Docs/MultiCollateralLoan/current_rate.success.json");

        Assert.Equal(2, currencies.LoanCurrencies.Count);
        Assert.Equal("BTC", currencies.LoanCurrencies[0].Currency);
        Assert.Equal(1212m, currencies.LoanCurrencies[0].Price);
        Assert.Single(currencies.CollateralCurrencies);
        Assert.Equal(0.7m, currencies.CollateralCurrencies[0].Discount);
        Assert.Equal(0.7m, ltv.InitialLtv);
        Assert.Equal(0.9m, ltv.LiquidateLtv);
        Assert.Equal(0.000023m, fixedRates[0].Rate7Days);
        Assert.Equal(0.1m, fixedRates[0].Rate30Days);
        Assert.NotEqual(default, fixedRates[0].UpdateTime);
        Assert.Equal(0.000023m, currentRates[0].CurrentRate);
    }

    [Fact]
    public void Captured_live_public_multi_collateral_loan_responses_deserialize()
    {
        var currencies = JsonFixture.Deserialize<GateMultiCollateralLoanCurrencies>("Live/MultiCollateralLoan/currencies.json");
        var ltv = JsonFixture.Deserialize<GateMultiCollateralLoanLtv>("Live/MultiCollateralLoan/ltv.json");
        var fixedRates = JsonFixture.Deserialize<List<GateMultiCollateralLoanFixedRate>>("Live/MultiCollateralLoan/fixed_rate.json");
        var currentRates = JsonFixture.Deserialize<List<GateMultiCollateralLoanCurrentRate>>("Live/MultiCollateralLoan/current_rate.BTC_GT.json");

        Assert.NotEmpty(currencies.LoanCurrencies);
        Assert.NotEmpty(currencies.CollateralCurrencies);
        Assert.Contains(currencies.LoanCurrencies, x => x.Currency == "BTC");
        Assert.Contains(currencies.CollateralCurrencies, x => x.Currency == "BTC");
        Assert.True(ltv.InitialLtv > 0m);
        Assert.True(ltv.LiquidateLtv >= ltv.AlertLtv);
        Assert.NotEmpty(fixedRates);
        Assert.Contains(fixedRates, x => x.Currency == "BTC");
        Assert.Equal(2, currentRates.Count);
        Assert.Contains(currentRates, x => x.Currency == "BTC");
        Assert.Contains(currentRates, x => x.Currency == "GT");
    }
}

using Gate.IO.Api.Tests.Infrastructure;
using Gate.IO.Api.Unified;

namespace Gate.IO.Api.Tests.Unified;

[Trait("Category", "Contract")]
public class UnifiedContractTests
{
    [Fact]
    public void Documented_unified_account_response_deserializes()
    {
        var account = JsonFixture.Deserialize<GateUnifiedAccountInfo>("Docs/Unified/account_info.success.json");

        Assert.Equal(GateUnifiedAccountMode.MultiCurrency, account.Mode);
        Assert.Equal(10001, account.UserId);
        Assert.False(account.Locked);
        Assert.Equal(230.94621713m, account.Total);
        Assert.Equal(3381470.892007440383m, account.UnifiedAccountTotal);
        Assert.True(account.UseFunding);
        Assert.False(account.IsAllCollateral);
        Assert.True(account.Balances["ETH"].IsCollateralEnabled);
        Assert.Equal(123456, account.Balances["ETH"].BalanceVersion);
    }

    [Fact]
    public void Documented_unified_amount_and_loan_responses_deserialize()
    {
        var borrowable = JsonFixture.Deserialize<GateUnifiedCurrencyAmount>("Docs/Unified/borrowable.success.json");
        var transferables = JsonFixture.Deserialize<List<GateUnifiedCurrencyAmount>>("Docs/Unified/transferables.success.json");
        var loanResult = JsonFixture.Deserialize<GateUnifiedLoanResult>("Docs/Unified/loan_result.success.json");
        var loans = JsonFixture.Deserialize<List<GateUnifiedLoan>>("Docs/Unified/loans.success.json");

        Assert.Equal("ETH", borrowable.Asset);
        Assert.Equal(10000m, borrowable.Amount);
        Assert.Single(transferables);
        Assert.Equal("BTC", transferables[0].Asset);
        Assert.Equal(9527, loanResult.TransactionId);
        Assert.Single(loans);
        Assert.Equal("GT_USDT", loans[0].Symbol);
        Assert.Equal(GateUnifiedLoanType.Margin, loans[0].Type);
        Assert.NotNull(loans[0].UpdateTime);
    }

    [Fact]
    public void Documented_unified_loan_record_and_interest_responses_deserialize()
    {
        var loanRecords = JsonFixture.Deserialize<List<GateUnifiedLoanRecord>>("Docs/Unified/loan_records.success.json");
        var interestRecords = JsonFixture.Deserialize<List<GateUnifiedInterestRecord>>("Docs/Unified/interest_records.success.json");

        Assert.Single(loanRecords);
        Assert.Equal(GateUnifiedLoanDirection.Borrow, loanRecords[0].Type);
        Assert.Equal(GateUnifiedRepayType.ManualRepay, loanRecords[0].RepaymentType);
        Assert.Equal(GateUnifiedBorrowType.ManualBorrow, loanRecords[0].BorrowType);
        Assert.Equal(1m, loanRecords[0].Quantity);
        Assert.Single(interestRecords);
        Assert.Equal(GateUnifiedInterestStatus.Success, interestRecords[0].Status);
        Assert.True(interestRecords[0].Success);
        Assert.Equal(GateUnifiedLoanType.Margin, interestRecords[0].Type);
        Assert.Equal(0.01m, interestRecords[0].Interest);
    }

    [Fact]
    public void Documented_unified_mode_risk_and_leverage_responses_deserialize()
    {
        var riskUnits = JsonFixture.Deserialize<GateUnifiedRiskUnits>("Docs/Unified/risk_units.success.json");
        var mode = JsonFixture.Deserialize<GateUnifiedAccountModeInfo>("Docs/Unified/unified_mode.success.json");
        var leverageConfig = JsonFixture.Deserialize<GateUnifiedLeverageConfig>("Docs/Unified/leverage_config.success.json");
        var leverageSettings = JsonFixture.Deserialize<List<GateUnifiedLeverageSetting>>("Docs/Unified/leverage_settings.success.json");

        Assert.True(riskUnits.SpotHedge);
        Assert.Single(riskUnits.RiskUnits);
        Assert.Equal(-13500.000001223m, riskUnits.RiskUnits[0].SpotInUse);
        Assert.Equal(GateUnifiedAccountMode.Portfolio, mode.Mode);
        Assert.True(mode.Settings.Options);
        Assert.Equal(10m, leverageConfig.MaxLeverage);
        Assert.Single(leverageSettings);
        Assert.Equal(3m, leverageSettings[0].Leverage);
    }

    [Fact]
    public void Documented_public_unified_market_responses_deserialize()
    {
        var currencies = JsonFixture.Deserialize<List<GateUnifiedCurrency>>("Docs/Unified/currencies.success.json");
        var historicalRates = JsonFixture.Deserialize<GateUnifiedHistoricalLendingRates>("Docs/Unified/history_loan_rate.success.json");
        var discountTiers = JsonFixture.Deserialize<List<List<GateUnifiedCurrencyDiscountTiers>>>("Docs/Unified/currency_discount_tiers.success.json")
            .SelectMany(x => x)
            .ToList();
        var loanMarginTiers = JsonFixture.Deserialize<List<GateUnifiedLoanMarginTiers>>("Docs/Unified/loan_margin_tiers.success.json");
        var estimateRates = JsonFixture.Deserialize<Dictionary<string, decimal?>>("Docs/Unified/estimate_rate.success.json");

        Assert.Single(currencies);
        Assert.Equal(GateUnifiedLendingStatus.Enable, currencies[0].Status);
        Assert.Equal("USDT", historicalRates.Currency);
        Assert.Equal(1, historicalRates.Tier);
        Assert.Single(historicalRates.Rates);
        Assert.Single(discountTiers);
        Assert.Equal("+", discountTiers[0].DiscountTiers[1].UpperLimit);
        Assert.Single(loanMarginTiers);
        Assert.Equal(0.02m, loanMarginTiers[0].MarginTiers[0].MarginRate);
        Assert.Equal(0.000002m, estimateRates["BTC"]);
        Assert.Null(estimateRates["ETH"]);
    }

    [Fact]
    public void Documented_unified_portfolio_and_collateral_responses_deserialize()
    {
        var portfolio = JsonFixture.Deserialize<GateUnifiedPortfolioCalculation>("Docs/Unified/portfolio_calculator.success.json");
        var collateral = JsonFixture.Deserialize<GateUnifiedIsSuccess>("Docs/Unified/collateral_currencies.success.json");

        Assert.Equal(0m, portfolio.TotalMaintenanceMargin);
        Assert.Single(portfolio.RiskUnits);
        Assert.Equal("original_position", portfolio.RiskUnits[0].MarginResults[0].Type);
        Assert.Equal(-0.2m, portfolio.RiskUnits[0].MarginResults[0].MaximumLoss.PricePercentage);
        Assert.True(collateral.IsSuccess);
    }

    [Fact]
    public void Captured_live_public_unified_responses_deserialize()
    {
        var currencies = JsonFixture.Deserialize<List<GateUnifiedCurrency>>("Live/Unified/currencies.BTC.json");
        var historicalRates = JsonFixture.Deserialize<GateUnifiedHistoricalLendingRates>("Live/Unified/history_loan_rate.USDT.limit1.json");
        var discountTiers = JsonFixture.Deserialize<List<GateUnifiedCurrencyDiscountTiers>>("Live/Unified/currency_discount_tiers.json");
        var loanMarginTiers = JsonFixture.Deserialize<List<GateUnifiedLoanMarginTiers>>("Live/Unified/loan_margin_tiers.json");
        var portfolio = JsonFixture.Deserialize<GateUnifiedPortfolioCalculation>("Live/Unified/portfolio_calculator.spot_hedge.json");

        Assert.NotEmpty(currencies);
        Assert.NotEmpty(historicalRates.Rates);
        Assert.NotEmpty(discountTiers);
        Assert.NotEmpty(loanMarginTiers);
        Assert.Equal(0m, portfolio.TotalMaintenanceMargin);
        Assert.NotEqual(default, portfolio.CalculationTime);
    }
}

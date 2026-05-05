using Gate.IO.Api.Margin;
using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.Margin;

[Trait("Category", "Contract")]
public class MarginContractTests
{
    [Fact]
    public void Documented_margin_account_responses_deserialize()
    {
        var accounts = JsonFixture.Deserialize<List<GateMarginBalance>>("Docs/Margin/accounts.success.json");
        var isolatedAccounts = JsonFixture.Deserialize<List<GateMarginBalance>>("Docs/Margin/isolated_accounts.success.json");
        var history = JsonFixture.Deserialize<List<GateMarginBalanceHistory>>("Docs/Margin/account_book.success.json");
        var funding = JsonFixture.Deserialize<List<GateMarginFundingBalance>>("Docs/Margin/funding_accounts.success.json");

        Assert.Single(accounts);
        Assert.Equal("BTC_USDT", accounts[0].Symbol);
        Assert.Equal(20m, accounts[0].Leverage);
        Assert.Equal(16.5949188975473644m, accounts[0].MMR);
        Assert.Single(isolatedAccounts);
        Assert.Single(history);
        Assert.Equal(123456, history[0].Id);
        Assert.Equal(1547633726123, history[0].TimeInMilliseconds);
        Assert.Equal(1.03m, history[0].Change);
        Assert.Single(funding);
        Assert.Equal(3.32m, funding[0].TotalLent);
    }

    [Fact]
    public void Documented_margin_settings_and_amount_responses_deserialize()
    {
        var autoRepay = JsonFixture.Deserialize<GateMarginAutoRepayment>("Docs/Margin/auto_repay.success.json");
        var transferable = JsonFixture.Deserialize<GateMarginAmount>("Docs/Margin/transferable.success.json");
        var borrowable = JsonFixture.Deserialize<GateMarginBorrowable>("Docs/Margin/borrowable.success.json");
        var leverage = JsonFixture.Deserialize<GateMarginLeverage>("Docs/Margin/leverage.success.json");

        Assert.Equal(GateMarginAutoRepaymentStatus.Enabled, autoRepay.Status);
        Assert.Equal("BTC_USDT", transferable.Symbol);
        Assert.Equal(1.1m, transferable.Amount);
        Assert.Equal(10000m, borrowable.Borrowable);
        Assert.Equal(10m, leverage.Leverage);
    }

    [Fact]
    public void Documented_public_margin_market_responses_deserialize()
    {
        var markets = JsonFixture.Deserialize<List<GateMarginMarket>>("Docs/Margin/currency_pairs.success.json");
        var market = JsonFixture.Deserialize<GateMarginMarket>("Docs/Margin/currency_pair.success.json");
        var estimateRates = JsonFixture.Deserialize<Dictionary<string, decimal>>("Docs/Margin/estimate_rate.success.json");
        var tiers = JsonFixture.Deserialize<List<GateMarginTier>>("Docs/Margin/loan_margin_tiers.success.json");

        Assert.Single(markets);
        Assert.Equal("AE_USDT", markets[0].Symbol);
        Assert.Equal(100m, market.MinimumBaseBorrowQuantity);
        Assert.Equal(0.0000703m, estimateRates["BTC"]);
        Assert.Single(tiers);
        Assert.Equal(100m, tiers[0].UpperLimit);
        Assert.Equal(0.9m, tiers[0].MMR);
    }

    [Fact]
    public void Documented_margin_loan_and_interest_responses_deserialize()
    {
        var loans = JsonFixture.Deserialize<List<GateMarginLoan>>("Docs/Margin/loans.success.json");
        var loanRecords = JsonFixture.Deserialize<List<GateMarginLoanRecord>>("Docs/Margin/loan_records.success.json");
        var interestRecords = JsonFixture.Deserialize<List<GateMarginInterest>>("Docs/Margin/interest_records.success.json");

        Assert.Single(loans);
        Assert.Equal("GT_USDT", loans[0].Symbol);
        Assert.Equal(GateMarginLoanType.Margin, loans[0].Type);
        Assert.NotNull(loans[0].UpdateTime);
        Assert.Single(loanRecords);
        Assert.Equal(GateMarginUniOrderType.Borrow, loanRecords[0].Type);
        Assert.Single(interestRecords);
        Assert.Equal(GateMarginUniInterestStatus.Success, interestRecords[0].Status);
        Assert.Equal(GateMarginLoanType.Margin, interestRecords[0].Type);
        Assert.Equal(0.01m, interestRecords[0].Interest);
    }

    [Fact]
    public void Captured_live_public_margin_responses_deserialize()
    {
        var markets = JsonFixture.Deserialize<List<GateMarginMarket>>("Live/Margin/currency_pairs.json");
        var market = JsonFixture.Deserialize<GateMarginMarket>("Live/Margin/currency_pairs.BTC_USDT.json");
        var tiers = JsonFixture.Deserialize<List<GateMarginTier>>("Live/Margin/loan_margin_tiers.BTC_USDT.json");

        Assert.NotEmpty(markets);
        Assert.Equal("BTC_USDT", market.Symbol);
        Assert.NotEmpty(tiers);
    }
}

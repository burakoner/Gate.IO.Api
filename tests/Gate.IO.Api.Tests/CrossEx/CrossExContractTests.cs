using Gate.IO.Api.CrossEx;
using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.CrossEx;

[Trait("Category", "Contract")]
public class CrossExContractTests
{
    [Fact]
    public void Public_rule_and_transfer_coin_responses_deserialize()
    {
        var documentedSymbols = JsonFixture.Deserialize<List<GateCrossExSymbol>>("Docs/CrossEx/symbols.success.json");
        var liveSymbols = JsonFixture.Deserialize<List<GateCrossExSymbol>>("Live/CrossEx/symbols.BINANCE_FUTURE_ADA_USDT.json");
        var riskLimits = JsonFixture.Deserialize<List<GateCrossExRiskLimit>>("Docs/CrossEx/risk_limits.success.json");
        var liveRiskLimits = JsonFixture.Deserialize<List<GateCrossExRiskLimit>>("Live/CrossEx/risk_limits.BINANCE_FUTURE_ADA_USDT.json");
        var transferCoins = JsonFixture.Deserialize<List<GateCrossExTransferCoin>>("Docs/CrossEx/transfer_coins.success.json");
        var liveTransferCoins = JsonFixture.Deserialize<List<GateCrossExTransferCoin>>("Live/CrossEx/transfer_coins.USDT.json");

        Assert.Equal("BINANCE_FUTURE_BTC_USDT", documentedSymbols[0].Symbol);
        Assert.Equal(0.001m, documentedSymbols[0].ContractSize);
        Assert.Null(liveSymbols[0].ContractSize);
        Assert.Equal("BINANCE_FUTURE_ADA_USDT", liveSymbols[0].Symbol);
        Assert.Equal(2, riskLimits[0].Tiers.Count);
        Assert.Equal(20m, riskLimits[0].Tiers[0].MaximumLeverage);
        Assert.Equal(4, liveRiskLimits[0].Tiers.Count);
        Assert.Equal("USDT", transferCoins[0].Coin);
        Assert.Equal(0.00000001m, transferCoins[0].MinimumTransferAmount);
        Assert.Equal(8, liveTransferCoins[0].Precision);
    }

    [Fact]
    public void Transfer_order_convert_and_account_responses_deserialize()
    {
        var transfers = JsonFixture.Deserialize<List<GateCrossExTransferRecord>>("Docs/CrossEx/transfer_history.success.json");
        var transfer = JsonFixture.Deserialize<GateCrossExTransferResult>("Docs/CrossEx/transfer.success.json");
        var action = JsonFixture.Deserialize<GateCrossExOrderActionResult>("Docs/CrossEx/order_action.success.json");
        var order = JsonFixture.Deserialize<GateCrossExOrder>("Docs/CrossEx/order.success.json");
        var orders = JsonFixture.Deserialize<List<GateCrossExOrder>>("Docs/CrossEx/orders.success.json");
        var quote = JsonFixture.Deserialize<GateCrossExConvertQuote>("Docs/CrossEx/convert_quote.success.json");
        var convertOrder = JsonFixture.Deserialize<GateCrossExConvertOrderResult>("Docs/CrossEx/convert_order.success.json");
        var account = JsonFixture.Deserialize<GateCrossExAccount>("Docs/CrossEx/account.success.json");
        var accountUpdate = JsonFixture.Deserialize<GateCrossExAccountUpdateResult>("Docs/CrossEx/account_update.success.json");
        var leverages = JsonFixture.Deserialize<Dictionary<string, decimal>>("Docs/CrossEx/leverages.success.json");
        var leverage = JsonFixture.Deserialize<GateCrossExLeverageResult>("Docs/CrossEx/leverage.success.json");

        Assert.Equal(123456, transfers[0].Id);
        Assert.Equal(100.5m, transfers[0].Amount);
        Assert.Equal(123456, transfer.TransactionId);
        Assert.Equal(234567, action.OrderId);
        Assert.Equal("t-cross-order", order.Text);
        Assert.Equal("t-cross-order", orders[0].Text);
        Assert.Equal(60000m, order.ExecutedAveragePrice);
        Assert.Equal("USDT", order.FeeCoin);
        Assert.Equal("cross-quote-001", quote.QuoteId);
        Assert.Equal(100m, quote.FromAmount);
        Assert.Equal(345678, convertOrder.OrderId);
        Assert.Equal(900001, account.UserId);
        Assert.Equal(1200.25m, account.Assets[0].Balance);
        Assert.Equal("DUAL", accountUpdate.PositionMode);
        Assert.Equal(5m, leverages["BINANCE_FUTURE_ETH_USDT"]);
        Assert.Equal(3m, leverage.Leverage);
    }

    [Fact]
    public void Position_history_fee_and_account_book_responses_deserialize()
    {
        var interestRates = JsonFixture.Deserialize<List<GateCrossExInterestRate>>("Docs/CrossEx/interest_rates.success.json");
        var fees = JsonFixture.Deserialize<List<GateCrossExFee>>("Docs/CrossEx/fees.success.json");
        var positions = JsonFixture.Deserialize<List<GateCrossExPosition>>("Docs/CrossEx/positions.success.json");
        var marginPositions = JsonFixture.Deserialize<List<GateCrossExMarginPosition>>("Docs/CrossEx/margin_positions.success.json");
        var adlRanks = JsonFixture.Deserialize<List<GateCrossExAdlRank>>("Docs/CrossEx/adl_rank.success.json");
        var history = JsonFixture.Deserialize<List<GateCrossExHistoricalPosition>>("Docs/CrossEx/history_positions.success.json");
        var marginHistory = JsonFixture.Deserialize<List<GateCrossExHistoricalMarginPosition>>("Docs/CrossEx/history_margin_positions.success.json");
        var marginInterests = JsonFixture.Deserialize<List<GateCrossExMarginInterestRecord>>("Docs/CrossEx/margin_interest_history.success.json");
        var trades = JsonFixture.Deserialize<List<GateCrossExTrade>>("Docs/CrossEx/trade_history.success.json");
        var accountBook = JsonFixture.Deserialize<List<GateCrossExAccountBookRecord>>("Docs/CrossEx/account_book.success.json");
        var discountRates = JsonFixture.Deserialize<List<GateCrossExCoinDiscountRate>>("Docs/CrossEx/coin_discount_rates.success.json");

        Assert.Equal(0.000001m, interestRates[0].HourlyInterestRate);
        Assert.Equal(0.0004m, fees[0].SpecialFees[0].TakerFeeRate);
        Assert.Equal(60000m, positions[0].EntryPrice);
        Assert.Equal(60550m, positions[0].MarkPrice);
        Assert.Equal("BTC", marginPositions[0].AssetCoin);
        Assert.Equal(0.01m, marginPositions[0].Interest);
        Assert.Equal(2, adlRanks[0].CrossExAdlRank);
        Assert.Equal(10m, history[0].ClosedPnl);
        Assert.Equal("MARGIN", marginHistory[0].BusinessType);
        Assert.Equal(567890, marginInterests[0].InterestId);
        Assert.Equal(678901, trades[0].TransactionId);
        Assert.Equal(-0.3m, accountBook[0].Change);
        Assert.Equal(1m, discountRates[0].DiscountRate);
    }
}

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
        var liveKrakenSymbols = JsonFixture.Deserialize<List<GateCrossExSymbol>>("Live/CrossEx/symbols.KRAKEN_FUTURE_ADA_USD.json");
        var riskLimits = JsonFixture.Deserialize<List<GateCrossExRiskLimit>>("Docs/CrossEx/risk_limits.success.json");
        var liveRiskLimits = JsonFixture.Deserialize<List<GateCrossExRiskLimit>>("Live/CrossEx/risk_limits.BINANCE_FUTURE_ADA_USDT.json");
        var transferCoins = JsonFixture.Deserialize<List<GateCrossExTransferCoin>>("Docs/CrossEx/transfer_coins.success.json");
        var liveTransferCoins = JsonFixture.Deserialize<List<GateCrossExTransferCoin>>("Live/CrossEx/transfer_coins.USDT.json");

        Assert.Equal("BINANCE_FUTURE_BTC_USDT", documentedSymbols[0].Symbol);
        Assert.Equal(0.001m, documentedSymbols[0].ContractSize);
        Assert.False(documentedSymbols[0].SupportsRpi);
        Assert.Null(liveSymbols[0].ContractSize);
        Assert.Equal("BINANCE_FUTURE_ADA_USDT", liveSymbols[0].Symbol);
        Assert.Equal("KRAKEN_FUTURE_ADA_USD", liveKrakenSymbols[0].Symbol);
        Assert.Equal("KRAKEN", liveKrakenSymbols[0].ExchangeType);
        Assert.Null(liveKrakenSymbols[0].MaximumMarketSize);
        Assert.False(liveKrakenSymbols[0].SupportsRpi);
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
        var batchCancellation = JsonFixture.Deserialize<List<GateCrossExBatchCancelOrderResult>>("Docs/CrossEx/batch_cancel_orders.success.json");
        var order = JsonFixture.Deserialize<GateCrossExOrder>("Docs/CrossEx/order.success.json");
        var orders = JsonFixture.Deserialize<List<GateCrossExOrder>>("Docs/CrossEx/orders.success.json");
        var quote = JsonFixture.Deserialize<GateCrossExConvertQuote>("Docs/CrossEx/convert_quote.success.json");
        var convertOrder = JsonFixture.Deserialize<GateCrossExConvertOrderResult>("Docs/CrossEx/convert_order.success.json");
        var account = JsonFixture.Deserialize<GateCrossExAccount>("Docs/CrossEx/account.success.json");
        var accountUpdate = JsonFixture.Deserialize<GateCrossExAccountUpdateResult>("Docs/CrossEx/account_update.success.json");
        var leverages = JsonFixture.Deserialize<Dictionary<string, decimal>>("Docs/CrossEx/leverages.success.json");
        var leverage = JsonFixture.Deserialize<GateCrossExLeverageResult>("Docs/CrossEx/leverage.success.json");

        Assert.Equal("33829017692939266", transfers[0].Id);
        Assert.Equal("CROSSEX_KRAKEN", transfers[0].FromAccountType);
        Assert.Equal(100.5m, transfers[0].Amount);
        Assert.Equal(DateTimeOffset.FromUnixTimeMilliseconds(1750681141933).UtcDateTime, transfers[0].CreateTime);
        Assert.Equal("33829017692939266", transfer.TransactionId);
        Assert.Equal("2072652940337152", action.OrderId);
        Assert.Equal("123456", batchCancellation[0].OrderId);
        Assert.Equal("true", batchCancellation[0].Accepted);
        Assert.Equal("crossex-test-1", batchCancellation[1].Text);
        Assert.Equal("false", batchCancellation[1].Accepted);
        Assert.Equal("TRADE_ORDER_NOT_FOUND_ERROR", batchCancellation[1].Label);
        Assert.Equal("The order was not found", batchCancellation[1].Message);
        Assert.Equal("t-cross-order", order.Text);
        Assert.Equal("2072652940337152", order.OrderId);
        Assert.Equal("900001", order.UserId);
        Assert.Equal("KRAKEN_FUTURE_ADA_USD", order.Symbol);
        Assert.Equal("KRAKEN", order.ExchangeType);
        Assert.Equal(DateTimeOffset.FromUnixTimeMilliseconds(1750681141933).UtcDateTime, order.CreateTime);
        Assert.Equal("2048522992198912", orders[0].Text);
        Assert.Equal(60000m, order.ExecutedAveragePrice);
        Assert.Equal("USD", order.FeeCoin);
        Assert.Equal("COMMON", orders[0].Attribute);
        Assert.Equal("BINANCE", orders[0].ExchangeType);
        Assert.Equal("SPOT", orders[0].BusinessType);
        Assert.Equal(12.9m, orders[0].ExecutedQuantity);
        Assert.False(orders[0].ReduceOnly);
        Assert.Equal(DateTimeOffset.FromUnixTimeMilliseconds(1750681141933).UtcDateTime, orders[0].CreateTime);
        Assert.Equal(DateTimeOffset.FromUnixTimeMilliseconds(1750681142379).UtcDateTime, orders[0].UpdateTime);
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
        Assert.Equal(0m, fees[0].SpotRpiMakerFee);
        Assert.Equal(0m, fees[0].FutureRpiMakerFee);
        Assert.Equal(0m, fees[0].SpecialFees[0].RpiFeeRate);
        Assert.Equal(new[] { "BINANCE", "OKX", "GATE", "BYBIT", "KRAKEN", "HYPERLIQUID", "DERIBIT" }, fees.Select(x => x.ExchangeType));
        Assert.Equal("10001004", positions[0].UserId);
        Assert.Equal("20062926505289216", positions[0].PositionId);
        Assert.Equal("OKX_FUTURE_ADA_USDT", positions[0].Symbol);
        Assert.Equal(0.5426m, positions[0].EntryPrice);
        Assert.Equal(0.5795m, positions[0].MarkPrice);
        Assert.Equal(0m, positions[0].FundingFee);
        Assert.Null(positions[0].FundingTime);
        Assert.Equal(DateTimeOffset.FromUnixTimeMilliseconds(1750682334273).UtcDateTime, positions[0].CreateTime);
        Assert.Equal(DateTimeOffset.FromUnixTimeMilliseconds(1750730699867).UtcDateTime, positions[0].UpdateTime);
        Assert.Equal("BTC", marginPositions[0].AssetCoin);
        Assert.Equal(0.01m, marginPositions[0].Interest);
        Assert.Equal("111", adlRanks[0].UserId);
        Assert.Equal(1, adlRanks[0].CrossExAdlRank);
        Assert.Equal(1, adlRanks[0].ExchangeAdlRank);
        Assert.Equal("KRAKEN_FUTURE_ADA_USD", adlRanks[1].Symbol);
        Assert.Equal(40, adlRanks[1].ExchangeAdlRank);
        Assert.Equal(10m, history[0].ClosedPnl);
        Assert.Equal("MARGIN", marginHistory[0].BusinessType);
        Assert.Equal(567890, marginInterests[0].InterestId);
        Assert.Equal(678901, trades[0].TransactionId);
        Assert.Equal("121", accountBook[0].Id);
        Assert.Equal("12345678", accountBook[0].UserId);
        Assert.Equal("20818182821", accountBook[0].BusinessId);
        Assert.Equal("FUNDING_FEE", accountBook[0].StatementType);
        Assert.Equal("BINANCE_FUTURE_BTC_USDT", accountBook[0].Symbol);
        Assert.Equal(-0.002m, accountBook[0].Change);
        Assert.Equal(DateTimeOffset.FromUnixTimeMilliseconds(1750941402661).UtcDateTime, accountBook[0].CreateTime);
        Assert.Equal(1m, discountRates[0].DiscountRate);
    }

    [Fact]
    public void Market_ticker_and_funding_info_responses_deserialize_complete_current_contracts()
    {
        var tickers = JsonFixture.Deserialize<List<GateCrossExMarketTicker>>("Docs/CrossEx/market_tickers.success.json");
        var fundingInfo = JsonFixture.Deserialize<List<GateCrossExMarketFundingInfo>>("Docs/CrossEx/market_funding_info.success.json");

        Assert.Equal(2, tickers.Count);
        Assert.Equal("GATE_FUTURE_BTC_USDT", tickers[0].Symbol);
        Assert.Equal(64052.4m, tickers[0].LastPrice);
        Assert.Equal(65144.7m, tickers[0].Open24h);
        Assert.Equal(64375m, tickers[0].Low24h);
        Assert.Equal(65734.8m, tickers[0].High24h);
        Assert.Equal(31705m, tickers[0].Volume24hBase);
        Assert.Equal(2063128626m, tickers[0].Volume24hQuote);
        Assert.Equal(65148.9m, tickers[0].MarkPrice);
        Assert.Equal(65174.38m, tickers[0].IndexPrice);
        Assert.Equal(65568.2144m, tickers[0].OpenInterest);
        Assert.Equal(4271697043.12416m, tickers[0].OpenInterestQuote);
        Assert.Equal(DateTimeOffset.FromUnixTimeMilliseconds(1785168000000).UtcDateTime, tickers[0].Timestamp);
        Assert.Null(tickers[1].Open24h);
        Assert.Null(tickers[1].MarkPrice);
        Assert.Null(tickers[1].IndexPrice);
        Assert.Null(tickers[1].OpenInterest);
        Assert.Null(tickers[1].OpenInterestQuote);

        Assert.Equal(5, fundingInfo.Count);
        Assert.Equal("BINANCE_FUTURE_BTC_USDT", fundingInfo[0].Symbol);
        Assert.Equal(0.00006537m, fundingInfo[0].FundingRate);
        Assert.Equal(28800, fundingInfo[0].FundingInterval);
        Assert.Equal(DateTimeOffset.FromUnixTimeMilliseconds(1785168000000).UtcDateTime, fundingInfo[0].FundingTime);
        Assert.Equal("KRAKEN_FUTURE_BTC_USD", fundingInfo[2].Symbol);
        Assert.Equal(0.000011898754310345m, fundingInfo[2].FundingRate);
        Assert.Equal(3600, fundingInfo[2].FundingInterval);
        Assert.Equal(DateTimeOffset.FromUnixTimeMilliseconds(1785139200000).UtcDateTime, fundingInfo[2].FundingTime);
    }
}

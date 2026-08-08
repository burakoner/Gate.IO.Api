using Gate.IO.Api.CrossEx;
using Gate.IO.Api.Tests.Infrastructure;
using ApiSharp.Converters;
using System.Text;

namespace Gate.IO.Api.Tests.CrossEx;

[Trait("Category", "Unit")]
public class CrossExRequestConstructionTests
{
    [Fact]
    public async Task Public_rule_requests_serialize_queries_without_authentication_headers()
    {
        var responses = new Queue<string>([
            JsonFixture.Read("Docs/CrossEx/symbols.success.json"),
            JsonFixture.Read("Docs/CrossEx/risk_limits.success.json"),
            JsonFixture.Read("Docs/CrossEx/transfer_coins.success.json"),
        ]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var symbols = await client.CrossEx.GetSymbolsAsync(new GateCrossExSymbolsQueryRequest
        {
            Symbols = ["BINANCE_FUTURE_BTC_USDT", "BINANCE_FUTURE_ETH_USDT"],
        });
        var riskLimits = await client.CrossEx.GetRiskLimitsAsync(new GateCrossExRiskLimitQueryRequest
        {
            Symbols = ["BINANCE_FUTURE_BTC_USDT"],
        });
        var transferCoins = await client.CrossEx.GetTransferCoinsAsync(new GateCrossExTransferCoinQueryRequest
        {
            Coin = "USDT",
        });

        Assert.True(symbols.Success, symbols.Error?.ToString());
        Assert.True(riskLimits.Success, riskLimits.Error?.ToString());
        Assert.True(transferCoins.Success, transferCoins.Error?.ToString());
        Assert.Equal(3, handler.Requests.Count);

        var symbolsQuery = ParseQuery(handler.Requests[0].RequestUri);
        Assert.Equal(HttpMethod.Get, handler.Requests[0].Method);
        Assert.Equal("/api/v4/crossex/rule/symbols", handler.Requests[0].RequestUri.AbsolutePath);
        Assert.Equal("BINANCE_FUTURE_BTC_USDT,BINANCE_FUTURE_ETH_USDT", symbolsQuery["symbols"]);

        var riskQuery = ParseQuery(handler.Requests[1].RequestUri);
        Assert.Equal("/api/v4/crossex/rule/risk_limits", handler.Requests[1].RequestUri.AbsolutePath);
        Assert.Equal("BINANCE_FUTURE_BTC_USDT", riskQuery["symbols"]);

        var transferCoinQuery = ParseQuery(handler.Requests[2].RequestUri);
        Assert.Equal("/api/v4/crossex/transfers/coin", handler.Requests[2].RequestUri.AbsolutePath);
        Assert.Equal("USDT", transferCoinQuery["coin"]);
        Assert.All(handler.Requests, AssertNoAuthHeaders);
    }

    [Fact]
    public async Task Signed_crossex_requests_serialize_bodies_queries_and_headers()
    {
        var responses = new Queue<string>([
            JsonFixture.Read("Docs/CrossEx/transfer_history.success.json"),
            JsonFixture.Read("Docs/CrossEx/transfer.success.json"),
            JsonFixture.Read("Docs/CrossEx/order_action.success.json"),
            JsonFixture.Read("Docs/CrossEx/order.success.json"),
            JsonFixture.Read("Docs/CrossEx/order_action.success.json"),
            JsonFixture.Read("Docs/CrossEx/order_action.success.json"),
            JsonFixture.Read("Docs/CrossEx/convert_quote.success.json"),
            JsonFixture.Read("Docs/CrossEx/convert_order.success.json"),
            JsonFixture.Read("Docs/CrossEx/account.success.json"),
            JsonFixture.Read("Docs/CrossEx/account_update.success.json"),
            JsonFixture.Read("Docs/CrossEx/leverages.success.json"),
            JsonFixture.Read("Docs/CrossEx/leverage.success.json"),
            JsonFixture.Read("Docs/CrossEx/leverages.success.json"),
            JsonFixture.Read("Docs/CrossEx/leverage.success.json"),
            JsonFixture.Read("Docs/CrossEx/order_action.success.json"),
            JsonFixture.Read("Docs/CrossEx/interest_rates.success.json"),
            JsonFixture.Read("Docs/CrossEx/fees.success.json"),
            JsonFixture.Read("Docs/CrossEx/positions.success.json"),
            JsonFixture.Read("Docs/CrossEx/margin_positions.success.json"),
            JsonFixture.Read("Docs/CrossEx/adl_rank.success.json"),
            JsonFixture.Read("Docs/CrossEx/orders.success.json"),
            JsonFixture.Read("Docs/CrossEx/orders.success.json"),
            JsonFixture.Read("Docs/CrossEx/history_positions.success.json"),
            JsonFixture.Read("Docs/CrossEx/history_margin_positions.success.json"),
            JsonFixture.Read("Docs/CrossEx/margin_interest_history.success.json"),
            JsonFixture.Read("Docs/CrossEx/trade_history.success.json"),
            JsonFixture.Read("Docs/CrossEx/account_book.success.json"),
            JsonFixture.Read("Docs/CrossEx/coin_discount_rates.success.json"),
        ]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");
        var secondsFrom = DateTimeOffset.FromUnixTimeSeconds(1739000000).UtcDateTime;
        var secondsTo = DateTimeOffset.FromUnixTimeSeconds(1739086400).UtcDateTime;
        var millisFrom = DateTimeOffset.FromUnixTimeMilliseconds(1739000000123).UtcDateTime;
        var millisTo = DateTimeOffset.FromUnixTimeMilliseconds(1739086400456).UtcDateTime;

        var transferHistory = await client.CrossEx.GetTransferHistoryAsync(new GateCrossExTransferHistoryQueryRequest
        {
            Coin = "USDT",
            OrderId = "t-cross-transfer",
            From = secondsFrom,
            To = secondsTo,
            Page = 2,
            Limit = 50,
        });
        var transfer = await client.CrossEx.TransferAsync(new GateCrossExTransferRequest
        {
            Coin = "USDT",
            Amount = 100.5m,
            From = GateCrossExTransferAccountType.CrossExKraken,
            To = GateCrossExTransferAccountType.Spot,
            Text = "t-cross-transfer",
        });
        var placed = await client.CrossEx.PlaceOrderAsync(new GateCrossExOrderRequest
        {
            Symbol = "GATE_FUTURE_AAVE_USDT",
            Side = GateCrossExOrderSide.Buy,
            Type = GateCrossExOrderType.Limit,
            TimeInForce = GateCrossExTimeInForce.RetailPriceImprovement,
            Quantity = 0.01m,
            Price = 60000m,
            QuoteQuantity = 600m,
            ReduceOnly = true,
            PositionSide = GateCrossExPositionSide.Long,
            Text = "t-cross-order",
        });
        var order = await client.CrossEx.GetOrderAsync("234567");
        var updated = await client.CrossEx.UpdateOrderAsync("234567", new GateCrossExOrderUpdateRequest
        {
            Quantity = 0.02m,
            Price = 60500m,
        });
        var cancelled = await client.CrossEx.CancelOrderAsync("234567");
        var quote = await client.CrossEx.GetConvertQuoteAsync(new GateCrossExConvertQuoteRequest
        {
            ExchangeType = GateCrossExExchangeType.Binance,
            FromCoin = "USDT",
            ToCoin = "BTC",
            FromAmount = 100m,
        });
        var convert = await client.CrossEx.CreateConvertOrderAsync(new GateCrossExConvertOrderRequest
        {
            QuoteId = "cross-quote-001",
        });
        var account = await client.CrossEx.GetAccountAsync(new GateCrossExAccountQueryRequest
        {
            ExchangeType = GateCrossExExchangeType.Hyperliquid,
        });
        var updatedAccount = await client.CrossEx.UpdateAccountAsync(new GateCrossExAccountUpdateRequest
        {
            PositionMode = GateCrossExPositionMode.Dual,
            AccountMode = GateCrossExAccountMode.CrossExchange,
            ExchangeType = GateCrossExExchangeType.Deribit,
        });
        var contractLeverages = await client.CrossEx.GetContractLeveragesAsync(new GateCrossExLeverageQueryRequest
        {
            Symbols = ["BINANCE_FUTURE_BTC_USDT", "BINANCE_FUTURE_ETH_USDT"],
        });
        var contractLeverage = await client.CrossEx.UpdateContractLeverageAsync(new GateCrossExLeverageRequest
        {
            Symbol = "BINANCE_FUTURE_BTC_USDT",
            Leverage = 3m,
        });
        var marginLeverages = await client.CrossEx.GetMarginLeveragesAsync(new GateCrossExLeverageQueryRequest
        {
            Symbols = ["BINANCE_MARGIN_BTC_USDT"],
        });
        var marginLeverage = await client.CrossEx.UpdateMarginLeverageAsync(new GateCrossExLeverageRequest
        {
            Symbol = "BINANCE_MARGIN_BTC_USDT",
            Leverage = 3m,
        });
        var closed = await client.CrossEx.ClosePositionAsync(new GateCrossExClosePositionRequest
        {
            Symbol = "BINANCE_FUTURE_BTC_USDT",
            PositionSide = GateCrossExPositionSide.Long,
        });
        var rates = await client.CrossEx.GetInterestRatesAsync(new GateCrossExCoinExchangeQueryRequest
        {
            Coin = "USDT",
            ExchangeType = GateCrossExExchangeType.Binance,
        });
        var fees = await client.CrossEx.GetFeesAsync();
        var positions = await client.CrossEx.GetPositionsAsync(new GateCrossExPositionQueryRequest
        {
            Symbol = "BINANCE_FUTURE_BTC_USDT",
            ExchangeType = GateCrossExExchangeType.Binance,
        });
        var marginPositions = await client.CrossEx.GetMarginPositionsAsync(new GateCrossExPositionQueryRequest
        {
            Symbol = "BINANCE_MARGIN_BTC_USDT",
            ExchangeType = GateCrossExExchangeType.Binance,
        });
        var adl = await client.CrossEx.GetAdlRankAsync(new GateCrossExAdlRankQueryRequest
        {
            Symbol = "BINANCE_FUTURE_BTC_USDT",
        });
        var openOrders = await client.CrossEx.GetOpenOrdersAsync(new GateCrossExOpenOrdersQueryRequest
        {
            Symbol = "BINANCE_FUTURE_BTC_USDT",
            ExchangeType = GateCrossExExchangeType.Binance,
            BusinessType = GateCrossExBusinessType.Future,
        });
        var historyOrders = await client.CrossEx.GetHistoricalOrdersAsync(new GateCrossExHistoryQueryRequest
        {
            Symbol = "BINANCE_FUTURE_BTC_USDT",
            From = millisFrom,
            To = millisTo,
            Page = 3,
            Limit = 100,
            Attributes =
            [
                GateCrossExOrderAttribute.Common,
                GateCrossExOrderAttribute.LiquidationTakeover,
                GateCrossExOrderAttribute.LiquidationReduction,
                GateCrossExOrderAttribute.AutoDeleverage,
                GateCrossExOrderAttribute.Settlement,
            ],
        });
        var historyPositions = await client.CrossEx.GetHistoricalPositionsAsync(new GateCrossExHistoryQueryRequest
        {
            Symbol = "BINANCE_FUTURE_BTC_USDT",
            From = millisFrom,
            To = millisTo,
            Page = 3,
            Limit = 100,
        });
        var historyMarginPositions = await client.CrossEx.GetHistoricalMarginPositionsAsync(new GateCrossExHistoryQueryRequest
        {
            Symbol = "BINANCE_MARGIN_BTC_USDT",
            From = millisFrom,
            To = millisTo,
            Page = 3,
            Limit = 100,
        });
        var marginInterests = await client.CrossEx.GetMarginInterestHistoryAsync(new GateCrossExMarginInterestHistoryQueryRequest
        {
            Symbol = "BINANCE_MARGIN_BTC_USDT",
            From = millisFrom,
            To = millisTo,
            Page = 3,
            Limit = 100,
            ExchangeType = GateCrossExExchangeType.Binance,
        });
        var trades = await client.CrossEx.GetTradeHistoryAsync(new GateCrossExHistoryQueryRequest
        {
            Symbol = "BINANCE_FUTURE_BTC_USDT",
            From = millisFrom,
            To = millisTo,
            Page = 3,
            Limit = 100,
        });
        var book = await client.CrossEx.GetAccountBookAsync(new GateCrossExAccountBookQueryRequest
        {
            Coin = "USDT",
            StatementType = "FUNDING_FEE",
            From = millisFrom,
            To = millisTo,
            Page = 3,
            Limit = 100,
        });
        var discounts = await client.CrossEx.GetCoinDiscountRatesAsync(new GateCrossExCoinExchangeQueryRequest
        {
            Coin = "USDT",
            ExchangeType = GateCrossExExchangeType.Binance,
        });

        Assert.True(transferHistory.Success, transferHistory.Error?.ToString());
        Assert.True(transfer.Success, transfer.Error?.ToString());
        Assert.True(placed.Success, placed.Error?.ToString());
        Assert.True(order.Success, order.Error?.ToString());
        Assert.True(updated.Success, updated.Error?.ToString());
        Assert.True(cancelled.Success, cancelled.Error?.ToString());
        Assert.True(quote.Success, quote.Error?.ToString());
        Assert.True(convert.Success, convert.Error?.ToString());
        Assert.True(account.Success, account.Error?.ToString());
        Assert.True(updatedAccount.Success, updatedAccount.Error?.ToString());
        Assert.True(contractLeverages.Success, contractLeverages.Error?.ToString());
        Assert.True(contractLeverage.Success, contractLeverage.Error?.ToString());
        Assert.True(marginLeverages.Success, marginLeverages.Error?.ToString());
        Assert.True(marginLeverage.Success, marginLeverage.Error?.ToString());
        Assert.True(closed.Success, closed.Error?.ToString());
        Assert.True(rates.Success, rates.Error?.ToString());
        Assert.True(fees.Success, fees.Error?.ToString());
        Assert.True(positions.Success, positions.Error?.ToString());
        Assert.True(marginPositions.Success, marginPositions.Error?.ToString());
        Assert.True(adl.Success, adl.Error?.ToString());
        Assert.True(openOrders.Success, openOrders.Error?.ToString());
        Assert.True(historyOrders.Success, historyOrders.Error?.ToString());
        Assert.True(historyPositions.Success, historyPositions.Error?.ToString());
        Assert.True(historyMarginPositions.Success, historyMarginPositions.Error?.ToString());
        Assert.True(marginInterests.Success, marginInterests.Error?.ToString());
        Assert.True(trades.Success, trades.Error?.ToString());
        Assert.True(book.Success, book.Error?.ToString());
        Assert.True(discounts.Success, discounts.Error?.ToString());
        Assert.Equal(28, handler.Requests.Count);

        var transferHistoryQuery = ParseQuery(handler.Requests[0].RequestUri);
        Assert.Equal("/api/v4/crossex/transfers", handler.Requests[0].RequestUri.AbsolutePath);
        Assert.Equal("USDT", transferHistoryQuery["coin"]);
        Assert.Equal("t-cross-transfer", transferHistoryQuery["order_id"]);
        Assert.Equal("1739000000", transferHistoryQuery["from"]);
        Assert.Equal("1739086400", transferHistoryQuery["to"]);
        Assert.Equal("2", transferHistoryQuery["page"]);
        Assert.Equal("50", transferHistoryQuery["limit"]);

        var transferBody = ParseBody(handler.Requests[1]);
        Assert.Equal(HttpMethod.Post, handler.Requests[1].Method);
        Assert.Equal("/api/v4/crossex/transfers", handler.Requests[1].RequestUri.AbsolutePath);
        Assert.Equal("USDT", transferBody["coin"]!.ToString());
        Assert.Equal("100.5", transferBody["amount"]!.ToString());
        Assert.Equal("CROSSEX_KRAKEN", transferBody["from"]!.ToString());
        Assert.Equal("SPOT", transferBody["to"]!.ToString());
        Assert.Equal("t-cross-transfer", transferBody["text"]!.ToString());

        var orderBody = ParseBody(handler.Requests[2]);
        Assert.Equal("/api/v4/crossex/orders", handler.Requests[2].RequestUri.AbsolutePath);
        Assert.Equal("GATE_FUTURE_AAVE_USDT", orderBody["symbol"]!.ToString());
        Assert.Equal("BUY", orderBody["side"]!.ToString());
        Assert.Equal("LIMIT", orderBody["type"]!.ToString());
        Assert.Equal("RPI", orderBody["time_in_force"]!.ToString());
        Assert.Equal("0.01", orderBody["qty"]!.ToString());
        Assert.Equal("60000", orderBody["price"]!.ToString());
        Assert.Equal("600", orderBody["quote_qty"]!.ToString());
        Assert.Equal("true", orderBody["reduce_only"]!.ToString());
        Assert.Equal("LONG", orderBody["position_side"]!.ToString());

        Assert.Equal("/api/v4/crossex/orders/234567", handler.Requests[3].RequestUri.AbsolutePath);
        Assert.Equal(HttpMethod.Get, handler.Requests[3].Method);

        var updateBody = ParseBody(handler.Requests[4]);
        Assert.Equal(HttpMethod.Put, handler.Requests[4].Method);
        Assert.Equal("/api/v4/crossex/orders/234567", handler.Requests[4].RequestUri.AbsolutePath);
        Assert.Equal("0.02", updateBody["qty"]!.ToString());
        Assert.Equal("60500", updateBody["price"]!.ToString());

        Assert.Equal(HttpMethod.Delete, handler.Requests[5].Method);
        Assert.Equal("/api/v4/crossex/orders/234567", handler.Requests[5].RequestUri.AbsolutePath);

        var quoteBody = ParseBody(handler.Requests[6]);
        Assert.Equal("/api/v4/crossex/convert/quote", handler.Requests[6].RequestUri.AbsolutePath);
        Assert.Equal("BINANCE", quoteBody["exchange_type"]!.ToString());
        Assert.Equal("USDT", quoteBody["from_coin"]!.ToString());
        Assert.Equal("BTC", quoteBody["to_coin"]!.ToString());
        Assert.Equal("100", quoteBody["from_amount"]!.ToString());

        Assert.Equal("/api/v4/crossex/convert/orders", handler.Requests[7].RequestUri.AbsolutePath);
        Assert.Equal("cross-quote-001", ParseBody(handler.Requests[7])["quote_id"]!.ToString());

        var accountQuery = ParseQuery(handler.Requests[8].RequestUri);
        Assert.Equal("/api/v4/crossex/accounts", handler.Requests[8].RequestUri.AbsolutePath);
        Assert.Equal("HYPERLIQUID", accountQuery["exchange_type"]);

        var accountUpdateBody = ParseBody(handler.Requests[9]);
        Assert.Equal(HttpMethod.Put, handler.Requests[9].Method);
        Assert.Equal("DUAL", accountUpdateBody["position_mode"]!.ToString());
        Assert.Equal("CROSS_EXCHANGE", accountUpdateBody["account_mode"]!.ToString());
        Assert.Equal("DERIBIT", accountUpdateBody["exchange_type"]!.ToString());

        var contractLeverageQuery = ParseQuery(handler.Requests[10].RequestUri);
        Assert.Equal("/api/v4/crossex/positions/leverage", handler.Requests[10].RequestUri.AbsolutePath);
        Assert.Equal("BINANCE_FUTURE_BTC_USDT,BINANCE_FUTURE_ETH_USDT", contractLeverageQuery["symbols"]);
        Assert.Equal("/api/v4/crossex/positions/leverage", handler.Requests[11].RequestUri.AbsolutePath);
        Assert.Equal("3", ParseBody(handler.Requests[11])["leverage"]!.ToString());
        Assert.Equal("/api/v4/crossex/margin_positions/leverage", handler.Requests[12].RequestUri.AbsolutePath);
        Assert.Equal("BINANCE_MARGIN_BTC_USDT", ParseQuery(handler.Requests[12].RequestUri)["symbols"]);
        Assert.Equal("/api/v4/crossex/margin_positions/leverage", handler.Requests[13].RequestUri.AbsolutePath);

        var closeBody = ParseBody(handler.Requests[14]);
        Assert.Equal("/api/v4/crossex/position", handler.Requests[14].RequestUri.AbsolutePath);
        Assert.Equal("BINANCE_FUTURE_BTC_USDT", closeBody["symbol"]!.ToString());
        Assert.Equal("LONG", closeBody["position_side"]!.ToString());

        var interestQuery = ParseQuery(handler.Requests[15].RequestUri);
        Assert.Equal("/api/v4/crossex/interest_rate", handler.Requests[15].RequestUri.AbsolutePath);
        Assert.Equal("USDT", interestQuery["coin"]);
        Assert.Equal("BINANCE", interestQuery["exchange_type"]);
        Assert.Equal("/api/v4/crossex/fee", handler.Requests[16].RequestUri.AbsolutePath);

        var positionsQuery = ParseQuery(handler.Requests[17].RequestUri);
        Assert.Equal("/api/v4/crossex/positions", handler.Requests[17].RequestUri.AbsolutePath);
        Assert.Equal("BINANCE_FUTURE_BTC_USDT", positionsQuery["symbol"]);
        Assert.Equal("BINANCE", positionsQuery["exchange_type"]);
        Assert.Equal("/api/v4/crossex/margin_positions", handler.Requests[18].RequestUri.AbsolutePath);
        Assert.Equal("/api/v4/crossex/adl_rank", handler.Requests[19].RequestUri.AbsolutePath);

        var openOrdersQuery = ParseQuery(handler.Requests[20].RequestUri);
        Assert.Equal("/api/v4/crossex/open_orders", handler.Requests[20].RequestUri.AbsolutePath);
        Assert.Equal("FUTURE", openOrdersQuery["business_type"]);

        var historicalOrdersQuery = ParseQuery(handler.Requests[21].RequestUri);
        Assert.Equal("/api/v4/crossex/history_orders", handler.Requests[21].RequestUri.AbsolutePath);
        Assert.Equal("1739000000123", historicalOrdersQuery["from"]);
        Assert.Equal("1739086400456", historicalOrdersQuery["to"]);
        Assert.Equal("3", historicalOrdersQuery["page"]);
        Assert.Equal("100", historicalOrdersQuery["limit"]);
        Assert.Equal("COMMON,LIQ,REDUCE,ADL,SETTLEMENT", historicalOrdersQuery["attributes"]);
        Assert.Equal("/api/v4/crossex/history_positions", handler.Requests[22].RequestUri.AbsolutePath);
        Assert.Equal("/api/v4/crossex/history_margin_positions", handler.Requests[23].RequestUri.AbsolutePath);
        Assert.Equal("/api/v4/crossex/history_margin_interests", handler.Requests[24].RequestUri.AbsolutePath);
        Assert.Equal("BINANCE", ParseQuery(handler.Requests[24].RequestUri)["exchange_type"]);
        Assert.Equal("/api/v4/crossex/history_trades", handler.Requests[25].RequestUri.AbsolutePath);

        var accountBookQuery = ParseQuery(handler.Requests[26].RequestUri);
        Assert.Equal("/api/v4/crossex/account_book", handler.Requests[26].RequestUri.AbsolutePath);
        Assert.Equal("USDT", accountBookQuery["coin"]);
        Assert.Equal("FUNDING_FEE", accountBookQuery["statement_type"]);
        Assert.Equal("1739000000123", accountBookQuery["from"]);

        var discountsQuery = ParseQuery(handler.Requests[27].RequestUri);
        Assert.Equal("/api/v4/crossex/coin_discount_rate", handler.Requests[27].RequestUri.AbsolutePath);
        Assert.Equal("USDT", discountsQuery["coin"]);
        Assert.Equal("BINANCE", discountsQuery["exchange_type"]);
        Assert.All(handler.Requests, AssertSignedHeaders);
    }

    [Fact]
    public async Task Risk_limit_request_rejects_empty_symbol_lists()
    {
        var client = new GateRestApiClient();

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => client.CrossEx.GetRiskLimitsAsync(new GateCrossExRiskLimitQueryRequest
        {
            Symbols = [],
        }));

        Assert.Equal("Symbols", exception.ParamName);
    }

    [Fact]
    public void Close_position_and_account_book_reject_documented_invalid_inputs_before_io()
    {
        var client = new GateRestApiClient();

        var missingSymbol = Assert.Throws<ArgumentException>(() =>
        {
            _ = client.CrossEx.ClosePositionAsync(new GateCrossExClosePositionRequest());
        });
        var missingMarginSide = Assert.Throws<ArgumentException>(() =>
        {
            _ = client.CrossEx.ClosePositionAsync(new GateCrossExClosePositionRequest
            {
                Symbol = "BINANCE_MARGIN_SOL_USDT",
            });
        });
        var invalidSide = Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            _ = client.CrossEx.ClosePositionAsync(new GateCrossExClosePositionRequest
            {
                Symbol = "BINANCE_FUTURE_SOL_USDT",
                PositionSide = (GateCrossExPositionSide)int.MaxValue,
            });
        });
        var excessiveLimit = Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            _ = client.CrossEx.GetAccountBookAsync(new GateCrossExAccountBookQueryRequest { Limit = 1001 });
        });

        Assert.Equal("Symbol", missingSymbol.ParamName);
        Assert.Equal("PositionSide", missingMarginSide.ParamName);
        Assert.Equal("PositionSide", invalidSide.ParamName);
        Assert.Equal("Limit", excessiveLimit.ParamName);
    }

    [Fact]
    public async Task Positions_and_account_book_omit_all_optional_filters()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse("[]"));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var positions = await client.CrossEx.GetPositionsAsync();
        var accountBook = await client.CrossEx.GetAccountBookAsync();

        Assert.True(positions.Success, positions.Error?.ToString());
        Assert.True(accountBook.Success, accountBook.Error?.ToString());
        Assert.Equal(2, handler.Requests.Count);
        Assert.Equal("/api/v4/crossex/positions", handler.Requests[0].RequestUri.AbsolutePath);
        Assert.Equal("/api/v4/crossex/account_book", handler.Requests[1].RequestUri.AbsolutePath);
        Assert.All(handler.Requests, request =>
        {
            Assert.Empty(request.RequestUri.Query);
            AssertSignedHeaders(request);
        });
    }

    [Fact]
    public async Task Signed_market_data_requests_preserve_optional_comma_separated_symbol_filters()
    {
        var responses = new Queue<string>([
            JsonFixture.Read("Docs/CrossEx/market_tickers.success.json"),
            JsonFixture.Read("Docs/CrossEx/market_tickers.success.json"),
            JsonFixture.Read("Docs/CrossEx/market_funding_info.success.json"),
            JsonFixture.Read("Docs/CrossEx/market_funding_info.success.json"),
        ]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var allTickers = await client.CrossEx.GetMarketTickersAsync();
        var filteredTickers = await client.CrossEx.GetMarketTickersAsync([
            "GATE_FUTURE_BTC_USDT",
            "GATE_SPOT_BTC_USDT",
        ]);
        var allFundingInfo = await client.CrossEx.GetMarketFundingInfoAsync();
        var filteredFundingInfo = await client.CrossEx.GetMarketFundingInfoAsync(new GateCrossExSymbolsQueryRequest
        {
            Symbols = ["BINANCE_FUTURE_BTC_USDT", "KRAKEN_FUTURE_BTC_USD"],
        });

        Assert.True(allTickers.Success, allTickers.Error?.ToString());
        Assert.True(filteredTickers.Success, filteredTickers.Error?.ToString());
        Assert.True(allFundingInfo.Success, allFundingInfo.Error?.ToString());
        Assert.True(filteredFundingInfo.Success, filteredFundingInfo.Error?.ToString());
        Assert.Equal(4, handler.Requests.Count);

        Assert.Equal(HttpMethod.Get, handler.Requests[0].Method);
        Assert.Equal("/api/v4/crossex/market/tickers", handler.Requests[0].RequestUri.AbsolutePath);
        Assert.Empty(handler.Requests[0].RequestUri.Query);
        Assert.Equal(
            "GATE_FUTURE_BTC_USDT,GATE_SPOT_BTC_USDT",
            ParseQuery(handler.Requests[1].RequestUri)["symbols"]);

        Assert.Equal(HttpMethod.Get, handler.Requests[2].Method);
        Assert.Equal("/api/v4/crossex/market/funding_info", handler.Requests[2].RequestUri.AbsolutePath);
        Assert.Empty(handler.Requests[2].RequestUri.Query);
        Assert.Equal(
            "BINANCE_FUTURE_BTC_USDT,KRAKEN_FUTURE_BTC_USD",
            ParseQuery(handler.Requests[3].RequestUri)["symbols"]);
        Assert.All(handler.Requests, AssertSignedHeaders);
    }

    [Fact]
    public void Market_ticker_request_rejects_documented_invalid_margin_symbol_filter()
    {
        var client = new GateRestApiClient();

        var exception = Assert.Throws<ArgumentException>(() =>
        {
            _ = client.CrossEx.GetMarketTickersAsync(["GATE_MARGIN_BTC_USDT"]);
        });

        Assert.Equal("Symbols", exception.ParamName);
    }

    [Fact]
    public async Task Batch_cancel_orders_serializes_documented_array_and_signed_route()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/CrossEx/batch_cancel_orders.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.CrossEx.CancelOrdersAsync([
            new GateCrossExBatchCancelOrderRequest { OrderId = "123456" },
            new GateCrossExBatchCancelOrderRequest { Text = "crossex-test-1" },
            new GateCrossExBatchCancelOrderRequest { OrderId = "234567", Text = "order-id-takes-precedence" },
        ]);

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/crossex/batch_cancel_orders", request.RequestUri.AbsolutePath);
        var body = JArray.Parse(request.Content);
        Assert.Equal("123456", body[0]!["order_id"]!.ToString());
        Assert.Null(body[0]!["text"]);
        Assert.Equal("crossex-test-1", body[1]!["text"]!.ToString());
        Assert.Null(body[1]!["order_id"]);
        Assert.Equal("234567", body[2]!["order_id"]!.ToString());
        Assert.Equal("order-id-takes-precedence", body[2]!["text"]!.ToString());
        AssertSignedHeaders(request);
    }

    [Fact]
    public void Batch_cancel_orders_rejects_items_without_an_identifier()
    {
        var client = new GateRestApiClient();

        var exception = Assert.Throws<ArgumentException>(() =>
        {
            _ = client.CrossEx.CancelOrdersAsync([
                new GateCrossExBatchCancelOrderRequest(),
            ]);
        });

        Assert.Equal("requests", exception.ParamName);
    }

    [Fact]
    public void Adl_rank_rejects_missing_required_symbol()
    {
        var client = new GateRestApiClient();

        var exception = Assert.Throws<ArgumentException>(() =>
        {
            _ = client.CrossEx.GetAdlRankAsync(new GateCrossExAdlRankQueryRequest());
        });

        Assert.Equal("Symbol", exception.ParamName);
    }

    [Fact]
    public void Current_crossex_venue_and_transfer_account_enums_map_to_wire_values()
    {
        Assert.Equal("KRAKEN", MapConverter.GetString(GateCrossExExchangeType.Kraken));
        Assert.Equal("HYPERLIQUID", MapConverter.GetString(GateCrossExExchangeType.Hyperliquid));
        Assert.Equal("DERIBIT", MapConverter.GetString(GateCrossExExchangeType.Deribit));
        Assert.Equal("CROSSEX_KRAKEN", MapConverter.GetString(GateCrossExTransferAccountType.CrossExKraken));
        Assert.Equal("CROSSEX_HYPERLIQUID", MapConverter.GetString(GateCrossExTransferAccountType.CrossExHyperliquid));
        Assert.Equal("CROSSEX_DERIBIT", MapConverter.GetString(GateCrossExTransferAccountType.CrossExDeribit));
    }

    [Theory]
    [InlineData("message")]
    [InlineData("detail")]
    public async Task Crossex_http_errors_preserve_machine_readable_labels_and_documented_message_fields(string messageField)
    {
        var error = new JObject
        {
            ["label"] = "TRADE_INVALID_ORDER_QTY",
            [messageField] = "Invalid order quantity",
        };
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(
            error.ToString(Formatting.None),
            System.Net.HttpStatusCode.BadRequest));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.CrossEx.GetOrderAsync("2072652940337152");

        Assert.False(result.Success);
        Assert.Equal("TRADE_INVALID_ORDER_QTY", Assert.IsType<string>(result.Error!.Data));
        Assert.Equal("Invalid order quantity", result.Error.Message);
    }

    private static GateRestApiClient CreateClient(RecordingHttpMessageHandler handler)
        => new(new GateRestApiClientOptions
        {
            HttpClient = new HttpClient(handler),
        });

    private static HttpResponseMessage JsonResponse(string json, System.Net.HttpStatusCode statusCode = System.Net.HttpStatusCode.OK)
        => new(statusCode)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json"),
        };

    private static JObject ParseBody(RecordedHttpRequest request)
        => JObject.Parse(request.Content);

    private static Dictionary<string, string> ParseQuery(Uri uri)
    {
        return uri.Query.TrimStart('?')
            .Split('&', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Split(['='], 2))
            .ToDictionary(
                x => Uri.UnescapeDataString(x[0]),
                x => x.Length == 1 ? string.Empty : Uri.UnescapeDataString(x[1]));
    }

    private static void AssertNoAuthHeaders(RecordedHttpRequest request)
    {
        Assert.DoesNotContain("KEY", request.Headers.Keys);
        Assert.DoesNotContain("SIGN", request.Headers.Keys);
    }

    private static void AssertSignedHeaders(RecordedHttpRequest request)
    {
        Assert.Equal("key", Assert.Single(request.Headers["KEY"]));
        Assert.NotEmpty(Assert.Single(request.Headers["Timestamp"]));
        Assert.NotEmpty(Assert.Single(request.Headers["SIGN"]));
        Assert.True(request.Headers.ContainsKey("X-Gate-Channel-Id"));
    }
}

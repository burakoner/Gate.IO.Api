using Gate.IO.Api.CrossEx;
using Gate.IO.Api.Tests.Infrastructure;
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
            From = GateCrossExTransferAccountType.Spot,
            To = GateCrossExTransferAccountType.CrossEx,
            Text = "t-cross-transfer",
        });
        var placed = await client.CrossEx.PlaceOrderAsync(new GateCrossExOrderRequest
        {
            Symbol = "BINANCE_FUTURE_BTC_USDT",
            Side = GateCrossExOrderSide.Buy,
            Type = GateCrossExOrderType.Limit,
            TimeInForce = GateCrossExTimeInForce.GoodTillCancelled,
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
            ExchangeType = GateCrossExExchangeType.Binance,
        });
        var updatedAccount = await client.CrossEx.UpdateAccountAsync(new GateCrossExAccountUpdateRequest
        {
            PositionMode = GateCrossExPositionMode.Dual,
            AccountMode = GateCrossExAccountMode.CrossExchange,
            ExchangeType = GateCrossExExchangeType.Binance,
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
            StatementType = "trade",
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
        Assert.Equal("SPOT", transferBody["from"]!.ToString());
        Assert.Equal("CROSSEX", transferBody["to"]!.ToString());
        Assert.Equal("t-cross-transfer", transferBody["text"]!.ToString());

        var orderBody = ParseBody(handler.Requests[2]);
        Assert.Equal("/api/v4/crossex/orders", handler.Requests[2].RequestUri.AbsolutePath);
        Assert.Equal("BINANCE_FUTURE_BTC_USDT", orderBody["symbol"]!.ToString());
        Assert.Equal("BUY", orderBody["side"]!.ToString());
        Assert.Equal("LIMIT", orderBody["type"]!.ToString());
        Assert.Equal("GTC", orderBody["time_in_force"]!.ToString());
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
        Assert.Equal("BINANCE", accountQuery["exchange_type"]);

        var accountUpdateBody = ParseBody(handler.Requests[9]);
        Assert.Equal(HttpMethod.Put, handler.Requests[9].Method);
        Assert.Equal("DUAL", accountUpdateBody["position_mode"]!.ToString());
        Assert.Equal("CROSS_EXCHANGE", accountUpdateBody["account_mode"]!.ToString());
        Assert.Equal("BINANCE", accountUpdateBody["exchange_type"]!.ToString());

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
        Assert.Equal("/api/v4/crossex/history_positions", handler.Requests[22].RequestUri.AbsolutePath);
        Assert.Equal("/api/v4/crossex/history_margin_positions", handler.Requests[23].RequestUri.AbsolutePath);
        Assert.Equal("/api/v4/crossex/history_margin_interests", handler.Requests[24].RequestUri.AbsolutePath);
        Assert.Equal("BINANCE", ParseQuery(handler.Requests[24].RequestUri)["exchange_type"]);
        Assert.Equal("/api/v4/crossex/history_trades", handler.Requests[25].RequestUri.AbsolutePath);

        var accountBookQuery = ParseQuery(handler.Requests[26].RequestUri);
        Assert.Equal("/api/v4/crossex/account_book", handler.Requests[26].RequestUri.AbsolutePath);
        Assert.Equal("USDT", accountBookQuery["coin"]);
        Assert.Equal("trade", accountBookQuery["statement_type"]);
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

    private static GateRestApiClient CreateClient(RecordingHttpMessageHandler handler)
        => new(new GateRestApiClientOptions
        {
            HttpClient = new HttpClient(handler),
        });

    private static HttpResponseMessage JsonResponse(string json)
        => new(System.Net.HttpStatusCode.OK)
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

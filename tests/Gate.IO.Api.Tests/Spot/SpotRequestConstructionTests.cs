using Gate.IO.Api.Spot;
using Gate.IO.Api.Tests.Infrastructure;
using System.Security.Cryptography;
using System.Text;

namespace Gate.IO.Api.Tests.Spot;

[Trait("Category", "Unit")]
public class SpotRequestConstructionTests
{
    [Theory]
    [InlineData(null, false)]
    [InlineData("USDC", false)]
    [InlineData("RLUSD", true)]
    public async Task Single_and_batch_orders_preserve_actual_quote_and_optional_omission(string? quote, bool market)
    {
        var handler = new RecordingHttpMessageHandler(request => request.RequestUri!.AbsolutePath.EndsWith("batch_orders", StringComparison.Ordinal)
            ? JsonResponse(JsonFixture.Read("Docs/Spot/batch_orders.success.json"))
            : JsonResponse(JsonFixture.Read("Docs/Spot/order.success.json"), System.Net.HttpStatusCode.Created));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");
        var order = new GateSpotOrderRequest
        {
            Symbol = "BTC_USD", TradeQuote = quote, ClientOrderId = "t-quote",
            Account = GateSpotAccountType.Unified, Side = GateSpotOrderSide.Buy,
            Type = market ? GateSpotOrderType.Market : GateSpotOrderType.Limit,
            Amount = market ? 100m : 0.001m, Price = market ? null : 65000m,
            TimeInForce = market ? GateSpotTimeInForce.ImmediateOrCancel : null,
        };

        Assert.True((await client.Spot.PlaceOrderAsync(order)).Success);
        Assert.True((await client.Spot.PlaceOrdersAsync([order])).Success);
        Assert.Equal(2, handler.Requests.Count);
        foreach (var request in handler.Requests)
        {
            var body = request.RequestUri.AbsolutePath.EndsWith("batch_orders", StringComparison.Ordinal)
                ? Assert.IsType<JObject>(Assert.Single(JArray.Parse(request.Content))) : JObject.Parse(request.Content);
            Assert.Equal("BTC_USD", body["currency_pair"]!.Value<string>());
            if (quote == null) Assert.Null(body["trade_quote"]);
            else
            {
                Assert.Equal(JTokenType.String, body["trade_quote"]!.Type);
                Assert.Equal(quote, body["trade_quote"]!.Value<string>());
            }
            Assert.Equal(JTokenType.String, body["amount"]!.Type);
            Assert.Equal(order.Amount, body["amount"]!.Value<decimal>());
            if (market) Assert.Null(body["price"]);
            AssertSignedSignature(request);
        }
    }

    [Fact]
    public async Task Bulk_cancel_quote_filter_is_signed_and_partial_failures_are_visible()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse("""
            [{"id":"123","currency_pair":"BTC_USD","succeeded":true,"status":"cancelled","amount":"1"},
             {"id":"124","succeeded":false,"label":"ORDER_NOT_FOUND","message":"Order not found"}]
            """));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");
        var result = await client.Spot.CancelOrdersAsync(new GateSpotCancelOrdersRequest
        {
            Symbol = "BTC_USD", TradeQuote = "USDC", Side = GateSpotOrderSide.Buy,
            Account = GateSpotAccountType.Unified, ActionMode = GateSpotActionMode.Full,
        });

        Assert.True(result.Success);
        Assert.Equal(2, result.Data.Count);
        Assert.True(result.Data[0].Succeeded);
        Assert.False(result.Data[1].Succeeded);
        Assert.Equal("ORDER_NOT_FOUND", result.Data[1].ErrorLabel);
        Assert.Equal("Order not found", result.Data[1].ErrorMessage);
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Delete, request.Method);
        Assert.Equal("/api/v4/spot/orders", request.RequestUri.AbsolutePath);
        var query = ParseQuery(request.RequestUri);
        Assert.Equal(5, query.Count);
        Assert.Equal("BTC_USD", query["currency_pair"]);
        Assert.Equal("USDC", query["trade_quote"]);
        Assert.Equal("buy", query["side"]);
        Assert.Equal("unified", query["account"]);
        Assert.Equal("FULL", query["action_mode"]);
        Assert.Equal(string.Empty, request.Content);
        AssertSignedSignature(request);
    }

    [Fact]
    public async Task Legacy_cancel_calls_and_quote_omission_keep_their_original_scope()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse("[]"));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");
        using var cancellation = new CancellationTokenSource();
        Assert.True((await client.Spot.CancelOrdersAsync("BTC_USD", GateSpotOrderSide.Sell, GateSpotAccountType.Unified, GateSpotActionMode.Result, cancellation.Token)).Success);
        Assert.True((await client.Spot.CancelOrdersAsync(new GateSpotCancelOrdersRequest { Symbol = "BTC_USD" }, cancellation.Token)).Success);
        Assert.True((await client.Spot.CancelOrdersAsync()).Success);

        Assert.Equal(3, handler.Requests.Count);
        Assert.Equal(4, ParseQuery(handler.Requests[0].RequestUri).Count);
        Assert.Single(ParseQuery(handler.Requests[1].RequestUri));
        Assert.Empty(ParseQuery(handler.Requests[2].RequestUri));
        foreach (var request in handler.Requests)
        {
            Assert.DoesNotContain("trade_quote", ParseQuery(request.RequestUri).Keys);
            AssertSignedSignature(request);
        }
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public async Task Empty_explicit_quotes_cannot_silently_broaden_cancellation_or_choose_an_order_quote(string quote)
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse("[]"));
        var client = CreateClient(handler);
        var order = new GateSpotOrderRequest { Symbol = "BTC_USD", Price = 10, Type = GateSpotOrderType.Limit, ClientOrderId = "t-quote", TradeQuote = quote };
        await Assert.ThrowsAsync<ArgumentException>(() => client.Spot.PlaceOrderAsync(order));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Spot.PlaceOrdersAsync([order]));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Spot.CancelOrdersAsync(new GateSpotCancelOrdersRequest { Symbol = "BTC_USD", TradeQuote = quote }));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Spot.CancelOrdersAsync(new GateSpotCancelOrdersRequest { Symbol = quote }));
        Assert.Empty(handler.Requests);
    }

    [Fact]
    public async Task Currency_pair_queries_are_unsigned_and_deserialize_unified_quotes()
    {
        var handler = new RecordingHttpMessageHandler(request => JsonResponse(JsonFixture.Read(
            request.RequestUri!.AbsolutePath.EndsWith("ETH_USDT", StringComparison.Ordinal)
                ? "Docs/Spot/currency_pair.success.json" : "Docs/Spot/currency_pairs.success.json")));
        var client = CreateClient(handler);
        var markets = await client.Spot.GetMarketsAsync();
        var market = await client.Spot.GetMarketAsync("ETH_USDT");
        Assert.True(markets.Success);
        Assert.True(market.Success);
        Assert.Equal(new[] { "USDC", "RLUSD" }, Assert.Single(markets.Data).TradeQuotes);
        Assert.Equal(new[] { "USDC", "RLUSD" }, market.Data.TradeQuotes);
        Assert.Equal("/api/v4/spot/currency_pairs", handler.Requests[0].RequestUri.AbsolutePath);
        Assert.Equal("/api/v4/spot/currency_pairs/ETH_USDT", handler.Requests[1].RequestUri.AbsolutePath);
        foreach (var request in handler.Requests)
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Empty(ParseQuery(request.RequestUri));
            Assert.DoesNotContain("KEY", request.Headers.Keys);
            Assert.DoesNotContain("SIGN", request.Headers.Keys);
        }
    }

    [Fact]
    public async Task Public_and_personal_trades_use_documented_queries_and_actual_quote_responses()
    {
        var response = """
            [{"id":"123","create_time_ms":"1548000000123.456","currency_pair":"BTC_USD","trade_quote":"RLUSD","deal":"65000"}]
            """;
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(response));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");
        var from = new DateTime(2026, 9, 1, 0, 0, 0, DateTimeKind.Utc);
        var to = from.AddDays(30);
        var trades = await client.Spot.GetTradesAsync(new GateSpotTradeQueryRequest
        { Symbol = "BTC_USD", From = from, To = to, Limit = 1000, Page = 101, LastId = "122", Reverse = true });
        var history = await client.Spot.GetTradeHistoryAsync(new GateSpotTradeHistoryQueryRequest
        { Symbol = "BTC_USD", From = from, To = to, Limit = 1000, Page = 101, OrderId = 456, Account = GateSpotAccountType.Unified });
        Assert.True(trades.Success);
        Assert.True(history.Success);
        Assert.Equal("RLUSD", Assert.Single(trades.Data).TradeQuote);
        Assert.Equal("RLUSD", Assert.Single(history.Data).TradeQuote);
        Assert.Equal(1548000000123.456m, trades.Data[0].CreateTimeInMillisecondsPrecise);
        Assert.Equal(1548000000123.456m, history.Data[0].CreateTimeInMillisecondsPrecise);
        Assert.Equal(65000m, history.Data[0].Deal);
        Assert.Equal("/api/v4/spot/trades", handler.Requests[0].RequestUri.AbsolutePath);
        Assert.Equal("/api/v4/spot/my_trades", handler.Requests[1].RequestUri.AbsolutePath);
        var publicQuery = ParseQuery(handler.Requests[0].RequestUri);
        Assert.Equal(7, publicQuery.Count);
        Assert.Equal("122", publicQuery["last_id"]);
        Assert.Equal("true", publicQuery["reverse"]);
        var privateQuery = ParseQuery(handler.Requests[1].RequestUri);
        Assert.Equal(7, privateQuery.Count);
        Assert.Equal("456", privateQuery["order_id"]);
        Assert.Equal("unified", privateQuery["account"]); // Deprecated, but retained for compatibility.
        foreach (var request in handler.Requests)
        {
            var query = ParseQuery(request.RequestUri);
            Assert.Equal("BTC_USD", query["currency_pair"]);
            Assert.Equal("1000", query["limit"]);
            Assert.Equal("101", query["page"]);
            Assert.Equal(new DateTimeOffset(from).ToUnixTimeSeconds().ToString(), query["from"]);
            Assert.Equal(new DateTimeOffset(to).ToUnixTimeSeconds().ToString(), query["to"]);
            Assert.DoesNotContain("trade_quote", query.Keys); // These endpoints document a response field, not a query filter.
        }
        Assert.DoesNotContain("KEY", handler.Requests[0].Headers.Keys);
        AssertSignedSignature(handler.Requests[1]);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1001, 1)]
    [InlineData(100, 0)]
    [InlineData(1000, 102)]
    [InlineData(1000, int.MaxValue)]
    public async Task Trade_pagination_rejects_invalid_limits_and_offsets_before_io(int limit, int page)
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse("[]"));
        var client = CreateClient(handler);
        await Assert.ThrowsAnyAsync<ArgumentException>(() => client.Spot.GetTradesAsync(new GateSpotTradeQueryRequest { Symbol = "BTC_USD", Limit = limit, Page = page }));
        await Assert.ThrowsAnyAsync<ArgumentException>(() => client.Spot.GetTradeHistoryAsync(new GateSpotTradeHistoryQueryRequest { Limit = limit, Page = page }));
        await Assert.ThrowsAnyAsync<ArgumentException>(() => client.Spot.GetTradeHistoryAsync(limit: limit, page: page));
        Assert.Empty(handler.Requests);
    }

    [Fact]
    public async Task Personal_trade_order_filters_and_time_ranges_enforce_current_contract()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse("[]"));
        var client = CreateClient(handler);
        await Assert.ThrowsAsync<ArgumentException>(() => client.Spot.GetTradeHistoryAsync(new GateSpotTradeHistoryQueryRequest { OrderId = 123 }));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Spot.GetTradeHistoryAsync(orderId: 123));
        var from = new DateTime(2026, 9, 1, 0, 0, 0, DateTimeKind.Utc);
        await Assert.ThrowsAsync<ArgumentException>(() => client.Spot.GetTradeHistoryAsync(new GateSpotTradeHistoryQueryRequest { From = from, To = from.AddDays(30).AddSeconds(1) }));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Spot.GetTradeHistoryAsync(from: 0, to: 2592001));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Spot.GetTradeHistoryAsync(from: 2, to: 1));
        Assert.Empty(handler.Requests);
    }

    [Fact]
    public async Task Order_validation_covers_price_hidden_icebergs_and_batch_limits()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse("[]"));
        var client = CreateClient(handler);
        var order = new GateSpotOrderRequest { Symbol = "BTC_USD", Type = GateSpotOrderType.Limit, ClientOrderId = "t-test", Account = GateSpotAccountType.Unified };
        await Assert.ThrowsAsync<ArgumentException>(() => client.Spot.PlaceOrderAsync(order));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Spot.PlaceOrdersAsync([order]));
        order.Price = 10;
        order.Iceberg = -1;
        await Assert.ThrowsAsync<ArgumentException>(() => client.Spot.PlaceOrderAsync(order));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Spot.PlaceOrdersAsync([order]));
        order.Iceberg = null;
        await Assert.ThrowsAsync<ArgumentException>(() => client.Spot.PlaceOrdersAsync([order, order with { Account = GateSpotAccountType.Spot }]));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Spot.PlaceOrdersAsync(Enumerable.Repeat(order, 11)));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Spot.PlaceOrdersAsync(Enumerable.Range(1, 5).Select(i => order with { Symbol = $"COIN{i}_USD" })));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Spot.PlaceOrdersAsync([]));
        Assert.Empty(handler.Requests);
    }

    [Fact]
    public async Task Receive_window_uses_expiration_header_for_order_creation_and_cancellation()
    {
        var handler = new RecordingHttpMessageHandler(request => JsonResponse(request.Method == HttpMethod.Delete ? "[]" : "{\"id\":\"123\"}"));
        var client = new GateRestApiClient(new GateRestApiClientOptions { HttpClient = new HttpClient(handler), ReceiveWindow = TimeSpan.FromSeconds(15) });
        client.SetApiCredentials("key", "secret");
        Assert.True((await client.Spot.PlaceOrderAsync("BTC_USD", GateSpotAccountType.Unified, GateSpotOrderType.Market, GateSpotOrderSide.Buy, GateSpotTimeInForce.ImmediateOrCancel, 100)).Success);
        Assert.True((await client.Spot.CancelOrdersAsync(new GateSpotCancelOrdersRequest { Symbol = "BTC_USD", TradeQuote = "USDC" })).Success);
        foreach (var request in handler.Requests)
        {
            var timestamp = long.Parse(Assert.Single(request.Headers["Timestamp"]));
            var expiration = long.Parse(Assert.Single(request.Headers["x-gate-exptime"]));
            // ApiSharp 4.5.1 rounds the seconds timestamp; the expiration header retains milliseconds.
            Assert.InRange(expiration - timestamp * 1000, 14500, 15500);
            Assert.DoesNotContain("x-gate-exptime", ParseQuery(request.RequestUri).Keys);
            AssertSignedSignature(request);
        }
    }

    [Fact]
    public async Task Batch_boundary_orders_are_enumerated_once_and_sent_unchanged()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse("[]"));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");
        var enumerations = 0;
        IEnumerable<GateSpotOrderRequest> Orders()
        {
            enumerations++;
            for (var market = 0; market < 4; market++)
                for (var order = 0; order < 10; order++)
                    yield return new GateSpotOrderRequest
                    {
                        Symbol = $"COIN{market}_USD", ClientOrderId = $"t-{market}-{order}", TradeQuote = "USDC",
                        Account = GateSpotAccountType.Unified, Type = GateSpotOrderType.Limit,
                        Side = GateSpotOrderSide.Buy, Amount = 1, Price = 10,
                    };
        }
        Assert.True((await client.Spot.PlaceOrdersAsync(Orders())).Success);
        Assert.Equal(1, enumerations);
        var request = Assert.Single(handler.Requests);
        var body = JArray.Parse(request.Content);
        Assert.Equal(40, body.Count);
        Assert.Equal(40, body.Select(x => x["text"]!.Value<string>()).Distinct().Count());
        Assert.All(body, x => Assert.Equal("USDC", x["trade_quote"]!.Value<string>()));
        AssertSignedSignature(request);
    }

    [Fact]
    public async Task Optional_personal_history_filters_remain_omitted_and_lower_limit_boundary_is_valid()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse("[]"));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");
        Assert.True((await client.Spot.GetTradeHistoryAsync(new GateSpotTradeHistoryQueryRequest())).Success);
        Assert.True((await client.Spot.GetTradeHistoryAsync(limit: 1, page: 100001)).Success);
        Assert.True((await client.Spot.GetTradesAsync(new GateSpotTradeQueryRequest { Symbol = "BTC_USD", Limit = 1, Page = 100001, Reverse = false })).Success);
        Assert.Empty(ParseQuery(handler.Requests[0].RequestUri));
        Assert.Equal("100001", ParseQuery(handler.Requests[1].RequestUri)["page"]);
        Assert.Equal("false", ParseQuery(handler.Requests[2].RequestUri)["reverse"]);
        AssertSignedSignature(handler.Requests[0]);
        AssertSignedSignature(handler.Requests[1]);
        Assert.DoesNotContain("KEY", handler.Requests[2].Headers.Keys);
    }

    [Fact]
    public async Task Public_spot_tickers_request_serializes_query_without_authentication_headers()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Spot/tickers.success.json")));
        var client = CreateClient(handler);

        var result = await client.Spot.GetTickersAsync("BTC_USDT", GateSpotTickerTimezone.UTC0);

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/spot/tickers", request.RequestUri.AbsolutePath);

        var query = ParseQuery(request.RequestUri);
        Assert.Equal("BTC_USDT", query["currency_pair"]);
        Assert.Equal("utc0", query["timezone"]);
        Assert.DoesNotContain("KEY", request.Headers.Keys);
        Assert.DoesNotContain("SIGN", request.Headers.Keys);
    }

    [Fact]
    public async Task Public_spot_insurance_history_request_is_unsigned()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Spot/insurance_history.success.json")));
        var client = CreateClient(handler);

        var result = await client.Spot.GetInsuranceHistoryAsync(new GateSpotInsuranceHistoryRequest
        {
            Business = "margin",
            Currency = "BTC",
            From = new DateTime(2024, 9, 23, 8, 2, 27, DateTimeKind.Utc),
            To = new DateTime(2024, 9, 23, 8, 2, 27, DateTimeKind.Utc),
            Page = 1,
            Limit = 1,
        });

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/spot/insurance_history", request.RequestUri.AbsolutePath);

        var query = ParseQuery(request.RequestUri);
        Assert.Equal("margin", query["business"]);
        Assert.Equal("BTC", query["currency"]);
        Assert.Equal("1727078547", query["from"]);
        Assert.Equal("1727078547", query["to"]);
        Assert.DoesNotContain("KEY", request.Headers.Keys);
        Assert.DoesNotContain("SIGN", request.Headers.Keys);
    }

    [Fact]
    public async Task Signed_spot_balance_requests_use_documented_endpoint_and_optional_currency()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Spot/accounts.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var unfilteredResult = await client.Spot.GetBalancesAsync();
        var filteredResult = await client.Spot.GetBalancesAsync("ETH");

        Assert.True(unfilteredResult.Success, unfilteredResult.Error?.ToString());
        Assert.True(filteredResult.Success, filteredResult.Error?.ToString());
        Assert.Equal("ETH", Assert.Single(filteredResult.Data).Currency);
        Assert.Equal(2, handler.Requests.Count);

        var unfilteredRequest = handler.Requests[0];
        Assert.Equal(HttpMethod.Get, unfilteredRequest.Method);
        Assert.Equal("/api/v4/spot/accounts", unfilteredRequest.RequestUri.AbsolutePath);
        Assert.Empty(ParseQuery(unfilteredRequest.RequestUri));
        AssertSignedHeaders(unfilteredRequest);

        var filteredRequest = handler.Requests[1];
        Assert.Equal(HttpMethod.Get, filteredRequest.Method);
        Assert.Equal("/api/v4/spot/accounts", filteredRequest.RequestUri.AbsolutePath);
        Assert.Equal("ETH", ParseQuery(filteredRequest.RequestUri)["currency"]);
        AssertSignedHeaders(filteredRequest);
    }

    [Fact]
    public async Task Signed_spot_order_request_serializes_mapped_body()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Spot/order.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Spot.PlaceOrderAsync(new GateSpotOrderRequest
        {
            ClientOrderId = "t-test",
            Symbol = "BTC_USDT",
            Type = GateSpotOrderType.Limit,
            Account = GateSpotAccountType.Unified,
            Side = GateSpotOrderSide.Buy,
            Amount = 0.001m,
            Price = 65000m,
            TimeInForce = GateSpotTimeInForce.ImmediateOrCancel,
            Iceberg = 0m,
            Slippage = 0.05m,
            AutoBorrow = false,
            AutoRepay = false,
            SelfTradeAction = GateSpotSelfTradeAction.CancelNewest,
            ActionMode = GateSpotActionMode.Full,
            StopProfit = new GateSpotOrderTpsl { TriggerPrice = "67000", OrderPrice = "66900" },
            StopLoss = new GateSpotOrderTpsl { TriggerPrice = "63000", OrderPrice = "62900" },
        });

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/spot/orders", request.RequestUri.AbsolutePath);

        var body = JObject.Parse(request.Content);
        Assert.Equal("t-test", body["text"]!.ToString());
        Assert.Equal("BTC_USDT", body["currency_pair"]!.ToString());
        Assert.Equal("limit", body["type"]!.ToString());
        Assert.Equal("unified", body["account"]!.ToString());
        Assert.Equal("buy", body["side"]!.ToString());
        Assert.Equal("0.001", body["amount"]!.ToString());
        Assert.Equal("65000", body["price"]!.ToString());
        Assert.Equal("ioc", body["time_in_force"]!.ToString());
        Assert.Equal("0.05", body["slippage"]!.ToString());
        Assert.Equal("cn", body["stp_act"]!.ToString());
        Assert.Equal("FULL", body["action_mode"]!.ToString());
        Assert.Equal(JTokenType.String, body["stop_profit"]!["trigger_price"]!.Type);
        Assert.Equal("67000", body["stop_profit"]!["trigger_price"]!.ToString());
        Assert.Equal("66900", body["stop_profit"]!["order_price"]!.ToString());
        Assert.Equal("63000", body["stop_loss"]!["trigger_price"]!.ToString());
        Assert.Equal("62900", body["stop_loss"]!["order_price"]!.ToString());
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Signed_spot_batch_order_request_serializes_tpsl_and_accepts_limit_fok()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Spot/batch_orders.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Spot.PlaceOrdersAsync(
        [
            new GateSpotOrderRequest
            {
                ClientOrderId = "t-batch",
                Symbol = "BTC_USDT",
                Type = GateSpotOrderType.Limit,
                Account = GateSpotAccountType.Unified,
                Side = GateSpotOrderSide.Buy,
                Amount = 0.001m,
                Price = 65000m,
                TimeInForce = GateSpotTimeInForce.FillOrKill,
                Iceberg = 0m,
                Slippage = 0.05m,
                StopProfit = new GateSpotOrderTpsl { TriggerPrice = "67000", OrderPrice = "67000" },
                StopLoss = new GateSpotOrderTpsl { TriggerPrice = "63000", OrderPrice = "63000" },
            },
        ]);

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/spot/batch_orders", request.RequestUri.AbsolutePath);

        var body = JArray.Parse(request.Content);
        var order = Assert.IsType<JObject>(Assert.Single(body));
        Assert.Equal("fok", order["time_in_force"]!.ToString());
        Assert.Equal(JTokenType.String, order["amount"]!.Type);
        Assert.Equal(JTokenType.String, order["price"]!.Type);
        Assert.Equal(JTokenType.String, order["iceberg"]!.Type);
        Assert.Equal(JTokenType.String, order["slippage"]!.Type);
        Assert.Equal("67000", order["stop_profit"]!["trigger_price"]!.ToString());
        Assert.Equal("63000", order["stop_loss"]!["trigger_price"]!.ToString());
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Signed_spot_amend_requests_preserve_update_cancel_and_unchanged_tpsl_semantics()
    {
        var handler = new RecordingHttpMessageHandler(request => request.RequestUri!.AbsolutePath.EndsWith("amend_batch_orders", StringComparison.Ordinal)
            ? JsonResponse(JsonFixture.Read("Docs/Spot/batch_orders.success.json"))
            : JsonResponse(JsonFixture.Read("Docs/Spot/order.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var singleResult = await client.Spot.AmendOrderAsync(new GateSpotAmendRequest
        {
            Symbol = "BTC_USDT",
            OrderId = 121212,
            Amount = "1",
            StopProfit = new GateSpotOrderTpsl(),
            StopLoss = null,
        });
        var batchResult = await client.Spot.AmendOrdersAsync(
        [
            new GateSpotAmendRequest
            {
                Symbol = "BTC_USDT",
                ClientOrderId = "t-batch-amend",
                Price = "65001",
                StopProfit = new GateSpotOrderTpsl { TriggerPrice = "67000", OrderPrice = "67000" },
                StopLoss = new GateSpotOrderTpsl(),
            },
            new GateSpotAmendRequest
            {
                Symbol = "ETH_USDT",
                OrderId = 121213,
                Amount = "2",
            },
        ]);

        Assert.True(singleResult.Success, singleResult.Error?.ToString());
        Assert.True(batchResult.Success, batchResult.Error?.ToString());
        Assert.Equal(2, handler.Requests.Count);

        var singleRequest = handler.Requests[0];
        Assert.Equal(HttpMethod.Patch, singleRequest.Method);
        Assert.Equal("/api/v4/spot/orders/121212", singleRequest.RequestUri.AbsolutePath);
        var singleBody = JObject.Parse(singleRequest.Content);
        Assert.Empty(Assert.IsType<JObject>(singleBody["stop_profit"]));
        Assert.Null(singleBody["stop_loss"]);

        var batchRequest = handler.Requests[1];
        Assert.Equal(HttpMethod.Post, batchRequest.Method);
        Assert.Equal("/api/v4/spot/amend_batch_orders", batchRequest.RequestUri.AbsolutePath);
        var batchBody = JArray.Parse(batchRequest.Content);
        Assert.Equal(2, batchBody.Count);
        var batchItem = Assert.IsType<JObject>(batchBody[0]);
        Assert.Equal(JTokenType.String, batchItem["order_id"]!.Type);
        Assert.Equal("t-batch-amend", batchItem["order_id"]!.ToString());
        Assert.Null(batchItem["text"]);
        Assert.Equal("67000", batchItem["stop_profit"]!["trigger_price"]!.ToString());
        Assert.Empty(Assert.IsType<JObject>(batchItem["stop_loss"]));
        var numericBatchItem = Assert.IsType<JObject>(batchBody[1]);
        Assert.Equal(JTokenType.String, numericBatchItem["order_id"]!.Type);
        Assert.Equal("121213", numericBatchItem["order_id"]!.ToString());
        AssertSignedHeaders(singleRequest);
        AssertSignedHeaders(batchRequest);
    }

    [Fact]
    public async Task Signed_spot_order_query_serializes_filters()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse($"[{JsonFixture.Read("Docs/Spot/order.success.json")}]"));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Spot.GetOrdersAsync(new GateSpotOrderQueryRequest
        {
            Symbol = "BTC_USDT",
            Status = GateSpotOrderQueryStatus.Finished,
            Account = GateSpotAccountType.CrossMargin,
            Side = GateSpotOrderSide.Sell,
            From = new DateTime(2024, 1, 2, 3, 4, 5, DateTimeKind.Utc),
            To = new DateTime(2024, 1, 3, 3, 4, 5, DateTimeKind.Utc),
            Page = 2,
            Limit = 50,
        });

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/spot/orders", request.RequestUri.AbsolutePath);

        var query = ParseQuery(request.RequestUri);
        Assert.Equal("BTC_USDT", query["currency_pair"]);
        Assert.Equal("finished", query["status"]);
        Assert.Equal("cross_margin", query["account"]);
        Assert.Equal("sell", query["side"]);
        Assert.Equal("1704164645", query["from"]);
        Assert.Equal("1704251045", query["to"]);
        Assert.Equal("2", query["page"]);
        Assert.Equal("50", query["limit"]);
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Signed_spot_price_triggered_order_request_serializes_nested_body()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Spot/price_order_id.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Spot.PlacePriceTriggeredOrderAsync(new GateSpotPriceTriggeredOrderRequest
        {
            Symbol = "GT_USDT",
            Trigger = new GateSpotTriggerPrice
            {
                Price = "1.5",
                Rule = GateSpotTriggerCondition.GreaterThanOrEqualTo,
                Expiration = 86400,
            },
            Order = new GateSpotTriggerOrder
            {
                Account = GateSpotPriceTriggeredOrderAccountType.Normal,
                Type = GateSpotOrderType.Limit,
                Side = GateSpotOrderSide.Buy,
                TimeInForce = GateSpotTriggerTimeInForce.GoodTillCancelled,
                Amount = "10",
                Price = "1.4",
                ClientOrderId = "t-auto",
            },
        });

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal(1432329, result.Data);
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/spot/price_orders", request.RequestUri.AbsolutePath);

        var body = JObject.Parse(request.Content);
        Assert.Equal("GT_USDT", body["market"]!.ToString());
        Assert.Equal(">=", body["trigger"]!["rule"]!.ToString());
        Assert.Equal("normal", body["put"]!["account"]!.ToString());
        Assert.Equal("limit", body["put"]!["type"]!.ToString());
        Assert.Equal("buy", body["put"]!["side"]!.ToString());
        Assert.Equal("gtc", body["put"]!["time_in_force"]!.ToString());
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Signed_spot_pov_list_requests_send_required_default_and_documented_filters()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse($"[{JsonFixture.Read("Docs/Spot/pov_order.success.json")}]"));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var defaultResult = await client.Spot.GetPovOrdersAsync();
        var filteredResult = await client.Spot.GetPovOrdersAsync(new GateSpotPovOrderQueryRequest
        {
            Symbol = "BTC_USDT",
            Status = GateSpotOrderQueryStatus.Finished,
            Side = GateSpotOrderSide.Sell,
            Page = 100,
            Limit = 50,
        });

        Assert.True(defaultResult.Success, defaultResult.Error?.ToString());
        Assert.True(filteredResult.Success, filteredResult.Error?.ToString());
        Assert.Equal(2, handler.Requests.Count);

        var defaultRequest = handler.Requests[0];
        Assert.Equal(HttpMethod.Get, defaultRequest.Method);
        Assert.Equal("/api/v4/spot/pov_orders", defaultRequest.RequestUri.AbsolutePath);
        var defaultQuery = ParseQuery(defaultRequest.RequestUri);
        Assert.Equal("open", defaultQuery["status"]);
        Assert.Single(defaultQuery);
        AssertSignedHeaders(defaultRequest);

        var filteredRequest = handler.Requests[1];
        Assert.Equal(HttpMethod.Get, filteredRequest.Method);
        Assert.Equal("/api/v4/spot/pov_orders", filteredRequest.RequestUri.AbsolutePath);
        var filteredQuery = ParseQuery(filteredRequest.RequestUri);
        Assert.Equal("BTC_USDT", filteredQuery["currency_pair"]);
        Assert.Equal("finished", filteredQuery["status"]);
        Assert.Equal("sell", filteredQuery["side"]);
        Assert.Equal("100", filteredQuery["page"]);
        Assert.Equal("50", filteredQuery["limit"]);
        AssertSignedHeaders(filteredRequest);
    }

    [Fact]
    public async Task Signed_spot_pov_create_request_serializes_exact_documented_body()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Spot/pov_order.success.json"), System.Net.HttpStatusCode.Created));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Spot.PlacePovOrderAsync(new GateSpotPovOrderRequest
        {
            Symbol = "BTC_USDT",
            Side = GateSpotOrderSide.Buy,
            Amount = 1m,
            ParticipationRate = GateSpotPovParticipationRate.FivePercent,
            TimeToLive = GateSpotPovTimeToLive.OneHour,
            LimitPrice = 63000m,
            TriggerPrice = 63000m,
            ClientOrderId = "t-pov_1",
        });
        var minimalResult = await client.Spot.PlacePovOrderAsync(new GateSpotPovOrderRequest
        {
            Symbol = "ETH_USDT",
            Side = GateSpotOrderSide.Sell,
            Amount = 1.25m,
            ParticipationRate = GateSpotPovParticipationRate.FortyPercent,
            TimeToLive = GateSpotPovTimeToLive.SevenDays,
        });

        Assert.True(result.Success, result.Error?.ToString());
        Assert.True(minimalResult.Success, minimalResult.Error?.ToString());
        Assert.Equal(2, handler.Requests.Count);
        var request = handler.Requests[0];
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/spot/pov_orders", request.RequestUri.AbsolutePath);
        Assert.Empty(ParseQuery(request.RequestUri));

        var body = JObject.Parse(request.Content);
        Assert.Equal("BTC_USDT", body["currency_pair"]!.ToString());
        Assert.Equal("buy", body["side"]!.ToString());
        Assert.Equal(JTokenType.String, body["amount"]!.Type);
        Assert.Equal("1", body["amount"]!.ToString());
        Assert.Equal(JTokenType.Integer, body["participation_rate"]!.Type);
        Assert.Equal("5", body["participation_rate"]!.ToString());
        Assert.Equal("1h", body["ttl"]!.ToString());
        Assert.Equal("63000", body["limit_price"]!.ToString());
        Assert.Equal("63000", body["trigger_price"]!.ToString());
        Assert.Equal("t-pov_1", body["text"]!.ToString());
        var minimalBody = JObject.Parse(handler.Requests[1].Content);
        Assert.Equal("40", minimalBody["participation_rate"]!.ToString());
        Assert.Equal("7d", minimalBody["ttl"]!.ToString());
        Assert.Null(minimalBody["limit_price"]);
        Assert.Null(minimalBody["trigger_price"]);
        Assert.Null(minimalBody["text"]);
        AssertSignedHeaders(request);
        AssertSignedHeaders(handler.Requests[1]);
    }

    [Fact]
    public async Task Signed_spot_pov_detail_and_cancel_requests_use_documented_delete_routes_without_bodies()
    {
        var handler = new RecordingHttpMessageHandler(request => request.Method == HttpMethod.Delete
            && request.RequestUri.AbsolutePath == "/api/v4/spot/pov_orders"
                ? JsonResponse($"[{JsonFixture.Read("Docs/Spot/pov_order.success.json")}]")
                : JsonResponse(JsonFixture.Read("Docs/Spot/pov_order.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var detailResult = await client.Spot.GetPovOrderAsync("t-pov_1");
        var cancelResult = await client.Spot.CancelPovOrderAsync("1216");
        var cancelAllResult = await client.Spot.CancelPovOrdersAsync("BTC_USDT");
        var cancelEveryResult = await client.Spot.CancelPovOrdersAsync();

        Assert.True(detailResult.Success, detailResult.Error?.ToString());
        Assert.True(cancelResult.Success, cancelResult.Error?.ToString());
        Assert.True(cancelAllResult.Success, cancelAllResult.Error?.ToString());
        Assert.True(cancelEveryResult.Success, cancelEveryResult.Error?.ToString());
        Assert.Equal(4, handler.Requests.Count);

        Assert.Equal(HttpMethod.Get, handler.Requests[0].Method);
        Assert.Equal("/api/v4/spot/pov_orders/t-pov_1", handler.Requests[0].RequestUri.AbsolutePath);
        Assert.Equal(HttpMethod.Delete, handler.Requests[1].Method);
        Assert.Equal("/api/v4/spot/pov_orders/1216", handler.Requests[1].RequestUri.AbsolutePath);
        Assert.Empty(ParseQuery(handler.Requests[1].RequestUri));
        Assert.True(string.IsNullOrEmpty(handler.Requests[1].Content));
        Assert.Equal(HttpMethod.Delete, handler.Requests[2].Method);
        Assert.Equal("/api/v4/spot/pov_orders", handler.Requests[2].RequestUri.AbsolutePath);
        var cancelQuery = ParseQuery(handler.Requests[2].RequestUri);
        Assert.Equal("BTC_USDT", cancelQuery["currency_pair"]);
        Assert.Single(cancelQuery);
        Assert.True(string.IsNullOrEmpty(handler.Requests[2].Content));
        Assert.Equal(HttpMethod.Delete, handler.Requests[3].Method);
        Assert.Equal("/api/v4/spot/pov_orders", handler.Requests[3].RequestUri.AbsolutePath);
        Assert.Empty(ParseQuery(handler.Requests[3].RequestUri));
        Assert.True(string.IsNullOrEmpty(handler.Requests[3].Content));
        Assert.All(handler.Requests, AssertSignedHeaders);
        Assert.All(handler.Requests.Skip(1), AssertSignedSignature);
        Assert.Equal(GateSpotPovOrderStatus.Created, cancelResult.Data.Status);
        Assert.Equal(GateSpotPovOrderStatus.Created, Assert.Single(cancelAllResult.Data).Status);
        Assert.Equal(GateSpotPovOrderStatus.Created, Assert.Single(cancelEveryResult.Data).Status);
    }

    [Theory]
    [InlineData("1216", "1216")]
    [InlineData("9223372036854775808", "9223372036854775808")]
    [InlineData("  t-pov_1-a.b  ", "t-pov_1-a.b")]
    public async Task Signed_spot_pov_single_cancel_accepts_exchange_and_custom_string_ids(string orderId, string expectedId)
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Spot/pov_order.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Spot.CancelPovOrderAsync(orderId);

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Delete, request.Method);
        Assert.Equal($"/api/v4/spot/pov_orders/{expectedId}", request.RequestUri.AbsolutePath);
        Assert.Empty(ParseQuery(request.RequestUri));
        Assert.True(string.IsNullOrEmpty(request.Content));
        AssertSignedSignature(request);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" \t ")]
    public async Task Spot_pov_single_cancel_rejects_missing_order_ids_before_network_io(string? orderId)
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Spot/pov_order.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        await Assert.ThrowsAsync<ArgumentException>(() => client.Spot.CancelPovOrderAsync(orderId!));

        Assert.Empty(handler.Requests);
    }

    [Theory]
    [InlineData(".")]
    [InlineData("..")]
    [InlineData("1216/..")]
    [InlineData("1216\\..")]
    [InlineData("1216?currency_pair=BTC_USDT")]
    [InlineData("1216#fragment")]
    [InlineData("%2e")]
    [InlineData("%2e%2e")]
    [InlineData("12\n16")]
    [InlineData("12\r16")]
    [InlineData("12\t16")]
    public async Task Spot_pov_single_cancel_rejects_routing_syntax_before_network_io(string orderId)
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Spot/pov_order.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var exception = await Record.ExceptionAsync(() => client.Spot.CancelPovOrderAsync(orderId));

        Assert.True(handler.Requests.Count == 0,
            $"Unexpected request: {string.Join(", ", handler.Requests.Select(x => $"{x.Method} {x.RequestUri.AbsolutePath}"))}");
        Assert.IsType<ArgumentException>(exception);
    }

    [Fact]
    public async Task Signed_spot_pov_bulk_cancel_preserves_an_empty_order_list()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse("[]"));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Spot.CancelPovOrdersAsync("BTC_USDT");

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Empty(result.Data);
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Delete, request.Method);
        Assert.Equal("/api/v4/spot/pov_orders", request.RequestUri.AbsolutePath);
        AssertSignedSignature(request);
    }

    [Fact]
    public async Task Spot_pov_validation_rejects_invalid_financial_inputs_before_network_io()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Spot/pov_order.success.json")));
        var client = CreateClient(handler);

        await Assert.ThrowsAsync<ArgumentException>(() => client.Spot.GetPovOrdersAsync(new GateSpotPovOrderQueryRequest { Page = 101 }));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Spot.GetPovOrdersAsync(new GateSpotPovOrderQueryRequest { Limit = 1001 }));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Spot.PlacePovOrderAsync(new GateSpotPovOrderRequest
        {
            Symbol = "BTC_USDT",
            Side = GateSpotOrderSide.Buy,
            ParticipationRate = GateSpotPovParticipationRate.FivePercent,
            TimeToLive = GateSpotPovTimeToLive.OneHour,
        }));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.Spot.PlacePovOrderAsync(new GateSpotPovOrderRequest
        {
            Symbol = "BTC_USDT",
            Side = GateSpotOrderSide.Buy,
            Amount = 1m,
            ParticipationRate = (GateSpotPovParticipationRate)7,
            TimeToLive = GateSpotPovTimeToLive.OneHour,
        }));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Spot.PlacePovOrderAsync(new GateSpotPovOrderRequest
        {
            Symbol = "BTC_USDT",
            Side = GateSpotOrderSide.Buy,
            Amount = 1m,
            ParticipationRate = GateSpotPovParticipationRate.FivePercent,
            TimeToLive = GateSpotPovTimeToLive.OneHour,
            ClientOrderId = "invalid",
        }));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Spot.GetPovOrderAsync(" "));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Spot.CancelPovOrderAsync(string.Empty));

        Assert.Empty(handler.Requests);
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

    private static Dictionary<string, string> ParseQuery(Uri uri)
    {
        return uri.Query.TrimStart('?')
            .Split('&', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Split(['='], 2))
            .ToDictionary(
                x => Uri.UnescapeDataString(x[0]),
                x => x.Length == 1 ? string.Empty : Uri.UnescapeDataString(x[1]));
    }

    private static void AssertSignedHeaders(RecordedHttpRequest request)
    {
        Assert.Equal("key", Assert.Single(request.Headers["KEY"]));
        Assert.NotEmpty(Assert.Single(request.Headers["Timestamp"]));
        Assert.NotEmpty(Assert.Single(request.Headers["SIGN"]));
        Assert.True(request.Headers.ContainsKey("X-Gate-Channel-Id"));
    }

    private static void AssertSignedSignature(RecordedHttpRequest request)
    {
        AssertSignedHeaders(request);
        var timestamp = Assert.Single(request.Headers["Timestamp"]);
        var bodyHash = Convert.ToHexString(SHA512.HashData(Encoding.UTF8.GetBytes(request.Content))).ToLowerInvariant();
        var query = System.Net.WebUtility.UrlDecode(request.RequestUri.Query.TrimStart('?'));
        var signatureInput = $"{request.Method.Method}\n{request.RequestUri.AbsolutePath}\n{query}\n{bodyHash}\n{timestamp}";
        using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes("secret"));
        var expectedSignature = Convert.ToHexString(hmac.ComputeHash(Encoding.UTF8.GetBytes(signatureInput))).ToLowerInvariant();

        Assert.Equal(expectedSignature, Assert.Single(request.Headers["SIGN"]));
    }
}

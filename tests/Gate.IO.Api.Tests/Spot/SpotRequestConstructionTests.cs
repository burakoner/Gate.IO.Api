using Gate.IO.Api.Spot;
using Gate.IO.Api.Tests.Infrastructure;
using System.Text;

namespace Gate.IO.Api.Tests.Spot;

[Trait("Category", "Unit")]
public class SpotRequestConstructionTests
{
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
    public async Task Signed_spot_pov_detail_and_cancel_requests_use_documented_post_routes_without_bodies()
    {
        var handler = new RecordingHttpMessageHandler(request => request.RequestUri!.AbsolutePath.EndsWith("/cancel", StringComparison.Ordinal)
            && request.RequestUri.AbsolutePath == "/api/v4/spot/pov_orders/cancel"
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
        Assert.Equal(HttpMethod.Post, handler.Requests[1].Method);
        Assert.Equal("/api/v4/spot/pov_orders/1216/cancel", handler.Requests[1].RequestUri.AbsolutePath);
        Assert.True(string.IsNullOrEmpty(handler.Requests[1].Content));
        Assert.Equal(HttpMethod.Post, handler.Requests[2].Method);
        Assert.Equal("/api/v4/spot/pov_orders/cancel", handler.Requests[2].RequestUri.AbsolutePath);
        Assert.Equal("BTC_USDT", ParseQuery(handler.Requests[2].RequestUri)["currency_pair"]);
        Assert.True(string.IsNullOrEmpty(handler.Requests[2].Content));
        Assert.Equal(HttpMethod.Post, handler.Requests[3].Method);
        Assert.Equal("/api/v4/spot/pov_orders/cancel", handler.Requests[3].RequestUri.AbsolutePath);
        Assert.Empty(ParseQuery(handler.Requests[3].RequestUri));
        Assert.True(string.IsNullOrEmpty(handler.Requests[3].Content));
        Assert.All(handler.Requests, AssertSignedHeaders);
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
}

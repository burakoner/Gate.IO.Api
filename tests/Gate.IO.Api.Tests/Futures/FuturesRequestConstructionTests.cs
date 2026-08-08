using Gate.IO.Api.Futures;
using Gate.IO.Api.Spot;
using Gate.IO.Api.Tests.Infrastructure;
using System.Text;

namespace Gate.IO.Api.Tests.Futures;

[Trait("Category", "Unit")]
public class FuturesRequestConstructionTests
{
    [Fact]
    public async Task Public_futures_contracts_request_serializes_query_without_authentication_headers()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Futures/contracts.success.json")));
        var client = CreateClient(handler);

        var result = await client.Futures.USDT.GetContractsAsync(limit: 1, offset: 2);

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/futures/usdt/contracts", request.RequestUri.AbsolutePath);

        var query = ParseQuery(request.RequestUri);
        Assert.Equal("1", query["limit"]);
        Assert.Equal("2", query["offset"]);
        Assert.DoesNotContain("KEY", request.Headers.Keys);
        Assert.DoesNotContain("SIGN", request.Headers.Keys);
    }

    [Fact]
    public async Task Public_futures_order_book_request_serializes_depth_filters()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Futures/order_book.success.json")));
        var client = CreateClient(handler);

        var result = await client.Futures.USDT.GetOrderBookAsync("BTC_USDT", interval: 0m, limit: 5, withId: true);

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/futures/usdt/order_book", request.RequestUri.AbsolutePath);

        var query = ParseQuery(request.RequestUri);
        Assert.Equal("BTC_USDT", query["contract"]);
        Assert.Equal("0", query["interval"]);
        Assert.Equal("5", query["limit"]);
        Assert.Equal("true", query["with_id"]);
        Assert.DoesNotContain("KEY", request.Headers.Keys);
        Assert.DoesNotContain("SIGN", request.Headers.Keys);
    }

    [Fact]
    public async Task Public_futures_stats_request_can_omit_every_optional_filter()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Futures/contract_stats.success.json")));
        var client = CreateClient(handler);

        var result = await client.Futures.USDT.GetStatsAsync(new GateFuturesStatsQueryRequest
        {
            Contract = "BTC_USDT",
        });

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/futures/usdt/contract_stats", request.RequestUri.AbsolutePath);

        var query = ParseQuery(request.RequestUri);
        Assert.Equal("BTC_USDT", query["contract"]);
        Assert.False(query.ContainsKey("from"));
        Assert.False(query.ContainsKey("interval"));
        Assert.False(query.ContainsKey("limit"));
        Assert.DoesNotContain("KEY", request.Headers.Keys);
        Assert.DoesNotContain("SIGN", request.Headers.Keys);
    }

    [Fact]
    public async Task Signed_futures_positions_request_preserves_optional_paging_contract()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Futures/positions.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var allPositions = await client.Futures.USDT.GetPositionsAsync();
        var pagedPositions = await client.Futures.USDT.GetPositionsAsync(new GateFuturesPositionQueryRequest
        {
            Holding = true,
            Limit = 25,
            Offset = 5,
        });

        Assert.True(allPositions.Success, allPositions.Error?.ToString());
        Assert.True(pagedPositions.Success, pagedPositions.Error?.ToString());

        var unpagedQuery = ParseQuery(handler.Requests[0].RequestUri);
        Assert.False(unpagedQuery.ContainsKey("holding"));
        Assert.False(unpagedQuery.ContainsKey("limit"));
        Assert.False(unpagedQuery.ContainsKey("offset"));

        var pagedQuery = ParseQuery(handler.Requests[1].RequestUri);
        Assert.Equal("True", pagedQuery["holding"]);
        Assert.Equal("25", pagedQuery["limit"]);
        Assert.Equal("5", pagedQuery["offset"]);

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.Futures.USDT.GetPositionsAsync(limit: 101));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.Futures.USDT.GetPositionsAsync(offset: -1));
    }

    [Fact]
    public async Task Public_futures_batch_funding_rate_request_accepts_documented_nested_response()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Futures/funding_rates.success.json")));
        var client = CreateClient(handler);

        var result = await client.Futures.USDT.GetBatchFundingRateHistoryAsync(new GateFuturesBatchFundingRateRequest
        {
            Contracts = ["BTC_USDT"],
        });

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Single(result.Data!);
        Assert.Equal("BTC_USDT", result.Data![0].Contract);
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/futures/usdt/funding_rates", request.RequestUri.AbsolutePath);

        var body = JObject.Parse(request.Content);
        Assert.Equal("BTC_USDT", body["contracts"]![0]!.ToString());
        Assert.DoesNotContain("KEY", request.Headers.Keys);
        Assert.DoesNotContain("SIGN", request.Headers.Keys);
    }

    [Fact]
    public async Task Public_futures_risk_limit_tiers_request_omits_authentication_headers()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Futures/risk_limit_tiers.success.json")));
        var client = CreateClient(handler);

        var result = await client.Futures.USDT.GetRiskLimitTiersAsync("BTC_USDT", limit: 1, offset: 0);

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/futures/usdt/risk_limit_tiers", request.RequestUri.AbsolutePath);

        var query = ParseQuery(request.RequestUri);
        Assert.Equal("BTC_USDT", query["contract"]);
        Assert.Equal("1", query["limit"]);
        Assert.Equal("0", query["offset"]);
        Assert.DoesNotContain("KEY", request.Headers.Keys);
        Assert.DoesNotContain("SIGN", request.Headers.Keys);
    }

    [Fact]
    public async Task Signed_futures_order_request_serializes_mapped_body()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Futures/order.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Futures.USDT.PlaceOrderAsync(new GateFuturesOrderRequest
        {
            Contract = "BTC_USDT",
            Size = 1.5m,
            Iceberg = 0.25m,
            Price = 65000m,
            TimeInForce = GateFuturesTimeInForce.GoodTillCancelled,
            ClientOrderId = "t-test",
            ReduceOnly = false,
            Close = false,
            SelfTradeAction = GateFuturesSelfTradeAction.CancelNewest,
            PositionId = 42,
            MarketOrderSlipRatio = 0.01m,
            PositionMarginMode = GateFuturesPositionMarginMode.Cross,
            ActionMode = GateFuturesActionMode.Full,
            TakeProfitTriggerPrice = 68000m,
            StopLossTriggerPrice = 62000m,
            TakeProfitBboType = "string",
            StopLossBboType = "string",
        });

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal(GateFuturesActionMode.Full, result.Data!.ActionMode);
        Assert.Equal(3800m, result.Data.TakeProfitTriggerPrice);
        Assert.Equal(3700m, result.Data.StopLossTriggerPrice);
        Assert.Equal("string", result.Data.TakeProfitBboType);
        Assert.Equal("string", result.Data.StopLossBboType);
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/futures/usdt/orders", request.RequestUri.AbsolutePath);

        var body = JObject.Parse(request.Content);
        Assert.Equal("BTC_USDT", body["contract"]!.ToString());
        Assert.Equal(JTokenType.String, body["size"]!.Type);
        Assert.Equal("1.5", body["size"]!.ToString());
        Assert.Equal(JTokenType.String, body["iceberg"]!.Type);
        Assert.Equal("0.25", body["iceberg"]!.ToString());
        Assert.Equal(JTokenType.String, body["price"]!.Type);
        Assert.Equal("65000", body["price"]!.ToString());
        Assert.Equal("gtc", body["tif"]!.ToString());
        Assert.Equal("t-test", body["text"]!.ToString());
        Assert.Equal("cn", body["stp_act"]!.ToString());
        Assert.Equal(42, body["pid"]!.Value<long>());
        Assert.Equal("0.01", body["market_order_slip_ratio"]!.ToString());
        Assert.Equal("cross", body["pos_margin_mode"]!.ToString());
        Assert.Equal("FULL", body["action_mode"]!.ToString());
        Assert.Equal("68000", body["tpsl_tp_trigger_price"]!.ToString());
        Assert.Equal("62000", body["tpsl_sl_trigger_price"]!.ToString());
        Assert.Equal("string", body["tpsl_tp_bbo_type"]!.ToString());
        Assert.Equal("string", body["tpsl_sl_bbo_type"]!.ToString());
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Signed_futures_batch_order_request_serializes_decimal_fields_as_strings()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse($"[{JsonFixture.Read("Docs/Futures/order.success.json")}]"));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Futures.USDT.PlaceOrdersAsync([
            new GateFuturesOrderRequest
            {
                Contract = "BTC_USDT",
                Size = 1.5m,
                Iceberg = 0.25m,
                Price = 65000.5m,
                MarketOrderSlipRatio = 0.01m,
                ActionMode = GateFuturesActionMode.Acknowledge,
                TakeProfitBboType = "string",
                StopLossBboType = "string",
            },
        ]);

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/futures/usdt/batch_orders", request.RequestUri.AbsolutePath);

        var body = JArray.Parse(request.Content);
        var order = Assert.IsType<JObject>(Assert.Single(body));
        Assert.Equal(JTokenType.String, order["size"]!.Type);
        Assert.Equal("1.5", order["size"]!.Value<string>());
        Assert.Equal(JTokenType.String, order["iceberg"]!.Type);
        Assert.Equal("0.25", order["iceberg"]!.Value<string>());
        Assert.Equal(JTokenType.String, order["price"]!.Type);
        Assert.Equal("65000.5", order["price"]!.Value<string>());
        Assert.Equal("ACK", order["action_mode"]!.Value<string>());
        Assert.Equal("string", order["tpsl_tp_bbo_type"]!.Value<string>());
        Assert.Equal("string", order["tpsl_sl_bbo_type"]!.Value<string>());
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Signed_futures_order_mutations_serialize_current_action_mode_contracts()
    {
        var orderJson = JsonFixture.Read("Docs/Futures/order.success.json");
        var responses = new Queue<string>([$"[{orderJson}]", orderJson, orderJson, $"[{orderJson}]"]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var cancelAll = await client.Futures.USDT.CancelOrdersAsync(new GateFuturesOrderCancelAllRequest
        {
            ActionMode = GateFuturesActionMode.Acknowledge,
            Side = GateFuturesOrderSide.Ask,
            ExcludeReduceOnly = true,
            Text = "t-cancel-all",
        });
        var cancelOne = await client.Futures.USDT.CancelOrderAsync(
            orderId: 15675394,
            actionMode: GateFuturesActionMode.Result);
        var amendOne = await client.Futures.USDT.AmendOrderAsync(
            orderId: 15675394,
            size: 10.25m,
            price: 5.1m,
            amendText: "t-amend-2",
            text: "t-new-id",
            actionMode: GateFuturesActionMode.Full);
        var amendBatch = await client.Futures.USDT.AmendOrdersAsync([
            new GateFuturesOrderAmendRequest
            {
                OrderId = 15675394,
                Size = 10.25m,
                Price = 5.1m,
                AmendText = "t-amend-3",
                ActionMode = GateFuturesActionMode.Result,
            },
        ]);

        Assert.True(cancelAll.Success, cancelAll.Error?.ToString());
        Assert.True(cancelOne.Success, cancelOne.Error?.ToString());
        Assert.True(amendOne.Success, amendOne.Error?.ToString());
        Assert.True(amendBatch.Success, amendBatch.Error?.ToString());
        Assert.Equal(4, handler.Requests.Count);

        var cancelAllQuery = ParseQuery(handler.Requests[0].RequestUri);
        Assert.Equal(HttpMethod.Delete, handler.Requests[0].Method);
        Assert.Equal("/api/v4/futures/usdt/orders", handler.Requests[0].RequestUri.AbsolutePath);
        Assert.False(cancelAllQuery.ContainsKey("contract"));
        Assert.Equal("ACK", cancelAllQuery["action_mode"]);
        Assert.Equal("ask", cancelAllQuery["side"]);
        Assert.Equal("true", cancelAllQuery["exclude_reduce_only"]);
        Assert.Equal("t-cancel-all", cancelAllQuery["text"]);

        var cancelOneQuery = ParseQuery(handler.Requests[1].RequestUri);
        Assert.Equal(HttpMethod.Delete, handler.Requests[1].Method);
        Assert.Equal("/api/v4/futures/usdt/orders/15675394", handler.Requests[1].RequestUri.AbsolutePath);
        Assert.Equal("RESULT", cancelOneQuery["action_mode"]);

        Assert.Equal(HttpMethod.Put, handler.Requests[2].Method);
        var amendOneBody = JObject.Parse(handler.Requests[2].Content);
        Assert.Equal("10.25", amendOneBody["size"]!.Value<string>());
        Assert.Equal("5.1", amendOneBody["price"]!.Value<string>());
        Assert.Equal("t-amend-2", amendOneBody["amend_text"]!.Value<string>());
        Assert.Equal("t-new-id", amendOneBody["text"]!.Value<string>());
        Assert.Equal("FULL", amendOneBody["action_mode"]!.Value<string>());
        Assert.Null(amendOneBody["biz_info"]);
        Assert.Null(amendOneBody["bbo"]);

        Assert.Equal(HttpMethod.Post, handler.Requests[3].Method);
        Assert.Equal("/api/v4/futures/usdt/batch_amend_orders", handler.Requests[3].RequestUri.AbsolutePath);
        var amendBatchBody = Assert.IsType<JObject>(Assert.Single(JArray.Parse(handler.Requests[3].Content)));
        Assert.Equal(JTokenType.String, amendBatchBody["size"]!.Type);
        Assert.Equal("10.25", amendBatchBody["size"]!.Value<string>());
        Assert.Equal(JTokenType.String, amendBatchBody["price"]!.Type);
        Assert.Equal("RESULT", amendBatchBody["action_mode"]!.Value<string>());

        foreach (var request in handler.Requests)
            AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Signed_futures_price_triggered_order_request_serializes_documented_numeric_trigger_body()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Futures/price_order_id.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Futures.USDT.PlacePriceTriggeredOrderAsync(new GateFuturesPriceTriggeredOrderRequest
        {
            Type = GateFuturesTriggerType.PlanCloseLongPosition,
            PositionMarginMode = GateFuturesPositionMarginMode.Cross,
            Order = new GateFuturesInitial
            {
                Contract = "BTC_USDT",
                Amount = "100.5",
                Price = "5.03",
                Close = false,
                TimeInForce = GateFuturesTimeInForce.GoodTillCancelled,
                ClientOrderId = "t-test",
                ReduceOnly = false,
                AutoSize = GateFuturesOrderAutoSize.CloseLong,
            },
            Trigger = new GateFuturesTrigger
            {
                PriceType = GateFuturesTriggerPrice.DealPrice,
                Price = "3000",
                Rule = GateSpotTriggerCondition.GreaterThanOrEqualTo,
            },
        });

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal(1283293, result.Data!.OrderId);
        Assert.Equal("1283293", result.Data.OrderIdString);
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/futures/usdt/price_orders", request.RequestUri.AbsolutePath);

        var body = JObject.Parse(request.Content);
        Assert.Equal("plan-close-long-position", body["order_type"]!.ToString());
        Assert.Equal("cross", body["pos_margin_mode"]!.ToString());
        Assert.Equal("BTC_USDT", body["initial"]!["contract"]!.ToString());
        Assert.Null(body["initial"]!["size"]);
        Assert.Equal(JTokenType.String, body["initial"]!["amount"]!.Type);
        Assert.Equal("100.5", body["initial"]!["amount"]!.ToString());
        Assert.Equal("gtc", body["initial"]!["tif"]!.ToString());
        Assert.Equal("close_long", body["initial"]!["auto_size"]!.ToString());
        Assert.Null(body["trigger"]!["strategy_type"]);
        Assert.Equal(0, body["trigger"]!["price_type"]!.Value<int>());
        Assert.Equal(1, body["trigger"]!["rule"]!.Value<int>());
        Assert.Null(body["trigger"]!["expiration"]);
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Signed_futures_price_triggered_order_amend_uses_body_order_id_and_current_path()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Futures/price_order_id.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Futures.USDT.AmendPriceTriggeredOrderAsync(new GateFuturesPriceTriggeredOrderUpdateRequest
        {
            OrderId = 1283293,
            Size = 0,
            Amount = "0.5",
            Price = "0",
            TriggerPrice = "988888",
            PriceType = GateFuturesTriggerPrice.DealPrice,
            AutoSize = GateFuturesOrderAutoSize.CloseLong,
            Close = true,
        });

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal(1283293, result.Data!.OrderId);
        Assert.Equal("1283293", result.Data.OrderIdString);
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Put, request.Method);
        Assert.Equal("/api/v4/futures/usdt/price_orders/amend", request.RequestUri.AbsolutePath);

        var body = JObject.Parse(request.Content);
        Assert.Equal(1283293, body["order_id"]!.Value<long>());
        Assert.Equal(0, body["size"]!.Value<long>());
        Assert.Equal(JTokenType.String, body["amount"]!.Type);
        Assert.Equal("0.5", body["amount"]!.Value<string>());
        Assert.Equal("0", body["price"]!.Value<string>());
        Assert.Equal("988888", body["trigger_price"]!.Value<string>());
        Assert.Equal(0, body["price_type"]!.Value<int>());
        Assert.Equal("close_long", body["auto_size"]!.Value<string>());
        Assert.True(body["close"]!.Value<bool>());
        Assert.Null(body["settle"]);
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Signed_futures_chase_order_request_serializes_current_documented_body()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Futures/chase_order_id.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Futures.USDT.PlaceChaseOrderAsync(new GateFuturesChaseOrderRequest
        {
            Contract = "BTC_USDT",
            Amount = "10.5",
            PriceLimit = "0",
            OffsetLimit = "100",
            ReduceOnly = true,
            ClientOrderId = "t-chase-1",
            IsDualMode = true,
            PriceType = GateFuturesChaseOrderPriceType.PriceGap,
            PriceGapType = GateFuturesChaseOrderPriceGapType.Percentage,
            PriceGapValue = "0.1",
            PositionMarginMode = GateFuturesPositionMarginMode.Cross,
            PositionMode = "dual_plus",
        });

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal("9007199254740993", result.Data);
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/futures/usdt/autoorder/v1/chase/create", request.RequestUri.AbsolutePath);

        var body = JObject.Parse(request.Content);
        Assert.Equal("BTC_USDT", body["contract"]!.Value<string>());
        Assert.Equal(JTokenType.String, body["amount"]!.Type);
        Assert.Equal("10.5", body["amount"]!.Value<string>());
        Assert.Equal(JTokenType.String, body["price_limit"]!.Type);
        Assert.Equal("0", body["price_limit"]!.Value<string>());
        Assert.Equal("100", body["offset_limit"]!.Value<string>());
        Assert.True(body["reduce_only"]!.Value<bool>());
        Assert.Equal("t-chase-1", body["text"]!.Value<string>());
        Assert.True(body["is_dual_mode"]!.Value<bool>());
        Assert.Equal(2, body["price_type"]!.Value<int>());
        Assert.Equal(2, body["price_gap_type"]!.Value<int>());
        Assert.Equal("0.1", body["price_gap_value"]!.Value<string>());
        Assert.Equal("cross", body["pos_margin_mode"]!.Value<string>());
        Assert.Equal("dual_plus", body["position_mode"]!.Value<string>());
        Assert.Null(body["settle"]);
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Signed_futures_chase_order_stop_request_serializes_string_order_id()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Futures/chase_order.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Futures.USDT.CancelChaseOrderAsync("9007199254740993");

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal("9007199254740993", result.Data!.OrderId);
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/futures/usdt/autoorder/v1/chase/stop", request.RequestUri.AbsolutePath);

        var body = JObject.Parse(request.Content);
        Assert.Equal(JTokenType.String, body["id"]!.Type);
        Assert.Equal("9007199254740993", body["id"]!.Value<string>());
        Assert.Null(body["text"]);
        Assert.Null(body["settle"]);
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Signed_futures_chase_order_stop_request_accepts_custom_order_tag()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Futures/chase_order.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Futures.USDT.CancelChaseOrderAsync(new GateFuturesChaseOrderCancelRequest
        {
            ClientOrderId = "t-chase-1",
        });

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        var body = JObject.Parse(request.Content);
        Assert.Null(body["id"]);
        Assert.Equal("t-chase-1", body["text"]!.Value<string>());
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Signed_futures_chase_order_batch_stop_request_serializes_current_documented_body()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Futures/chase_orders.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Futures.USDT.CancelChaseOrdersAsync(new GateFuturesChaseOrdersCancelRequest
        {
            Contract = "BTC_USDT",
            PositionMarginMode = GateFuturesPositionMarginMode.Isolated,
        });

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Single(result.Data!);
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/futures/usdt/autoorder/v1/chase/stop_all", request.RequestUri.AbsolutePath);

        var body = JObject.Parse(request.Content);
        Assert.Equal("BTC_USDT", body["contract"]!.Value<string>());
        Assert.Equal("isolated", body["pos_margin_mode"]!.Value<string>());
        Assert.Null(body["settle"]);
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Signed_futures_chase_order_list_request_serializes_all_current_filters()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Futures/chase_orders.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Futures.USDT.GetChaseOrdersAsync(new GateFuturesChaseOrderQueryRequest
        {
            Contract = "BTC_USDT",
            IsFinished = true,
            StartAt = DateTimeOffset.FromUnixTimeSeconds(1778716800).UtcDateTime,
            EndAt = DateTimeOffset.FromUnixTimeSeconds(1778716860).UtcDateTime,
            PageNumber = 2,
            PageSize = 50,
            SortBy = GateFuturesChaseOrderSort.FinishedAt,
            HideCancelled = true,
            ReduceOnly = GateFuturesChaseOrderReduceOnlyFilter.NotReduceOnly,
            Side = GateFuturesChaseOrderSide.Short,
        });

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Single(result.Data!);
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/futures/usdt/autoorder/v1/chase/list", request.RequestUri.AbsolutePath);

        var query = ParseQuery(request.RequestUri);
        Assert.Equal("BTC_USDT", query["contract"]);
        Assert.Equal("true", query["is_finished"]);
        Assert.Equal("1778716800", query["start_at"]);
        Assert.Equal("1778716860", query["end_at"]);
        Assert.Equal("2", query["page_num"]);
        Assert.Equal("50", query["page_size"]);
        Assert.Equal("2", query["sort_by"]);
        Assert.Equal("true", query["hide_cancel"]);
        Assert.Equal("2", query["reduce_only"]);
        Assert.Equal("2", query["side"]);
        AssertSignedHeaders(request);
    }

    [Fact]
    public async Task Signed_futures_chase_order_detail_request_serializes_string_order_id()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(JsonFixture.Read("Docs/Futures/chase_order.success.json")));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");

        var result = await client.Futures.USDT.GetChaseOrderAsync("9007199254740993");

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal("9007199254740993", result.Data!.OrderId);
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/futures/usdt/autoorder/v1/chase/detail", request.RequestUri.AbsolutePath);
        Assert.Equal("9007199254740993", ParseQuery(request.RequestUri)["id"]);
        AssertSignedHeaders(request);
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
        Assert.Equal("1", Assert.Single(request.Headers["X-Gate-Size-Decimal"]));
    }
}

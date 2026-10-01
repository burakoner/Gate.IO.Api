using Gate.IO.Api.Futures;
using Gate.IO.Api.Spot;
using Gate.IO.Api.Tests.Infrastructure;
using Newtonsoft.Json;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Gate.IO.Api.Tests.Futures;

[Trait("Category", "Unit")]
public class FuturesPriceOrderRequestTests
{
    [Theory]
    [InlineData(GateFuturesSettlement.BTC, "btc")]
    [InlineData(GateFuturesSettlement.USDT, "usdt")]
    [InlineData(GateFuturesSettlement.USD1, "usd1")]
    public async Task All_six_price_order_routes_use_selected_settlement_and_sign_the_exact_request(
        GateFuturesSettlement settlement, string wireSettlement)
    {
        var handler = new RecordingHttpMessageHandler(request => request.Method == HttpMethod.Post || request.Method == HttpMethod.Put
            ? JsonResponse(JsonFixture.Read("Docs/Futures/price_order_id.success.json"), request.Method == HttpMethod.Post ? System.Net.HttpStatusCode.Created : System.Net.HttpStatusCode.OK)
            : JsonResponse(JsonFixture.Read(request.RequestUri.AbsolutePath.EndsWith("/price_orders", StringComparison.Ordinal)
                ? "Docs/Futures/price_orders.success.json" : "Docs/Futures/price_order.success.json")));
        using var client = CreateClient(handler);
        var api = client.Futures[settlement];
        var contract = $"BTC_{wireSettlement.ToUpperInvariant()}";
        var create = ValidCreate();
        create.Order.Contract = contract;
        create.Order.Size = -3;
        create.Order.Amount = "-2.500";
        create.Order.Close = false;
        create.Order.ReduceOnly = false;
        create.Order.TimeInForce = GateFuturesTimeInForce.GoodTillCancelled;
        create.Order.AutoSize = GateFuturesOrderAutoSize.CloseLong;
        create.Order.ClientOrderId = "t-test";
        create.Type = GateFuturesTriggerType.PlanCloseLongPosition;
        create.PositionMarginMode = GateFuturesPositionMarginMode.Cross;
        create.Trigger.StrategyType = GateFuturesTriggerStrategy.ByPrice;
        create.Trigger.PriceType = GateFuturesTriggerPrice.IndexPrice;
        create.Trigger.Expiration = 0;
        Assert.True((await api.PlacePriceTriggeredOrderAsync(create)).Success);
        // The body field is optional; when supplied it must agree with the route.
        var amend = JsonConvert.DeserializeObject<GateFuturesPriceTriggeredOrderUpdateRequest>(
            $"{{\"settle\":\"{wireSettlement}\",\"order_id\":9007199254740993,\"size\":0,\"amount\":\"0.500\",\"price\":\"0\",\"trigger_price\":\"988888\",\"price_type\":1,\"auto_size\":\"close_short\",\"close\":false}}")!;
        Assert.True((await api.AmendPriceTriggeredOrderAsync(amend)).Success);
        Assert.True((await api.GetPriceTriggeredOrdersAsync(GateSpotTriggerFilter.Open, contract, 25, 7)).Success);
        Assert.True((await api.CancelPriceTriggeredOrdersAsync(contract)).Success);
        const long id = 9007199254740993;
        Assert.True((await api.GetPriceTriggeredOrderAsync(id)).Success);
        Assert.True((await api.CancelPriceTriggeredOrderAsync(id)).Success);

        var requests = handler.Requests;
        Assert.Equal(6, requests.Count);
        var route = $"/api/v4/futures/{wireSettlement}/price_orders";
        Assert.Equal(new[] { HttpMethod.Post, HttpMethod.Put, HttpMethod.Get, HttpMethod.Delete, HttpMethod.Get, HttpMethod.Delete }, requests.Select(r => r.Method));
        Assert.Equal(new[] { route, route + "/amend", route, route, route + "/" + id, route + "/" + id }, requests.Select(r => r.RequestUri.AbsolutePath));
        Assert.All(requests, AssertExactSignature);
        Assert.All(requests.Skip(2), r => Assert.Empty(r.Content));
        Assert.All(requests.Take(2).Concat(requests.Skip(4)), r => Assert.Empty(r.RequestUri.Query));
        Assert.Equal(new Dictionary<string, string> { ["status"] = "open", ["contract"] = contract, ["limit"] = "25", ["offset"] = "7" }, ParseQuery(requests[2].RequestUri));
        Assert.Equal(new Dictionary<string, string> { ["contract"] = contract }, ParseQuery(requests[3].RequestUri));
        var createBody = JObject.Parse(requests[0].Content);
        Assert.Equal("plan-close-long-position", createBody["order_type"]!.Value<string>());
        Assert.Equal("cross", createBody["pos_margin_mode"]!.Value<string>());
        var initial = (JObject)createBody["initial"]!;
        Assert.Equal(-3, initial["size"]!.Value<long>());
        Assert.Equal("-2.500", initial["amount"]!.Value<string>());
        Assert.Equal(JTokenType.String, initial["price"]!.Type);
        Assert.False(initial["close"]!.Value<bool>());
        Assert.False(initial["reduce_only"]!.Value<bool>());
        Assert.Equal("gtc", initial["tif"]!.Value<string>());
        Assert.Equal("close_long", initial["auto_size"]!.Value<string>());
        Assert.Equal("t-test", initial["text"]!.Value<string>());
        Assert.Null(initial["is_reduce_only"]);
        Assert.Null(initial["is_close"]);
        var trigger = (JObject)createBody["trigger"]!;
        Assert.Equal(0, trigger["strategy_type"]!.Value<int>());
        Assert.Equal(2, trigger["price_type"]!.Value<int>());
        Assert.Equal(2, trigger["rule"]!.Value<int>());
        Assert.Equal(0, trigger["expiration"]!.Value<int>());
        var amendBody = JObject.Parse(requests[1].Content);
        Assert.Equal(wireSettlement, amendBody["settle"]!.Value<string>());
        Assert.Equal(id, amendBody["order_id"]!.Value<long>());
        Assert.Equal(0, amendBody["size"]!.Value<long>());
        Assert.Equal("0.500", amendBody["amount"]!.Value<string>());
        Assert.Equal("0", amendBody["price"]!.Value<string>());
        Assert.Equal("988888", amendBody["trigger_price"]!.Value<string>());
        Assert.Equal(1, amendBody["price_type"]!.Value<int>());
        Assert.Equal("close_short", amendBody["auto_size"]!.Value<string>());
        Assert.False(amendBody["close"]!.Value<bool>());
        Assert.Equal("-2.500", create.Order.Amount); // Neither quantity is normalized or silently removed.
    }

    [Fact]
    public async Task Omitted_fields_keep_server_defaults_and_bulk_scope_explicit()
    {
        var handler = new RecordingHttpMessageHandler(r => JsonResponse(r.Method == HttpMethod.Post || r.Method == HttpMethod.Put ? "{}" : "[]"));
        using var client = CreateClient(handler);
        var createResult = await client.Futures.USD1.PlacePriceTriggeredOrderAsync(ValidCreate());
        Assert.True(createResult.Success);
        Assert.Equal(0, createResult.Data!.OrderId); // An optional/missing ID is not proof of a created order.
        Assert.Null(createResult.Data.OrderIdString);
        Assert.True((await client.Futures.USD1.AmendPriceTriggeredOrderAsync(new() { OrderId = 1 })).Success);
        Assert.True((await client.Futures.USD1.GetPriceTriggeredOrdersAsync(new GateFuturesPriceTriggeredOrderQueryRequest { Status = GateSpotTriggerFilter.Finished })).Success);
        Assert.True((await client.Futures.USD1.CancelPriceTriggeredOrdersAsync()).Success);
        var createBody = JObject.Parse(handler.Requests[0].Content);
        Assert.Equal(new[] { "initial", "trigger" }, createBody.Properties().Select(p => p.Name));
        Assert.Equal(new[] { "contract", "price" }, ((JObject)createBody["initial"]!).Properties().Select(p => p.Name));
        Assert.Equal(new[] { "price", "rule" }, ((JObject)createBody["trigger"]!).Properties().Select(p => p.Name));
        Assert.Equal(new[] { "order_id" }, JObject.Parse(handler.Requests[1].Content).Properties().Select(p => p.Name));
        Assert.Equal(new Dictionary<string, string> { ["status"] = "finished", ["limit"] = "100", ["offset"] = "0" }, ParseQuery(handler.Requests[2].RequestUri));
        Assert.Empty(handler.Requests[3].RequestUri.Query);
        Assert.All(handler.Requests, AssertExactSignature);
    }

    [Theory]
    [InlineData("0")]
    [InlineData("0.000")]
    [InlineData("0e-100")]
    [InlineData(" 0.000 ")]
    public async Task Market_order_creation_requires_explicit_ioc_without_filling_it_in(string price)
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse("{\"id\":1}"));
        using var client = CreateClient(handler);
        var request = ValidCreate();
        request.Order.Price = price;
        await Assert.ThrowsAnyAsync<ArgumentException>(() => client.Futures.USD1.PlacePriceTriggeredOrderAsync(request));
        Assert.Empty(handler.Requests);
        Assert.Null(request.Order.TimeInForce);
        request.Order.TimeInForce = GateFuturesTimeInForce.ImmediateOrCancel;
        Assert.True((await client.Futures.USD1.PlacePriceTriggeredOrderAsync(request)).Success);
        Assert.Equal(price, JObject.Parse(Assert.Single(handler.Requests).Content)["initial"]!["price"]!.Value<string>());
    }

    [Fact]
    public async Task Tiny_nonzero_price_is_not_rounded_into_a_market_order()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse("{\"id\":1}"));
        using var client = CreateClient(handler);
        var request = ValidCreate();
        request.Order.Price = "1e-100";
        Assert.True((await client.Futures.USDT.PlacePriceTriggeredOrderAsync(request)).Success);
        Assert.Equal("1e-100", JObject.Parse(Assert.Single(handler.Requests).Content)["initial"]!["price"]!.Value<string>());
    }

    [Theory]
    [InlineData(GateFuturesTriggerType.CloseLongPosition, "close-long-position")]
    [InlineData(GateFuturesTriggerType.CloseShortPosition, "close-short-position")]
    [InlineData(GateFuturesTriggerType.PlanCloseLongPosition, "plan-close-long-position")]
    [InlineData(GateFuturesTriggerType.PlanCloseShortPosition, "plan-close-short-position")]
    public async Task All_four_writable_types_remain_available_without_inventing_a_position_mode(GateFuturesTriggerType type, string wire)
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse("{\"id\":1}"));
        using var client = CreateClient(handler);
        var request = ValidCreate();
        request.Type = type;
        Assert.True((await client.Futures.USD1.PlacePriceTriggeredOrderAsync(request)).Success);
        var body = JObject.Parse(Assert.Single(handler.Requests).Content);
        Assert.Equal(wire, body["order_type"]!.Value<string>());
        Assert.Null(body["initial"]!["size"]);
        Assert.Null(body["initial"]!["close"]);
        Assert.Null(body["initial"]!["auto_size"]);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task Cancellation_transport_success_does_not_turn_open_or_triggered_orders_into_cancelled_orders(bool bulk)
    {
        var open = JObject.Parse(JsonFixture.Read("Docs/Futures/price_order.success.json"));
        open["status"] = "open";
        open.Property("finish_as")!.Remove();
        open.Property("finish_time")!.Remove();
        var triggered = (JObject)open.DeepClone();
        triggered["id"] = 2;
        triggered["status"] = "finished";
        triggered["finish_as"] = "succeeded";
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(bulk ? new JArray(open, triggered).ToString() : open.ToString()));
        using var client = CreateClient(handler);
        if (bulk)
        {
            var result = await client.Futures.USD1.CancelPriceTriggeredOrdersAsync("BTC_USD1");
            Assert.True(result.Success);
            Assert.Equal(2, result.Data!.Count);
            Assert.Equal(GateFuturesPriceTriggerStatus.Open, result.Data[0].Status);
            Assert.Null(result.Data[0].FinishAs);
            Assert.Equal(GateFuturesOrderFinishAs.Succeeded, result.Data[1].FinishAs);
        }
        else
        {
            var result = await client.Futures.USD1.CancelPriceTriggeredOrderAsync(1);
            Assert.True(result.Success);
            Assert.Equal(GateFuturesPriceTriggerStatus.Open, result.Data!.Status);
            Assert.Null(result.Data.FinishAs);
        }
        Assert.Single(handler.Requests); // No hidden polling, retry, or terminal-state synthesis.
    }

    [Theory]
    [InlineData("list")]
    [InlineData("cancel-all")]
    [InlineData("detail")]
    [InlineData("cancel-one")]
    public async Task Missing_required_response_objects_fail_even_when_http_status_is_200(string operation)
    {
        var handler = new RecordingHttpMessageHandler(r => JsonResponse(r.RequestUri.AbsolutePath.EndsWith("/price_orders", StringComparison.Ordinal) ? "[{}]" : "{}"));
        using var client = CreateClient(handler);
        var api = client.Futures.USD1;
        switch (operation)
        {
            case "list": AssertMalformed(await api.GetPriceTriggeredOrdersAsync(GateSpotTriggerFilter.Open)); break;
            case "cancel-all": AssertMalformed(await api.CancelPriceTriggeredOrdersAsync()); break;
            case "detail": AssertMalformed(await api.GetPriceTriggeredOrderAsync(1)); break;
            case "cancel-one": AssertMalformed(await api.CancelPriceTriggeredOrderAsync(1)); break;
        }
        Assert.Single(handler.Requests);
    }

    public static IEnumerable<object[]> InvalidCreateCases()
    {
        foreach (var field in new[] { "null-request", "null-initial", "null-trigger", "blank-contract", "pipe-contract", "null-price", "blank-price", "null-trigger-price", "blank-trigger-price", "blank-amount", "readonly-long", "readonly-short", "unknown-type", "unknown-margin", "fok", "poc", "unknown-tif", "market-gtc", "spread-strategy", "unknown-strategy", "unknown-price-type", "missing-rule", "unknown-rule", "negative-expiration", "unknown-auto-size", "empty-auto-size", "readonly-is-close", "readonly-is-reduce-only", "response-as-request" })
            yield return new object[] { field };
    }

    [Theory]
    [MemberData(nameof(InvalidCreateCases))]
    public async Task Invalid_creation_is_rejected_before_any_financial_request(string field)
    {
        var request = ValidCreate();
        switch (field)
        {
            case "null-request": request = null!; break;
            case "null-initial": request.Order = null!; break;
            case "null-trigger": request.Trigger = null!; break;
            case "blank-contract": request.Order.Contract = " "; break;
            case "pipe-contract": request.Order.Contract = "BT|_USD1"; break;
            case "null-price": request.Order.Price = null!; break;
            case "blank-price": request.Order.Price = " "; break;
            case "null-trigger-price": request.Trigger.Price = null!; break;
            case "blank-trigger-price": request.Trigger.Price = " "; break;
            case "blank-amount": request.Order.Amount = " "; break;
            case "readonly-long": request.Type = GateFuturesTriggerType.CloseLongOrder; break;
            case "readonly-short": request.Type = GateFuturesTriggerType.CloseShortOrder; break;
            case "unknown-type": request.Type = (GateFuturesTriggerType)200; break;
            case "unknown-margin": request.PositionMarginMode = (GateFuturesPositionMarginMode)200; break;
            case "fok": request.Order.TimeInForce = GateFuturesTimeInForce.FillOrKill; break;
            case "poc": request.Order.TimeInForce = GateFuturesTimeInForce.PendingOrCancelled; break;
            case "unknown-tif": request.Order.TimeInForce = (GateFuturesTimeInForce)200; break;
            case "market-gtc": request.Order.Price = "0"; request.Order.TimeInForce = GateFuturesTimeInForce.GoodTillCancelled; break;
            case "spread-strategy": request.Trigger.StrategyType = GateFuturesTriggerStrategy.ByPriceGap; break;
            case "unknown-strategy": request.Trigger.StrategyType = (GateFuturesTriggerStrategy)200; break;
            case "unknown-price-type": request.Trigger.PriceType = (GateFuturesTriggerPrice)200; break;
            case "missing-rule": request.Trigger.Rule = default; break;
            case "unknown-rule": request.Trigger.Rule = (GateSpotTriggerCondition)200; break;
            case "negative-expiration": request.Trigger.Expiration = -1; break;
            case "unknown-auto-size": request.Order.AutoSize = (GateFuturesOrderAutoSize)200; break;
            case "empty-auto-size": request.Order.AutoSize = GateFuturesOrderAutoSize.None; break;
            case "readonly-is-close": request.Order.IsClose = false; break;
            case "readonly-is-reduce-only": request.Order.IsReduceOnly = true; break;
            case "response-as-request": request = new GateFuturesPriceTriggeredOrder { Order = request.Order, Trigger = request.Trigger }; break;
        }
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse("{\"id\":1}"));
        using var client = CreateClient(handler);
        await Assert.ThrowsAnyAsync<ArgumentException>(() => client.Futures.USD1.PlacePriceTriggeredOrderAsync(request));
        Assert.Empty(handler.Requests);
    }

    [Theory]
    [InlineData("null")]
    [InlineData("zero-id")]
    [InlineData("negative-id")]
    [InlineData("mismatch")]
    [InlineData("unknown-settlement")]
    [InlineData("blank-settlement")]
    [InlineData("whitespace-settlement")]
    [InlineData("uppercase-settlement")]
    [InlineData("price")]
    [InlineData("trigger-price")]
    [InlineData("amount")]
    [InlineData("price-type")]
    [InlineData("auto-size")]
    [InlineData("empty-auto-size")]
    public async Task Invalid_amendment_is_rejected_without_retargeting_or_mutating_it(string field)
    {
        var request = new GateFuturesPriceTriggeredOrderUpdateRequest { OrderId = 1 };
        switch (field)
        {
            case "null": request = null!; break;
            case "zero-id": request.OrderId = 0; break;
            case "negative-id": request.OrderId = -1; break;
            case "mismatch": request = JsonConvert.DeserializeObject<GateFuturesPriceTriggeredOrderUpdateRequest>("{\"settle\":\"usdt\",\"order_id\":1}")!; break;
            case "unknown-settlement": request = JsonConvert.DeserializeObject<GateFuturesPriceTriggeredOrderUpdateRequest>("{\"settle\":\"bogus\",\"order_id\":1}")!; break;
            case "blank-settlement": request.Settlement = ""; break;
            case "whitespace-settlement": request.Settlement = " "; break;
            case "uppercase-settlement": request.Settlement = "USD1"; break;
            case "price": request.Price = " "; break;
            case "trigger-price": request.TriggerPrice = " "; break;
            case "amount": request.Amount = " "; break;
            case "price-type": request.PriceType = (GateFuturesTriggerPrice)200; break;
            case "auto-size": request.AutoSize = (GateFuturesOrderAutoSize)200; break;
            case "empty-auto-size": request.AutoSize = GateFuturesOrderAutoSize.None; break;
        }
        var before = JsonConvert.SerializeObject(request);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse("{\"id\":1}"));
        using var client = CreateClient(handler);
        await Assert.ThrowsAnyAsync<ArgumentException>(() => client.Futures.USD1.AmendPriceTriggeredOrderAsync(request));
        Assert.Empty(handler.Requests);
        Assert.Equal(before, JsonConvert.SerializeObject(request));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task Nonpositive_single_order_ids_do_not_produce_get_or_delete_requests(long id)
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse("{}"));
        using var client = CreateClient(handler);
        await Assert.ThrowsAnyAsync<ArgumentException>(() => client.Futures.USD1.GetPriceTriggeredOrderAsync(id));
        await Assert.ThrowsAnyAsync<ArgumentException>(() => client.Futures.USD1.CancelPriceTriggeredOrderAsync(id));
        Assert.Empty(handler.Requests);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("BTC_USD1 ")]
    [InlineData("BTC_USD1,ETH_USD1")]
    [InlineData("BT|_USD1")]
    [InlineData("BTC_USD1?status=open")]
    public async Task Explicit_invalid_contract_filter_cannot_become_a_broad_cancellation(string contract)
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse("[]"));
        using var client = CreateClient(handler);
        await Assert.ThrowsAnyAsync<ArgumentException>(() => client.Futures.USD1.CancelPriceTriggeredOrdersAsync(contract));
        await Assert.ThrowsAnyAsync<ArgumentException>(() => client.Futures.USD1.GetPriceTriggeredOrdersAsync(GateSpotTriggerFilter.Open, contract));
        Assert.Empty(handler.Requests);
    }

    [Theory]
    [InlineData(0, 100, 0)]
    [InlineData(3, 100, 0)]
    [InlineData(1, 0, 0)]
    [InlineData(1, -1, 0)]
    [InlineData(1, 100, -1)]
    public async Task Invalid_list_status_and_pagination_fail_before_io(int status, int limit, int offset)
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse("[]"));
        using var client = CreateClient(handler);
        await Assert.ThrowsAnyAsync<ArgumentException>(() => client.Futures.USD1.GetPriceTriggeredOrdersAsync((GateSpotTriggerFilter)status, limit: limit, offset: offset));
        Assert.Empty(handler.Requests);
    }

    [Fact]
    public async Task Null_query_request_has_a_deliberate_argument_error()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse("[]"));
        using var client = CreateClient(handler);
        await Assert.ThrowsAsync<ArgumentNullException>(() => client.Futures.USD1.GetPriceTriggeredOrdersAsync((GateFuturesPriceTriggeredOrderQueryRequest)null!));
        Assert.Empty(handler.Requests);
    }

    [Theory]
    [InlineData("X_USD1")]
    [InlineData("S_USD1")]
    [InlineData("1000SHIB_USD1")]
    public async Task Literal_contracts_do_not_require_an_undocumented_two_character_base_asset(string contract)
    {
        var handler = new RecordingHttpMessageHandler(r => JsonResponse(r.Method == HttpMethod.Post ? "{\"id\":1}" : "[]"));
        using var client = CreateClient(handler);
        var request = ValidCreate();
        request.Order.Contract = contract;
        Assert.True((await client.Futures.USD1.PlacePriceTriggeredOrderAsync(request)).Success);
        Assert.True((await client.Futures.USD1.GetPriceTriggeredOrdersAsync(GateSpotTriggerFilter.Open, contract)).Success);
        Assert.True((await client.Futures.USD1.CancelPriceTriggeredOrdersAsync(contract)).Success);
        Assert.Equal(contract, JObject.Parse(handler.Requests[0].Content)["initial"]!["contract"]!.Value<string>());
        Assert.All(handler.Requests.Skip(1), r => Assert.Equal(contract, ParseQuery(r.RequestUri)["contract"]));
    }

    [Theory]
    [InlineData("create")]
    [InlineData("amend")]
    [InlineData("list")]
    [InlineData("cancel-all")]
    [InlineData("detail")]
    [InlineData("cancel-one")]
    public async Task Financial_errors_are_preserved_and_never_automatically_retried(string operation)
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse("{\"label\":\"INVALID_ARGUMENT\",\"message\":\"rejected\"}", System.Net.HttpStatusCode.BadRequest));
        using var client = CreateClient(handler);
        var api = client.Futures.USD1;
        switch (operation)
        {
            case "create": AssertRejected(await api.PlacePriceTriggeredOrderAsync(ValidCreate())); break;
            case "amend": AssertRejected(await api.AmendPriceTriggeredOrderAsync(new() { OrderId = 1 })); break;
            case "list": AssertRejected(await api.GetPriceTriggeredOrdersAsync(GateSpotTriggerFilter.Open)); break;
            case "cancel-all": AssertRejected(await api.CancelPriceTriggeredOrdersAsync()); break;
            case "detail": AssertRejected(await api.GetPriceTriggeredOrderAsync(1)); break;
            case "cancel-one": AssertRejected(await api.CancelPriceTriggeredOrderAsync(1)); break;
        }
        Assert.Single(handler.Requests);
    }

    private static void AssertRejected<T>(ApiSharp.Models.RestCallResult<T> result)
    {
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("rejected", result.Error!.Message);
        Assert.Contains("INVALID_ARGUMENT", result.Error.ToString());
    }

    private static void AssertMalformed<T>(ApiSharp.Models.RestCallResult<T> result)
    {
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.NotNull(result.Error);
    }

    [Fact]
    public async Task Legacy_creation_overload_can_omit_auto_size_without_changing_its_signature()
    {
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse("{\"id\":1}"));
        using var client = CreateClient(handler);
        var result = await client.Futures.USD1.PlacePriceTriggeredOrderAsync(
            GateFuturesTriggerType.PlanCloseShortPosition, GateFuturesTriggerPrice.MarkPrice,
            GateFuturesTriggerStrategy.ByPrice, GateSpotTriggerCondition.GreaterThanOrEqualTo,
            100.01m, TimeSpan.FromMinutes(15), "BTC_USD1", 100m, 25, false,
            GateFuturesTimeInForce.GoodTillCancelled, "t-test", true, GateFuturesOrderAutoSize.None);
        Assert.True(result.Success);
        var body = JObject.Parse(Assert.Single(handler.Requests).Content);
        Assert.Null(body["initial"]!["auto_size"]);
        Assert.Equal(25, body["initial"]!["size"]!.Value<long>());
        Assert.False(body["initial"]!["close"]!.Value<bool>());
        AssertExactSignature(handler.Requests[0]);
    }

    private static GateFuturesPriceTriggeredOrderRequest ValidCreate() => new()
    {
        Order = new GateFuturesInitial { Contract = "BTC_USD1", Price = "5.03" },
        Trigger = new GateFuturesTrigger { Price = "3000", Rule = GateSpotTriggerCondition.LessThanOrEqualTo },
    };

    private static GateRestApiClient CreateClient(RecordingHttpMessageHandler handler)
    {
        var client = new GateRestApiClient(new GateRestApiClientOptions { HttpClient = new HttpClient(handler) });
        client.SetApiCredentials("key", "secret");
        return client;
    }

    private static HttpResponseMessage JsonResponse(string json, System.Net.HttpStatusCode status = System.Net.HttpStatusCode.OK)
        => new(status) { Content = new StringContent(json, Encoding.UTF8, "application/json") };

    private static Dictionary<string, string> ParseQuery(Uri uri) => uri.Query.TrimStart('?').Split('&', StringSplitOptions.RemoveEmptyEntries)
        .Select(x => x.Split('=', 2)).ToDictionary(x => Uri.UnescapeDataString(x[0]), x => x.Length == 1 ? string.Empty : Uri.UnescapeDataString(x[1]));

    private static void AssertExactSignature(RecordedHttpRequest request)
    {
        Assert.Equal("key", Assert.Single(request.Headers["KEY"]));
        Assert.Equal("1", Assert.Single(request.Headers["X-Gate-Size-Decimal"]));
        var timestamp = Assert.Single(request.Headers["Timestamp"]);
        Assert.True(long.TryParse(timestamp, NumberStyles.Integer, CultureInfo.InvariantCulture, out _));
        var bodyHash = Convert.ToHexString(SHA512.HashData(Encoding.UTF8.GetBytes(request.Content))).ToLowerInvariant();
        var signatureInput = $"{request.Method.Method}\n{request.RequestUri.AbsolutePath}\n{request.RequestUri.Query.TrimStart('?')}\n{bodyHash}\n{timestamp}";
        using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes("secret"));
        var signature = Convert.ToHexString(hmac.ComputeHash(Encoding.UTF8.GetBytes(signatureInput))).ToLowerInvariant();
        Assert.Equal(signature, Assert.Single(request.Headers["SIGN"]));
    }
}

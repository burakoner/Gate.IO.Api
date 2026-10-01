using System.Net;
using System.Security.Cryptography;
using System.Text;
using Gate.IO.Api.TradFi;
using Gate.IO.Api.Tests.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Gate.IO.Api.Tests.TradFi;

[Trait("Category", "Contract")]
public class TradFiCurrentContractTests
{
    private static GateTradFiOrderRequest Order() => new()
    {
        Symbol = "EURUSD", Side = GateTradFiOrderSide.Buy,
        PriceType = GateTradFiOrderPriceType.Trigger, Price = 0.9m, Volume = 10m,
    };

    [Theory]
    [InlineData("account")]
    [InlineData("user")]
    [InlineData("assets")]
    [InlineData("commissions")]
    [InlineData("details")]
    [InlineData("order")]
    [InlineData("cancel")]
    public async Task Business_errors_are_not_transport_success(string operation)
    {
        var handler = new RecordingHttpMessageHandler(_ => Response("{\"code\":27,\"label\":\"INVALID_ARGUMENT\",\"message\":\"rejected\",\"data\":null}"));
        using var client = Client(handler);
        var result = await Invoke(client, operation);
        Assert.False(result.Success);
        Assert.Equal(27, result.Error!.Code);
        Assert.Equal("rejected", result.Error.Message);
        Assert.Equal("INVALID_ARGUMENT", result.Error.Data);
        Assert.Equal(HttpStatusCode.OK, result.Response!.StatusCode);
        Assert.Single(handler.Requests);
    }

    [Theory]
    [InlineData("{\"label\":\"INVALID_ARGUMENT\",\"message\":\"rejected\",\"data\":{\"id\":\"117\"}}")]
    [InlineData("{\"code\":0,\"label\":\"INVALID_ARGUMENT\",\"message\":\"rejected\"}")]
    [InlineData("{\"code\":-1,\"message\":\"rejected\"}")]
    public async Task Either_documented_error_indicator_rejects_the_order(string json)
    {
        var handler = new RecordingHttpMessageHandler(_ => Response(json));
        using var client = Client(handler);
        var result = await client.TradFi.PlaceOrderAsync(Order());
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Single(handler.Requests);
    }

    [Theory]
    [InlineData(typeof(GateTradFiMt5Account))]
    [InlineData(typeof(GateTradFiUser))]
    [InlineData(typeof(GateTradFiAccountAssets))]
    public void Removed_mt5_identity_is_not_a_public_or_serialized_zero(Type type)
    {
        Assert.Null(type.GetProperty("Mt5Uid"));
        var value = JsonConvert.DeserializeObject("{\"mt5_uid\":\"10122\"}", type)!;
        Assert.Null(JObject.FromObject(value)["mt5_uid"]);
    }

    [Theory]
    [InlineData("117", 117L)]
    [InlineData("00117", 117L)]
    [InlineData("9007199254740993", 9007199254740993L)]
    [InlineData("9223372036854775807", long.MaxValue)]
    public void Creation_task_id_keeps_the_requested_exact_long_contract(string id, long expected)
    {
        var result = JsonConvert.DeserializeObject<GateTradFiOrderId>(new JObject { ["id"] = id }.ToString())!;
        Assert.Equal(expected, result.Id);
        Assert.Equal(JTokenType.Integer, JObject.FromObject(result)["id"]!.Type);
    }

    [Fact]
    public void Omitted_task_id_keeps_legacy_zero_without_proving_an_identified_task()
        => Assert.Equal(0, JsonConvert.DeserializeObject<GateTradFiOrderId>("{}")!.Id);

    [Theory]
    [InlineData("job-117")]
    [InlineData("9223372036854775808")]
    [InlineData("1.5")]
    public async Task Non_int64_task_ids_are_errors_not_rounded_ids(string id)
    {
        var handler = new RecordingHttpMessageHandler(_ => Response(new JObject { ["data"] = new JObject { ["id"] = id } }.ToString()));
        using var client = Client(handler);
        var result = await client.TradFi.PlaceOrderAsync(Order());
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Single(handler.Requests);
    }

    [Theory]
    [InlineData("25")]
    [InlineData("")]
    [InlineData("future-format")]
    public void Symbol_leverage_preserves_the_documented_string(string leverage)
    {
        var result = JsonConvert.DeserializeObject<GateTradFiSymbolDetails>(new JObject { ["leverage"] = leverage }.ToString())!;
        Assert.Equal(leverage, Assert.IsType<string>((object)result.Leverage));
    }

    [Fact]
    public async Task Invalid_creation_enums_and_required_symbol_fail_before_io()
    {
        var handler = new RecordingHttpMessageHandler(_ => Response("{}"));
        using var client = Client(handler);
        await Assert.ThrowsAsync<ArgumentNullException>(() => client.TradFi.PlaceOrderAsync((GateTradFiOrderRequest)null!));
        foreach (var symbol in new[] { null, "", " " })
            await Assert.ThrowsAnyAsync<ArgumentException>(() => client.TradFi.PlaceOrderAsync(Order() with { Symbol = symbol! }));
        foreach (var side in new[] { (GateTradFiOrderSide)0, (GateTradFiOrderSide)3 })
            await Assert.ThrowsAnyAsync<ArgumentException>(() => client.TradFi.PlaceOrderAsync(Order() with { Side = side }));
        await Assert.ThrowsAnyAsync<ArgumentException>(() => client.TradFi.PlaceOrderAsync(Order() with { PriceType = (GateTradFiOrderPriceType)99 }));
        Assert.Empty(handler.Requests);
    }

    [Fact]
    public async Task Detail_count_cannot_be_bypassed_with_embedded_csv()
    {
        var handler = new RecordingHttpMessageHandler(_ => Response("{\"data\":{\"list\":[]}}"));
        using var client = Client(handler);
        await Assert.ThrowsAnyAsync<ArgumentException>(() => client.TradFi.GetSymbolDetailsAsync(new[] { string.Join(",", Enumerable.Range(0, 11).Select(x => "S" + x)) }));
        Assert.Empty(handler.Requests);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(500)]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task Leverage_is_optional_integer_without_inferred_default_or_symbol_rules(int? leverage)
    {
        var handler = new RecordingHttpMessageHandler(_ => Response("{\"data\":{\"id\":\"117\"}}"));
        using var client = Client(handler);
        var result = await client.TradFi.PlaceOrderAsync(Order() with { Leverage = leverage, TakeProfitPrice = 0, StopLossPrice = 0 });
        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        var body = JObject.Parse(request.Content);
        if (leverage.HasValue)
        {
            Assert.Equal(JTokenType.Integer, body["leverage"]!.Type);
            Assert.Equal(leverage, body["leverage"]!.Value<int>());
        }
        else Assert.Null(body["leverage"]);
        Assert.Equal("0", body["price_tp"]!.ToString());
        Assert.Equal("0", body["price_sl"]!.ToString());
        AssertSignature(request);
    }

    [Fact]
    public async Task Existing_positional_creation_overload_still_omits_leverage_and_optional_prices()
    {
        var handler = new RecordingHttpMessageHandler(_ => Response("{\"data\":{\"id\":\"117\"}}"));
        using var client = Client(handler);
        using var cts = new CancellationTokenSource();
        var result = await client.TradFi.PlaceOrderAsync("EURUSD", GateTradFiOrderSide.Sell, 1, GateTradFiOrderPriceType.Market, 0, null, null, cts.Token);
        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        var body = JObject.Parse(request.Content);
        Assert.Equal(5, body.Count);
        Assert.Equal("market", body["price_type"]!.ToString());
        Assert.Equal(1, body["side"]!.Value<int>());
        AssertSignature(request);
    }

    [Theory]
    [InlineData("account", "/api/v4/tradfi/users/mt5-account", "GET")]
    [InlineData("user", "/api/v4/tradfi/users", "POST")]
    [InlineData("assets", "/api/v4/tradfi/users/assets", "GET")]
    [InlineData("commissions", "/api/v4/tradfi/symbols/commissions", "GET")]
    [InlineData("details", "/api/v4/tradfi/symbols/detail", "GET")]
    [InlineData("order", "/api/v4/tradfi/orders", "POST")]
    public async Task All_six_routes_use_exact_current_auth_and_no_implicit_lead_context(string operation, string path, string method)
    {
        var handler = new RecordingHttpMessageHandler(_ => Response("{\"code\":0,\"label\":\"\",\"data\":{\"list\":[],\"id\":\"117\"}}"));
        using var client = Client(handler);
        var result = await Invoke(client, operation);
        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(path, request.RequestUri.AbsolutePath);
        Assert.Equal(new HttpMethod(method), request.Method);
        Assert.False(request.Headers.ContainsKey("x-gate-trader-copy-type"));
        if (operation is "account" or "user" or "assets")
        {
            Assert.Equal("", request.RequestUri.Query);
            Assert.Equal("", request.Content);
        }
        AssertSignature(request);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task Lead_context_is_captured_request_scoped_and_excludes_activation_and_transfers(bool enabled)
    {
        var handler = new RecordingHttpMessageHandler(_ => Response("{\"data\":{\"list\":[],\"id\":\"117\"}}"));
        using var http = new HttpClient(handler);
        var options = new GateRestApiClientOptions { HttpClient = http, TradFiLeadTrading = enabled };
        using var client = new GateRestApiClient(options);
        client.SetApiCredentials("key", "secret");
        options.TradFiLeadTrading = !enabled;
        foreach (var operation in new[] { "account", "assets", "commissions", "details", "order", "user" })
            Assert.True((await Invoke(client, operation)).Success);
        Assert.True((await client.TradFi.GetTransactionsAsync()).Success);
        Assert.True((await client.TradFi.CreateTransactionAsync("USDT", 1m, GateTradFiTransactionType.Deposit)).Success);
        Assert.True((await client.TradFi.CancelOrderAsync(2630591)).Success);
        foreach (var request in handler.Requests)
        {
            var excluded = request.RequestUri.AbsolutePath is "/api/v4/tradfi/users" or "/api/v4/tradfi/transactions";
            if (enabled && !excluded) Assert.Equal("cfd_copy", Assert.Single(request.Headers["x-gate-trader-copy-type"]));
            else Assert.False(request.Headers.ContainsKey("x-gate-trader-copy-type"));
            AssertSignature(request);
        }
        Assert.False(http.DefaultRequestHeaders.Contains("x-gate-trader-copy-type"));
        handler.Requests.Clear();
        await client.Stock.GetExchangesAsync();
        Assert.False(Assert.Single(handler.Requests).Headers.ContainsKey("x-gate-trader-copy-type"));
    }

    [Fact]
    public async Task Query_guards_preserve_documented_filters_and_limits()
    {
        var handler = new RecordingHttpMessageHandler(_ => Response("{\"data\":{\"list\":[]}}"));
        using var client = Client(handler);
        await Assert.ThrowsAsync<ArgumentNullException>(() => client.TradFi.GetSymbolDetailsAsync((GateTradFiSymbolDetailsRequest)null!));
        await Assert.ThrowsAsync<ArgumentNullException>(() => client.TradFi.GetSymbolCommissionsAsync((GateTradFiSymbolCommissionQueryRequest)null!));
        await Assert.ThrowsAnyAsync<ArgumentException>(() => client.TradFi.GetSymbolDetailsAsync(Array.Empty<string>()));
        await Assert.ThrowsAnyAsync<ArgumentException>(() => client.TradFi.GetSymbolDetailsAsync(Enumerable.Range(0, 11).Select(x => "S" + x)));
        await Assert.ThrowsAnyAsync<ArgumentException>(() => client.TradFi.GetSymbolCommissionsAsync(new[] { "EURUSD,AUDUSD" }));
        await Assert.ThrowsAnyAsync<ArgumentException>(() => client.TradFi.GetSymbolCommissionsAsync(Array.Empty<string>(), new[] { "forex,metal" }));
        Assert.Empty(handler.Requests);
        var symbols = Enumerable.Range(0, 10).Select(x => "S" + x).ToArray();
        Assert.True((await client.TradFi.GetSymbolDetailsAsync(symbols)).Success);
        Assert.Equal("?symbols=" + string.Join(",", symbols), Uri.UnescapeDataString(Assert.Single(handler.Requests).RequestUri.Query));
    }

    [Theory]
    [InlineData("account")]
    [InlineData("user")]
    [InlineData("assets")]
    [InlineData("commissions")]
    [InlineData("details")]
    [InlineData("order")]
    [InlineData("cancel")]
    public async Task Http_errors_preserve_metadata_without_retry(string operation)
    {
        var handler = new RecordingHttpMessageHandler(_ => Response("{\"label\":\"INVALID_ARGUMENT\",\"message\":\"rejected\",\"data\":null}", HttpStatusCode.BadRequest));
        using var client = Client(handler);
        var result = await Invoke(client, operation);
        Assert.False(result.Success);
        Assert.Equal("INVALID_ARGUMENT", result.Error!.Data);
        Assert.Equal(HttpStatusCode.BadRequest, result.Response!.StatusCode);
        Assert.Single(handler.Requests);
    }

    private static void AssertSignature(RecordedHttpRequest request)
    {
        Assert.Equal("key", Assert.Single(request.Headers["KEY"]));
        var timestamp = Assert.Single(request.Headers["Timestamp"]);
        var bodyHash = Convert.ToHexString(SHA512.HashData(Encoding.UTF8.GetBytes(request.Content))).ToLowerInvariant();
        var payload = string.Join("\n", request.Method.Method, request.RequestUri.AbsolutePath, request.RequestUri.Query.TrimStart('?'), bodyHash, timestamp);
        var signature = Convert.ToHexString(HMACSHA512.HashData(Encoding.UTF8.GetBytes("secret"), Encoding.UTF8.GetBytes(payload))).ToLowerInvariant();
        Assert.Equal(signature, Assert.Single(request.Headers["SIGN"]));
    }

    [Fact]
    public void Full_current_symbol_detail_fields_and_numeric_strings_are_mapped()
    {
        var detail = JObject.Parse(JsonFixture.Read("Docs/TradFi/symbol_details.success.json"))["data"]!["list"]![0]!.ToObject<GateTradFiSymbolDetails>()!;
        Assert.Equal("PLNJPY", detail.Symbol);
        Assert.Equal("PLN/JPY", detail.Description);
        Assert.Equal("Forex", detail.CategoryName);
        Assert.Equal("JPY", detail.SettlementCurrency);
        Assert.Equal(100000m, detail.ContractVolume);
        Assert.Equal(100m, detail.MaxOrderVolume);
        Assert.Equal(10m, detail.MinOrderVolume);
        Assert.Equal("25", detail.Leverage);
        Assert.Equal(4, detail.PricePrecision);
        Assert.Equal(100m, detail.StopLossPriceLevel);
        Assert.Equal("1", detail.SwapCostType);
        Assert.Equal(7.937347m, detail.BuySwapCostRate);
        Assert.Equal(-172.856426m, detail.SellSwapCostRate);
        Assert.Equal("3", detail.SwapCost3Day);
        Assert.Equal("GMT+2", detail.TradeTimezone);
        Assert.Equal(GateTradFiTradeMode.Full, detail.TradeMode);
        Assert.Equal("", detail.IconLink);
        Assert.Equal(17, JObject.FromObject(detail).Count);
    }

    [Fact]
    public async Task Cancellation_keeps_the_empty_acknowledgement_and_exact_actual_long_id()
    {
        var handler = new RecordingHttpMessageHandler(_ => Response("{}"));
        using var client = Client(handler);
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.TradFi.CancelOrderAsync(0));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.TradFi.CancelOrderAsync(-1));
        Assert.Empty(handler.Requests);
        var result = await client.TradFi.CancelOrderAsync(9007199254740993);
        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Delete, request.Method);
        Assert.Equal("/api/v4/tradfi/orders/9007199254740993", request.RequestUri.AbsolutePath);
        Assert.Equal("", request.Content);
        Assert.Equal("", request.RequestUri.Query);
        AssertSignature(request);
    }

    private static GateRestApiClient Client(RecordingHttpMessageHandler handler)
    {
        var client = new GateRestApiClient(new GateRestApiClientOptions { HttpClient = new HttpClient(handler) });
        client.SetApiCredentials("key", "secret");
        return client;
    }

    private static HttpResponseMessage Response(string json, HttpStatusCode status = HttpStatusCode.OK)
        => new(status) { Content = new StringContent(json, Encoding.UTF8, "application/json") };

    private static async Task<ApiSharp.Models.RestCallResult> Invoke(GateRestApiClient client, string operation)
        => operation switch
        {
            "account" => (await client.TradFi.GetMt5AccountAsync()).AsDataless(),
            "user" => (await client.TradFi.CreateUserAsync()).AsDataless(),
            "assets" => (await client.TradFi.GetAccountAssetsAsync()).AsDataless(),
            "commissions" => (await client.TradFi.GetSymbolCommissionsAsync(new[] { "EURUSD" })).AsDataless(),
            "details" => (await client.TradFi.GetSymbolDetailsAsync(new[] { "EURUSD" })).AsDataless(),
            "order" => (await client.TradFi.PlaceOrderAsync(Order())).AsDataless(),
            "cancel" => (await client.TradFi.CancelOrderAsync(2630591)).AsDataless(),
            _ => throw new ArgumentOutOfRangeException(nameof(operation)),
        };
}

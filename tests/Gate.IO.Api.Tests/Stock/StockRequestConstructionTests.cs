using System.Text;
using Gate.IO.Api.Stock;
using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.Stock;

[Trait("Category", TestCategories.Unit)]
public class StockRequestConstructionTests
{
    [Fact]
    public async Task Public_stock_market_requests_target_documented_routes_and_include_gateway_timestamp()
    {
        var responses = new Queue<string>([
            JsonFixture.Read("Docs/Stock/symbols.success.json"),
            JsonFixture.Read("Docs/Stock/symbol_details.success.json"),
            JsonFixture.Read("Docs/Stock/orderbook.success.json"),
            JsonFixture.Read("Docs/Stock/fee_rates.success.json"),
        ]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateClient(handler);

        var symbols = await client.Stock.GetSymbolsAsync(new GateStockSymbolQueryRequest
        {
            Symbols = [" AAPL ", "MSFT"],
            Exchange = GateStockExchange.UnitedStates,
            IncludeLocalizedDescriptions = true,
            Page = 2,
            PageSize = 50,
        });
        var details = await client.Stock.GetSymbolDetailsAsync(new GateStockSymbolDetailsQueryRequest
        {
            Symbols = ["AAPL"],
            Exchange = GateStockExchange.UnitedStates,
            Page = 1,
            PageSize = 10,
        });
        var orderBook = await client.Stock.GetOrderBookAsync("AAPL");
        var fees = await client.Stock.GetFeeRatesAsync();

        Assert.True(symbols.Success, symbols.Error?.ToString());
        Assert.True(details.Success, details.Error?.ToString());
        Assert.True(orderBook.Success, orderBook.Error?.ToString());
        Assert.True(fees.Success, fees.Error?.ToString());
        Assert.Equal(4, handler.Requests.Count);

        AssertRequest(handler.Requests[0], HttpMethod.Get, "/api/v4/stock/symbols", signed: false);
        var symbolsQuery = ParseQuery(handler.Requests[0].RequestUri);
        Assert.Equal("AAPL,MSFT", symbolsQuery["symbols"]);
        Assert.Equal("us", symbolsQuery["exchange"]);
        Assert.Equal("true", symbolsQuery["with_desc_i18n"]);
        Assert.Equal("2", symbolsQuery["page"]);
        Assert.Equal("50", symbolsQuery["page_size"]);

        AssertRequest(handler.Requests[1], HttpMethod.Get, "/api/v4/stock/symbols/detail", signed: false);
        Assert.Equal("AAPL", ParseQuery(handler.Requests[1].RequestUri)["symbols"]);
        AssertRequest(handler.Requests[2], HttpMethod.Get, "/api/v4/stock/market/AAPL/orderbook", signed: false);
        AssertRequest(handler.Requests[3], HttpMethod.Get, "/api/v4/stock/fee-rate", signed: false);
    }

    [Fact]
    public async Task Signed_stock_asset_request_serializes_pnl_filters()
    {
        var handler = FixtureHandler("Docs/Stock/assets.success.json");
        var client = CreateSignedClient(handler);

        var result = await client.Stock.GetAssetsAsync(GateStockPnlCalculationType.DilutedCost, GateStockPnlPriceType.ExtendedHours);

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        AssertRequest(request, HttpMethod.Get, "/api/v4/stock/users/assets", signed: true);
        var query = ParseQuery(request.RequestUri);
        Assert.Equal("2", query["pnl_calc_type"]);
        Assert.Equal("2", query["pnl_calc_price"]);
    }

    [Fact]
    public async Task Signed_stock_order_collection_requests_match_current_contract()
    {
        var responses = new Queue<string>([
            JsonFixture.Read("Docs/Stock/orders.success.json"),
            JsonFixture.Read("Docs/Stock/order_id.success.json"),
            JsonFixture.Read("Docs/Stock/empty.success.json"),
        ]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateSignedClient(handler);

        var orders = await client.Stock.GetOrdersAsync("AAPL");
        var created = await client.Stock.PlaceOrderAsync(new GateStockOrderRequest
        {
            Volume = 10m,
            Symbol = "AAPL",
            Side = GateStockOrderSide.Buy,
            PriceType = GateStockOrderPriceType.Limit,
            TradingSession = GateStockTradingSession.All,
            TimeInForce = GateStockTimeInForce.Day,
            Price = 200.12m,
            ClientOrderId = "client-202607070001",
        });
        var cancelled = await client.Stock.CancelAllOrdersAsync();

        Assert.True(orders.Success, orders.Error?.ToString());
        Assert.True(created.Success, created.Error?.ToString());
        Assert.True(cancelled.Success, cancelled.Error?.ToString());
        AssertRequest(handler.Requests[0], HttpMethod.Get, "/api/v4/stock/orders", signed: true);
        Assert.Equal("AAPL", ParseQuery(handler.Requests[0].RequestUri)["symbol"]);
        AssertRequest(handler.Requests[1], HttpMethod.Post, "/api/v4/stock/orders", signed: true);
        var body = JObject.Parse(handler.Requests[1].Content);
        Assert.Equal("10", body["volume"]!.ToString());
        Assert.Equal("AAPL", body["symbol"]!.ToString());
        Assert.Equal(2, body["side"]!.Value<int>());
        Assert.Equal("limit", body["price_type"]!.ToString());
        Assert.Equal("all", body["trading_session"]!.ToString());
        Assert.Equal("day", body["time_in_force"]!.ToString());
        Assert.Equal("200.12", body["price"]!.ToString());
        Assert.Equal("client-202607070001", body["client_order_id"]!.ToString());
        AssertRequest(handler.Requests[2], HttpMethod.Delete, "/api/v4/stock/orders", signed: true);
    }

    [Fact]
    public async Task Signed_stock_order_resource_requests_serialize_filters_and_body()
    {
        var responses = new Queue<string>([
            JsonFixture.Read("Docs/Stock/order_history.success.json"),
            JsonFixture.Read("Docs/Stock/order_update.success.json"),
            JsonFixture.Read("Docs/Stock/empty.success.json"),
        ]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateSignedClient(handler);

        var history = await client.Stock.GetOrderHistoryAsync(new GateStockOrderHistoryQueryRequest
        {
            Symbol = "AAPL",
            OrderIds = [123456, 123457],
            BeginTime = DateTimeOffset.FromUnixTimeSeconds(1769378400).UtcDateTime,
            EndTime = DateTimeOffset.FromUnixTimeSeconds(1769378500).UtcDateTime,
            Side = GateStockOrderSide.Buy,
            Page = 1,
            PageSize = 20,
        });
        var updated = await client.Stock.UpdateOrderAsync(123456, new GateStockOrderUpdateRequest { Volume = 8m, Price = 201.23m });
        var cancelled = await client.Stock.CancelOrderAsync(123456);

        Assert.True(history.Success, history.Error?.ToString());
        Assert.True(updated.Success, updated.Error?.ToString());
        Assert.True(cancelled.Success, cancelled.Error?.ToString());
        AssertRequest(handler.Requests[0], HttpMethod.Get, "/api/v4/stock/orders/history", signed: true);
        var query = ParseQuery(handler.Requests[0].RequestUri);
        Assert.Equal("123456,123457", query["order_ids"]);
        Assert.Equal("1769378400", query["begin_time"]);
        Assert.Equal("1769378500", query["end_time"]);
        Assert.Equal("2", query["side"]);
        Assert.Equal("20", query["page_size"]);
        AssertRequest(handler.Requests[1], HttpMethod.Put, "/api/v4/stock/orders/123456", signed: true);
        var body = JObject.Parse(handler.Requests[1].Content);
        Assert.Equal("8", body["volume"]!.ToString());
        Assert.Equal("201.23", body["price"]!.ToString());
        AssertRequest(handler.Requests[2], HttpMethod.Delete, "/api/v4/stock/orders/123456", signed: true);
    }

    [Fact]
    public async Task Signed_stock_position_requests_match_current_contract()
    {
        var responses = new Queue<string>([
            JsonFixture.Read("Docs/Stock/positions.success.json"),
            JsonFixture.Read("Docs/Stock/position_close.success.json"),
        ]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateSignedClient(handler);

        var positions = await client.Stock.GetPositionsAsync(new GateStockPositionQueryRequest
        {
            PnlCalculationType = GateStockPnlCalculationType.AverageCost,
            PnlPriceType = GateStockPnlPriceType.Intraday,
            Symbol = "AAPL",
            Exchange = GateStockExchange.UnitedStates,
        });
        var close = await client.Stock.ClosePositionAsync(new GateStockClosePositionRequest
        {
            Symbol = "AAPL",
            CloseType = GateStockPositionCloseType.Partial,
            CloseVolume = 2m,
        });

        Assert.True(positions.Success, positions.Error?.ToString());
        Assert.True(close.Success, close.Error?.ToString());
        AssertRequest(handler.Requests[0], HttpMethod.Get, "/api/v4/stock/positions", signed: true);
        var query = ParseQuery(handler.Requests[0].RequestUri);
        Assert.Equal("1", query["pnl_calc_type"]);
        Assert.Equal("1", query["pnl_calc_price"]);
        Assert.Equal("AAPL", query["symbol"]);
        Assert.Equal("us", query["exchange"]);
        AssertRequest(handler.Requests[1], HttpMethod.Post, "/api/v4/stock/positions/close", signed: true);
        var body = JObject.Parse(handler.Requests[1].Content);
        Assert.Equal("AAPL", body["symbol"]!.ToString());
        Assert.Equal("2", body["close_volume"]!.ToString());
        Assert.Equal(1, body["close_type"]!.Value<int>());
    }

    [Fact]
    public async Task Signed_stock_transaction_requests_apply_reference_precedence_and_serialize_transfer()
    {
        var responses = new Queue<string>([
            JsonFixture.Read("Docs/Stock/transactions.success.json"),
            JsonFixture.Read("Docs/Stock/empty.success.json"),
        ]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateSignedClient(handler);

        var transactions = await client.Stock.GetTransactionsAsync(new GateStockTransactionQueryRequest
        {
            ReferenceId = "transfer-202607070001",
            BeginTime = new DateTime(2026, 4, 2, 0, 0, 0, DateTimeKind.Utc),
            EndTime = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            Type = GateStockTransactionType.Deposit,
            Page = 0,
            PageSize = 501,
        });
        var transfer = await client.Stock.CreateTransactionAsync(new GateStockTransferRequest
        {
            Asset = "usdt",
            Change = 100m,
            Type = GateStockTransferType.Deposit,
            ReferenceId = "transfer-202607070001",
        });

        Assert.True(transactions.Success, transactions.Error?.ToString());
        Assert.True(transfer.Success, transfer.Error?.ToString());
        AssertRequest(handler.Requests[0], HttpMethod.Get, "/api/v4/stock/transactions", signed: true);
        var query = ParseQuery(handler.Requests[0].RequestUri);
        Assert.Single(query);
        Assert.Equal("transfer-202607070001", query["ref_id"]);
        AssertRequest(handler.Requests[1], HttpMethod.Post, "/api/v4/stock/transactions", signed: true);
        var body = JObject.Parse(handler.Requests[1].Content);
        Assert.Equal("USDT", body["asset"]!.ToString());
        Assert.Equal("100", body["change"]!.ToString());
        Assert.Equal("deposit", body["type"]!.ToString());
        Assert.Equal("transfer-202607070001", body["ref_id"]!.ToString());
    }

    [Fact]
    public async Task Stock_exchange_request_is_signed_to_match_current_production_gateway()
    {
        var handler = FixtureHandler("Docs/Stock/exchanges.success.json");
        var client = CreateSignedClient(handler);

        var result = await client.Stock.GetExchangesAsync();

        Assert.True(result.Success, result.Error?.ToString());
        AssertRequest(Assert.Single(handler.Requests), HttpMethod.Get, "/api/v4/stock/exchanges", signed: true);
    }

    [Fact]
    public async Task Stock_order_validation_rejects_unsafe_or_unsupported_combinations_before_io()
    {
        var client = new GateRestApiClient();

        await Assert.ThrowsAsync<ArgumentException>(() => client.Stock.PlaceOrderAsync(new GateStockOrderRequest
        {
            Symbol = "AAPL",
            Volume = 1,
            Side = GateStockOrderSide.Buy,
            PriceType = GateStockOrderPriceType.Limit,
            TradingSession = GateStockTradingSession.Regular,
        }));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Stock.PlaceOrderAsync(new GateStockOrderRequest
        {
            Symbol = "AAPL",
            Volume = 1,
            Side = GateStockOrderSide.Buy,
            PriceType = GateStockOrderPriceType.Market,
            TradingSession = GateStockTradingSession.All,
        }));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.Stock.PlaceOrderAsync(new GateStockOrderRequest
        {
            Symbol = "AAPL",
            Volume = 1,
            Side = GateStockOrderSide.Buy,
            PriceType = GateStockOrderPriceType.Market,
            TradingSession = GateStockTradingSession.Regular,
            TimeInForce = (GateStockTimeInForce)int.MaxValue,
        }));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Stock.ClosePositionAsync(new GateStockClosePositionRequest
        {
            Symbol = "AAPL",
            CloseType = GateStockPositionCloseType.Partial,
        }));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Stock.GetOrderHistoryAsync(new GateStockOrderHistoryQueryRequest
        {
            BeginTime = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            EndTime = new DateTime(2026, 4, 2, 0, 0, 0, DateTimeKind.Utc),
        }));
    }

    private static RecordingHttpMessageHandler FixtureHandler(string path)
        => new(_ => JsonResponse(JsonFixture.Read(path)));

    private static GateRestApiClient CreateClient(RecordingHttpMessageHandler handler)
        => new(new GateRestApiClientOptions { HttpClient = new HttpClient(handler) });

    private static GateRestApiClient CreateSignedClient(RecordingHttpMessageHandler handler)
    {
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");
        return client;
    }

    private static HttpResponseMessage JsonResponse(string json)
        => new(System.Net.HttpStatusCode.OK)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json"),
        };

    private static Dictionary<string, string> ParseQuery(Uri uri)
        => uri.Query.TrimStart('?')
            .Split('&', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Split(['='], 2))
            .ToDictionary(
                x => Uri.UnescapeDataString(x[0]),
                x => x.Length == 1 ? string.Empty : Uri.UnescapeDataString(x[1]));

    private static void AssertRequest(RecordedHttpRequest request, HttpMethod method, string path, bool signed)
    {
        Assert.Equal(method, request.Method);
        Assert.Equal(path, request.RequestUri.AbsolutePath);
        Assert.NotEmpty(Assert.Single(request.Headers["Timestamp"]));

        if (!signed)
        {
            Assert.DoesNotContain("KEY", request.Headers.Keys);
            Assert.DoesNotContain("SIGN", request.Headers.Keys);
            return;
        }

        Assert.Equal("key", Assert.Single(request.Headers["KEY"]));
        Assert.NotEmpty(Assert.Single(request.Headers["SIGN"]));
        Assert.True(request.Headers.ContainsKey("X-Gate-Channel-Id"));
    }
}

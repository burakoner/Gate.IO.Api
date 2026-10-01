using ApiSharp.Converters;
using Gate.IO.Api.Stock;
using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.Stock;

[Trait("Category", TestCategories.Contract)]
public class StockContractTests
{
    [Fact]
    public void Documented_stock_assets_and_market_responses_deserialize()
    {
        var assets = Data<GateStockAssets>("Docs/Stock/assets.success.json");
        var symbols = Data<GateStockPage<GateStockSymbol>>("Docs/Stock/symbols.success.json");
        var details = Data<GateStockPage<GateStockSymbolDetails>>("Docs/Stock/symbol_details.success.json");
        var orderBook = Data<GateStockOrderBook>("Docs/Stock/orderbook.success.json");
        var feeRates = DataList<GateStockFeeRate>("Docs/Stock/fee_rates.success.json");

        Assert.Equal(10000.12m, assets.Equity);
        Assert.Equal(6500.5m, assets.Available);
        Assert.True(assets.UserExists);
        Assert.Equal(0m, assets.OptionPositionMarketValue);
        Assert.Equal(0m, assets.OptionPositionPnl);
        Assert.Equal(0m, assets.OptionTodayPnl);
        Assert.Equal(1, symbols.Total);
        Assert.Equal(1, symbols.TotalPages);
        var symbol = Assert.Single(symbols.List);
        Assert.Equal(GateStockExchange.UnitedStates, symbol.Exchange);
        Assert.Equal("CS", symbol.Category);
        Assert.Equal(GateStockAssetType.Stock, symbol.AssetType);
        Assert.Equal(GateStockTradingStatus.Open, symbol.TradingStatus);
        Assert.Equal(GateStockTradeMode.BuyAndSell, symbol.TradeMode);
        Assert.Equal(GateStockOrderFillTiming.Immediate, symbol.OrderFillTiming);
        Assert.Equal("Apple Inc.", Assert.Single(symbol.Descriptions).Value);
        var detail = Assert.Single(details.List);
        Assert.Equal(10000m, detail.MaximumOrderVolume);
        Assert.Equal("CS", detail.Category);
        Assert.Equal(GateStockAssetType.Stock, detail.AssetType);
        Assert.Equal(0.001m, detail.CommissionRate);
        Assert.Equal("open", detail.Status);
        Assert.Equal(200.11m, Assert.Single(orderBook.Bids).Price);
        Assert.Equal(200.12m, Assert.Single(orderBook.Asks).Price);
        Assert.Equal(0.001m, Assert.Single(feeRates).MakerFee);
    }

    [Fact]
    public void Documented_stock_order_responses_deserialize()
    {
        var orders = DataList<GateStockOrder>("Docs/Stock/orders.success.json");
        var created = Data<GateStockOrderId>("Docs/Stock/order_id.success.json");
        var history = Data<GateStockPage<GateStockOrderHistory>>("Docs/Stock/order_history.success.json");
        var updated = Data<GateStockOrderUpdateResult>("Docs/Stock/order_update.success.json");

        var order = Assert.Single(orders);
        Assert.Equal("123456", order.OrderId);
        Assert.Equal(GateStockOrderPriceType.Limit, order.PriceType);
        Assert.Equal(GateStockOrderSide.Buy, order.Side);
        Assert.Equal(10m, order.Volume);
        Assert.Equal(2m, order.FilledVolume);
        Assert.Equal(DateTimeOffset.FromUnixTimeSeconds(1769378400).UtcDateTime, order.CreateTime);
        Assert.Equal("123456", created.Id);

        var historicalOrder = Assert.Single(history.List);
        Assert.Equal(GateStockTimeInForce.Day, historicalOrder.TimeInForce);
        Assert.Equal(200.10m, historicalOrder.AverageFillPrice);
        Assert.Equal("Filled", historicalOrder.StatusDetail.Title);
        Assert.Equal(123456, updated.OrderId);
    }

    [Fact]
    public void Documented_stock_position_transaction_and_exchange_responses_deserialize()
    {
        var positions = DataList<GateStockPosition>("Docs/Stock/positions.success.json");
        var close = Data<GateStockPositionCloseResult>("Docs/Stock/position_close.success.json");
        var transactions = Data<GateStockPage<GateStockTransaction>>("Docs/Stock/transactions.success.json");
        var exchanges = DataList<GateStockExchangeInfo>("Docs/Stock/exchanges.success.json");

        var position = Assert.Single(positions);
        Assert.Equal(10m, position.Volume);
        Assert.Equal(8m, position.Available);
        Assert.Equal(200.2m, position.ExtendedLastPrice);
        Assert.Equal(123456, close.OrderId);

        var transaction = Assert.Single(transactions.List);
        Assert.Equal(GateStockTransactionType.Deposit, transaction.Type);
        Assert.Equal(100m, transaction.Change);
        Assert.Equal("api", transaction.Detail["source"]!.ToString());
        Assert.Equal(DateTimeOffset.FromUnixTimeSeconds(1769378400).UtcDateTime, transaction.Time);

        var exchange = Assert.Single(exchanges);
        Assert.Equal(GateStockExchange.UnitedStates, exchange.Exchange);
        Assert.True(exchange.SupportsTransfer);
    }

    [Fact]
    public void Current_stock_enum_contract_matches_schema_and_production_values()
    {
        Assert.Equal("4", MapConverter.GetString(GateStockTradeMode.BuyAndSell));
        Assert.Equal(new[] { GateStockTimeInForce.Day }, Enum.GetValues<GateStockTimeInForce>());
        Assert.Equal("day", MapConverter.GetString(GateStockTimeInForce.Day));
        Assert.Equal("stock_transfer_out", MapConverter.GetString(GateStockTransactionType.StockTransferOut));
        Assert.Equal("kr", MapConverter.GetString(GateStockExchange.SouthKorea));
        Assert.Equal("jp", MapConverter.GetString(GateStockExchange.Japan));
        Assert.Equal(0, (int)GateStockExchange.UnitedStates);
        Assert.Equal(1, (int)GateStockExchange.HongKong);
        Assert.Equal(2, (int)GateStockExchange.SouthKorea);
        Assert.Equal(3, (int)GateStockExchange.Japan);
    }

    [Theory]
    [InlineData("CS")]
    [InlineData("ETF")]
    [InlineData("ADRC")]
    [InlineData("ADR")]
    [InlineData("ETV")]
    [InlineData("PFD")]
    [InlineData("ETS")]
    [InlineData("ETN")]
    [InlineData("FUND")]
    public void Current_stock_categories_remain_strings_without_a_breaking_accessor_change(string category)
    {
        var json = new JObject { ["category"] = category };
        Assert.Equal(category, json.ToObject<GateStockSymbol>()!.Category);
        Assert.Equal(category, json.ToObject<GateStockSymbolDetails>()!.Category);
    }

    [Theory]
    [InlineData("STOCK", GateStockAssetType.Stock)]
    [InlineData("ETF", GateStockAssetType.ExchangeTradedFund)]
    public void Asset_types_deserialize_and_round_trip_in_both_symbol_models(string wireValue, GateStockAssetType expected)
    {
        var json = new JObject { ["asset_type"] = wireValue };
        var symbol = json.ToObject<GateStockSymbol>()!;
        var detail = json.ToObject<GateStockSymbolDetails>()!;

        Assert.Equal(expected, symbol.AssetType);
        Assert.Equal(expected, detail.AssetType);
        Assert.Equal(wireValue, JObject.FromObject(symbol)["asset_type"]!.ToString());
        Assert.Equal(wireValue, JObject.FromObject(detail)["asset_type"]!.ToString());
    }

    [Fact]
    public void Japanese_exchange_deserializes_and_round_trips_in_all_six_affected_response_models()
    {
        AssertJapaneseExchange<GateStockSymbol>("symbols");
        AssertJapaneseExchange<GateStockSymbolDetails>("symbol_details");
        AssertJapaneseExchange<GateStockOrder>("orders");
        AssertJapaneseExchange<GateStockOrderHistory>("order_history");
        AssertJapaneseExchange<GateStockPosition>("positions");
        AssertJapaneseExchange<GateStockExchangeInfo>("exchanges");
    }

    [Fact]
    public void Option_asset_fields_preserve_decimal_precision_and_negative_pnl()
    {
        var assets = JObject.Parse("{\"option_position_market_value\":\"123.4567890123456789\",\"option_position_pnl\":\"-0.00000001\",\"option_today_pnl\":\"-1.23\"}")
            .ToObject<GateStockAssets>()!;

        Assert.Equal(123.4567890123456789m, assets.OptionPositionMarketValue);
        Assert.Equal(-0.00000001m, assets.OptionPositionPnl);
        Assert.Equal(-1.23m, assets.OptionTodayPnl);
    }

    [Theory]
    [InlineData("{}")]
    [InlineData("{\"asset_type\":null,\"option_position_market_value\":null,\"option_position_pnl\":null,\"option_today_pnl\":null}")]
    public void Missing_or_null_new_fields_do_not_invent_an_asset_type_or_zero_option_balances(string json)
    {
        Assert.Null(JsonConvert.DeserializeObject<GateStockSymbol>(json)!.AssetType);
        Assert.Null(JsonConvert.DeserializeObject<GateStockSymbolDetails>(json)!.AssetType);
        var assets = JsonConvert.DeserializeObject<GateStockAssets>(json)!;
        Assert.Null(assets.OptionPositionMarketValue);
        Assert.Null(assets.OptionPositionPnl);
        Assert.Null(assets.OptionTodayPnl);
    }

    private static void AssertJapaneseExchange<T>(string fixture)
    {
        var json = (JObject)JsonFixture.Parse($"Docs/Stock/{fixture}.success.json")["data"]!["list"]![0]!;
        json["exchange"] = "jp";
        var value = json.ToObject<T>();
        Assert.NotNull(value);
        Assert.Equal("jp", JObject.FromObject(value)["exchange"]!.ToString());
    }

    private static T Data<T>(string path)
    {
        var value = JsonFixture.Parse(path)["data"]!.ToObject<T>();
        Assert.NotNull(value);
        return value!;
    }

    private static List<T> DataList<T>(string path)
    {
        var value = JsonFixture.Parse(path)["data"]!["list"]!.ToObject<List<T>>();
        Assert.NotNull(value);
        return value!;
    }
}

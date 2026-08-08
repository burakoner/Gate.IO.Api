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
        Assert.Equal(1, symbols.Total);
        Assert.Equal(1, symbols.TotalPages);
        var symbol = Assert.Single(symbols.List);
        Assert.Equal(GateStockExchange.UnitedStates, symbol.Exchange);
        Assert.Equal(GateStockTradingStatus.Open, symbol.TradingStatus);
        Assert.Equal(GateStockTradeMode.BuyAndSell, symbol.TradeMode);
        Assert.Equal(GateStockOrderFillTiming.Immediate, symbol.OrderFillTiming);
        Assert.Equal("Apple Inc.", Assert.Single(symbol.Descriptions).Value);
        var detail = Assert.Single(details.List);
        Assert.Equal(10000m, detail.MaximumOrderVolume);
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
        Assert.Equal("day", MapConverter.GetString(GateStockTimeInForce.Day));
        Assert.Equal("stock_transfer_out", MapConverter.GetString(GateStockTransactionType.StockTransferOut));
        Assert.Equal("kr", MapConverter.GetString(GateStockExchange.SouthKorea));
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

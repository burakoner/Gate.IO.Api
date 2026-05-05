using Gate.IO.Api.Tests.Infrastructure;
using Gate.IO.Api.TradFi;

namespace Gate.IO.Api.Tests.TradFi;

[Trait("Category", "Contract")]
public class TradFiContractTests
{
    [Fact]
    public void Documented_tradfi_public_market_responses_deserialize()
    {
        var categories = DataList<GateTradFiCategory>("Docs/TradFi/categories.success.json");
        var symbols = DataList<GateTradFiSymbol>("Docs/TradFi/symbols.success.json");
        var symbolDetails = DataList<GateTradFiSymbolDetails>("Docs/TradFi/symbol_details.success.json");
        var candlesticks = DataList<GateTradFiCandlestick>("Docs/TradFi/candlesticks.success.json");
        var ticker = Data<GateTradFiTicker>("Docs/TradFi/ticker.success.json");

        Assert.Single(categories);
        Assert.Equal(1, categories[0].CategoryId);
        Assert.Single(symbols);
        Assert.Equal("EURUSD", symbols[0].Symbol);
        Assert.Equal(GateTradFiTradingStatus.Open, symbols[0].Status);
        Assert.Equal(GateTradFiTradeMode.Full, symbols[0].TradeMode);
        Assert.Single(symbolDetails);
        Assert.Equal(100000m, symbolDetails[0].ContractVolume);
        Assert.Equal(25, symbolDetails[0].Leverage);
        Assert.Equal(10m, symbolDetails[0].MinOrderVolume);
        Assert.Equal(2, candlesticks.Count);
        Assert.Equal(1.17213m, candlesticks[0].Close);
        Assert.Equal(5074.19m, ticker.LastPrice);
        Assert.Equal(GateTradFiTradingStatus.Open, ticker.Status);
        Assert.Equal(GateTradFiTradeMode.Full, ticker.TradeMode);
    }

    [Fact]
    public void Documented_tradfi_private_account_and_transfer_responses_deserialize()
    {
        var mt5Account = Data<GateTradFiMt5Account>("Docs/TradFi/mt5_account.success.json");
        var user = Data<GateTradFiUser>("Docs/TradFi/user.success.json");
        var accountAssets = Data<GateTradFiAccountAssets>("Docs/TradFi/account_assets.success.json");
        var transactions = Data<GateTradFiTransactionList>("Docs/TradFi/transactions.success.json");

        Assert.Equal(GateTradFiAccountStatus.NotOpened, mt5Account.Status);
        Assert.Equal(GateTradFiAccountStatus.Active, user.Status);
        Assert.Equal(1, user.Mt5Uid);
        Assert.Equal(10122, accountAssets.Mt5Uid);
        Assert.Equal(0m, accountAssets.Equity);
        Assert.Equal(2, transactions.Total);
        Assert.Equal(GateTradFiTransactionType.Dividend, transactions.List[0].Type);
        Assert.Equal(GateTradFiTransactionType.FillNegative, transactions.List[1].Type);
        Assert.Equal(0.5m, transactions.List[1].Change);
    }

    [Fact]
    public void Documented_tradfi_private_order_and_position_responses_deserialize()
    {
        var orders = DataList<GateTradFiOrder>("Docs/TradFi/orders.success.json");
        var orderId = Data<GateTradFiOrderId>("Docs/TradFi/order_id.success.json");
        var orderUpdate = Data<GateTradFiOrderUpdateResult>("Docs/TradFi/order_update.success.json");
        var orderHistory = DataList<GateTradFiOrderHistory>("Docs/TradFi/order_history.success.json");
        var positions = DataList<GateTradFiPosition>("Docs/TradFi/positions.success.json");
        var positionHistory = DataList<GateTradFiPositionHistory>("Docs/TradFi/position_history.success.json");

        Assert.Single(orders);
        Assert.Equal(GateTradFiOrderPriceType.Trigger, orders[0].PriceType);
        Assert.Equal(GateTradFiOrderSide.Buy, orders[0].Side);
        Assert.Equal(1.6m, orders[0].Volume);
        Assert.Equal(117, orderId.Id);
        Assert.Equal(2651172, orderUpdate.OrderId);
        Assert.Equal(1, orderUpdate.State);
        Assert.Single(orderHistory);
        Assert.Equal(GateTradFiOrderPriceType.Market, orderHistory[0].PriceType);
        Assert.Equal(4, orderHistory[0].OperationType);
        Assert.Equal(-1.49755m, orderHistory[0].ClosePnl);
        Assert.Single(positions);
        Assert.Equal(GateTradFiPositionDirection.Long, positions[0].Direction);
        Assert.Equal(0.01m, positions[0].Volume);
        Assert.Single(positionHistory);
        Assert.Equal(GateTradFiPositionDirection.Long, positionHistory[0].Direction);
        Assert.Equal(1, positionHistory[0].PositionStatus);
        Assert.Equal(-2.4m, positionHistory[0].RealizedPnlDetail.Fee);
        Assert.Null(positionHistory[0].CloseDetail);
    }

    [Fact]
    public void Captured_live_public_tradfi_responses_deserialize()
    {
        var categories = DataList<GateTradFiCategory>("Live/TradFi/categories.json");
        var symbols = DataList<GateTradFiSymbol>("Live/TradFi/symbols.json");
        var ticker = Data<GateTradFiTicker>("Live/TradFi/ticker.EURUSD.json");
        var candlesticks = DataList<GateTradFiCandlestick>("Live/TradFi/candlesticks.EURUSD.1m.limit1.json");

        Assert.NotEmpty(categories);
        Assert.NotEmpty(symbols);
        Assert.Contains(symbols, x => x.Symbol == "EURUSD");
        Assert.Equal(GateTradFiTradingStatus.Open, ticker.Status);
        Assert.NotNull(candlesticks);
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

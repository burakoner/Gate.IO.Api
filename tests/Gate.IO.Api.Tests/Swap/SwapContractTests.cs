using Gate.IO.Api.Swap;
using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.Swap;

[Trait("Category", "Contract")]
public class SwapContractTests
{
    [Fact]
    public void Documented_flash_swap_market_responses_deserialize()
    {
        var markets = JsonFixture.Deserialize<List<GateSwapMarket>>("Docs/Swap/currency_pairs.success.json");

        Assert.Single(markets);
        Assert.Equal("BTC_USDT", markets[0].Symbol);
        Assert.Equal("BTC", markets[0].SellCurrency);
        Assert.Equal("USDT", markets[0].BuyCurrency);
        Assert.Equal(0.00001m, markets[0].SellMinimumAmount);
        Assert.Equal(10000000m, markets[0].BuyMaximumAmount);
    }

    [Fact]
    public void Documented_flash_swap_order_responses_deserialize()
    {
        var orders = JsonFixture.Deserialize<List<GateSwapOrder>>("Docs/Swap/orders.success.json");
        var order = JsonFixture.Deserialize<GateSwapOrder>("Docs/Swap/order.success.json");
        var preview = JsonFixture.Deserialize<GateSwapOrderPreview>("Docs/Swap/order_preview.success.json");

        Assert.Single(orders);
        Assert.Equal(54646, orders[0].OrderId);
        Assert.Equal(GateSwapOrderStatus.Success, orders[0].Status);
        Assert.Equal(0.01m, order.SellAmount);
        Assert.Equal(10m, order.BuyAmount);
        Assert.Equal(100m, order.Price);
        Assert.NotEqual(default, order.CreateTime);
        Assert.NotNull(order.UpdateTime);
        Assert.Equal(3453434, preview.PreviewId);
        Assert.Equal(0.1m, preview.SellAmount);
        Assert.Equal(10m, preview.BuyAmount);
    }

    [Fact]
    public void Captured_live_public_flash_swap_market_responses_deserialize()
    {
        var btcMarkets = JsonFixture.Deserialize<List<GateSwapMarket>>("Live/Swap/currency_pairs.BTC.limit1.json");
        var markets = JsonFixture.Deserialize<List<GateSwapMarket>>("Live/Swap/currency_pairs.limit1.json");

        Assert.NotEmpty(btcMarkets);
        Assert.Equal("BTC", btcMarkets[0].SellCurrency);
        Assert.NotEmpty(markets);
    }
}

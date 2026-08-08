using Gate.IO.Api.Stock;
using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.Stock;

[Trait("Category", TestCategories.PublicIntegration)]
public class StockPublicIntegrationTests
{
    [Fact]
    public async Task Public_stock_endpoints_return_deserializable_data_when_live_tests_are_enabled()
    {
        if (!LiveTestSettings.Enabled)
            return;

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(45));
        var client = new GateRestApiClient();

        var symbols = await client.Stock.GetSymbolsAsync(new GateStockSymbolQueryRequest
        {
            IncludeLocalizedDescriptions = true,
            Page = 1,
            PageSize = 1,
        }, cts.Token);
        Assert.True(symbols.Success, symbols.Error?.ToString());
        var symbol = Assert.Single(symbols.Data!.List).Symbol;

        var details = await client.Stock.GetSymbolDetailsAsync(new GateStockSymbolDetailsQueryRequest
        {
            Symbols = [symbol],
            Page = 1,
            PageSize = 1,
        }, cts.Token);
        var orderBook = await client.Stock.GetOrderBookAsync(symbol, cts.Token);
        var feeRates = await client.Stock.GetFeeRatesAsync(cts.Token);

        Assert.True(details.Success, details.Error?.ToString());
        Assert.Single(details.Data!.List);
        Assert.True(orderBook.Success, orderBook.Error?.ToString());
        Assert.Equal(symbol, orderBook.Data!.Symbol);
        Assert.NotNull(orderBook.Data.Bids);
        Assert.NotNull(orderBook.Data.Asks);
        Assert.True(feeRates.Success, feeRates.Error?.ToString());
        Assert.NotEmpty(feeRates.Data!);
    }
}

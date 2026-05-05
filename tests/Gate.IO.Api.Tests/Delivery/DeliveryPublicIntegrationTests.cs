using Gate.IO.Api.Futures;
using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.Delivery;

[Trait("Category", "PublicIntegration")]
public class DeliveryPublicIntegrationTests
{
    [Fact]
    public async Task Public_delivery_endpoints_return_deserializable_data_when_live_tests_are_enabled()
    {
        if (!LiveTestSettings.Enabled)
            return;

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(45));
        var client = new GateRestApiClient();

        var contracts = await client.Delivery.USDT.GetContractsAsync(cts.Token);
        Assert.True(contracts.Success, contracts.Error?.ToString());
        Assert.NotEmpty(contracts.Data!);
        var contractName = contracts.Data![0].Contract;

        var contract = await client.Delivery.USDT.GetContractAsync(contractName, cts.Token);
        var tickers = await client.Delivery.USDT.GetTickersAsync(contractName, cts.Token);
        var orderBook = await client.Delivery.USDT.GetOrderBookAsync(contractName, limit: 5, ct: cts.Token);
        var trades = await client.Delivery.USDT.GetTradesAsync(contractName, limit: 1, ct: cts.Token);
        var candlesticks = await client.Delivery.USDT.GetMarkPriceCandlesticksAsync(contractName, GateFuturesCandlestickInterval.OneMinute, limit: 1, ct: cts.Token);
        var insurance = await client.Delivery.USDT.GetInsuranceHistoryAsync(limit: 1, ct: cts.Token);
        var riskLimitTiers = await client.Delivery.USDT.GetRiskLimitTiersAsync(contractName, limit: 1, ct: cts.Token);

        Assert.True(contract.Success, contract.Error?.ToString());
        Assert.Equal(contractName, contract.Data!.Contract);
        Assert.True(tickers.Success, tickers.Error?.ToString());
        Assert.NotEmpty(tickers.Data!);
        Assert.True(orderBook.Success, orderBook.Error?.ToString());
        Assert.NotEmpty(orderBook.Data!.Asks);
        Assert.True(trades.Success, trades.Error?.ToString());
        Assert.NotNull(trades.Data);
        Assert.True(candlesticks.Success, candlesticks.Error?.ToString());
        Assert.NotNull(candlesticks.Data);
        Assert.True(insurance.Success, insurance.Error?.ToString());
        Assert.NotEmpty(insurance.Data!);
        Assert.True(riskLimitTiers.Success, riskLimitTiers.Error?.ToString());
        Assert.NotEmpty(riskLimitTiers.Data!);
    }

    [Fact]
    public async Task Public_delivery_http_json_is_available_when_live_tests_are_enabled()
    {
        if (!LiveTestSettings.Enabled)
            return;

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(20));
        var json = await PublicHttpCapture.GetStringAsync("https://api.gateio.ws/api/v4/delivery/usdt/contracts", cts.Token);
        var token = JToken.Parse(json);

        Assert.Equal(JTokenType.Array, token.Type);
        Assert.NotEmpty(token);
    }
}

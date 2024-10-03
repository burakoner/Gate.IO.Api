namespace Gate.IO.Api.Examples;

internal class Program
{
    static async Task Main(string[] args)
    {
        var api = new GateRestApiClient(new GateRestApiClientOptions
        {
        });
        var api01 = await api.Futures.Perpetual.USDT.GetContractsAsync();
        var api02 = await api.Futures.Perpetual.USDT.GetMarkPriceCandlesticksAsync("BTC_USDT", Futures.GateFuturesCandlestickInterval.FourHours);
        var a = 0;
    }
}

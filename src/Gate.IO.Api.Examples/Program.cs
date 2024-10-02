namespace Gate.IO.Api.Examples;

internal class Program
{
    static async Task Main(string[] args)
    {
        var api = new GateRestApiClient(new GateRestApiClientOptions
        {
    });
        var api01 = await api.Spot.GetAllCurrenciesAsync();
        var a = 0;
    }
}

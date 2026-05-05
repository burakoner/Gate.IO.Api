namespace Gate.IO.Api.Tests;

[Trait("Category", "Unit")]
public class RestClientSurfaceTests
{
    [Fact]
    public void Rest_client_exposes_all_top_level_modules()
    {
        var client = new GateRestApiClient();

        Assert.NotNull(client.Wallet);
        Assert.NotNull(client.Withdrawal);
        Assert.NotNull(client.SubAccount);
        Assert.NotNull(client.Unified);
        Assert.NotNull(client.IsolatedMargin);
        Assert.NotNull(client.Spot);
        Assert.NotNull(client.FlashSwap);
        Assert.NotNull(client.Futures);
        Assert.NotNull(client.Delivery);
        Assert.NotNull(client.TradFi);
        Assert.NotNull(client.Options);
        Assert.NotNull(client.EarnUni);
        Assert.NotNull(client.MultiCollateralLoan);
        Assert.NotNull(client.Earn);
        Assert.NotNull(client.Account);
        Assert.NotNull(client.Rebate);
        Assert.NotNull(client.Otc);
        Assert.NotNull(client.P2p);
        Assert.NotNull(client.CrossEx);
        Assert.NotNull(client.Alpha);
        Assert.NotNull(client.Bot);
    }
}

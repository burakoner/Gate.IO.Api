using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.Alpha;

[Trait("Category", "Contract")]
public class AlphaContractTests
{
    [Fact]
    public void Documented_accounts_response_deserializes()
    {
        var accounts = JsonFixture.Deserialize<List<GateAlphaAccount>>("Docs/Alpha/accounts.success.json");

        Assert.Single(accounts);
        Assert.Equal("memeboxELON", accounts[0].Currency);
        Assert.Equal(1m, accounts[0].Available);
        Assert.Equal(0m, accounts[0].Locked);
        Assert.Equal("SOL", accounts[0].Chain);
    }

    [Fact]
    public void Documented_account_book_response_deserializes()
    {
        var records = JsonFixture.Deserialize<List<GateAlphaAccountBookRecord>>("Docs/Alpha/account_book.success.json");

        Assert.Single(records);
        Assert.Equal(123456, records[0].Id);
        Assert.Equal("memeboxELON", records[0].Currency);
        Assert.Equal(1.03m, records[0].Change);
        Assert.Equal(4.59316525194m, records[0].Balance);
    }

    [Fact]
    public void Documented_quote_response_deserializes()
    {
        var quote = JsonFixture.Deserialize<GateAlphaQuote>("Docs/Alpha/quote.success.json");

        Assert.Equal("12345678", quote.QuoteId);
        Assert.Equal(0.1m, quote.MinimumAmount);
        Assert.Equal("1000:0.0", quote.MaximumAmount);
        Assert.Equal(11.666m, quote.Price);
        Assert.Equal(GateAlphaQuoteErrorType.Success, quote.ErrorType);
    }

    [Fact]
    public void Documented_order_placement_response_deserializes()
    {
        var order = JsonFixture.Deserialize<GateAlphaOrderPlacement>("Docs/Alpha/orders.create.success.json");

        Assert.Equal("12345678", order.OrderId);
        Assert.Equal(GateAlphaOrderStatus.Processing, order.Status);
        Assert.Equal(GateAlphaOrderSide.Buy, order.Side);
        Assert.Equal("custom", order.GasMode);
        Assert.Equal(324m, order.Amount);
    }

    [Fact]
    public void Documented_order_list_response_deserializes()
    {
        var orders = JsonFixture.Deserialize<List<GateAlphaOrder>>("Docs/Alpha/orders.list.success.json");

        Assert.Single(orders);
        Assert.Equal("12345678", orders[0].OrderId);
        Assert.Equal(GateAlphaOrderSide.Buy, orders[0].Side);
        Assert.Equal(565455643.6400m, orders[0].CurrencyAmount);
        Assert.Equal(GateAlphaOrderStatus.Processing, orders[0].Status);
    }

    [Fact]
    public void Documented_single_order_response_deserializes()
    {
        var order = JsonFixture.Deserialize<GateAlphaOrder>("Docs/Alpha/order.success.json");

        Assert.Equal("12345678", order.OrderId);
        Assert.Equal("aaaaaaa", order.TransactionHash);
        Assert.Equal(GateAlphaOrderSide.Buy, order.Side);
        Assert.Equal("MEME", order.Currency);
        Assert.Equal(GateAlphaOrderStatus.Processing, order.Status);
    }

    [Fact]
    public void Documented_public_market_responses_deserialize()
    {
        var currencies = JsonFixture.Deserialize<List<GateAlphaCurrency>>("Docs/Alpha/currencies.success.json");
        var tickers = JsonFixture.Deserialize<List<GateAlphaTicker>>("Docs/Alpha/tickers.success.json");
        var tokens = JsonFixture.Deserialize<List<GateAlphaToken>>("Docs/Alpha/tokens.success.json");

        Assert.Single(currencies);
        Assert.Single(tickers);
        Assert.Single(tokens);
        Assert.Equal(GateAlphaCurrencyStatus.NormalTrading, currencies[0].Status);
        Assert.Equal(11.38m, tickers[0].Last);
        Assert.Equal("6p6xgHyF7AeE6TZkSmFsko444wqoP15icUSqi2jfGiPN", tokens[0].Address);
    }

    [Fact]
    public void Captured_live_public_alpha_responses_deserialize()
    {
        var currencies = JsonFixture.Deserialize<List<GateAlphaCurrency>>("Live/Alpha/currencies.limit1.json");
        var tickers = JsonFixture.Deserialize<List<GateAlphaTicker>>("Live/Alpha/tickers.limit1.json");
        var tokens = JsonFixture.Deserialize<List<GateAlphaToken>>("Live/Alpha/tokens.page1.json");

        Assert.NotNull(currencies);
        Assert.NotNull(tickers);
        Assert.NotNull(tokens);
    }
}

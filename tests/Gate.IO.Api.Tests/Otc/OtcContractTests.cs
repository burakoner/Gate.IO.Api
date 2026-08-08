using Gate.IO.Api.Otc;
using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.Otc;

[Trait("Category", "Contract")]
public class OtcContractTests
{
    [Fact]
    public void Documented_quote_and_action_responses_deserialize()
    {
        var quote = JsonFixture.Parse("Docs/Otc/quote.success.json")["data"]!.ToObject<GateOtcQuote>()!;
        var action = JsonFixture.Deserialize<GateOtcActionResult>("Docs/Otc/action.success.json");
        var stableAction = JsonFixture.Deserialize<GateOtcActionResult>("Docs/Otc/stablecoin_order_create.success.json");

        Assert.Equal(GateOtcOrderType.Buy, quote.Type);
        Assert.Equal("USD", quote.PayCoin);
        Assert.Equal("USDT", quote.GetCoin);
        Assert.Equal(30000m, quote.PayAmount);
        Assert.Equal(29891m, quote.GetAmount);
        Assert.Equal(1.0036m, quote.Rate);
        Assert.Equal(0.9964m, quote.RateReciprocal);
        Assert.Equal(GateOtcQuoteSide.Pay, quote.Side);
        Assert.Equal(GateOtcOrderKind.Fiat, quote.OrderType);
        Assert.Equal(300, quote.ValidityPeriod);
        Assert.Equal(20, quote.RefreshLimit);
        Assert.Equal(string.Empty, quote.RefreshLimitMessage);
        Assert.Equal(0, action.Code);
        Assert.Equal("success", action.Message);
        Assert.NotEqual(default, action.Timestamp);
        Assert.Equal("success", stableAction.Message);
        Assert.NotEqual(default, stableAction.Timestamp);
    }

    [Fact]
    public void Documented_bank_responses_deserialize()
    {
        var bankList = JsonFixture.Parse("Docs/Otc/bank_list.success.json")["data"]!["lists"]!.ToObject<List<GateOtcBankAccount>>()!;
        var created = JsonFixture.Parse("Docs/Otc/bank_create.success.json")["data"]!.ToObject<GateOtcBankCreateResult>()!;
        var checklist = JsonFixture.Parse("Docs/Otc/bank_supplement_checklist.success.json")["data"]!.ToObject<GateOtcBankSupplementChecklist>()!;

        Assert.Single(bankList);
        Assert.Equal("762", bankList[0].Id);
        Assert.Equal("1554 **** 8756", bankList[0].Iban);
        Assert.Equal("455876663", bankList[0].Swift);
        Assert.Equal(1, bankList[0].IsDefault);
        Assert.Equal("2026-01-21 05:56:49", bankList[0].SubmitTime);
        Assert.Equal(762, created.BankId);
        Assert.Equal(0, created.Status);
        Assert.Equal(GateOtcBankUserType.Personal, checklist.UserType);
        Assert.Single(checklist.Items);
        Assert.Contains("identity document", checklist.Items[0].Description);
    }

    [Fact]
    public void Documented_order_responses_deserialize()
    {
        var fiatOrders = JsonFixture.Parse("Docs/Otc/fiat_orders.success.json")["data"]!.ToObject<GateOtcFiatOrderPage>()!;
        var stableOrders = JsonFixture.Parse("Docs/Otc/stablecoin_orders.success.json")["data"]!.ToObject<GateOtcStableCoinOrderPage>()!;
        var detail = JsonFixture.Parse("Docs/Otc/fiat_order_detail.success.json")["data"]!.ToObject<GateOtcFiatOrderDetail>()!;
        var disbursedOrder = JObject.Parse("{\"status\":\"DISBURSED\"}").ToObject<GateOtcFiatOrder>()!;
        Assert.Equal(1, fiatOrders.PageNumber);
        Assert.Equal(2, fiatOrders.Count);
        Assert.Single(fiatOrders.List);
        Assert.Equal("41", fiatOrders.List[0].OrderId);
        Assert.Equal(GateOtcOrderType.Sell, fiatOrders.List[0].Type);
        Assert.Equal("PROCESSING", fiatOrders.List[0].Status);
        Assert.Equal("DISBURSED", disbursedOrder.Status);
        Assert.Equal("USDT", fiatOrders.List[0].CryptoCurrency);
        Assert.Equal(199600m, fiatOrders.List[0].FiatAmount);
        Assert.Equal(0.998m, fiatOrders.List[0].Rate);
        Assert.Equal("USD", fiatOrders.List[0].FiatCurrencyInfo.Name);
        Assert.Equal(20, stableOrders.Total);
        Assert.Equal(1L, stableOrders.List[0].Id);
        Assert.Equal("https://icon.com", stableOrders.List[0].PayIcon);
        Assert.Equal("https://icon.com", stableOrders.List[0].GetIcon);
        Assert.Equal(30000m, stableOrders.List[0].PayAmount);
        Assert.Equal(0.6667m, stableOrders.List[0].RateReciprocal);
        Assert.NotEqual(default, stableOrders.List[0].CreateTimeStamp);
        Assert.Equal("265", detail.OrderId);
        Assert.Equal("2124269088", detail.UserId);
        Assert.Equal(GateOtcOrderType.Buy, detail.Type);
        Assert.Equal(string.Empty, detail.BankAccountName);
        Assert.Equal(string.Empty, detail.BankName);
        Assert.Equal(string.Empty, detail.BankCountry);
        Assert.Equal(string.Empty, detail.BankAddress);
        Assert.Equal(string.Empty, detail.BankAccountNumberIban);
        Assert.Equal(string.Empty, detail.SwiftCode);
        Assert.Equal(string.Empty, detail.IntermediaryBankName);
        Assert.Equal(string.Empty, detail.IntermediaryBankSwiftCode);
        Assert.Equal(string.Empty, detail.GateBankAccountName);
        Assert.Equal(string.Empty, detail.GateBankName);
        Assert.Equal(string.Empty, detail.GateBankCountry);
        Assert.Equal(string.Empty, detail.GateBankAddress);
        Assert.Equal(string.Empty, detail.GateBankAccountNumberIban);
        Assert.Equal(string.Empty, detail.GateSwiftCode);
        Assert.Equal(string.Empty, detail.GateIntermediaryBankName);
        Assert.Equal(string.Empty, detail.GateIntermediaryBankSwiftCode);
        Assert.Equal(string.Empty, detail.GateTransferRemark);
        Assert.Equal("OTIK7M39QF42", detail.GateReferenceCode);
        Assert.Equal("PROCESSING", detail.Status);
        Assert.NotEqual(default, detail.CreateTime);

        Assert.Null(typeof(GateOtcQuote).GetProperty("UsdcRate"));
        Assert.Null(typeof(GateOtcFiatOrder).GetProperty("DatabaseStatus"));
        Assert.Null(typeof(GateOtcFiatOrder).GetProperty("ReferenceCode"));
        Assert.Null(typeof(GateOtcFiatOrderDetail).GetProperty("DatabaseStatus"));
        Assert.Null(typeof(GateOtcFiatOrderDetail).GetProperty("TransferRemark"));
        Assert.DoesNotContain("All", Enum.GetNames(typeof(GateOtcOrderType)));
    }
}

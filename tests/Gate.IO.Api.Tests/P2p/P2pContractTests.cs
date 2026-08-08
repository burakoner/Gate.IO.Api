using Gate.IO.Api.P2p;
using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.P2p;

[Trait("Category", "Contract")]
public class P2pContractTests
{
    [Fact]
    public void Documented_account_and_payment_responses_deserialize()
    {
        var user = JsonFixture.Parse("Docs/P2p/user_info.success.json")["data"]!.ToObject<GateP2pUserInfo>()!;
        var counterparty = JsonFixture.Parse("Docs/P2p/counterparty_user_info.success.json")["data"]!.ToObject<GateP2pUserInfo>()!;
        var paymentMethods = JsonFixture.Parse("Docs/P2p/payment_methods.success.json")["data"]!.ToObject<List<GateP2pPaymentMethodGroup>>()!;
        var workHours = JsonFixture.Parse("Docs/P2p/work_hours.success.json")["data"]!.ToObject<GateP2pMerchantWorkHours>()!;

        Assert.True(user.IsSelf);
        Assert.Equal("merchant_demo", user.UserName);
        Assert.Equal(128, user.CompleteTransactions);
        Assert.Equal(96m, user.CompleteRateMonth);
        Assert.Equal("USD", user.MerchantInfo.Market);
        Assert.Equal(28600.75m, user.TransactionsAll);
        Assert.False(counterparty.IsSelf);
        Assert.Equal("biz_uid_demo_b84d21", counterparty.BusinessUserId);
        Assert.Single(paymentMethods);
        Assert.Equal("bank", paymentMethods[0].PayType);
        Assert.Equal(10001, Assert.Single(paymentMethods[0].Ids));
        Assert.Equal("155400008756", paymentMethods[0].List[0].Account);
        Assert.Equal(GateP2pMerchantWorkStatus.CustomWorking, workHours.WorkStatus);
    }

    [Fact]
    public void Documented_transaction_responses_deserialize()
    {
        var pending = JsonFixture.Parse("Docs/P2p/pending_transactions.success.json")["data"]!.ToObject<GateP2pTransactionPage>()!;
        var completed = JsonFixture.Parse("Docs/P2p/completed_transactions.success.json")["data"]!.ToObject<GateP2pTransactionPage>()!;
        var detail = JsonFixture.Parse("Docs/P2p/transaction_detail.success.json")["data"]!.ToObject<GateP2pTransactionDetail>()!;
        var action = JsonFixture.Deserialize<GateP2pActionResult>("Docs/P2p/action.success.json");

        Assert.Equal(1, pending.Count);
        Assert.Equal(40000001, pending.List[0].TransactionId);
        Assert.Equal(1.002m, pending.List[0].Rate);
        Assert.Equal(50.5m, pending.List[0].Amount);
        Assert.Equal("bank", pending.List[0].OtherPaymentOptions[0].PayType);
        Assert.Equal(0.9998m, pending.List[0].ConvertInfo.Rate);
        Assert.Equal(1200, pending.TransactionTimes[0].OrderTime);
        Assert.Equal(40000002, completed.List[0].TransactionId);
        Assert.Equal("DONE", completed.List[0].Status);
        Assert.Equal(1, completed.ExportedNumber);
        Assert.Equal(40000001, detail.TransactionId);
        Assert.Equal(2124000001, detail.OrderId);
        Assert.Equal(50.601m, detail.Total);
        Assert.Equal("Counterparty Demo", detail.CounterpartyRealName);
        Assert.Equal("https://example.invalid/voucher.png", Assert.Single(detail.PaymentVoucherUrls));
        Assert.Equal("swift", detail.SupportedPaymentTypes[1]);
        Assert.Equal(0, action.Code);
        Assert.Equal("success", action.Message);
        Assert.NotEqual(default, action.Timestamp);
    }

    [Fact]
    public void Documented_advertisement_and_chat_responses_deserialize()
    {
        var status = JsonFixture.Parse("Docs/P2p/ad_status.success.json")["data"]!.ToObject<GateP2pAdStatusResult>()!;
        var advertisement = JsonFixture.Parse("Docs/P2p/advertisement.success.json")["data"]!.ToObject<GateP2pAdvertisement>()!;
        var myAdvertisements = JsonFixture.Parse("Docs/P2p/my_ads.success.json")["data"]!["lists"]!.ToObject<List<GateP2pAdvertisement>>()!;
        var marketAdvertisements = JsonFixture.Parse("Docs/P2p/market_ads.success.json")["data"]!.ToObject<List<GateP2pMarketAdvertisement>>()!;
        var chat = JsonFixture.Parse("Docs/P2p/chat_history.success.json")["data"]!.ToObject<GateP2pChatHistory>()!;
        var submitted = JsonFixture.Deserialize<GateP2pActionResult>("Docs/P2p/ad_risk.success.json");
        var sent = JsonFixture.Parse("Docs/P2p/send_chat.success.json")["data"]!.ToObject<GateP2pSendChatMessageResult>()!;
        var file = JsonFixture.Parse("Docs/P2p/chat_file.success.json")["data"]!.ToObject<GateP2pChatFile>()!;

        Assert.Equal(GateP2pAdStatusUpdate.Listed, status.Status);
        Assert.Equal(2124000001, advertisement.Id);
        Assert.Equal(GateP2pOrderSide.Sell, advertisement.Type);
        Assert.Equal(1.270m, advertisement.Rate);
        Assert.Equal("USDT", advertisement.CurrencyType);
        Assert.Equal(45, advertisement.ExpireMinutes);
        Assert.Equal(7, advertisement.UserOrdersLimit);
        Assert.Equal(GateP2pOrderSide.Sell, myAdvertisements[0].Type);
        Assert.Equal("OFFLIN", myAdvertisements[0].Status);
        Assert.Equal(2124000001, marketAdvertisements[0].AdvertisementId);
        Assert.Equal(1.270m, marketAdvertisements[0].Price);
        Assert.Equal(2, chat.Messages.Count);
        Assert.Equal("Payment sent", chat.Messages[0].Message);
        Assert.Equal(1, chat.Messages[0].RiskType);
        Assert.Equal("This message may contain security risks.", chat.Messages[0].ToastMessage);
        Assert.Equal("c2cchat_image/c2ctrade-demo-receipt|s3-gateio-payments", chat.Messages[1].MessageObject.FileKey);
        Assert.Equal("image", chat.Messages[1].FileType);
        Assert.Equal(40000001, chat.TransactionId);
        Assert.Equal("PAID", chat.OrderStatus);
        Assert.Equal(70305102, submitted.Code);
        Assert.Equal(0, submitted.Data.RiskCode);
        Assert.Equal("trade_tips_auto_reply", submitted.Data.RiskEvent.ContentRiskType);
        Assert.Equal("close", Assert.Single(submitted.Data.RiskEvent.Actions).ActionType);
        Assert.Equal(40000001, sent.TransactionId);
        Assert.Equal(GateP2pChatMessageType.Text, sent.MessageType);
        Assert.Equal(1, sent.RiskType);
        Assert.Equal("This message may contain security risks.", sent.ToastMessage);
        Assert.Equal("c2cchat_image/c2ctrade-demo-receipt|s3-gateio-payments", file.FileKey);
    }
}

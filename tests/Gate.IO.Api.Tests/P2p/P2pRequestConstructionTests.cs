using Gate.IO.Api.P2p;
using Gate.IO.Api.Tests.Infrastructure;
using System.Text;

namespace Gate.IO.Api.Tests.P2p;

[Trait("Category", "Unit")]
public class P2pRequestConstructionTests
{
    [Fact]
    public async Task Signed_p2p_requests_serialize_bodies_and_headers()
    {
        var responses = new Queue<string>([
            JsonFixture.Read("Docs/P2p/user_info.success.json"),
            JsonFixture.Read("Docs/P2p/counterparty_user_info.success.json"),
            JsonFixture.Read("Docs/P2p/payment_methods.success.json"),
            JsonFixture.Read("Docs/P2p/work_hours.success.json"),
            JsonFixture.Read("Docs/P2p/pending_transactions.success.json"),
            JsonFixture.Read("Docs/P2p/completed_transactions.success.json"),
            JsonFixture.Read("Docs/P2p/transaction_detail.success.json"),
            JsonFixture.Read("Docs/P2p/action.success.json"),
            JsonFixture.Read("Docs/P2p/action.success.json"),
            JsonFixture.Read("Docs/P2p/action.success.json"),
            JsonFixture.Read("Docs/P2p/ad_risk.success.json"),
            JsonFixture.Read("Docs/P2p/ad_status.success.json"),
            JsonFixture.Read("Docs/P2p/advertisement.success.json"),
            JsonFixture.Read("Docs/P2p/my_ads.success.json"),
            JsonFixture.Read("Docs/P2p/market_ads.success.json"),
            JsonFixture.Read("Docs/P2p/chat_history.success.json"),
            JsonFixture.Read("Docs/P2p/send_chat.success.json"),
            JsonFixture.Read("Docs/P2p/chat_file.success.json"),
        ]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");
        var start = DateTimeOffset.FromUnixTimeSeconds(1739000013).UtcDateTime;
        var end = DateTimeOffset.FromUnixTimeSeconds(1739086413).UtcDateTime;
        var firstReceived = DateTimeOffset.FromUnixTimeSeconds(1739015013).UtcDateTime;
        var lastReceived = DateTimeOffset.FromUnixTimeSeconds(1739015113).UtcDateTime;

        var user = await client.P2p.GetUserInfoAsync();
        var counterparty = await client.P2p.GetCounterpartyUserInfoAsync(new GateP2pCounterpartyUserInfoRequest
        {
            BusinessUserId = "biz_uid_demo_b84d21",
        });
        var paymentMethods = await client.P2p.GetPaymentMethodsAsync(new GateP2pPaymentMethodsRequest
        {
            Fiat = "USD",
        });
        var workHours = await client.P2p.SetMerchantWorkHoursAsync(new GateP2pMerchantWorkHoursRequest
        {
            WorkStatus = GateP2pMerchantWorkMode.CustomHours,
            CycleType = GateP2pMerchantWorkCycle.Weekly,
            DayOfWeek = "1,2,3,4,5",
            TimeZone = "+8",
            StartTime = "09:00",
            EndTime = "18:00",
        });
        var pending = await client.P2p.GetPendingTransactionsAsync(new GateP2pPendingTransactionsRequest
        {
            CryptoCurrency = "USDT",
            FiatCurrency = "USD",
            OrderTab = GateP2pOrderTab.Pending,
            SelectType = GateP2pOrderSide.Buy,
            Status = "PAYMENT_PENDING",
            TransactionId = 40000001,
            StartTime = start,
            EndTime = end,
        });
        var completed = await client.P2p.GetCompletedTransactionsAsync(new GateP2pCompletedTransactionsRequest
        {
            CryptoCurrency = "USDT",
            FiatCurrency = "USD",
            SelectType = GateP2pOrderSide.Sell,
            Status = "DONE",
            TransactionId = 40000002,
            StartTime = start,
            EndTime = end,
            QueryDispute = true,
            Page = 2,
            PerPage = 50,
        });
        var detail = await client.P2p.GetTransactionDetailsAsync(new GateP2pTransactionDetailsRequest
        {
            TransactionId = 40000001,
            Channel = "web",
        });
        var paid = await client.P2p.ConfirmPaymentAsync(new GateP2pConfirmPaymentRequest
        {
            TransactionId = 40000001,
            PaymentMethod = "bank",
        });
        var received = await client.P2p.ConfirmReceiptAsync(new GateP2pTransactionIdRequest
        {
            TransactionId = 40000001,
        });
        var cancelled = await client.P2p.CancelOrderAsync(new GateP2pCancelOrderRequest
        {
            TransactionId = 40000001,
            ReasonId = "1",
            ReasonMemo = "Canceled after agreement with the counterparty",
        });
        var submitted = await client.P2p.SubmitAdvertisementAsync(new GateP2pAdRequest
        {
            CurrencyType = "USDT",
            ExchangeType = "USD",
            Type = GateP2pAdOperationType.PublishSell,
            UnitPrice = 1.1m,
            Number = 100m,
            PayType = "bank",
            PayTypeJson = """{"bank":"10001","swift":"10002"}""",
            RateFixed = 1,
            OrderId = "2124000001",
            LimitBasis = GateP2pAdLimitBasis.Fiat,
            FiatMinAmount = 100m,
            FiatMaxAmount = 110m,
            TierLimit = 0,
            VerifiedLimit = 0,
            RegistrationTimeLimit = 0,
            AdvertisersLimit = 0,
            PolymarketRestricted = false,
            ExpireMinutes = 20,
            TradeTips = "Please pay from an account under your real name",
            AutoReply = "Thanks for your order. I will process it soon.",
            MinCompletedLimit = 2,
            MaxCompletedLimit = 100,
            CompletedRateLimit = 90m,
            UserCountryLimit = "-1",
            UserOrderLimit = 3,
            RateReferenceId = 1,
            RateOffset = 0.5m,
            FloatTrend = 0,
            TeamPaymentUserId = "1000001",
        });
        var status = await client.P2p.UpdateAdvertisementStatusAsync(new GateP2pAdStatusUpdateRequest
        {
            AdvertisementId = 2124000001,
            Status = GateP2pAdStatusUpdate.Delisted,
        });
        var advertisement = await client.P2p.GetAdvertisementAsync(new GateP2pAdvertisementIdRequest
        {
            AdvertisementId = "2124000001",
        });
        var myAdvertisements = await client.P2p.GetMyAdvertisementsAsync(new GateP2pAdListRequest
        {
            Asset = "USDT",
            FiatUnit = "USD",
            TradeType = GateP2pOrderSide.Sell,
        });
        var marketAdvertisements = await client.P2p.GetAdvertisementsAsync(new GateP2pMarketAdListRequest
        {
            Asset = "USDT",
            FiatUnit = "USD",
            TradeType = GateP2pOrderSide.Buy,
        });
        var chat = await client.P2p.GetChatHistoryAsync(new GateP2pChatHistoryRequest
        {
            TransactionId = 40000001,
            FirstReceived = firstReceived,
            LastReceived = lastReceived,
        });
        var sent = await client.P2p.SendChatMessageAsync(new GateP2pSendChatMessageRequest
        {
            TransactionId = 40000001,
            Message = "Payment sent",
            Type = GateP2pChatMessageType.Text,
        });
        var upload = await client.P2p.UploadChatFileAsync(new GateP2pUploadChatFileRequest
        {
            ContentType = "image/png",
            Base64Content = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAAB...",
        });

        Assert.True(user.Success, user.Error?.ToString());
        Assert.True(counterparty.Success, counterparty.Error?.ToString());
        Assert.True(paymentMethods.Success, paymentMethods.Error?.ToString());
        Assert.True(workHours.Success, workHours.Error?.ToString());
        Assert.True(pending.Success, pending.Error?.ToString());
        Assert.True(completed.Success, completed.Error?.ToString());
        Assert.True(detail.Success, detail.Error?.ToString());
        Assert.True(paid.Success, paid.Error?.ToString());
        Assert.True(received.Success, received.Error?.ToString());
        Assert.True(cancelled.Success, cancelled.Error?.ToString());
        Assert.True(submitted.Success, submitted.Error?.ToString());
        Assert.True(status.Success, status.Error?.ToString());
        Assert.True(advertisement.Success, advertisement.Error?.ToString());
        Assert.True(myAdvertisements.Success, myAdvertisements.Error?.ToString());
        Assert.True(marketAdvertisements.Success, marketAdvertisements.Error?.ToString());
        Assert.True(chat.Success, chat.Error?.ToString());
        Assert.True(sent.Success, sent.Error?.ToString());
        Assert.True(upload.Success, upload.Error?.ToString());
        Assert.Equal(GateP2pMerchantWorkStatus.CustomWorking, workHours.Data!.WorkStatus);
        Assert.Equal(70305102, submitted.Data!.Code);
        Assert.Equal("trade_tips_auto_reply", submitted.Data.Data!.RiskEvent.ContentRiskType);
        Assert.Equal(1, sent.Data!.RiskType);
        Assert.Equal("This message may contain security risks.", sent.Data.ToastMessage);
        Assert.Equal(18, handler.Requests.Count);

        Assert.Equal(HttpMethod.Post, handler.Requests[0].Method);
        Assert.Equal("/api/v4/p2p/merchant/account/get_user_info", handler.Requests[0].RequestUri.AbsolutePath);

        var counterpartyBody = ParseBody(handler.Requests[1]);
        Assert.Equal("/api/v4/p2p/merchant/account/get_counterparty_user_info", handler.Requests[1].RequestUri.AbsolutePath);
        Assert.Equal("biz_uid_demo_b84d21", counterpartyBody["biz_uid"]!.ToString());

        var paymentBody = ParseBody(handler.Requests[2]);
        Assert.Equal("/api/v4/p2p/merchant/account/get_myself_payment", handler.Requests[2].RequestUri.AbsolutePath);
        Assert.Equal("USD", paymentBody["fiat"]!.ToString());

        var workHoursBody = ParseBody(handler.Requests[3]);
        Assert.Equal("/api/v4/p2p/merchant/account/set_merchant_work_hours", handler.Requests[3].RequestUri.AbsolutePath);
        Assert.Equal("2", workHoursBody["work_status"]!.ToString());
        Assert.Equal("Weekly", workHoursBody["cycle_type"]!.ToString());
        Assert.Equal("1,2,3,4,5", workHoursBody["day_of_week"]!.ToString());
        Assert.Equal("+8", workHoursBody["time_zone"]!.ToString());
        Assert.Equal("09:00", workHoursBody["start_time"]!.ToString());
        Assert.Equal("18:00", workHoursBody["end_time"]!.ToString());

        var pendingBody = ParseBody(handler.Requests[4]);
        Assert.Equal("/api/v4/p2p/merchant/transaction/get_pending_transaction_list", handler.Requests[4].RequestUri.AbsolutePath);
        Assert.Equal("USDT", pendingBody["crypto_currency"]!.ToString());
        Assert.Equal("USD", pendingBody["fiat_currency"]!.ToString());
        Assert.Equal("pending", pendingBody["order_tab"]!.ToString());
        Assert.Equal("buy", pendingBody["select_type"]!.ToString());
        Assert.Equal("PAYMENT_PENDING", pendingBody["status"]!.ToString());
        Assert.Equal("40000001", pendingBody["txid"]!.ToString());
        Assert.Equal("1739000013", pendingBody["start_time"]!.ToString());
        Assert.Equal("1739086413", pendingBody["end_time"]!.ToString());

        var completedBody = ParseBody(handler.Requests[5]);
        Assert.Equal("/api/v4/p2p/merchant/transaction/get_completed_transaction_list", handler.Requests[5].RequestUri.AbsolutePath);
        Assert.Equal("sell", completedBody["select_type"]!.ToString());
        Assert.Equal("DONE", completedBody["status"]!.ToString());
        Assert.Equal("40000002", completedBody["txid"]!.ToString());
        Assert.Equal("1", completedBody["query_dispute"]!.ToString());
        Assert.Equal("2", completedBody["page"]!.ToString());
        Assert.Equal("50", completedBody["per_page"]!.ToString());

        var detailBody = ParseBody(handler.Requests[6]);
        Assert.Equal("/api/v4/p2p/merchant/transaction/get_transaction_details", handler.Requests[6].RequestUri.AbsolutePath);
        Assert.Equal("40000001", detailBody["txid"]!.ToString());
        Assert.Equal("web", detailBody["channel"]!.ToString());

        var paidBody = ParseBody(handler.Requests[7]);
        Assert.Equal("/api/v4/p2p/merchant/transaction/confirm-payment", handler.Requests[7].RequestUri.AbsolutePath);
        Assert.Equal("40000001", paidBody["txid"]!.ToString());
        Assert.Equal("bank", paidBody["payment_method"]!.ToString());

        Assert.Equal("/api/v4/p2p/merchant/transaction/confirm-receipt", handler.Requests[8].RequestUri.AbsolutePath);
        Assert.Equal("40000001", ParseBody(handler.Requests[8])["txid"]!.ToString());

        var cancelBody = ParseBody(handler.Requests[9]);
        Assert.Equal("/api/v4/p2p/merchant/transaction/cancel", handler.Requests[9].RequestUri.AbsolutePath);
        Assert.Equal("40000001", cancelBody["txid"]!.ToString());
        Assert.Equal("1", cancelBody["reason_id"]!.ToString());
        Assert.Equal("Canceled after agreement with the counterparty", cancelBody["reason_memo"]!.ToString());

        var adBody = ParseBody(handler.Requests[10]);
        Assert.Equal("/api/v4/p2p/merchant/books/place_biz_push_order", handler.Requests[10].RequestUri.AbsolutePath);
        Assert.Equal("USDT", adBody["currencyType"]!.ToString());
        Assert.Equal("USD", adBody["exchangeType"]!.ToString());
        Assert.Equal("0", adBody["type"]!.ToString());
        Assert.Equal("1.1", adBody["unitPrice"]!.ToString());
        Assert.Equal("100", adBody["number"]!.ToString());
        Assert.Equal("bank", adBody["payType"]!.ToString());
        Assert.Equal("""{"bank":"10001","swift":"10002"}""", adBody["pay_type_json"]!.ToString());
        Assert.Equal("1", adBody["rateFixed"]!.ToString());
        Assert.Equal("2124000001", adBody["oid"]!.ToString());
        Assert.Equal("1", adBody["limitBasis"]!.ToString());
        Assert.Equal("100", adBody["fiatMinAmount"]!.ToString());
        Assert.Equal("110", adBody["fiatMaxAmount"]!.ToString());
        Assert.Equal("0", adBody["polymarket_limit"]!.ToString());
        Assert.Equal("20", adBody["expire_min"]!.ToString());
        Assert.Equal("90", adBody["completed_rate_limit"]!.ToString());
        Assert.Equal("-1", adBody["user_country_limit"]!.ToString());
        Assert.Equal("1000001", adBody["team_payment_uid"]!.ToString());

        var statusBody = ParseBody(handler.Requests[11]);
        Assert.Equal("/api/v4/p2p/merchant/books/ads_update_status", handler.Requests[11].RequestUri.AbsolutePath);
        Assert.Equal("2124000001", statusBody["adv_no"]!.ToString());
        Assert.Equal("3", statusBody["adv_status"]!.ToString());

        Assert.Equal("/api/v4/p2p/merchant/books/ads_detail", handler.Requests[12].RequestUri.AbsolutePath);
        Assert.Equal("2124000001", ParseBody(handler.Requests[12])["adv_no"]!.ToString());

        var myAdsBody = ParseBody(handler.Requests[13]);
        Assert.Equal("/api/v4/p2p/merchant/books/my_ads_list", handler.Requests[13].RequestUri.AbsolutePath);
        Assert.Equal("USDT", myAdsBody["asset"]!.ToString());
        Assert.Equal("USD", myAdsBody["fiat_unit"]!.ToString());
        Assert.Equal("sell", myAdsBody["trade_type"]!.ToString());

        var marketAdsBody = ParseBody(handler.Requests[14]);
        Assert.Equal("/api/v4/p2p/merchant/books/ads_list", handler.Requests[14].RequestUri.AbsolutePath);
        Assert.Equal("buy", marketAdsBody["trade_type"]!.ToString());

        var chatBody = ParseBody(handler.Requests[15]);
        Assert.Equal("/api/v4/p2p/merchant/chat/get_chats_list", handler.Requests[15].RequestUri.AbsolutePath);
        Assert.Equal("40000001", chatBody["txid"]!.ToString());
        Assert.Equal("1739015113", chatBody["lastreceived"]!.ToString());
        Assert.Equal("1739015013", chatBody["firstreceived"]!.ToString());

        var sendChatBody = ParseBody(handler.Requests[16]);
        Assert.Equal("/api/v4/p2p/merchant/chat/send_chat_message", handler.Requests[16].RequestUri.AbsolutePath);
        Assert.Equal("40000001", sendChatBody["txid"]!.ToString());
        Assert.Equal("Payment sent", sendChatBody["message"]!.ToString());
        Assert.Equal("0", sendChatBody["type"]!.ToString());

        var uploadBody = ParseBody(handler.Requests[17]);
        Assert.Equal("/api/v4/p2p/merchant/chat/upload_chat_file", handler.Requests[17].RequestUri.AbsolutePath);
        Assert.Equal("image/png", uploadBody["image_content_type"]!.ToString());
        Assert.Equal("iVBORw0KGgoAAAANSUhEUgAAAAEAAAAB...", uploadBody["base64_img"]!.ToString());
        Assert.All(handler.Requests, AssertSignedHeaders);
    }

    [Fact]
    public async Task Work_hours_ad_limits_and_chat_content_validate_conditional_rules()
    {
        var client = new GateRestApiClient();

        var cycleException = await Assert.ThrowsAsync<ArgumentException>(() => client.P2p.SetMerchantWorkHoursAsync(new GateP2pMerchantWorkHoursRequest
        {
            WorkStatus = GateP2pMerchantWorkMode.CustomHours,
        }));
        var dayException = await Assert.ThrowsAsync<ArgumentException>(() => client.P2p.SetMerchantWorkHoursAsync(new GateP2pMerchantWorkHoursRequest
        {
            WorkStatus = GateP2pMerchantWorkMode.CustomHours,
            CycleType = GateP2pMerchantWorkCycle.Weekly,
            TimeZone = "+8",
            StartTime = "09:00",
            EndTime = "18:00",
        }));
        var fiatLimitException = await Assert.ThrowsAsync<ArgumentException>(() => client.P2p.SubmitAdvertisementAsync(new GateP2pAdRequest
        {
            CurrencyType = "USDT",
            ExchangeType = "USD",
            PayType = "bank",
            LimitBasis = GateP2pAdLimitBasis.Fiat,
        }));
        var marketListException = await Assert.ThrowsAsync<ArgumentException>(() => client.P2p.GetAdvertisementsAsync(new GateP2pMarketAdListRequest()));
        var chatException = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.P2p.SendChatMessageAsync(40000001, new string('x', 501)));

        Assert.Equal("CycleType", cycleException.ParamName);
        Assert.Equal("DayOfWeek", dayException.ParamName);
        Assert.Equal("FiatMinAmount", fiatLimitException.ParamName);
        Assert.Equal("Asset", marketListException.ParamName);
        Assert.Equal("Message", chatException.ParamName);
    }

    private static GateRestApiClient CreateClient(RecordingHttpMessageHandler handler)
        => new(new GateRestApiClientOptions
        {
            HttpClient = new HttpClient(handler),
        });

    private static HttpResponseMessage JsonResponse(string json)
        => new(System.Net.HttpStatusCode.OK)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json"),
        };

    private static JObject ParseBody(RecordedHttpRequest request)
        => JObject.Parse(request.Content);

    private static void AssertSignedHeaders(RecordedHttpRequest request)
    {
        Assert.Equal("key", Assert.Single(request.Headers["KEY"]));
        Assert.NotEmpty(Assert.Single(request.Headers["Timestamp"]));
        Assert.NotEmpty(Assert.Single(request.Headers["SIGN"]));
        Assert.True(request.Headers.ContainsKey("X-Gate-Channel-Id"));
    }
}

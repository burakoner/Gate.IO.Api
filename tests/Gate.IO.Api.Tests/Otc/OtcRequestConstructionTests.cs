using ApiSharp.Authentication;
using Gate.IO.Api.Otc;
using Gate.IO.Api.Tests.Infrastructure;
using System.Text;

namespace Gate.IO.Api.Tests.Otc;

[Trait("Category", "Unit")]
public class OtcRequestConstructionTests
{
    [Fact]
    public async Task Signed_otc_order_requests_serialize_bodies_queries_and_headers()
    {
        var responses = new Queue<string>([
            JsonFixture.Read("Docs/Otc/quote.success.json"),
            JsonFixture.Read("Docs/Otc/action.success.json"),
            JsonFixture.Read("Docs/Otc/stablecoin_order_create.success.json"),
            JsonFixture.Read("Docs/Otc/bank_list.success.json"),
            JsonFixture.Read("Docs/Otc/bank_create.success.json"),
            JsonFixture.Read("Docs/Otc/action.success.json"),
            JsonFixture.Read("Docs/Otc/action.success.json"),
            JsonFixture.Read("Docs/Otc/bank_supplement_checklist.success.json"),
            JsonFixture.Read("Docs/Otc/action.success.json"),
            JsonFixture.Read("Docs/Otc/action.success.json"),
            JsonFixture.Read("Docs/Otc/action.success.json"),
            JsonFixture.Read("Docs/Otc/action.success.json"),
            JsonFixture.Read("Docs/Otc/fiat_orders.success.json"),
            JsonFixture.Read("Docs/Otc/stablecoin_orders.success.json"),
            JsonFixture.Read("Docs/Otc/fiat_order_detail.success.json"),
        ]);
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(responses.Dequeue()));
        var client = CreateClient(handler);
        client.SetApiCredentials("key", "secret");
        var start = new DateTime(2025, 02, 11, 07, 45, 06, DateTimeKind.Utc);
        var end = new DateTime(2025, 09, 09, 10, 00, 00, DateTimeKind.Utc);

        var quote = await client.Otc.GetQuoteAsync(new GateOtcQuoteRequest
        {
            Side = GateOtcQuoteSide.Pay,
            PayCoin = "USDT",
            GetCoin = "USD",
            PayAmount = 30000m,
            GetAmount = 30000m,
            CreateQuoteToken = false,
            PromotionCode = "",
        });
        var fiatOrder = await client.Otc.CreateFiatOrderAsync(new GateOtcFiatOrderRequest
        {
            Type = GateOtcOrderType.Buy,
            Side = GateOtcOrderKind.Fiat,
            CryptoCurrency = "USDT",
            FiatCurrency = "USD",
            CryptoAmount = 30000m,
            FiatAmount = 30000m,
            PromotionCode = "",
            QuoteToken = "quote-token",
            BankId = 2,
        });
        var stableOrder = await client.Otc.CreateStableCoinOrderAsync(new GateOtcStableCoinOrderRequest
        {
            PayCoin = "USDC",
            GetCoin = "USDT",
            PayAmount = 30000m,
            GetAmount = 20000m,
            Side = GateOtcQuoteSide.Pay,
            PromotionCode = "",
            QuoteToken = "dsafjkdshfjdsjkfah",
        });
        var banks = await client.Otc.GetBankAccountsAsync();
        var createdBank = await client.Otc.CreateBankCardAsync(new GateOtcBankCreateRequest
        {
            BankAccountName = "Ada Lovelace",
            BankName = "Example Bank",
            BankCountry = "GB",
            BankAddress = "1 Bank Street",
            Iban = "GB82WEST12345698765432",
            Swift = "WESTGB2L",
            RemittanceLineNumber = "021000021",
            AgentBankName = "Correspondent Bank",
            AgentBankSwift = "CORRGB2L",
            DocumentationFile = "BASE64-ACCOUNT-PROOF",
        });
        var deletedBank = await client.Otc.DeleteBankCardAsync("762");
        var defaultBank = await client.Otc.SetDefaultBankCardAsync("762");
        var checklist = await client.Otc.GetBankSupplementChecklistAsync("762");
        var personalSupplement = await client.Otc.SubmitPersonalBankSupplementAsync(new GateOtcBankPersonalSupplementRequest
        {
            BankId = "762",
            IdDocumentFront = "BASE64-ID-FRONT",
            IdDocumentBack = "BASE64-ID-BACK",
            AddressProof = "BASE64-ADDRESS-PROOF",
            RelationshipProof = "{\"relationship\":\"account-holder\"}",
        });
        var enterpriseSupplement = await client.Otc.SubmitEnterpriseBankSupplementAsync(new GateOtcBankEnterpriseSupplementRequest
        {
            UserId = "10001",
            BankId = "762",
            Certificate = "BASE64-CERTIFICATE",
            ShareHolders = "BASE64-SHAREHOLDERS",
            Passport = "BASE64-PASSPORT",
            ShareHoldingStructure = "BASE64-STRUCTURE",
            FundsStatement = "BASE64-FUNDS",
            Additional = "BASE64-ADDITIONAL",
            RelationshipProof = "{\"relationship\":\"beneficial-owner\"}",
        });
        var paid = await client.Otc.MarkFiatOrderAsPaidAsync(new GateOtcMarkOrderPaidRequest
        {
            OrderId = "203",
            ClientOrderId = "merchant-order-203",
            PaymentReceiptFileKey = "receipt-file-key",
            PaymentReceipt = "receipt-file-key",
        });
        var cancelled = await client.Otc.CancelFiatOrderAsync(203);
        var fiatOrders = await client.Otc.GetFiatOrdersAsync(new GateOtcFiatOrderListRequest
        {
            Type = GateOtcOrderType.Sell,
            FiatCurrency = "USD",
            CryptoCurrency = "USDT",
            StartTime = start,
            EndTime = end,
            Status = "DONE",
            PageNumber = 1,
            PageSize = 10,
        });
        var stableOrders = await client.Otc.GetStableCoinOrdersAsync(new GateOtcStableCoinOrderListRequest
        {
            PageSize = 10,
            PageNumber = 1,
            CoinName = "USDT",
            StartTime = start,
            EndTime = end,
            Status = "PROCESSING",
        });
        var detail = await client.Otc.GetFiatOrderAsync(41);

        Assert.True(quote.Success, quote.Error?.ToString());
        Assert.True(fiatOrder.Success, fiatOrder.Error?.ToString());
        Assert.True(stableOrder.Success, stableOrder.Error?.ToString());
        Assert.True(banks.Success, banks.Error?.ToString());
        Assert.True(createdBank.Success, createdBank.Error?.ToString());
        Assert.True(deletedBank.Success, deletedBank.Error?.ToString());
        Assert.True(defaultBank.Success, defaultBank.Error?.ToString());
        Assert.True(checklist.Success, checklist.Error?.ToString());
        Assert.True(personalSupplement.Success, personalSupplement.Error?.ToString());
        Assert.True(enterpriseSupplement.Success, enterpriseSupplement.Error?.ToString());
        Assert.True(paid.Success, paid.Error?.ToString());
        Assert.True(cancelled.Success, cancelled.Error?.ToString());
        Assert.True(fiatOrders.Success, fiatOrders.Error?.ToString());
        Assert.True(stableOrders.Success, stableOrders.Error?.ToString());
        Assert.True(detail.Success, detail.Error?.ToString());
        Assert.Equal("[multipart/form-data content omitted]", createdBank.Request!.Body);
        Assert.Equal("[multipart/form-data content omitted]", personalSupplement.Request!.Body);
        Assert.Equal("[multipart/form-data content omitted]", enterpriseSupplement.Request!.Body);
        Assert.Equal(15, handler.Requests.Count);

        var quoteBody = JObject.Parse(handler.Requests[0].Content);
        Assert.Equal(HttpMethod.Post, handler.Requests[0].Method);
        Assert.Equal("/api/v4/otc/quote", handler.Requests[0].RequestUri.AbsolutePath);
        Assert.Equal("PAY", quoteBody["side"]!.ToString());
        Assert.Equal("USDT", quoteBody["pay_coin"]!.ToString());
        Assert.Equal("USD", quoteBody["get_coin"]!.ToString());
        Assert.Equal("30000", quoteBody["pay_amount"]!.ToString());
        Assert.Equal("30000", quoteBody["get_amount"]!.ToString());
        Assert.Equal("0", quoteBody["create_quote_token"]!.ToString());
        Assert.Equal("", quoteBody["promotion_code"]!.ToString());

        var fiatBody = JObject.Parse(handler.Requests[1].Content);
        Assert.Equal("/api/v4/otc/order/create", handler.Requests[1].RequestUri.AbsolutePath);
        Assert.Equal("BUY", fiatBody["type"]!.ToString());
        Assert.Equal("FIAT", fiatBody["side"]!.ToString());
        Assert.Equal("USDT", fiatBody["crypto_currency"]!.ToString());
        Assert.Equal("USD", fiatBody["fiat_currency"]!.ToString());
        Assert.Equal("30000", fiatBody["crypto_amount"]!.ToString());
        Assert.Equal("30000", fiatBody["fiat_amount"]!.ToString());
        Assert.Equal("quote-token", fiatBody["quote_token"]!.ToString());
        Assert.Equal("2", fiatBody["bank_id"]!.ToString());

        var stableBody = JObject.Parse(handler.Requests[2].Content);
        Assert.Equal("/api/v4/otc/stable_coin/order/create", handler.Requests[2].RequestUri.AbsolutePath);
        Assert.Equal("USDC", stableBody["pay_coin"]!.ToString());
        Assert.Equal("USDT", stableBody["get_coin"]!.ToString());
        Assert.Equal("30000", stableBody["pay_amount"]!.ToString());
        Assert.Equal("20000", stableBody["get_amount"]!.ToString());
        Assert.Equal("PAY", stableBody["side"]!.ToString());
        Assert.Equal("dsafjkdshfjdsjkfah", stableBody["quote_token"]!.ToString());

        Assert.Equal(HttpMethod.Get, handler.Requests[3].Method);
        Assert.Equal("/api/v4/otc/bank/list", handler.Requests[3].RequestUri.AbsolutePath);

        Assert.Equal("/api/v4/otc/bank/create", handler.Requests[4].RequestUri.AbsolutePath);
        AssertMultipartField(handler.Requests[4], "bank_account_name", "Ada Lovelace");
        AssertMultipartField(handler.Requests[4], "bank_name", "Example Bank");
        AssertMultipartField(handler.Requests[4], "bank_country", "GB");
        AssertMultipartField(handler.Requests[4], "bank_address", "1 Bank Street");
        AssertMultipartField(handler.Requests[4], "iban", "GB82WEST12345698765432");
        AssertMultipartField(handler.Requests[4], "swift", "WESTGB2L");
        AssertMultipartField(handler.Requests[4], "remittance_line_number", "021000021");
        AssertMultipartField(handler.Requests[4], "agent_bank_name", "Correspondent Bank");
        AssertMultipartField(handler.Requests[4], "agent_bank_swift", "CORRGB2L");
        AssertMultipartField(handler.Requests[4], "documentation_file", "BASE64-ACCOUNT-PROOF");
        AssertMultipartSignature(handler.Requests[4]);

        var deleteBody = JObject.Parse(handler.Requests[5].Content);
        Assert.Equal("/api/v4/otc/bank/delete", handler.Requests[5].RequestUri.AbsolutePath);
        Assert.Equal("762", deleteBody["bank_id"]!.ToString());

        var defaultBody = JObject.Parse(handler.Requests[6].Content);
        Assert.Equal("/api/v4/otc/bank/set_default", handler.Requests[6].RequestUri.AbsolutePath);
        Assert.Equal("762", defaultBody["bank_id"]!.ToString());

        var checklistQuery = ParseQuery(handler.Requests[7].RequestUri);
        Assert.Equal("/api/v4/otc/bank/bank_supplement_checklist", handler.Requests[7].RequestUri.AbsolutePath);
        Assert.Equal("762", checklistQuery["bank_id"]);

        Assert.Equal("/api/v4/otc/bank/personal/bank_supplement", handler.Requests[8].RequestUri.AbsolutePath);
        AssertMultipartField(handler.Requests[8], "bank_id", "762");
        AssertMultipartField(handler.Requests[8], "id_document_front", "BASE64-ID-FRONT");
        AssertMultipartField(handler.Requests[8], "id_document_back", "BASE64-ID-BACK");
        AssertMultipartField(handler.Requests[8], "address_proof", "BASE64-ADDRESS-PROOF");
        AssertMultipartField(handler.Requests[8], "relationship_proof", "{\"relationship\":\"account-holder\"}");
        AssertMultipartSignature(handler.Requests[8]);

        Assert.Equal("/api/v4/otc/bank/enterprise/bank_supplement", handler.Requests[9].RequestUri.AbsolutePath);
        AssertMultipartField(handler.Requests[9], "uid", "10001");
        AssertMultipartField(handler.Requests[9], "bank_id", "762");
        AssertMultipartField(handler.Requests[9], "certificate", "BASE64-CERTIFICATE");
        AssertMultipartField(handler.Requests[9], "share_holders", "BASE64-SHAREHOLDERS");
        AssertMultipartField(handler.Requests[9], "passport", "BASE64-PASSPORT");
        AssertMultipartField(handler.Requests[9], "share_holding_structure", "BASE64-STRUCTURE");
        AssertMultipartField(handler.Requests[9], "funds_statement", "BASE64-FUNDS");
        AssertMultipartField(handler.Requests[9], "additional", "BASE64-ADDITIONAL");
        AssertMultipartField(handler.Requests[9], "relationship_proof", "{\"relationship\":\"beneficial-owner\"}");
        AssertMultipartSignature(handler.Requests[9]);

        var paidBody = JObject.Parse(handler.Requests[10].Content);
        Assert.Equal("/api/v4/otc/order/paid", handler.Requests[10].RequestUri.AbsolutePath);
        Assert.Equal("203", paidBody["order_id"]!.ToString());
        Assert.Equal("merchant-order-203", paidBody["client_order_id"]!.ToString());
        Assert.Equal("receipt-file-key", paidBody["payment_receipt_file_key"]!.ToString());
        Assert.Equal("receipt-file-key", paidBody["payment_receipt"]!.ToString());

        var cancelQuery = ParseQuery(handler.Requests[11].RequestUri);
        Assert.Equal("/api/v4/otc/order/cancel", handler.Requests[11].RequestUri.AbsolutePath);
        Assert.Equal("203", cancelQuery["order_id"]);
        Assert.Equal(string.Empty, handler.Requests[11].Content);

        var fiatQuery = ParseQuery(handler.Requests[12].RequestUri);
        Assert.Equal("/api/v4/otc/order/list", handler.Requests[12].RequestUri.AbsolutePath);
        Assert.Equal("SELL", fiatQuery["type"]);
        Assert.Equal("USD", fiatQuery["fiat_currency"]);
        Assert.Equal("USDT", fiatQuery["crypto_currency"]);
        Assert.Equal("2025-02-11 07:45:06", fiatQuery["start_time"]);
        Assert.Equal("2025-09-09 10:00:00", fiatQuery["end_time"]);
        Assert.Equal("DONE", fiatQuery["status"]);
        Assert.Equal("1", fiatQuery["pn"]);
        Assert.Equal("10", fiatQuery["ps"]);

        var stableQuery = ParseQuery(handler.Requests[13].RequestUri);
        Assert.Equal("/api/v4/otc/stable_coin/order/list", handler.Requests[13].RequestUri.AbsolutePath);
        Assert.Equal("10", stableQuery["page_size"]);
        Assert.Equal("1", stableQuery["page_number"]);
        Assert.Equal("USDT", stableQuery["coin_name"]);
        Assert.Equal("PROCESSING", stableQuery["status"]);

        var detailQuery = ParseQuery(handler.Requests[14].RequestUri);
        Assert.Equal("/api/v4/otc/order/detail", handler.Requests[14].RequestUri.AbsolutePath);
        Assert.Equal("41", detailQuery["order_id"]);
        Assert.All(handler.Requests, AssertSignedHeaders);
    }

    [Fact]
    public async Task Quote_requests_validate_required_amount_for_direction()
    {
        var client = new GateRestApiClient();

        var payException = await Assert.ThrowsAsync<ArgumentException>(() => client.Otc.GetQuoteAsync(new GateOtcQuoteRequest
        {
            Side = GateOtcQuoteSide.Pay,
            PayCoin = "USDT",
            GetCoin = "USD",
            GetAmount = 30000m,
        }));
        var getException = await Assert.ThrowsAsync<ArgumentException>(() => client.Otc.GetQuoteAsync(new GateOtcQuoteRequest
        {
            Side = GateOtcQuoteSide.Get,
            PayCoin = "USDT",
            GetCoin = "USD",
            PayAmount = 30000m,
        }));

        Assert.Equal("PayAmount", payException.ParamName);
        Assert.Equal("GetAmount", getException.ParamName);
    }

    [Fact]
    public async Task Bank_and_payment_requests_validate_required_identifiers_and_materials()
    {
        var client = new GateRestApiClient();

        var bankException = await Assert.ThrowsAsync<ArgumentException>(() => client.Otc.CreateBankCardAsync(new GateOtcBankCreateRequest
        {
            BankAccountName = "Ada Lovelace",
            BankName = "Example Bank",
            BankCountry = "GB",
            BankAddress = "1 Bank Street",
            Iban = "GB82WEST12345698765432",
            Swift = "WESTGB2L",
        }));
        var paidException = await Assert.ThrowsAsync<ArgumentException>(() => client.Otc.MarkFiatOrderAsPaidAsync(new GateOtcMarkOrderPaidRequest
        {
            OrderId = "203",
        }));

        Assert.Equal("DocumentationFile", bankException.ParamName);
        Assert.Equal("PaymentReceiptFileKey", paidException.ParamName);
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

    private static Dictionary<string, string> ParseQuery(Uri uri)
    {
        return uri.Query.TrimStart('?')
            .Split('&', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Split(['='], 2))
            .ToDictionary(
                x => Uri.UnescapeDataString(x[0]),
                x => x.Length == 1 ? string.Empty : Uri.UnescapeDataString(x[1]).Replace("+", " ", StringComparison.Ordinal));
    }

    private static void AssertSignedHeaders(RecordedHttpRequest request)
    {
        Assert.Equal("key", Assert.Single(request.Headers["KEY"]));
        Assert.NotEmpty(Assert.Single(request.Headers["Timestamp"]));
        Assert.NotEmpty(Assert.Single(request.Headers["SIGN"]));
        Assert.True(request.Headers.ContainsKey("X-Gate-Channel-Id"));
    }

    private static void AssertMultipartField(RecordedHttpRequest request, string name, string value)
    {
        var contentType = Assert.Single(request.Headers["Content-Type"]);
        Assert.StartsWith("multipart/form-data; boundary=", contentType, StringComparison.OrdinalIgnoreCase);
        Assert.Contains($"Content-Disposition: form-data; name=\"{name}\"\r\n\r\n{value}\r\n", request.Content, StringComparison.Ordinal);

        var boundary = contentType[(contentType.IndexOf("boundary=", StringComparison.OrdinalIgnoreCase) + "boundary=".Length)..];
        Assert.EndsWith($"--{boundary}--\r\n", request.Content, StringComparison.Ordinal);
    }

    private static void AssertMultipartSignature(RecordedHttpRequest request)
    {
        var timestamp = Assert.Single(request.Headers["Timestamp"]);
        var authentication = new GateAuthentication(new ApiCredentials("key", "secret"));
        var signature = authentication.CreateRestSignature(
            request.Method,
            request.RequestUri.AbsolutePath,
            Uri.UnescapeDataString(request.RequestUri.Query.TrimStart('?')),
            request.Content,
            timestamp);

        Assert.Equal(signature, Assert.Single(request.Headers["SIGN"]));
    }
}

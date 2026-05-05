namespace Gate.IO.Api.P2p;

/// <summary>
/// Gate.IO P2P REST API Client
/// </summary>
public class GateP2pRestApiClient
{
    // Api
    private const string api = "api";
    private const string v4 = "4";
    private const string p2p = "p2p";

    // Root Client
    internal GateRestApiClient _ { get; }

    // Constructor
    internal GateP2pRestApiClient(GateRestApiClient root) => _ = root;

    private static long? ToSeconds(DateTime? time)
        => time?.ConvertToSeconds();

    private static string ToStringInvariant<T>(T value)
        => Convert.ToString(value, CultureInfo.InvariantCulture);

    private static GateP2pActionResult ToActionResult(GateP2pResponse<object> response)
        => new()
        {
            Code = response.Code,
            Message = response.Message,
            Method = response.Method,
            Timestamp = response.Timestamp,
            Version = response.Version,
        };

    private static void AddTransactionParameters(ParameterCollection parameters, GateP2pPendingTransactionsRequest request)
    {
        parameters.Add("crypto_currency", request.CryptoCurrency);
        parameters.Add("fiat_currency", request.FiatCurrency);
        parameters.AddOptionalEnum("order_tab", request.OrderTab);
        parameters.AddOptionalEnum("select_type", request.SelectType);
        parameters.AddOptional("status", request.Status);
        parameters.AddOptional("txid", request.TransactionId);
        parameters.AddOptional("start_time", ToSeconds(request.StartTime));
        parameters.AddOptional("end_time", ToSeconds(request.EndTime));
    }

    private static void AddTransactionParameters(ParameterCollection parameters, GateP2pCompletedTransactionsRequest request)
    {
        parameters.Add("crypto_currency", request.CryptoCurrency);
        parameters.Add("fiat_currency", request.FiatCurrency);
        parameters.AddOptionalEnum("select_type", request.SelectType);
        parameters.AddOptional("status", request.Status);
        parameters.AddOptional("txid", request.TransactionId);
        parameters.AddOptional("start_time", ToSeconds(request.StartTime));
        parameters.AddOptional("end_time", ToSeconds(request.EndTime));
        parameters.AddOptional("query_dispute", request.QueryDispute.HasValue ? (request.QueryDispute.Value ? 1 : 0) : null);
        parameters.AddOptional("page", request.Page);
        parameters.AddOptional("per_page", request.PerPage);
    }

    private static void AddAdvertisementListParameters(ParameterCollection parameters, GateP2pAdListRequest request)
    {
        parameters.AddOptional("asset", request.Asset);
        parameters.AddOptional("fiat_unit", request.FiatUnit);
        parameters.AddOptionalEnum("trade_type", request.TradeType);
    }

    private static ParameterCollection CreateAdvertisementParameters(GateP2pAdRequest request)
    {
        var parameters = new ParameterCollection
        {
            { "currencyType", request.CurrencyType },
            { "exchangeType", request.ExchangeType },
            { "unitPrice", ToStringInvariant(request.UnitPrice) },
            { "number", ToStringInvariant(request.Number) },
            { "payType", request.PayType },
            { "minAmount", ToStringInvariant(request.MinAmount) },
            { "maxAmount", ToStringInvariant(request.MaxAmount) },
        };
        parameters.AddEnum("type", request.Type);
        parameters.AddOptional("pay_type_json", request.PayTypeJson);
        parameters.AddOptional("rateFixed", request.RateFixed.HasValue ? ToStringInvariant(request.RateFixed.Value) : null);
        parameters.AddOptional("oid", request.OrderId.HasValue ? ToStringInvariant(request.OrderId.Value) : null);
        parameters.AddOptional("tierLimit", request.TierLimit.HasValue ? ToStringInvariant(request.TierLimit.Value) : null);
        parameters.AddOptional("verifiedLimit", request.VerifiedLimit.HasValue ? ToStringInvariant(request.VerifiedLimit.Value) : null);
        parameters.AddOptional("regTimeLimit", request.RegistrationTimeLimit.HasValue ? ToStringInvariant(request.RegistrationTimeLimit.Value) : null);
        parameters.AddOptional("advertisersLimit", request.AdvertisersLimit.HasValue ? ToStringInvariant(request.AdvertisersLimit.Value) : null);
        parameters.AddOptional("expire_min", request.ExpireMinutes.HasValue ? ToStringInvariant(request.ExpireMinutes.Value) : null);
        parameters.AddOptional("trade_tips", request.TradeTips);
        parameters.AddOptional("auto_reply", request.AutoReply);
        parameters.AddOptional("min_completed_limit", request.MinCompletedLimit.HasValue ? ToStringInvariant(request.MinCompletedLimit.Value) : null);
        parameters.AddOptional("max_completed_limit", request.MaxCompletedLimit.HasValue ? ToStringInvariant(request.MaxCompletedLimit.Value) : null);
        parameters.AddOptional("completed_rate_limit", request.CompletedRateLimit.HasValue ? ToStringInvariant(request.CompletedRateLimit.Value) : null);
        parameters.AddOptional("user_country_limit", request.UserCountryLimit.HasValue ? ToStringInvariant(request.UserCountryLimit.Value) : null);
        parameters.AddOptional("user_order_limit", request.UserOrderLimit.HasValue ? ToStringInvariant(request.UserOrderLimit.Value) : null);
        parameters.AddOptional("rateReferenceId", request.RateReferenceId.HasValue ? ToStringInvariant(request.RateReferenceId.Value) : null);
        parameters.AddOptional("rateOffset", request.RateOffset.HasValue ? ToStringInvariant(request.RateOffset.Value) : null);
        parameters.AddOptional("float_trend", request.FloatTrend.HasValue ? ToStringInvariant(request.FloatTrend.Value) : null);
        parameters.AddOptional("team_payment_uid", request.TeamPaymentUserId.HasValue ? ToStringInvariant(request.TeamPaymentUserId.Value) : null);

        return parameters;
    }

    private async Task<RestCallResult<T>> SendP2pDataRequestAsync<T>(
        string endpoint,
        HttpMethod method,
        CancellationToken ct,
        ParameterCollection bodyParameters = null) where T : class
    {
        var result = await _.SendRequestInternal<GateP2pResponse<T>>(_.GetUrl(api, v4, p2p, endpoint), method, ct, true, bodyParameters: bodyParameters).ConfigureAwait(false);
        return result.Success ? result.As(result.Data?.Data) : result.As<T>(default);
    }

    private async Task<RestCallResult<GateP2pActionResult>> SendP2pActionRequestAsync(
        string endpoint,
        HttpMethod method,
        CancellationToken ct,
        ParameterCollection bodyParameters = null)
    {
        var result = await _.SendRequestInternal<GateP2pResponse<object>>(_.GetUrl(api, v4, p2p, endpoint), method, ct, true, bodyParameters: bodyParameters).ConfigureAwait(false);
        return result.Success ? result.As(ToActionResult(result.Data)) : result.As<GateP2pActionResult>(default);
    }

    /// <summary>
    /// Get P2P account information
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateP2pUserInfo>> GetUserInfoAsync(CancellationToken ct = default)
        => SendP2pDataRequestAsync<GateP2pUserInfo>("merchant/account/get_user_info", HttpMethod.Post, ct);

    /// <summary>
    /// Get P2P counterparty information
    /// </summary>
    /// <param name="businessUserId">Counterparty encrypted UID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateP2pUserInfo>> GetCounterpartyUserInfoAsync(string businessUserId, CancellationToken ct = default)
        => GetCounterpartyUserInfoAsync(new GateP2pCounterpartyUserInfoRequest { BusinessUserId = businessUserId }, ct);

    /// <summary>
    /// Get P2P counterparty information
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateP2pUserInfo>> GetCounterpartyUserInfoAsync(GateP2pCounterpartyUserInfoRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "biz_uid", request.BusinessUserId },
        };

        return SendP2pDataRequestAsync<GateP2pUserInfo>("merchant/account/get_counterparty_user_info", HttpMethod.Post, ct, parameters);
    }

    /// <summary>
    /// Get P2P payment method list
    /// </summary>
    /// <param name="fiat">Fiat currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateP2pPaymentMethodGroup>>> GetPaymentMethodsAsync(string fiat = null, CancellationToken ct = default)
        => GetPaymentMethodsAsync(new GateP2pPaymentMethodsRequest { Fiat = fiat }, ct);

    /// <summary>
    /// Get P2P payment method list
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateP2pPaymentMethodGroup>>> GetPaymentMethodsAsync(GateP2pPaymentMethodsRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("fiat", request.Fiat);

        return SendP2pDataRequestAsync<List<GateP2pPaymentMethodGroup>>("merchant/account/get_myself_payment", HttpMethod.Post, ct, parameters);
    }

    /// <summary>
    /// Get pending P2P orders
    /// </summary>
    /// <param name="cryptoCurrency">Cryptocurrency symbol</param>
    /// <param name="fiatCurrency">Fiat currency</param>
    /// <param name="orderTab">Order tab</param>
    /// <param name="selectType">Order side filter</param>
    /// <param name="status">Order status filter</param>
    /// <param name="transactionId">Order ID</param>
    /// <param name="startTime">Start timestamp</param>
    /// <param name="endTime">End timestamp</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateP2pTransactionPage>> GetPendingTransactionsAsync(
        string cryptoCurrency,
        string fiatCurrency,
        GateP2pOrderTab? orderTab = null,
        GateP2pOrderSide? selectType = null,
        string status = null,
        long? transactionId = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        CancellationToken ct = default)
        => GetPendingTransactionsAsync(new GateP2pPendingTransactionsRequest
        {
            CryptoCurrency = cryptoCurrency,
            FiatCurrency = fiatCurrency,
            OrderTab = orderTab,
            SelectType = selectType,
            Status = status,
            TransactionId = transactionId,
            StartTime = startTime,
            EndTime = endTime,
        }, ct);

    /// <summary>
    /// Get pending P2P orders
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateP2pTransactionPage>> GetPendingTransactionsAsync(GateP2pPendingTransactionsRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        AddTransactionParameters(parameters, request);

        return SendP2pDataRequestAsync<GateP2pTransactionPage>("merchant/transaction/get_pending_transaction_list", HttpMethod.Post, ct, parameters);
    }

    /// <summary>
    /// Get completed P2P orders
    /// </summary>
    /// <param name="cryptoCurrency">Cryptocurrency symbol</param>
    /// <param name="fiatCurrency">Fiat currency</param>
    /// <param name="selectType">Order side filter</param>
    /// <param name="status">Order status filter</param>
    /// <param name="transactionId">Order ID</param>
    /// <param name="startTime">Start timestamp</param>
    /// <param name="endTime">End timestamp</param>
    /// <param name="queryDispute">Whether to flag dispute status</param>
    /// <param name="page">Page number</param>
    /// <param name="perPage">Orders per page</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateP2pTransactionPage>> GetCompletedTransactionsAsync(
        string cryptoCurrency,
        string fiatCurrency,
        GateP2pOrderSide? selectType = null,
        string status = null,
        long? transactionId = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        bool? queryDispute = null,
        int? page = null,
        int? perPage = null,
        CancellationToken ct = default)
        => GetCompletedTransactionsAsync(new GateP2pCompletedTransactionsRequest
        {
            CryptoCurrency = cryptoCurrency,
            FiatCurrency = fiatCurrency,
            SelectType = selectType,
            Status = status,
            TransactionId = transactionId,
            StartTime = startTime,
            EndTime = endTime,
            QueryDispute = queryDispute,
            Page = page,
            PerPage = perPage,
        }, ct);

    /// <summary>
    /// Get completed P2P orders
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateP2pTransactionPage>> GetCompletedTransactionsAsync(GateP2pCompletedTransactionsRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        AddTransactionParameters(parameters, request);

        return SendP2pDataRequestAsync<GateP2pTransactionPage>("merchant/transaction/get_completed_transaction_list", HttpMethod.Post, ct, parameters);
    }

    /// <summary>
    /// Get P2P order details
    /// </summary>
    /// <param name="transactionId">Order ID</param>
    /// <param name="channel">Channel tag</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateP2pTransactionDetail>> GetTransactionDetailsAsync(long transactionId, string channel = null, CancellationToken ct = default)
        => GetTransactionDetailsAsync(new GateP2pTransactionDetailsRequest { TransactionId = transactionId, Channel = channel }, ct);

    /// <summary>
    /// Get P2P order details
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateP2pTransactionDetail>> GetTransactionDetailsAsync(GateP2pTransactionDetailsRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "txid", request.TransactionId },
        };
        parameters.AddOptional("channel", request.Channel);

        return SendP2pDataRequestAsync<GateP2pTransactionDetail>("merchant/transaction/get_transaction_details", HttpMethod.Post, ct, parameters);
    }

    /// <summary>
    /// Confirm P2P payment
    /// </summary>
    /// <param name="transactionId">Order ID</param>
    /// <param name="paymentMethod">Payment method used for this payment</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateP2pActionResult>> ConfirmPaymentAsync(long transactionId, string paymentMethod = null, CancellationToken ct = default)
        => ConfirmPaymentAsync(new GateP2pConfirmPaymentRequest { TransactionId = transactionId, PaymentMethod = paymentMethod }, ct);

    /// <summary>
    /// Confirm P2P payment
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateP2pActionResult>> ConfirmPaymentAsync(GateP2pConfirmPaymentRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "txid", ToStringInvariant(request.TransactionId) },
        };
        parameters.AddOptional("payment_method", request.PaymentMethod);

        return SendP2pActionRequestAsync("merchant/transaction/confirm-payment", HttpMethod.Post, ct, parameters);
    }

    /// <summary>
    /// Confirm P2P receipt
    /// </summary>
    /// <param name="transactionId">Order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateP2pActionResult>> ConfirmReceiptAsync(long transactionId, CancellationToken ct = default)
        => ConfirmReceiptAsync(new GateP2pTransactionIdRequest { TransactionId = transactionId }, ct);

    /// <summary>
    /// Confirm P2P receipt
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateP2pActionResult>> ConfirmReceiptAsync(GateP2pTransactionIdRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "txid", ToStringInvariant(request.TransactionId) },
        };

        return SendP2pActionRequestAsync("merchant/transaction/confirm-receipt", HttpMethod.Post, ct, parameters);
    }

    /// <summary>
    /// Cancel P2P order
    /// </summary>
    /// <param name="transactionId">Order ID</param>
    /// <param name="reasonId">Cancel reason ID</param>
    /// <param name="reasonMemo">Cancel note</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateP2pActionResult>> CancelOrderAsync(long transactionId, string reasonId = null, string reasonMemo = null, CancellationToken ct = default)
        => CancelOrderAsync(new GateP2pCancelOrderRequest { TransactionId = transactionId, ReasonId = reasonId, ReasonMemo = reasonMemo }, ct);

    /// <summary>
    /// Cancel P2P order
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateP2pActionResult>> CancelOrderAsync(GateP2pCancelOrderRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "txid", ToStringInvariant(request.TransactionId) },
        };
        parameters.AddOptional("reason_id", request.ReasonId);
        parameters.AddOptional("reason_memo", request.ReasonMemo);

        return SendP2pActionRequestAsync("merchant/transaction/cancel", HttpMethod.Post, ct, parameters);
    }

    /// <summary>
    /// Publish or edit P2P advertisement
    /// </summary>
    /// <param name="currencyType">Cryptocurrency symbol</param>
    /// <param name="exchangeType">Fiat currency</param>
    /// <param name="type">Ad operation type</param>
    /// <param name="unitPrice">Per-unit price in fixed-price mode</param>
    /// <param name="number">Ad amount priced in currencyType</param>
    /// <param name="payType">Payment types, comma-separated</param>
    /// <param name="payTypeJson">JSON map of payment type to payment method ID</param>
    /// <param name="minAmount">Minimum trade amount</param>
    /// <param name="maxAmount">Maximum trade amount</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateP2pActionResult>> SubmitAdvertisementAsync(
        string currencyType,
        string exchangeType,
        GateP2pAdOperationType type,
        decimal unitPrice,
        decimal number,
        string payType,
        string payTypeJson,
        decimal minAmount,
        decimal maxAmount,
        CancellationToken ct = default)
        => SubmitAdvertisementAsync(new GateP2pAdRequest
        {
            CurrencyType = currencyType,
            ExchangeType = exchangeType,
            Type = type,
            UnitPrice = unitPrice,
            Number = number,
            PayType = payType,
            PayTypeJson = payTypeJson,
            MinAmount = minAmount,
            MaxAmount = maxAmount,
        }, ct);

    /// <summary>
    /// Publish or edit P2P advertisement
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateP2pActionResult>> SubmitAdvertisementAsync(GateP2pAdRequest request, CancellationToken ct = default)
        => SendP2pActionRequestAsync("merchant/books/place_biz_push_order", HttpMethod.Post, ct, CreateAdvertisementParameters(request));

    /// <summary>
    /// Update P2P advertisement status
    /// </summary>
    /// <param name="advertisementId">Advertisement ID</param>
    /// <param name="status">Ad status</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateP2pAdStatusResult>> UpdateAdvertisementStatusAsync(long advertisementId, GateP2pAdStatusUpdate status, CancellationToken ct = default)
        => UpdateAdvertisementStatusAsync(new GateP2pAdStatusUpdateRequest { AdvertisementId = advertisementId, Status = status }, ct);

    /// <summary>
    /// Update P2P advertisement status
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateP2pAdStatusResult>> UpdateAdvertisementStatusAsync(GateP2pAdStatusUpdateRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "adv_no", request.AdvertisementId },
            { "adv_status", (int)request.Status },
        };

        return SendP2pDataRequestAsync<GateP2pAdStatusResult>("merchant/books/ads_update_status", HttpMethod.Post, ct, parameters);
    }

    /// <summary>
    /// Get P2P advertisement details
    /// </summary>
    /// <param name="advertisementId">Advertisement ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateP2pAdvertisement>> GetAdvertisementAsync(long advertisementId, CancellationToken ct = default)
        => GetAdvertisementAsync(new GateP2pAdvertisementIdRequest { AdvertisementId = advertisementId }, ct);

    /// <summary>
    /// Get P2P advertisement details
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateP2pAdvertisement>> GetAdvertisementAsync(GateP2pAdvertisementIdRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "adv_no", ToStringInvariant(request.AdvertisementId) },
        };

        return SendP2pDataRequestAsync<GateP2pAdvertisement>("merchant/books/ads_detail", HttpMethod.Post, ct, parameters);
    }

    /// <summary>
    /// Get user's P2P advertisement list
    /// </summary>
    /// <param name="asset">Cryptocurrency symbol</param>
    /// <param name="fiatUnit">Fiat currency</param>
    /// <param name="tradeType">Advertisement side</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateP2pAdvertisement>>> GetMyAdvertisementsAsync(string asset = null, string fiatUnit = null, GateP2pOrderSide? tradeType = null, CancellationToken ct = default)
        => GetMyAdvertisementsAsync(new GateP2pAdListRequest { Asset = asset, FiatUnit = fiatUnit, TradeType = tradeType }, ct);

    /// <summary>
    /// Get user's P2P advertisement list
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<GateP2pAdvertisement>>> GetMyAdvertisementsAsync(GateP2pAdListRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        AddAdvertisementListParameters(parameters, request);

        var result = await SendP2pDataRequestAsync<GateP2pMyAdvertisementPage>("merchant/books/my_ads_list", HttpMethod.Post, ct, parameters).ConfigureAwait(false);
        return result.Success ? result.As(result.Data?.Lists ?? []) : result.As<List<GateP2pAdvertisement>>(default);
    }

    /// <summary>
    /// Get P2P market advertisement list
    /// </summary>
    /// <param name="asset">Cryptocurrency symbol</param>
    /// <param name="fiatUnit">Fiat currency</param>
    /// <param name="tradeType">Advertisement side</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateP2pMarketAdvertisement>>> GetAdvertisementsAsync(string asset, string fiatUnit, GateP2pOrderSide tradeType, CancellationToken ct = default)
        => GetAdvertisementsAsync(new GateP2pAdListRequest { Asset = asset, FiatUnit = fiatUnit, TradeType = tradeType }, ct);

    /// <summary>
    /// Get P2P market advertisement list
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateP2pMarketAdvertisement>>> GetAdvertisementsAsync(GateP2pAdListRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        AddAdvertisementListParameters(parameters, request);

        return SendP2pDataRequestAsync<List<GateP2pMarketAdvertisement>>("merchant/books/ads_list", HttpMethod.Post, ct, parameters);
    }

    /// <summary>
    /// Get P2P chat history
    /// </summary>
    /// <param name="transactionId">Order ID</param>
    /// <param name="lastReceived">Timestamp of the last received message</param>
    /// <param name="firstReceived">Timestamp of first received message</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateP2pChatHistory>> GetChatHistoryAsync(long? transactionId = null, DateTime? lastReceived = null, DateTime? firstReceived = null, CancellationToken ct = default)
        => GetChatHistoryAsync(new GateP2pChatHistoryRequest { TransactionId = transactionId, LastReceived = lastReceived, FirstReceived = firstReceived }, ct);

    /// <summary>
    /// Get P2P chat history
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateP2pChatHistory>> GetChatHistoryAsync(GateP2pChatHistoryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("txid", request.TransactionId);
        parameters.AddOptional("lastreceived", ToSeconds(request.LastReceived));
        parameters.AddOptional("firstreceived", ToSeconds(request.FirstReceived));

        return SendP2pDataRequestAsync<GateP2pChatHistory>("merchant/chat/get_chats_list", HttpMethod.Post, ct, parameters);
    }

    /// <summary>
    /// Send P2P chat message
    /// </summary>
    /// <param name="transactionId">Order ID</param>
    /// <param name="message">Message body</param>
    /// <param name="type">Message type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateP2pActionResult>> SendChatMessageAsync(long transactionId, string message, GateP2pChatMessageType? type = null, CancellationToken ct = default)
        => SendChatMessageAsync(new GateP2pSendChatMessageRequest { TransactionId = transactionId, Message = message, Type = type }, ct);

    /// <summary>
    /// Send P2P chat message
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateP2pActionResult>> SendChatMessageAsync(GateP2pSendChatMessageRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "txid", request.TransactionId },
            { "message", request.Message },
        };
        parameters.AddOptional("type", request.Type.HasValue ? (int)request.Type.Value : null);

        return SendP2pActionRequestAsync("merchant/chat/send_chat_message", HttpMethod.Post, ct, parameters);
    }

    /// <summary>
    /// Upload P2P chat file
    /// </summary>
    /// <param name="contentType">File MIME type</param>
    /// <param name="base64Content">Base64 file content</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateP2pChatFile>> UploadChatFileAsync(string contentType, string base64Content, CancellationToken ct = default)
        => UploadChatFileAsync(new GateP2pUploadChatFileRequest { ContentType = contentType, Base64Content = base64Content }, ct);

    /// <summary>
    /// Upload P2P chat file
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateP2pChatFile>> UploadChatFileAsync(GateP2pUploadChatFileRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "image_content_type", request.ContentType },
            { "base64_img", request.Base64Content },
        };

        return SendP2pDataRequestAsync<GateP2pChatFile>("merchant/chat/upload_chat_file", HttpMethod.Post, ct, parameters);
    }
}

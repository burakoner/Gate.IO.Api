namespace Gate.IO.Api.P2p;

/// <summary>
/// Counterparty user info request
/// </summary>
public record GateP2pCounterpartyUserInfoRequest
{
    /// <summary>
    /// Counterparty encrypted UID
    /// </summary>
    public string BusinessUserId { get; set; }
}

/// <summary>
/// Payment method list request
/// </summary>
public record GateP2pPaymentMethodsRequest
{
    /// <summary>
    /// Fiat currency
    /// </summary>
    public string Fiat { get; set; }
}

/// <summary>
/// Pending transaction list request
/// </summary>
public record GateP2pPendingTransactionsRequest
{
    /// <summary>
    /// Cryptocurrency symbol
    /// </summary>
    public string CryptoCurrency { get; set; }

    /// <summary>
    /// Fiat currency
    /// </summary>
    public string FiatCurrency { get; set; }

    /// <summary>
    /// Order tab
    /// </summary>
    public GateP2pOrderTab? OrderTab { get; set; }

    /// <summary>
    /// Order side filter
    /// </summary>
    public GateP2pOrderSide? SelectType { get; set; }

    /// <summary>
    /// Order status filter
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Order ID
    /// </summary>
    public long? TransactionId { get; set; }

    /// <summary>
    /// Start timestamp
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// End timestamp
    /// </summary>
    public DateTime? EndTime { get; set; }
}

/// <summary>
/// Completed transaction list request
/// </summary>
public record GateP2pCompletedTransactionsRequest
{
    /// <summary>
    /// Cryptocurrency symbol
    /// </summary>
    public string CryptoCurrency { get; set; }

    /// <summary>
    /// Fiat currency
    /// </summary>
    public string FiatCurrency { get; set; }

    /// <summary>
    /// Order side filter
    /// </summary>
    public GateP2pOrderSide? SelectType { get; set; }

    /// <summary>
    /// Order status filter
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Order ID
    /// </summary>
    public long? TransactionId { get; set; }

    /// <summary>
    /// Start timestamp
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// End timestamp
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// Whether to flag dispute status in the response
    /// </summary>
    public bool? QueryDispute { get; set; }

    /// <summary>
    /// Page number
    /// </summary>
    public int? Page { get; set; }

    /// <summary>
    /// Orders per page
    /// </summary>
    public int? PerPage { get; set; }
}

/// <summary>
/// Transaction details request
/// </summary>
public record GateP2pTransactionDetailsRequest
{
    /// <summary>
    /// Order ID
    /// </summary>
    public long TransactionId { get; set; }

    /// <summary>
    /// Channel tag
    /// </summary>
    public string Channel { get; set; }
}

/// <summary>
/// Transaction ID request
/// </summary>
public record GateP2pTransactionIdRequest
{
    /// <summary>
    /// Order ID
    /// </summary>
    public long TransactionId { get; set; }
}

/// <summary>
/// Confirm payment request
/// </summary>
public record GateP2pConfirmPaymentRequest
{
    /// <summary>
    /// Order ID
    /// </summary>
    public long TransactionId { get; set; }

    /// <summary>
    /// Payment type used for this payment
    /// </summary>
    public string PaymentMethod { get; set; }
}

/// <summary>
/// Cancel order request
/// </summary>
public record GateP2pCancelOrderRequest
{
    /// <summary>
    /// Order ID
    /// </summary>
    public long TransactionId { get; set; }

    /// <summary>
    /// Cancel reason ID
    /// </summary>
    public string ReasonId { get; set; }

    /// <summary>
    /// Extra cancel notes
    /// </summary>
    public string ReasonMemo { get; set; }
}

/// <summary>
/// Publish or edit ad request
/// </summary>
public record GateP2pAdRequest
{
    /// <summary>
    /// Cryptocurrency symbol
    /// </summary>
    public string CurrencyType { get; set; }

    /// <summary>
    /// Fiat currency
    /// </summary>
    public string ExchangeType { get; set; }

    /// <summary>
    /// Ad operation type
    /// </summary>
    public GateP2pAdOperationType Type { get; set; }

    /// <summary>
    /// Per-unit price in fixed-price mode
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Ad amount priced in CurrencyType
    /// </summary>
    public decimal Number { get; set; }

    /// <summary>
    /// Payment types, comma-separated
    /// </summary>
    public string PayType { get; set; }

    /// <summary>
    /// JSON map of payment type to payment method ID
    /// </summary>
    public string PayTypeJson { get; set; }

    /// <summary>
    /// Price type
    /// </summary>
    public int? RateFixed { get; set; }

    /// <summary>
    /// Advertisement ID when editing
    /// </summary>
    public long? OrderId { get; set; }

    /// <summary>
    /// Minimum trade amount in ExchangeType
    /// </summary>
    public decimal MinAmount { get; set; }

    /// <summary>
    /// Maximum amount per trade in ExchangeType
    /// </summary>
    public decimal MaxAmount { get; set; }

    /// <summary>
    /// Minimum counterparty VIP level
    /// </summary>
    public int? TierLimit { get; set; }

    /// <summary>
    /// Minimum counterparty verification level
    /// </summary>
    public int? VerifiedLimit { get; set; }

    /// <summary>
    /// Minimum counterparty account age in days
    /// </summary>
    public int? RegistrationTimeLimit { get; set; }

    /// <summary>
    /// Whether trading with the advertiser is restricted
    /// </summary>
    public int? AdvertisersLimit { get; set; }

    /// <summary>
    /// Payment timeout in minutes
    /// </summary>
    public int? ExpireMinutes { get; set; }

    /// <summary>
    /// Ad trading terms shown to the taker
    /// </summary>
    public string TradeTips { get; set; }

    /// <summary>
    /// Auto-reply message after order creation
    /// </summary>
    public string AutoReply { get; set; }

    /// <summary>
    /// Minimum completed orders for counterparty
    /// </summary>
    public int? MinCompletedLimit { get; set; }

    /// <summary>
    /// Maximum completed orders for counterparty
    /// </summary>
    public int? MaxCompletedLimit { get; set; }

    /// <summary>
    /// Counterparty minimum 30-day completion rate
    /// </summary>
    public decimal? CompletedRateLimit { get; set; }

    /// <summary>
    /// KYC nationality restriction
    /// </summary>
    public int? UserCountryLimit { get; set; }

    /// <summary>
    /// Maximum concurrent orders allowed for the counterparty
    /// </summary>
    public int? UserOrderLimit { get; set; }

    /// <summary>
    /// Floating price reference
    /// </summary>
    public int? RateReferenceId { get; set; }

    /// <summary>
    /// Absolute floating offset ratio
    /// </summary>
    public decimal? RateOffset { get; set; }

    /// <summary>
    /// Floating direction
    /// </summary>
    public int? FloatTrend { get; set; }

    /// <summary>
    /// Team payee UID
    /// </summary>
    public long? TeamPaymentUserId { get; set; }
}

/// <summary>
/// Ad status update request
/// </summary>
public record GateP2pAdStatusUpdateRequest
{
    /// <summary>
    /// Advertisement ID
    /// </summary>
    public long AdvertisementId { get; set; }

    /// <summary>
    /// New ad status
    /// </summary>
    public GateP2pAdStatusUpdate Status { get; set; }
}

/// <summary>
/// Advertisement ID request
/// </summary>
public record GateP2pAdvertisementIdRequest
{
    /// <summary>
    /// Advertisement ID
    /// </summary>
    public long AdvertisementId { get; set; }
}

/// <summary>
/// Ad list request
/// </summary>
public record GateP2pAdListRequest
{
    /// <summary>
    /// Cryptocurrency symbol
    /// </summary>
    public string Asset { get; set; }

    /// <summary>
    /// Fiat currency
    /// </summary>
    public string FiatUnit { get; set; }

    /// <summary>
    /// Ad side
    /// </summary>
    public GateP2pOrderSide? TradeType { get; set; }
}

/// <summary>
/// Chat history request
/// </summary>
public record GateP2pChatHistoryRequest
{
    /// <summary>
    /// Order ID
    /// </summary>
    public long? TransactionId { get; set; }

    /// <summary>
    /// Timestamp of the last received message
    /// </summary>
    public DateTime? LastReceived { get; set; }

    /// <summary>
    /// Timestamp of first received message
    /// </summary>
    public DateTime? FirstReceived { get; set; }
}

/// <summary>
/// Send chat message request
/// </summary>
public record GateP2pSendChatMessageRequest
{
    /// <summary>
    /// Order ID
    /// </summary>
    public long TransactionId { get; set; }

    /// <summary>
    /// Message type
    /// </summary>
    public GateP2pChatMessageType? Type { get; set; }

    /// <summary>
    /// Message body
    /// </summary>
    public string Message { get; set; }
}

/// <summary>
/// Upload chat file request
/// </summary>
public record GateP2pUploadChatFileRequest
{
    /// <summary>
    /// File MIME type
    /// </summary>
    public string ContentType { get; set; }

    /// <summary>
    /// Base64 file content
    /// </summary>
    public string Base64Content { get; set; }
}

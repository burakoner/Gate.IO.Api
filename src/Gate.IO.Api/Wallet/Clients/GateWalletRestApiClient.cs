namespace Gate.IO.Api.Wallet;

/// <summary>
/// Gate.IO Wallet Rest API Client
/// </summary>
public class GateWalletRestApiClient
{
    // Api
    private const string api = "api";
    private const string v4 = "4";
    private const string wallet = "wallet";
    private const string withdrawals = "withdrawals";

    // Root Client
    internal GateRestApiClient _ { get; }

    // Constructor
    internal GateWalletRestApiClient(GateRestApiClient root) => _ = root;

    /// <summary>
    /// Withdraw
    /// Withdrawals to Gate addresses do not incur transaction fees.
    /// </summary>
    /// <param name="currency">Currency name</param>
    /// <param name="amount">Currency amount</param>
    /// <param name="chain">Name of the chain used in withdrawals</param>
    /// <param name="address">Withdrawal address. Required for withdrawals</param>
    /// <param name="memo">Additional remarks with regards to the withdrawal</param>
    /// <param name="withdrawalId">The withdrawal record id starts with w, such as: w1879219868. When withdraw_id is not empty, the value querys this withdrawal record and no longer querys according to time</param>
    /// <param name="withdrawalOrderId">Client order id, up to 32 length and can only include 0-9, A-Z, a-z, underscore(_), hyphen(-) or dot(.)</param>
    /// <param name="assetClass">The currency type of withdrawal record is empty by default. It supports users to query the withdrawal records in the main and innovation areas on demand.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateWalletTransaction>> WithdrawAsync(
        string currency,
        decimal amount,
        string chain,
        string address,
        string memo = null,
        string withdrawalId = null,
        string withdrawalOrderId = null,
        GateWalletAssetClass? assetClass = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "currency", currency },
            { "address", address },
            { "chain", chain },
        };
        parameters.AddOptional("withdraw_order_id", withdrawalOrderId);
        parameters.AddString("amount", amount);
        parameters.AddOptional("memo", memo);
        parameters.AddOptional("withdraw_id", withdrawalId);
        parameters.AddOptionalEnum("asset_class", assetClass);

        return _.SendRequestInternal<GateWalletTransaction>(_.GetUrl(api, v4, withdrawals, null), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// UID transfer
    /// Transfers between main spot accounts are allowed; however, both parties cannot be sub-accounts
    /// </summary>
    /// <param name="receiverUid">Recipient UID</param>
    /// <param name="currency">Currency name</param>
    /// <param name="amount">Transfer amount</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateWalletTransferId>> TransferAsync(
        long receiverUid,
        string currency,
        decimal amount,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "receive_uid", receiverUid },
            { "currency", currency },
        };
        parameters.AddString("amount", amount);

        return _.SendRequestInternal<GateWalletTransferId>(_.GetUrl(api, v4, withdrawals, "push"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Cancel withdrawal with specified ID
    /// </summary>
    /// <param name="withdrawalId">Withdrawal Id</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateWalletTransaction>> CancelWithdrawalAsync(long withdrawalId, CancellationToken ct = default)
    {
        return _.SendRequestInternal<GateWalletTransaction>(_.GetUrl(api, v4, withdrawals, withdrawalId.ToString()), HttpMethod.Delete, ct, true);
    }

    /// <summary>
    /// List chains supported for specified currency
    /// </summary>
    /// <param name="currency">Currency name</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateWalletCurencyChain>>> GetCurrencyChainsAsync(string currency, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "currency", currency },
        };
        return _.SendRequestInternal<List<GateWalletCurencyChain>>(_.GetUrl(api, v4, wallet, "currency_chains"), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    /// <summary>
    /// Generate currency deposit address
    /// </summary>
    /// <param name="currency">Currency name</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateWalletDepositAddress>> GetDepositAddressAsync(string currency, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "currency", currency },
        };
        return _.SendRequestInternal<GateWalletDepositAddress>(_.GetUrl(api, v4, wallet, "deposit_address"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve withdrawal records
    /// </summary>
    /// <param name="currency">Currency name</param>
    /// <param name="assetClass">The currency type of withdrawal record is empty by default. It supports users to query the withdrawal records in the main and innovation areas on demand.</param>
    /// <param name="withdrawalId">The withdrawal record id starts with w, such as: w1879219868. When withdraw_id is not empty, the value querys this withdrawal record and no longer querys according to time</param>
    /// <param name="withdrawalOrderId">Client order id, up to 32 length and can only include 0-9, A-Z, a-z, underscore(_), hyphen(-) or dot(.)</param>
    /// <param name="from">Time range beginning, default to 7 days before current time</param>
    /// <param name="to">Time range ending, default to current time</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateWalletTransaction>>> GetWithdrawalsAsync(
        string currency = null,
        string withdrawalId = null,
        string withdrawalOrderId = null,
        GateWalletAssetClass? assetClass = null,
        DateTime? from = null, DateTime? to = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("currency", currency);
        parameters.AddOptionalEnum("asset_class", assetClass);
        parameters.AddOptional("withdraw_id", withdrawalId);
        parameters.AddOptional("withdraw_order_id", withdrawalOrderId);
        parameters.AddOptionalMilliseconds("from", from);
        parameters.AddOptionalMilliseconds("to", to);
        parameters.AddOptional("limit", limit);
        parameters.AddOptional("offset", offset);

        return _.SendRequestInternal<List<GateWalletTransaction>>(_.GetUrl(api, v4, wallet, "withdrawals"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve deposit records
    /// </summary>
    /// <param name="currency">Filter by currency. Return all currency records if not specified</param>
    /// <param name="from">Time range beginning, default to 7 days before current time</param>
    /// <param name="to">Time range ending, default to current time</param>
    /// <param name="limit">The maximum number of entries returned in the list is limited to 500 transactions.</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateWalletTransaction>>> GetDepositsAsync(string currency = null, DateTime? from = null, DateTime? to = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("currency", currency);
        parameters.AddOptionalMilliseconds("from", from);
        parameters.AddOptionalMilliseconds("to", to);
        parameters.AddOptional("limit", limit);
        parameters.AddOptional("offset", offset);

        return _.SendRequestInternal<List<GateWalletTransaction>>(_.GetUrl(api, v4, wallet, "deposits"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Transfer between trading accounts
    /// </summary>
    /// <param name="currency">Transfer currency. For futures account, currency can be set to POINT or settle currency</param>
    /// <param name="from">Account to transfer from</param>
    /// <param name="to">Account to transfer to</param>
    /// <param name="amount">Transfer amount</param>
    /// <param name="symbol">Margin currency pair. Required if transfer from or to margin account</param>
    /// <param name="settle">Futures settle currency. Required if transferring from or to futures account</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateWalletTransactionId>> TransfersBetweenTradingAccountsAsync(
        string currency,
        GateWalletAccountType from,
        GateWalletAccountType to,
        decimal amount,
        string symbol = null,
        string settle = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection()
        {
            { "currency", currency },
        };
        parameters.AddString("amount", amount);
        parameters.AddEnum("from", from);
        parameters.AddEnum("to", to);
        parameters.AddOptional("currency_pair", symbol);
        parameters.AddOptional("settle", settle);

        return _.SendRequestInternal<GateWalletTransactionId>(_.GetUrl(api, v4, wallet, "transfers"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Transfer between main and sub accounts
    /// </summary>
    /// <param name="currency">Transfer currency name</param>
    /// <param name="subAccountId">Sub account user ID</param>
    /// <param name="direction">Transfer direction. to - transfer into sub account; from - transfer out from sub account</param>
    /// <param name="amount">Transfer amount</param>
    /// <param name="clientOrderId">The custom ID provided by the customer serves as a safeguard against duplicate transfers. It can be a combination of letters (case-sensitive), numbers, hyphens '-', and underscores '_', with a length ranging from 1 to 64 characters.</param>
    /// <param name="subAccountType">Target sub user's account. spot - spot account, futures - perpetual contract account, delivery - delivery account</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateWalletTransactionId>> TransferBetweenMainAndSubAccountsAsync(
        string currency,
        long subAccountId,
        GateWalletTransferDirection direction,
        decimal amount,
        string clientOrderId = null,
        GateWalletSubAccountType? subAccountType = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection()
        {
            { "currency", currency },
            { "sub_account", subAccountId },
        };
        parameters.AddString("amount", amount);
        parameters.AddEnum("direction", direction);
        parameters.AddOptional("client_order_id", clientOrderId);
        parameters.AddOptionalEnum("sub_account_type", subAccountType);

        return _.SendRequestInternal<GateWalletTransactionId>(_.GetUrl(api, v4, wallet, "sub_account_transfers"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Retrieve transfer records between main and sub accounts
    /// Retrieve transfer records between main and sub accounts
    /// Record time range cannot exceed 30 days
    /// </summary>
    /// <param name="subUserAccounts">User ID of sub-account, you can query multiple records separated by ,. If not specified, it will return the records of all sub accounts</param>
    /// <param name="from">Time range beginning, default to 7 days before current time</param>
    /// <param name="to">Time range ending, default to current time</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateWalletTransferRecord>>> GetTransfersBetweenMainAndSubAccountsAsync(List<long> subUserAccounts = null, DateTime? from = null, DateTime? to = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        if (subUserAccounts != null && subUserAccounts.Count > 0) parameters.AddOptional("sub_uid", string.Join(",", subUserAccounts));
        parameters.AddOptionalMilliseconds("from", from);
        parameters.AddOptionalMilliseconds("to", to);
        parameters.AddOptional("limit", limit);
        parameters.AddOptional("offset", offset);

        return _.SendRequestInternal<List<GateWalletTransferRecord>>(_.GetUrl(api, v4, wallet, "sub_account_transfers"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Sub-account transfers to sub-account
    /// It is possible to perform balance transfers between two sub-accounts under the same main account. You can use either the API Key of the main account or the API Key of the sub-account to initiate the transfer.
    /// </summary>
    /// <param name="currency">Transfer currency name</param>
    /// <param name="senderSubAccountId">Transfer from the user id of the sub-account</param>
    /// <param name="senderSubAccountType">Transfer from the account. (deprecate, use sub_account_from_type and sub_account_to_type instead)</param>
    /// <param name="recipientSubAccountId">Transfer to the user id of the sub-account</param>
    /// <param name="recipientSubAccountType">The sub-account's outgoing trading account, spot - spot account, futures - perpetual contract account, delivery - delivery contract account.</param>
    /// <param name="amount">Transfer amount</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateWalletTransactionId>> TransferBetweenSubAccountsAsync(
        string currency,
        long senderSubAccountId,
        GateWalletSubAccountType senderSubAccountType,
        long recipientSubAccountId,
        GateWalletSubAccountType recipientSubAccountType,
        decimal amount,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection()
        {
            { "currency", currency },
            { "sub_account_from", senderSubAccountId },
            { "sub_account_to", recipientSubAccountId },
        };
        parameters.AddEnum("sub_account_from_type", senderSubAccountType);
        parameters.AddEnum("sub_account_to_type", recipientSubAccountType);
        parameters.AddString("amount", amount);

        return _.SendRequestInternal<GateWalletTransactionId>(_.GetUrl(api, v4, wallet, "sub_account_to_sub_account"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Transfer status query
    /// Support querying transfer status based on user-defined client_order_id or tx_id returned by the transfer interface
    /// </summary>
    /// <param name="transactionId">The transfer operation number and client_order_id cannot be empty at the same time</param>
    /// <param name="clientOrderId">The custom ID provided by the customer serves as a safeguard against duplicate transfers. It can be a combination of letters (case-sensitive), numbers, hyphens '-', and underscores '_', with a length ranging from 1 to 64 characters.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateWalletTransferStatus>> GetTransferStatusAsync(
        string transactionId = null,
        string clientOrderId = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("tx_id", transactionId);
        parameters.AddOptional("client_order_id", clientOrderId);

        return _.SendRequestInternal<GateWalletTransferStatus>(_.GetUrl(api, v4, wallet, "order_status"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve withdrawal status
    /// </summary>
    /// <param name="currency">Retrieve data of the specified currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateWalletWithdrawal>>> GetWithdrawalStatusAsync(string currency = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("currency", currency);

        return _.SendRequestInternal<List<GateWalletWithdrawal>>(_.GetUrl(api, v4, wallet, "withdraw_status"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve sub account balances
    /// </summary>
    /// <param name="subAccounts">User ID of sub-account, you can query multiple records separated by ,. If not specified, it will return the records of all sub accounts</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateWalletSubAccountBalance>>> GetSubAccountBalancesAsync(List<long> subAccounts = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        if (subAccounts != null && subAccounts.Count > 0) parameters.AddOptional("sub_uid", string.Join(",", subAccounts));

        return _.SendRequestInternal<List<GateWalletSubAccountBalance>>(_.GetUrl(api, v4, wallet, "sub_account_balances"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Query sub accounts' margin balances
    /// </summary>
    /// <param name="subAccounts"></param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateWalletSubAccountMarginBalance>>> GetSubAccountMarginBalancesAsync(List<long> subAccounts = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        if (subAccounts != null && subAccounts.Count > 0) parameters.AddOptional("sub_uid", string.Join(",", subAccounts));

        return _.SendRequestInternal<List<GateWalletSubAccountMarginBalance>>(_.GetUrl(api, v4, wallet, "sub_account_margin_balances"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Query sub accounts' futures account balances
    /// </summary>
    /// <param name="subAccounts">User ID of sub-account, you can query multiple records separated by ,. If not specified, it will return the records of all sub accounts</param>
    /// <param name="settle">Query only balances of specified settle currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateWalletSubAccountFuturesBalance>>> GetSubAccountFuturesBalancesAsync(List<long> subAccounts = null, string settle = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        if (subAccounts != null && subAccounts.Count > 0) parameters.AddOptional("sub_uid", string.Join(",", subAccounts));
        parameters.AddOptional("settle", settle);

        return _.SendRequestInternal<List<GateWalletSubAccountFuturesBalance>>(_.GetUrl(api, v4, wallet, "sub_account_futures_balances"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Query subaccount's cross_margin account info
    /// </summary>
    /// <param name="subAccounts">User ID of sub-account, you can query multiple records separated by ,. If not specified, it will return the records of all sub accounts</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateWalletSubAccountCrossMarginBalance>>> GetSubAccountCrossMarginBalancesAsync(List<long> subAccounts = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        if (subAccounts != null && subAccounts.Count > 0) parameters.AddOptional("sub_uid", string.Join(",", subAccounts));

        return _.SendRequestInternal<List<GateWalletSubAccountCrossMarginBalance>>(_.GetUrl(api, v4, wallet, "sub_account_cross_margin_balances"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Query saved address
    /// </summary>
    /// <param name="currency">Currency name</param>
    /// <param name="chain">Chain name</param>
    /// <param name="limit">Maximum number returned, 100 at most</param>
    /// <param name="page">Page number</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateWalletSavedAddress>>> GetSavedAddressesAsync(string currency, string chain = null, int limit = 100, int page = 1, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "currency", currency },
            { "limit", limit },
            { "page", page },
        };
        parameters.AddOptional("chain", chain);

        return _.SendRequestInternal<List<GateWalletSavedAddress>>(_.GetUrl(api, v4, wallet, "saved_address"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve personal trading fee
    /// </summary>
    /// <param name="symbol">Specify a currency pair to retrieve precise fee rate</param>
    /// <param name="settle">Specify the settlement currency of the contract to get more accurate rate settings</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateWalletUserTradingFee>> GetUserFeeRatesAsync(string symbol = null, string settle = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("currency_pair", symbol);
        parameters.AddOptional("settle", settle);

        return _.SendRequestInternal<GateWalletUserTradingFee>(_.GetUrl(api, v4, wallet, "fee"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve user's total balances
    /// 
    /// This endpoint returns an approximate sum of exchanged amount from all currencies to input currency for each account.The exchange rate and account balance could have been cached for at most 1 minute. It is not recommended to use its result for any trading calculation.
    /// For trading calculation, use the corresponding account query endpoint for each account type. For example:
    /// 
    /// GET /spot/accounts to query spot account balance
    /// GET /margin/accounts to query margin account balance
    /// GET /futures/{settle}/accounts to query futures account balance
    /// </summary>
    /// <param name="currency">Currency unit used to calculate the balance amount. BTC, CNY, USD and USDT are allowed. USDT is the default.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateWalletTotalBalance>> GetTotalBalancesAsync(string currency = "USDT", CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("currency", currency);

        return _.SendRequestInternal<GateWalletTotalBalance>(_.GetUrl(api, v4, wallet, "total_balance"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// List small balance
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateWalletSmallBalance>>> GetSmallBalancesAsync(CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();

        return _.SendRequestInternal<List<GateWalletSmallBalance>>(_.GetUrl(api, v4, wallet, "small_balance"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Convert small balance
    /// </summary>
    /// <param name="currencies">Currency</param>
    /// <param name="isAll">Whether to exchange all</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> ConvertSmallBalancesAsync(IEnumerable<string> currencies = null, bool? isAll = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("currency", currencies);
        parameters.AddOptional("is_all", isAll);

        return _.SendRequestInternal<object>(_.GetUrl(api, v4, wallet, "small_balance"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// List small balance history
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="page">Page number</param>
    /// <param name="limit">Maximum response items. Default: 100, minimum: 1, Maximum: 100</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<GateWalletSmallBalanceHistory>>> GetSmallBalancesHistoryAsync(string currency = null, int? page = null, int? limit = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("currency", currency);
        parameters.AddOptional("page", page);
        parameters.AddOptional("limit", limit);

        var result = await _.SendRequestInternal<List<List<GateWalletSmallBalanceHistory>>>(_.GetUrl(api, v4, wallet, "small_balance_history"), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
        if (!result.Success) return result.As<List<GateWalletSmallBalanceHistory>>([]);

        return result.As(result.Data.SelectMany(x => x).ToList());
    }

    /// <summary>
    /// Retrieve the UID transfer history
    /// </summary>
    /// <param name="id"></param>
    /// <param name="type"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateWalletTransfer>>> GetTransferHistoryAsync(
    long? id = null,
    GateWalletTransferType? type = null,
    DateTime? from = null, DateTime? to = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("id", id);
        parameters.AddOptionalEnum("transaction_type", type);
        parameters.AddOptionalMilliseconds("from", from);
        parameters.AddOptionalMilliseconds("to", to);
        parameters.AddOptional("limit", limit);
        parameters.AddOptional("offset", offset);

        return _.SendRequestInternal<List<GateWalletTransfer>>(_.GetUrl(api, v4, withdrawals, "push"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }
}

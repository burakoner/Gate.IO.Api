namespace Gate.IO.Api.Wallet;

/// <summary>
/// Gate.IO Wallet Rest API Client
/// </summary>
public class GateWalletRestApiClient
{
    // Api
    private const string api = "api";
    private const string version = "4";
    private const string walletSection = "wallet";
    private const string withdrawalsSection = "withdrawals";

    // Endpoints
    private const string pushEndpoint = "push";
    private const string currencyChainsEndpoint = "currency_chains";
    private const string depositAddressEndpoint = "deposit_address";
    private const string withdrawalsEndpoint = "withdrawals";
    private const string depositsEndpoint = "deposits";
    private const string transfersEndpoint = "transfers";
    private const string subAccountTransfersEndpoint = "sub_account_transfers";
    private const string subAccountToSubAccountEndpoint = "sub_account_to_sub_account";
    private const string withdrawStatusEndpoint = "withdraw_status";
    private const string subAccountBalancesEndpoint = "sub_account_balances";
    private const string subAccountMarginBalancesEndpoint = "sub_account_margin_balances";
    private const string subAccountFuturesBalancesEndpoint = "sub_account_futures_balances";
    private const string subAccountCrossMarginBalancesEndpoint = "sub_account_cross_margin_balances";
    private const string savedAddressEndpoint = "saved_address";
    private const string feeEndpoint = "fee";
    private const string totalBalanceEndpoint = "total_balance";

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
    /// <param name="clientOrderId">Client order id, up to 32 length and can only include 0-9, A-Z, a-z, underscore(_), hyphen(-) or dot(.)</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateWalletTransaction>> WithdrawAsync(
        string currency,
        decimal amount,
        string chain,
        string address,
        string memo = null,
        string clientOrderId = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "currency", currency },
            { "chain", chain },
            { "address", address },
        };
        parameters.AddString("amount", amount);
        parameters.AddOptionalParameter("memo", memo);
        parameters.AddOptionalParameter("withdraw_order_id", clientOrderId);

        return _.SendRequestInternal<GateWalletTransaction>(_.GetUrl(api, version, withdrawalsSection, null), HttpMethod.Post, ct, true, bodyParameters: parameters);
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
    public Task<RestCallResult<GateWalletPush>> PushAsync(
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

        return _.SendRequestInternal<GateWalletPush>(_.GetUrl(api, version, withdrawalsSection, pushEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Cancel withdrawal with specified ID
    /// </summary>
    /// <param name="withdrawalId">Withdrawal Id</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateWalletTransaction>> CancelWithdrawalAsync(long withdrawalId, CancellationToken ct = default)
    {
        return _.SendRequestInternal<GateWalletTransaction>(_.GetUrl(api, version, withdrawalsSection, withdrawalId.ToString()), HttpMethod.Delete, ct, true);
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
        return _.SendRequestInternal<List<GateWalletCurencyChain>>(_.GetUrl(api, version, walletSection, currencyChainsEndpoint), HttpMethod.Get, ct, false, queryParameters: parameters);
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
        return _.SendRequestInternal<GateWalletDepositAddress>(_.GetUrl(api, version, walletSection, depositAddressEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve withdrawal records
    /// </summary>
    /// <param name="currency">Currency name</param>
    /// <param name="from">Time range beginning, default to 7 days before current time</param>
    /// <param name="to">Time range ending, default to current time</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateWalletTransaction>>> GetWithdrawalsAsync(string currency, DateTime from, DateTime to, int limit = 100, int offset = 0, CancellationToken ct = default)
    => GetWithdrawalsAsync(currency, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, offset, ct);

    /// <summary>
    /// Retrieve withdrawal records
    /// </summary>
    /// <param name="currency">Currency name</param>
    /// <param name="from">Time range beginning, default to 7 days before current time</param>
    /// <param name="to">Time range ending, default to current time</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateWalletTransaction>>> GetWithdrawalsAsync(string currency = null, long? from = null, long? to = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("currency", currency);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);
        parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("offset", offset);

        return _.SendRequestInternal<List<GateWalletTransaction>>(_.GetUrl(api, version, walletSection, withdrawalsEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
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
    public Task<RestCallResult<List<GateWalletTransaction>>> GetDepositsAsync(string currency, DateTime from, DateTime to, int limit = 100, int offset = 0, CancellationToken ct = default)
    => GetDepositsAsync(currency, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, offset, ct);

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
    public Task<RestCallResult<List<GateWalletTransaction>>> GetDepositsAsync(string currency = null, long? from = null, long? to = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("currency", currency);
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);
        parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("offset", offset);

        return _.SendRequestInternal<List<GateWalletTransaction>>(_.GetUrl(api, version, walletSection, depositsEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
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

        return _.SendRequestInternal<GateWalletTransactionId>(_.GetUrl(api, version, walletSection, transfersEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
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

        return _.SendRequestInternal<GateWalletTransactionId>(_.GetUrl(api, version, walletSection, subAccountTransfersEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
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
    public Task<RestCallResult<List<GateWalletTransferRecord>>> GetTransfersBetweenMainAndSubAccountsAsync(List<long> subUserAccounts, DateTime from, DateTime to, int limit = 100, int offset = 0, CancellationToken ct = default)
    => GetTransfersBetweenMainAndSubAccountsAsync(subUserAccounts, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, offset, ct);

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
    public Task<RestCallResult<List<GateWalletTransferRecord>>> GetTransfersBetweenMainAndSubAccountsAsync(List<long> subUserAccounts = null, long? from = null, long? to = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        if (subUserAccounts != null && subUserAccounts.Count > 0) parameters.AddOptionalParameter("sub_uid", string.Join(",", subUserAccounts));
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);
        parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("offset", offset);

        return _.SendRequestInternal<List<GateWalletTransferRecord>>(_.GetUrl(api, version, walletSection, subAccountTransfersEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
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
        parameters.AddString("amount", amount);
        parameters.AddEnum("sub_account_from_type", senderSubAccountType);
        parameters.AddEnum("sub_account_to_type", recipientSubAccountType);

        return _.SendRequestInternal<GateWalletTransactionId>(_.GetUrl(api, version, walletSection, subAccountToSubAccountEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
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
        parameters.AddOptionalParameter("currency", currency);

        return _.SendRequestInternal<List<GateWalletWithdrawal>>(_.GetUrl(api, version, walletSection, withdrawStatusEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve sub account balances
    /// </summary>
    /// <param name="subUserAccounts">User ID of sub-account, you can query multiple records separated by ,. If not specified, it will return the records of all sub accounts</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateWalletSubAccountBalance>>> GetSubAccountBalancesAsync(List<long> subUserAccounts = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        if (subUserAccounts != null && subUserAccounts.Count > 0) parameters.AddOptionalParameter("sub_uid", string.Join(",", subUserAccounts));

        return _.SendRequestInternal<List<GateWalletSubAccountBalance>>(_.GetUrl(api, version, walletSection, subAccountBalancesEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Query sub accounts' margin balances
    /// </summary>
    /// <param name="subUserAccounts"></param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateWalletSubAccountMarginBalance>>> GetSubAccountMarginBalancesAsync(List<long> subUserAccounts = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        if (subUserAccounts != null && subUserAccounts.Count > 0) parameters.AddOptionalParameter("sub_uid", string.Join(",", subUserAccounts));

        return _.SendRequestInternal<List<GateWalletSubAccountMarginBalance>>(_.GetUrl(api, version, walletSection, subAccountMarginBalancesEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Query sub accounts' futures account balances
    /// </summary>
    /// <param name="subUserAccounts">User ID of sub-account, you can query multiple records separated by ,. If not specified, it will return the records of all sub accounts</param>
    /// <param name="settle">Query only balances of specified settle currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateWalletSubAccountFuturesBalance>>> GetSubAccountFuturesBalancesAsync(List<long> subUserAccounts = null, string settle = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        if (subUserAccounts != null && subUserAccounts.Count > 0) parameters.AddOptionalParameter("sub_uid", string.Join(",", subUserAccounts));
        parameters.AddOptionalParameter("settle", settle);

        return _.SendRequestInternal<List<GateWalletSubAccountFuturesBalance>>(_.GetUrl(api, version, walletSection, subAccountFuturesBalancesEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Query subaccount's cross_margin account info
    /// </summary>
    /// <param name="subUserAccounts">User ID of sub-account, you can query multiple records separated by ,. If not specified, it will return the records of all sub accounts</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateWalletSubAccountCrossMarginBalance>>> GetSubAccountCrossMarginBalancesAsync(List<long> subUserAccounts = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        if (subUserAccounts != null && subUserAccounts.Count > 0) parameters.AddOptionalParameter("sub_uid", string.Join(",", subUserAccounts));

        return _.SendRequestInternal<List<GateWalletSubAccountCrossMarginBalance>>(_.GetUrl(api, version, walletSection, subAccountCrossMarginBalancesEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Query saved address
    /// </summary>
    /// <param name="currency">Currency name</param>
    /// <param name="chain">Chain name</param>
    /// <param name="limit">Maximum number returned, 100 at most</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateWalletSavedAddress>>> GetSavedAddressesAsync(string currency, string chain = null, int limit = 100, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "currency", currency },
            { "limit", limit },
        };
        parameters.AddOptionalParameter("chain", chain);

        return _.SendRequestInternal<List<GateWalletSavedAddress>>(_.GetUrl(api, version, walletSection, savedAddressEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
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
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("currency_pair", symbol);
        parameters.AddOptionalParameter("settle", settle);

        return _.SendRequestInternal<GateWalletUserTradingFee>(_.GetUrl(api, version, walletSection, feeEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
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
    public Task<RestCallResult<GateWalletTotalBalance>> GetTotalBalanceAsync(string currency = "USDT", CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("currency", currency);

        return _.SendRequestInternal<GateWalletTotalBalance>(_.GetUrl(api, version, walletSection, totalBalanceEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // TODO: List small balance
    // TODO: Convert small balance
    // TODO: List small balance history
    // TODO: Retrieve the UID transfer history
}

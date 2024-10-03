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
    /// <param name="currency"></param>
    /// <param name="amount"></param>
    /// <param name="chain"></param>
    /// <param name="address"></param>
    /// <param name="memo"></param>
    /// <param name="clientOrderId"></param>
    /// <param name="ct"></param>
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
        var parameters = new Dictionary<string, object>
        {
            { "currency", currency },
            { "amount", amount.ToGateString() },
            { "chain", chain },
            { "address", address },
        };
        parameters.AddOptionalParameter("memo", memo);
        parameters.AddOptionalParameter("withdraw_order_id", clientOrderId);

        return _.SendRequestInternal<GateWalletTransaction>(_.GetUrl(api, version, withdrawalsSection, null), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// UID transfer
    /// Transfers between main spot accounts are allowed; however, both parties cannot be sub-accounts
    /// </summary>
    /// <param name="receiverUid"></param>
    /// <param name="currency"></param>
    /// <param name="amount"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<RestCallResult<GateWalletPush>> PushAsync(
        long receiverUid,
        string currency,
        decimal amount,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "receive_uid", receiverUid },
            { "currency", currency },
            { "amount", amount.ToGateString() },
        };

        return _.SendRequestInternal<GateWalletPush>(_.GetUrl(api, version, withdrawalsSection, pushEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Cancel withdrawal with specified ID
    /// </summary>
    /// <param name="withdrawalId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<RestCallResult<GateWalletTransaction>> CancelWithdrawalAsync(long withdrawalId, CancellationToken ct = default)
    {
        return _.SendRequestInternal<GateWalletTransaction>(_.GetUrl(api, version, withdrawalsSection, withdrawalId.ToString()), HttpMethod.Delete, ct, true);
    }

    /// <summary>
    /// List chains supported for specified currency
    /// </summary>
    /// <param name="currency"></param>
    /// <param name="ct"></param>
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
    /// <param name="currency"></param>
    /// <param name="ct"></param>
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
    /// <param name="currency"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateWalletTransaction>>> GetWithdrawalsAsync(string currency, DateTime from, DateTime to, int limit = 100, int offset = 0, CancellationToken ct = default)
    => GetWithdrawalsAsync(currency, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, offset, ct);

    /// <summary>
    /// Retrieve withdrawal records
    /// </summary>
    /// <param name="currency"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <param name="ct"></param>
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
    /// <param name="currency"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateWalletTransaction>>> GetDepositsAsync(string currency, DateTime from, DateTime to, int limit = 100, int offset = 0, CancellationToken ct = default)
    => GetDepositsAsync(currency, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, offset, ct);

    /// <summary>
    /// Retrieve deposit records
    /// </summary>
    /// <param name="currency"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <param name="ct"></param>
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
    /// <param name="currency"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="amount"></param>
    /// <param name="symbol"></param>
    /// <param name="settle"></param>
    /// <param name="ct"></param>
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
            { "amount", amount.ToGateString() },
        };
        parameters.AddEnum("from", from);
        parameters.AddEnum("to", to);
        parameters.AddOptional("currency_pair", symbol);
        parameters.AddOptional("settle", settle);

        return _.SendRequestInternal<GateWalletTransactionId>(_.GetUrl(api, version, walletSection, transfersEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Transfer between main and sub accounts
    /// </summary>
    /// <param name="currency"></param>
    /// <param name="subAccountId"></param>
    /// <param name="direction"></param>
    /// <param name="amount"></param>
    /// <param name="clientOrderId"></param>
    /// <param name="subAccountType"></param>
    /// <param name="ct"></param>
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
            { "amount", amount.ToGateString() },
        };
        parameters.AddEnum("direction", direction);
        parameters.AddOptional("client_order_id", clientOrderId);
        parameters.AddOptionalEnum("sub_account_type", subAccountType);

        return _.SendRequestInternal<GateWalletTransactionId>(_.GetUrl(api, version, walletSection, subAccountTransfersEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Retrieve transfer records between main and sub accounts
    /// </summary>
    /// <param name="subUserAccounts"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateWalletTransferRecord>>> GetTransfersBetweenMainAndSubAccountsAsync(List<long> subUserAccounts, DateTime from, DateTime to, int limit = 100, int offset = 0, CancellationToken ct = default)
    => GetTransfersBetweenMainAndSubAccountsAsync(subUserAccounts, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, offset, ct);

    /// <summary>
    /// Retrieve transfer records between main and sub accounts
    /// </summary>
    /// <param name="subUserAccounts"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <param name="ct"></param>
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
    /// </summary>
    /// <param name="currency"></param>
    /// <param name="senderSubAccountId"></param>
    /// <param name="recipientSubAccountId"></param>
    /// <param name="senderSubAccountType"></param>
    /// <param name="recipientSubAccountType"></param>
    /// <param name="amount"></param>
    /// <param name="ct"></param>
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
            { "amount", amount.ToGateString() },
        };
        parameters.AddEnum("sub_account_from_type", senderSubAccountType);
        parameters.AddEnum("sub_account_to_type", recipientSubAccountType);

        return _.SendRequestInternal<GateWalletTransactionId>(_.GetUrl(api, version, walletSection, subAccountToSubAccountEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Retrieve withdrawal status
    /// </summary>
    /// <param name="currency"></param>
    /// <param name="ct"></param>
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
    /// <param name="subUserAccounts"></param>
    /// <param name="ct"></param>
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
    /// <param name="ct"></param>
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
    /// <param name="subUserAccounts"></param>
    /// <param name="settle"></param>
    /// <param name="ct"></param>
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
    /// <param name="subUserAccounts"></param>
    /// <param name="ct"></param>
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
    /// <param name="currency"></param>
    /// <param name="chain"></param>
    /// <param name="limit"></param>
    /// <param name="ct"></param>
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
    /// <param name="symbol"></param>
    /// <param name="settle"></param>
    /// <param name="ct"></param>
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
    /// </summary>
    /// <param name="currency"></param>
    /// <param name="ct"></param>
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

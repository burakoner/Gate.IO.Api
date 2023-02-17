using Gate.IO.Api.Models.RestApi;
using Gate.IO.Api.Models.RestApi.Wallet;

namespace Gate.IO.Api.Clients.RestApi;

public class WalletRestApiClient : RestApiClient
{
    // Api
    protected const string api = "api";
    protected const string version = "4";
    protected const string wallet = "wallet";
    protected const string withdrawals = "withdrawals";

    // Endpoints
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

    // Internal
    internal Log Log { get => this.log; }
    internal TimeSyncState TimeSyncState = new("Gate.IO Wallet RestApi");

    // Root Client
    internal GateRestApiClient RootClient { get; }
    internal CultureInfo CI { get { return RootClient.CI; } }
    public new GateRestApiClientOptions ClientOptions { get { return RootClient.ClientOptions; } }

    internal WalletRestApiClient(GateRestApiClient root) : base("Gate.IO Wallet RestApi", root.ClientOptions)
    {
        RootClient = root;

        RequestBodyFormat = RestRequestBodyFormat.Json;
        ArraySerialization = ArraySerialization.MultipleValues;

        Thread.CurrentThread.CurrentCulture = CI;
        Thread.CurrentThread.CurrentUICulture = CI;
    }

    #region Override Methods
    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
        => new GateAuthenticationProvider(credentials);

    protected override CallError ParseErrorResponse(JToken error)
        => RootClient.ParseErrorResponse(error);

    protected override Task<RestCallResult<DateTime>> GetServerTimestampAsync()
        => RootClient.Spot.GetServerTimeAsync();

    protected override TimeSyncInfo GetTimeSyncInfo()
        => new(log, ClientOptions.AutoTimestamp, ClientOptions.TimestampRecalculationInterval, TimeSyncState);

    public override TimeSpan GetTimeOffset()
        => TimeSyncState.TimeOffset;
    #endregion

    #region Internal Methods
    internal async Task<RestCallResult<T>> SendRequestInternal<T>(
        Uri uri,
        HttpMethod method,
        CancellationToken cancellationToken,
        bool signed = false,
        Dictionary<string, object> queryParameters = null,
        Dictionary<string, object> bodyParameters = null,
        Dictionary<string, string> headerParameters = null,
        ArraySerialization? arraySerialization = null,
        JsonSerializer deserializer = null,
        bool ignoreRatelimit = false,
        int requestWeight = 1) where T : class
    {
        Thread.CurrentThread.CurrentCulture = CI;
        Thread.CurrentThread.CurrentUICulture = CI;
        return await SendRequestAsync<T>(uri, method, cancellationToken, signed, queryParameters, bodyParameters, headerParameters, arraySerialization, deserializer, ignoreRatelimit, requestWeight).ConfigureAwait(false);
    }
    #endregion

    #region Withdraw
    public async Task<RestCallResult<WalletTransaction>> WithdrawAsync(
        string currency,
        decimal amount,
        string chain,
        string address,
        string memo = "",
        string clientOrderId = "",
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "currency", currency },
            { "amount", amount },
            { "chain", chain },
            { "address", address },
        };
        parameters.AddOptionalParameter("memo", memo);
        parameters.AddOptionalParameter("withdraw_order_id", clientOrderId);

        return await SendRequestInternal<WalletTransaction>(RootClient.GetUrl(api, version, withdrawals, ""), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Cancel withdrawal with specified ID
    public async Task<RestCallResult<WalletTransaction>> CancelWithdrawalAsync(long withdrawalId, CancellationToken ct = default)
    {
        return await SendRequestInternal<WalletTransaction>(RootClient.GetUrl(api, version, withdrawals, withdrawalId.ToString()), HttpMethod.Delete, ct, true).ConfigureAwait(false);
    }
    #endregion

    #region List chains supported for specified currency
    public async Task<RestCallResult<IEnumerable<WalletCurencyChain>>> GetCurrencyChainsAsync(string currency, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "currency", currency },
        };
        return await SendRequestInternal<IEnumerable<WalletCurencyChain>>(RootClient.GetUrl(api, version, wallet, currencyChainsEndpoint), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Generate currency deposit address
    public async Task<RestCallResult<WalletDepositAddress>> GetDepositAddressAsync(string currency, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "currency", currency },
        };
        return await SendRequestInternal<WalletDepositAddress>(RootClient.GetUrl(api, version, wallet, depositAddressEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Retrieve withdrawal records
    public async Task<RestCallResult<IEnumerable<WalletTransaction>>> GetWithdrawalsAsync(string currency, DateTime from, DateTime to, int limit = 100, int offset = 0, CancellationToken ct = default)
    => await GetWithdrawalsAsync(currency, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, offset, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<WalletTransaction>>> GetWithdrawalsAsync(string currency = "", long? from = null, long? to = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "currency", currency },
            { "limit", limit },
            { "offset", offset },
        };
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        return await SendRequestInternal<IEnumerable<WalletTransaction>>(RootClient.GetUrl(api, version, wallet, withdrawalsEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Retrieve deposit records
    public async Task<RestCallResult<IEnumerable<WalletTransaction>>> GetDepositsAsync(string currency, DateTime from, DateTime to, int limit = 100, int offset = 0, CancellationToken ct = default)
    => await GetDepositsAsync(currency, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, offset, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<WalletTransaction>>> GetDepositsAsync(string currency = "", long? from = null, long? to = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "currency", currency },
            { "limit", limit },
            { "offset", offset },
        };
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        return await SendRequestInternal<IEnumerable<WalletTransaction>>(RootClient.GetUrl(api, version, wallet, depositsEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Transfer between trading accounts
    public async Task<RestCallResult<IEnumerable<WalletTransaction>>> TransferBetweenTradingAccountAsync(
        string currency,
        WalletAccount from,
        WalletAccount to,
        decimal amount,
        string symbol = "",
        string settle = "",
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "currency", currency },
            { "from", JsonConvert.SerializeObject(from, new WalletAccountConverter(false)) },
            { "to", JsonConvert.SerializeObject(to, new WalletAccountConverter(false)) },
            { "amount", amount },
        };
        parameters.AddOptionalParameter("currency_pair", symbol);
        parameters.AddOptionalParameter("settle", settle);

        return await SendRequestInternal<IEnumerable<WalletTransaction>>(RootClient.GetUrl(api, version, wallet, transfersEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Transfer between main and sub accounts
    public async Task<RestCallResult<WalletTransfer>> TransferBetweenMainAndSubAccountsAsync(
        string currency,
        long subAccountId,
        TransferDirection direction,
        decimal amount,
        AccountType? subAccountType = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "currency", currency },
            { "sub_account", subAccountId },
            { "direction", JsonConvert.SerializeObject(direction, new TransferDirectionConverter(false)) },
            { "amount", amount },
        };
        parameters.AddOptionalParameter("sub_account_type", JsonConvert.SerializeObject(subAccountType, new AccountType3Converter(false)));

        return await SendRequestInternal<WalletTransfer>(RootClient.GetUrl(api, version, wallet, subAccountTransfersEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Retrieve transfer records between main and sub accounts
    public async Task<RestCallResult<IEnumerable<WalletTransferRecord>>> GetTransfersBetweenMainAndSubAccountsAsync(List<long> subUserAccounts, DateTime from, DateTime to, int limit = 100, int offset = 0, CancellationToken ct = default)
    => await GetTransfersBetweenMainAndSubAccountsAsync(subUserAccounts, from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), limit, offset, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<WalletTransferRecord>>> GetTransfersBetweenMainAndSubAccountsAsync(List<long> subUserAccounts = null, long? from = null, long? to = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "limit", limit },
            { "offset", offset },
        };
        if (subUserAccounts != null && subUserAccounts.Count > 0) parameters.AddOptionalParameter("sub_uid", string.Join(",", subUserAccounts));
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);

        return await SendRequestInternal<IEnumerable<WalletTransferRecord>>(RootClient.GetUrl(api, version, wallet, subAccountTransfersEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Sub-account transfers to sub-account
    public async Task<RestCallResult<SubAccountTransfer>> TransferBetweenSubAccountsAsync(
        string currency,
        long senderSubAccountId,
        long recipientSubAccountId,
        AccountType3 senderSubAccountType,
        AccountType3 recipientSubAccountType,
        decimal amount,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "currency", currency },
            { "sub_account_from", senderSubAccountId },
            { "sub_account_to", recipientSubAccountId },
            { "sub_account_from_type", JsonConvert.SerializeObject(senderSubAccountType, new AccountType3Converter(false)) },
            { "sub_account_to_type", JsonConvert.SerializeObject(recipientSubAccountType, new AccountType3Converter(false)) },
            { "amount", amount },
        };

        return await SendRequestInternal<SubAccountTransfer>(RootClient.GetUrl(api, version, wallet, subAccountToSubAccountEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Retrieve withdrawal status
    public async Task<RestCallResult<IEnumerable<WalletWithdrawal>>> TransferBetweenSubAccountsAsync(string currency = "", CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("currency", currency);

        return await SendRequestInternal<IEnumerable<WalletWithdrawal>>(RootClient.GetUrl(api, version, wallet, withdrawStatusEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Retrieve sub account balances
    public async Task<RestCallResult<IEnumerable<SubAccountBalance>>> GetSubAccountBalancesAsync(List<long> subUserAccounts = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        if (subUserAccounts != null && subUserAccounts.Count > 0) parameters.AddOptionalParameter("sub_uid", string.Join(",", subUserAccounts));

        return await SendRequestInternal<IEnumerable<SubAccountBalance>>(RootClient.GetUrl(api, version, wallet, subAccountBalancesEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Query sub accounts' margin balances
    public async Task<RestCallResult<IEnumerable<SubAccountMarginBalance>>> GetSubAccountMarginBalancesAsync(List<long> subUserAccounts = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        if (subUserAccounts != null && subUserAccounts.Count > 0) parameters.AddOptionalParameter("sub_uid", string.Join(",", subUserAccounts));

        return await SendRequestInternal<IEnumerable<SubAccountMarginBalance>>(RootClient.GetUrl(api, version, wallet, subAccountMarginBalancesEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Query sub accounts' futures account balances
    public async Task<RestCallResult<IEnumerable<SubAccountFuturesBalance>>> GetSubAccountFuturesBalancesAsync(List<long> subUserAccounts = null, string settle = "", CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        if (subUserAccounts != null && subUserAccounts.Count > 0) parameters.AddOptionalParameter("sub_uid", string.Join(",", subUserAccounts));
        parameters.AddOptionalParameter("settle", settle);

        return await SendRequestInternal<IEnumerable<SubAccountFuturesBalance>>(RootClient.GetUrl(api, version, wallet, subAccountFuturesBalancesEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Query subaccount's cross_margin account info
    public async Task<RestCallResult<IEnumerable<SubAccountCrossMarginBalance>>> GetSubAccountCrossMarginBalancesAsync(List<long> subUserAccounts = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        if (subUserAccounts != null && subUserAccounts.Count > 0) parameters.AddOptionalParameter("sub_uid", string.Join(",", subUserAccounts));

        return await SendRequestInternal<IEnumerable<SubAccountCrossMarginBalance>>(RootClient.GetUrl(api, version, wallet, subAccountCrossMarginBalancesEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Query saved address
    public async Task<RestCallResult<IEnumerable<WalletSavedAddress>>> GetSavedAddressesAsync(string currency, string chain = "", int limit = 100, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "currency", currency },
            { "limit", limit },
        };
        parameters.AddOptionalParameter("chain", chain);

        return await SendRequestInternal<IEnumerable<WalletSavedAddress>>(RootClient.GetUrl(api, version, wallet, savedAddressEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Retrieve personal trading fee
    public async Task<RestCallResult<UserTradingFee>> GetUserFeeRatesAsync(string symbol = "", string settle = "", CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("currency_pair", symbol);
        parameters.AddOptionalParameter("settle", settle);

        return await SendRequestInternal<UserTradingFee>(RootClient.GetUrl(api, version, wallet, feeEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Retrieve user's total balances
    public async Task<RestCallResult<WalletTotalBalance>> GetTotalBalanceAsync(string currency = "USDT", CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("currency", currency);

        return await SendRequestInternal<WalletTotalBalance>(RootClient.GetUrl(api, version, wallet, totalBalanceEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

}

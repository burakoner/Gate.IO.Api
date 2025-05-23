namespace Gate.IO.Api.Unified;

/// <summary>
/// Gate.IO Unified REST API Client
/// </summary>
public class GateUnifiedRestApiClient
{
    // Api
    private const string api = "api";
    private const string v4 = "4";
    private const string unified = "unified";

    // Root Client
    internal GateRestApiClient _ { get; }

    // Constructor
    internal GateUnifiedRestApiClient(GateRestApiClient root) => _ = root;

    /// <summary>
    /// Get unified account info
    /// <para><a href="https://www.gate.io/docs/developers/apiv4/#get-unified-account-information" /></para>
    /// </summary>
    /// <param name="currency">Filter by asset, for example `ETH`</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateUnifiedAccountInfo>> GetAccountInfoAsync(
        string currency = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("currency", currency);

        return _.SendRequestInternal<GateUnifiedAccountInfo>(_.GetUrl(api, v4, unified, "accounts"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Get max borrowable amount
    /// <para><a href="https://www.gate.io/docs/developers/apiv4/#query-about-the-maximum-borrowing-for-the-unified-account" /></para>
    /// </summary>
    /// <param name="currency">Asset, for example `ETH`</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateUnifiedAccountMax>> GetBorrowableAsync(
        string currency,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.Add("currency", currency);

        return _.SendRequestInternal<GateUnifiedAccountMax>(_.GetUrl(api, v4, unified, "borrowable"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Get max transferable amount
    /// <para><a href="https://www.gate.io/docs/developers/apiv4/#query-about-the-maximum-transferable-for-the-unified-account" /></para>
    /// </summary>
    /// <param name="currency">Asset, for example `ETH`</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateUnifiedAccountMax>> GetTransferableAsync(
        string currency,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.Add("currency", currency);

        return _.SendRequestInternal<GateUnifiedAccountMax>(_.GetUrl(api, v4, unified, "transferable"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // TODO: transferables
    // TODO: batch_borrowable

    /// <summary>
    /// Borrow or repay
    /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#borrow-or-repay" /></para>
    /// </summary>
    /// <param name="currency">Asset name, for example `ETH`</param>
    /// <param name="quantity">Quantity</param>
    /// <param name="text">User defined text</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> BorrowAsync(
        string currency,
        decimal quantity,
        string text = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.Add("currency", currency);
        parameters.Add("type", "borrow");
        parameters.AddString("amount", quantity);
        parameters.AddOptional("text", text);

        return _.SendRequestInternal<object>(_.GetUrl(api, v4, unified, "loans"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Borrow or repay
    /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#borrow-or-repay" /></para>
    /// </summary>
    /// <param name="currency">Asset name, for example `ETH`</param>
    /// <param name="quantity">Quantity</param>
    /// <param name="repayAll">When set to 'true,' it overrides the 'amount,' allowing for direct full repayment.</param>
    /// <param name="text">User defined text</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> RepayAsync(
        string currency,
        decimal quantity,
        bool? repayAll = null,
        string text = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.Add("currency", currency);
        parameters.Add("type", "repay");
        parameters.AddString("amount", quantity);
        parameters.AddOptional("repaid_all", repayAll);
        parameters.AddOptional("text", text);

        return _.SendRequestInternal<object>(_.GetUrl(api, v4, unified, "loans"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Get loans
    /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#list-loans" /></para>
    /// </summary>
    /// <param name="currency">Asset, for example `ETH`</param>
    /// <param name="page">Page</param>
    /// <param name="limit">Limit</param>
    /// <param name="type">Loan type</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateUnifiedLoan>>> GetLoansAsync(
        string currency = null,
        int? page = null,
        int? limit = null,
        GateUnifiedLoanType? type = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("currency", currency);
        parameters.AddOptional("page", page);
        parameters.AddOptional("limit", limit);
        parameters.AddOptionalEnum("type", type);

        return _.SendRequestInternal<List<GateUnifiedLoan>>(_.GetUrl(api, v4, unified, "loans"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Get loan history
    /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#get-load-records" /></para>
    /// </summary>
    /// <param name="currency">Asset, for example `ETH`</param>
    /// <param name="direction">Direction</param>
    /// <param name="page">Page</param>
    /// <param name="limit">Max number of results</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateUnifiedLoanRecord>>> GetLoanHistoryAsync(
        string currency = null,
        GateUnifiedLoanDirection? direction = null,
        int? page = null,
        int? limit = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("currency", currency);
        parameters.AddOptional("page", page);
        parameters.AddOptional("limit", limit);
        parameters.AddOptionalEnum("type", direction);

        return _.SendRequestInternal<List<GateUnifiedLoanRecord>>(_.GetUrl(api, v4, unified, "loan_records"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Get interest history
    /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#list-interest-records" /></para>
    /// </summary>
    /// <param name="currency">Filter by asset, for example `ETH`</param>
    /// <param name="page">Page</param>
    /// <param name="limit">Max number of results</param>
    /// <param name="type">Filter by type</param>
    /// <param name="startTime">Filter by start time</param>
    /// <param name="endTime">Filter by end time</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateUnifiedInterestRecord>>> GetInterestHistoryAsync(
        string currency = null,
        int? page = null,
        int? limit = null,
        GateUnifiedLoanType? type = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("currency", currency);
        parameters.AddOptional("page", page);
        parameters.AddOptional("limit", limit);
        parameters.AddOptionalEnum("type", type);
        parameters.AddOptionalMilliseconds("from", startTime);
        parameters.AddOptionalMilliseconds("to", endTime);

        return _.SendRequestInternal<List<GateUnifiedInterestRecord>>(_.GetUrl(api, v4, unified, "interest_records"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Get user risk unit details
    /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#retrieve-user-risk-unit-details-only-valid-in-portfolio-margin-mode" /></para>
    /// </summary>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateUnifiedRiskUnits>> GetRiskUnitsAsync(CancellationToken ct = default)
    {
        return _.SendRequestInternal<GateUnifiedRiskUnits>(_.GetUrl(api, v4, unified, "risk_units"), HttpMethod.Get, ct, true);
    }

    /// <summary>
    /// Set unified account mode
    /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#set-mode-of-the-unified-account" /></para>
    /// </summary>
    /// <param name="mode">New mode</param>
    /// <param name="usdtFutures">USDT contract switch. This parameter is required when the mode is multi-currency margin mode</param>
    /// <param name="spotHedge">Spot hedging switch. This parameter is required when the mode is portfolio margin mode</param>
    /// <param name="useFunding">When the mode is set to combined margin mode, will funds be used as margin</param>
    /// <param name="options">Option switch. If not transmitted, the current switch value is used. If not transmitted for the first time, the default value is off</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> SetAccountModeAsync(GateUnifiedAccountMode mode, bool? usdtFutures = null, bool? spotHedge = null, bool? useFunding = null, bool? options = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("mode", mode);
        if (usdtFutures != null || spotHedge != null || useFunding != null)
        {
            var inner = new ParameterCollection();
            inner.AddOptional("usdt_futures", usdtFutures);
            inner.AddOptional("spot_hedge", spotHedge);
            inner.AddOptional("use_funding", useFunding);
            inner.AddOptional("options", options);
            parameters.Add("settings", inner);
        }

        return _.SendRequestInternal<object>(_.GetUrl(api, v4, unified, "unified_mode"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Get unified account mode
    /// <para><a href="https://www.gate.io/docs/developers/apiv4/#query-mode-of-the-unified-account" /></para>
    /// </summary>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateUnifiedAccountModeInfo>> GetAccountModeAsync(CancellationToken ct = default)
    {
        return _.SendRequestInternal<GateUnifiedAccountModeInfo>(_.GetUrl(api, v4, unified, "unified_mode"), HttpMethod.Get, ct, true);
    }

    /// <summary>
    /// Get estimated lending rates
    /// <para><a href="https://www.gate.io/docs/developers/apiv4/#get-unified-estimate-rate" /></para>
    /// </summary>
    /// <param name="currencies">Up to 10 assets, for example `ETH`</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    public Task<RestCallResult<Dictionary<string, decimal?>>> GetEstimatedLendingRatesAsync(IEnumerable<string> currencies, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.Add("currencies", string.Join(",", currencies));

        return _.SendRequestInternal<Dictionary<string, decimal?>>(_.GetUrl(api, v4, unified, "estimate_rate"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    // TODO: currency_discount_tiers
    // TODO: loan_margin_tiers
    // TODO: portfolio_calculator

    /// <summary>
    /// Get unified account min and max leverage rates
    /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#the-maximum-and-minimum-leverage-multiples-that-users-can-set-for-a-currency-type-are" /></para>
    /// </summary>
    /// <param name="currency">The asset, for example `ETH`</param>
    /// <param name="ct">Cancellation token</param>
    public Task<RestCallResult<GateUnifiedLeverageConfig>> GetLeverageConfigsAsync(string currency, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.Add("currency", currency);

        return _.SendRequestInternal<GateUnifiedLeverageConfig>(_.GetUrl(api, v4, unified, "leverage/user_currency_config"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Get the current leverage setttings
    /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#get-the-user-s-currency-leverage-if-currency-is-not-passed-query-all-currencies" /></para>
    /// </summary>
    /// <param name="currency">Filter by asset, for example `ETH`</param>
    /// <param name="ct">Cancellation token</param>
    public async Task<RestCallResult<List<GateUnifiedLeverageSetting>>> GetLeverageSettingsAsync(string currency = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("currency", currency);

        if (currency == null)
        {
            return await _.SendRequestInternal<List<GateUnifiedLeverageSetting>>(_.GetUrl(api, v4, unified, "leverage/user_currency_setting"), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
        }
        else
        {
            var result = await _.SendRequestInternal<GateUnifiedLeverageSetting>(_.GetUrl(api, v4, unified, "leverage/user_currency_setting"), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
            return result.As<List<GateUnifiedLeverageSetting>>([result.Data]);
        }
    }

    /// <summary>
    /// Set the leverage for an asset
    /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#get-the-user-s-currency-leverage-if-currency-is-not-passed-query-all-currencies" /></para>
    /// </summary>
    /// <param name="currency">The asset, for example `ETH`</param>
    /// <param name="leverage">Leverage</param>
    /// <param name="ct">Cancellation token</param>
    public Task<RestCallResult<object>> SetLeverageSettingsAsync(string currency, decimal leverage, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.Add("currency", currency);
        parameters.AddString("leverage", leverage);

        return _.SendRequestInternal<object>(_.GetUrl(api, v4, unified, "leverage/user_currency_setting"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    // TODO: currencies
    // TODO: history_loan_rate
}
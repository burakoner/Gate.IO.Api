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
    public Task<RestCallResult<GateUnifiedAccountInfo>> GetAccountInfoAsync(string currency = null, CancellationToken ct = default)
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
    public Task<RestCallResult<GateUnifiedCurrencyAmount>> GetBorrowableAsync(string currency, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.Add("currency", currency);

        return _.SendRequestInternal<GateUnifiedCurrencyAmount>(_.GetUrl(api, v4, unified, "borrowable"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Get max transferable amount
    /// <para><a href="https://www.gate.io/docs/developers/apiv4/#query-about-the-maximum-transferable-for-the-unified-account" /></para>
    /// </summary>
    /// <param name="currency">Asset, for example `ETH`</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateUnifiedCurrencyAmount>> GetTransferableAsync(string currency, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.Add("currency", currency);

        return _.SendRequestInternal<GateUnifiedCurrencyAmount>(_.GetUrl(api, v4, unified, "transferable"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Batch query maximum transferable amount for unified accounts. Each currency shows the maximum value. After user withdrawal, the transferable amount for all currencies will change
    /// </summary>
    /// <param name="currencies">Specify the currency name to query in batches, and support up to 100 pass parameters at a time</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateUnifiedCurrencyAmount>>> GetTransferablesAsync(IEnumerable<string> currencies, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.Add("currencies", string.Join(",", currencies));

        return _.SendRequestInternal<List<GateUnifiedCurrencyAmount>>(_.GetUrl(api, v4, unified, "transferables"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Borrow or repay
    /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#borrow-or-repay" /></para>
    /// </summary>
    /// <param name="currency">Asset name, for example `ETH`</param>
    /// <param name="quantity">Quantity</param>
    /// <param name="text">User defined text</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> BorrowAsync(string currency, decimal quantity, string text = null, CancellationToken ct = default)
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
    public Task<RestCallResult<object>> RepayAsync(string currency, decimal quantity, bool? repayAll = null, string text = null, CancellationToken ct = default)
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
    public Task<RestCallResult<List<GateUnifiedLoan>>> GetLoansAsync(string currency = null, int? page = null, int? limit = null, GateUnifiedLoanType? type = null, CancellationToken ct = default)
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
    public Task<RestCallResult<List<GateUnifiedLoanRecord>>> GetLoanHistoryAsync(string currency = null, GateUnifiedLoanDirection? direction = null, int? page = null, int? limit = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("type", direction);
        parameters.AddOptional("currency", currency);
        parameters.AddOptional("page", page);
        parameters.AddOptional("limit", limit);

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
    public Task<RestCallResult<List<GateUnifiedInterestRecord>>> GetInterestHistoryAsync(string currency = null, int? page = null, int? limit = null, GateUnifiedLoanType? type = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
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

    /// <summary>
    /// Query unified account tiered
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateUnifiedCurrencyDiscountTiers>>> GetCurrencyDiscountTiersAsync(CancellationToken ct = default)
    {
        return _.SendRequestInternal<List<GateUnifiedCurrencyDiscountTiers>>(_.GetUrl(api, v4, unified, "currency_discount_tiers"), HttpMethod.Get, ct, true);
    }

    /// <summary>
    /// Query unified account tiered loan margin
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateUnifiedLoanMarginTiers>>> GetLoanMarginTiersAsync(CancellationToken ct = default)
    {
        return _.SendRequestInternal<List<GateUnifiedLoanMarginTiers>>(_.GetUrl(api, v4, unified, "loan_margin_tiers"), HttpMethod.Get, ct, true);
    }

    /// <summary>
    /// Portfolio Margin Calculator
    /// When inputting simulated position portfolios, each position includes the position name and quantity held, supporting markets within the range of BTC and ETH perpetual contracts, options, and spot markets.When inputting simulated orders, each order includes the market identifier, order price, and order quantity, supporting markets within the range of BTC and ETH perpetual contracts, options, and spot markets.Market orders are not included.
    /// </summary>
    /// <param name="requests">Prtfolio Calculator Requests</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateUnifiedPortfolioCalculation>> CalculatePortfolioAsync(IEnumerable<GateUnifiedPortfolioCalculatorRequest> requests, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.SetBody(requests);

        return _.SendRequestInternal<GateUnifiedPortfolioCalculation>(_.GetUrl(api, v4, unified, "portfolio_calculator"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

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

    /// <summary>
    /// List of loan currencies supported by unified account
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateUnifiedCurrency>>> GetCurrenciesAsync(string currency=null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("currency", currency);

        return _.SendRequestInternal<List<GateUnifiedCurrency>>(_.GetUrl(api, v4, unified, "currencies"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Get historical lending rates
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="tier">VIP level for the floating rate to be queried</param>
    /// <param name="page">Page number</param>
    /// <param name="limit">Maximum number of items returned. Default: 100, minimum: 1, maximum: 100</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateUnifiedHistoricalLendingRates>> GetHistoricalLendingRatesAsync(string currency,int? tier =null, int? page = null, int? limit = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("currency", currency);
        parameters.AddOptionalString("tier", tier);
        parameters.AddOptional("page", page);
        parameters.AddOptional("limit", limit);

        return _.SendRequestInternal<GateUnifiedHistoricalLendingRates>(_.GetUrl(api, v4, unified, "history_loan_rate"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Set collateral currency
    /// </summary>
    /// <param name="type">Collateral Type</param>
    /// <param name="enableList">Currency list, where collateral_type=1(custom) indicates the addition logic</param>
    /// <param name="disableList">Disable list, indicating the disable logic</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateUnifiedIsSuccess>> SetCollateralCurenciesAsync(GateUnifiedCollateralType type, IEnumerable<string> enableList = null, IEnumerable<string> disableList = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("collateral_type", type);
        parameters.AddOptional("enable_list", enableList);
        parameters.AddOptional("disable_list", disableList);

        return _.SendRequestInternal<GateUnifiedIsSuccess>(_.GetUrl(api, v4, unified, "collateral_currencies"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }
}

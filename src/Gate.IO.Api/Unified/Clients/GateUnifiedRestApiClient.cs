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
    /// <param name="subAccountId">Sub-account user ID</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateUnifiedAccountInfo>> GetAccountInfoAsync(string currency = null, long? subAccountId = null, CancellationToken ct = default)
        => GetAccountInfoAsync(new GateUnifiedAccountInfoRequest
        {
            Currency = currency,
            SubAccountId = subAccountId,
        }, ct);

    /// <summary>
    /// Get unified account info
    /// </summary>
    /// <param name="currency">Filter by asset, for example `ETH`</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateUnifiedAccountInfo>> GetAccountInfoAsync(string currency, CancellationToken ct)
        => GetAccountInfoAsync(currency, null, ct);

    /// <summary>
    /// Get unified account info
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateUnifiedAccountInfo>> GetAccountInfoAsync(GateUnifiedAccountInfoRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("currency", request.Currency);
        parameters.AddOptional("sub_uid", request.SubAccountId);

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
    /// Batch query unified account maximum borrowable amount
    /// </summary>
    /// <param name="currencies">Specify currency names for querying in an array, maximum 10 currencies</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateUnifiedCurrencyAmount>>> GetBatchBorrowableAsync(IEnumerable<string> currencies, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.Add("currencies", string.Join(",", currencies));

        return _.SendRequestInternal<List<GateUnifiedCurrencyAmount>>(_.GetUrl(api, v4, unified, "batch_borrowable"), HttpMethod.Get, ct, true, queryParameters: parameters);
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
    public Task<RestCallResult<GateUnifiedLoanResult>> BorrowAsync(string currency, decimal quantity, string text = null, CancellationToken ct = default)
        => BorrowOrRepayAsync(new GateUnifiedLoanRequest
        {
            Currency = currency,
            Type = GateUnifiedLoanDirection.Borrow,
            Amount = quantity,
            Text = text,
        }, ct);

    /// <summary>
    /// Borrow or repay
    /// </summary>
    /// <param name="currency">Asset name, for example `ETH`</param>
    /// <param name="quantity">Quantity</param>
    /// <param name="repayAll">When set to 'true,' it overrides the 'amount,' allowing for direct full repayment.</param>
    /// <param name="text">User defined text</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateUnifiedLoanResult>> RepayAsync(string currency, decimal quantity, bool? repayAll = null, string text = null, CancellationToken ct = default)
        => BorrowOrRepayAsync(new GateUnifiedLoanRequest
        {
            Currency = currency,
            Type = GateUnifiedLoanDirection.Repay,
            Amount = quantity,
            RepaidAll = repayAll,
            Text = text,
        }, ct);

    /// <summary>
    /// Borrow or repay
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateUnifiedLoanResult>> BorrowOrRepayAsync(GateUnifiedLoanRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.Add("currency", request.Currency);
        parameters.AddEnum("type", request.Type);
        parameters.AddString("amount", request.Amount);
        parameters.AddOptional("repaid_all", request.RepaidAll);
        parameters.AddOptional("text", request.Text);

        return _.SendRequestInternal<GateUnifiedLoanResult>(_.GetUrl(api, v4, unified, "loans"), HttpMethod.Post, ct, true, bodyParameters: parameters);
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
        => GetLoansAsync(new GateUnifiedLoanQueryRequest
        {
            Currency = currency,
            Page = page,
            Limit = limit,
            Type = type,
        }, ct);

    /// <summary>
    /// Get loans
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateUnifiedLoan>>> GetLoansAsync(GateUnifiedLoanQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("currency", request.Currency);
        parameters.AddOptional("page", request.Page);
        parameters.AddOptional("limit", request.Limit);
        parameters.AddOptionalEnum("type", request.Type);

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
        => GetLoanHistoryAsync(new GateUnifiedLoanRecordQueryRequest
        {
            Currency = currency,
            Type = direction,
            Page = page,
            Limit = limit,
        }, ct);

    /// <summary>
    /// Get loan history
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateUnifiedLoanRecord>>> GetLoanHistoryAsync(GateUnifiedLoanRecordQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("type", request.Type);
        parameters.AddOptional("currency", request.Currency);
        parameters.AddOptional("page", request.Page);
        parameters.AddOptional("limit", request.Limit);

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
        => GetInterestHistoryAsync(new GateUnifiedInterestRecordQueryRequest
        {
            Currency = currency,
            Page = page,
            Limit = limit,
            Type = type,
            From = startTime,
            To = endTime,
        }, ct);

    /// <summary>
    /// Get interest history
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateUnifiedInterestRecord>>> GetInterestHistoryAsync(GateUnifiedInterestRecordQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("currency", request.Currency);
        parameters.AddOptional("page", request.Page);
        parameters.AddOptional("limit", request.Limit);
        parameters.AddOptionalEnum("type", request.Type);
        parameters.AddOptionalSeconds("from", request.From);
        parameters.AddOptionalSeconds("to", request.To);

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
        => SetAccountModeAsync(new GateUnifiedAccountModeRequest
        {
            Mode = mode,
            Settings = new GateUnifiedAccountModeSettings
            {
                UsdtFutures = usdtFutures,
                SpotHedge = spotHedge,
                UseFunding = useFunding,
                Options = options,
            },
        }, ct);

    /// <summary>
    /// Set unified account mode
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    public Task<RestCallResult<object>> SetAccountModeAsync(GateUnifiedAccountModeRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("mode", request.Mode);
        if (request.Settings != null && (request.Settings.UsdtFutures != null || request.Settings.SpotHedge != null || request.Settings.UseFunding != null || request.Settings.Options != null))
        {
            var inner = new ParameterCollection();
            inner.AddOptional("usdt_futures", request.Settings.UsdtFutures);
            inner.AddOptional("spot_hedge", request.Settings.SpotHedge);
            inner.AddOptional("use_funding", request.Settings.UseFunding);
            inner.AddOptional("options", request.Settings.Options);
            parameters.Add("settings", inner);
        }

        return _.SendRequestInternal<object>(_.GetUrl(api, v4, unified, "unified_mode"), HttpMethod.Put, ct, true, bodyParameters: parameters);
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
    public async Task<RestCallResult<List<GateUnifiedCurrencyDiscountTiers>>> GetCurrencyDiscountTiersAsync(CancellationToken ct = default)
    {
        var result = await _.SendRequestInternal<JToken>(_.GetUrl(api, v4, unified, "currency_discount_tiers"), HttpMethod.Get, ct, false).ConfigureAwait(false);
        if (!result.Success) return result.As<List<GateUnifiedCurrencyDiscountTiers>>([]);

        if (result.Data is not JArray array)
            return result.As<List<GateUnifiedCurrencyDiscountTiers>>([]);

        var tiers = array.First?.Type == JTokenType.Array
            ? array.ToObject<List<List<GateUnifiedCurrencyDiscountTiers>>>()?.SelectMany(x => x).ToList()
            : array.ToObject<List<GateUnifiedCurrencyDiscountTiers>>();

        return result.As(tiers ?? []);
    }

    /// <summary>
    /// Query unified account tiered loan margin
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateUnifiedLoanMarginTiers>>> GetLoanMarginTiersAsync(CancellationToken ct = default)
    {
        return _.SendRequestInternal<List<GateUnifiedLoanMarginTiers>>(_.GetUrl(api, v4, unified, "loan_margin_tiers"), HttpMethod.Get, ct, false);
    }

    /// <summary>
    /// Portfolio Margin Calculator
    /// When inputting simulated position portfolios, each position includes the position name and quantity held, supporting markets within the range of BTC and ETH perpetual contracts, options, and spot markets.When inputting simulated orders, each order includes the market identifier, order price, and order quantity, supporting markets within the range of BTC and ETH perpetual contracts, options, and spot markets.Market orders are not included.
    /// </summary>
    /// <param name="request">Portfolio calculator request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateUnifiedPortfolioCalculation>> CalculatePortfolioAsync(GateUnifiedPortfolioCalculatorRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.SetBody(request);

        return _.SendRequestInternal<GateUnifiedPortfolioCalculation>(_.GetUrl(api, v4, unified, "portfolio_calculator"), HttpMethod.Post, ct, false, bodyParameters: parameters);
    }

    /// <summary>
    /// Portfolio Margin Calculator
    /// </summary>
    /// <param name="requests">Portfolio calculator requests</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateUnifiedPortfolioCalculation>> CalculatePortfolioAsync(IEnumerable<GateUnifiedPortfolioCalculatorRequest> requests, CancellationToken ct = default)
    {
        return CalculatePortfolioAsync(requests?.FirstOrDefault(), ct);
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
    public Task<RestCallResult<List<GateUnifiedLeverageSetting>>> GetLeverageSettingsAsync(string currency = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("currency", currency);

        return _.SendRequestInternal<List<GateUnifiedLeverageSetting>>(_.GetUrl(api, v4, unified, "leverage/user_currency_setting"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Set the leverage for an asset
    /// <para><a href="https://www.gate.io/docs/developers/apiv4/en/#get-the-user-s-currency-leverage-if-currency-is-not-passed-query-all-currencies" /></para>
    /// </summary>
    /// <param name="currency">The asset, for example `ETH`</param>
    /// <param name="leverage">Leverage</param>
    /// <param name="ct">Cancellation token</param>
    public Task<RestCallResult<object>> SetLeverageSettingsAsync(string currency, decimal leverage, CancellationToken ct = default)
        => SetLeverageSettingsAsync(new GateUnifiedLeverageSettingRequest
        {
            Currency = currency,
            Leverage = leverage,
        }, ct);

    /// <summary>
    /// Set the leverage for an asset
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation token</param>
    public Task<RestCallResult<object>> SetLeverageSettingsAsync(GateUnifiedLeverageSettingRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.Add("currency", request.Currency);
        parameters.AddString("leverage", request.Leverage);

        return _.SendRequestInternal<object>(_.GetUrl(api, v4, unified, "leverage/user_currency_setting"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// List of loan currencies supported by unified account
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<GateUnifiedCurrency>>> GetCurrenciesAsync(string currency = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("currency", currency);

        return _.SendRequestInternal<List<GateUnifiedCurrency>>(_.GetUrl(api, v4, unified, "currencies"), HttpMethod.Get, ct, false, queryParameters: parameters);
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
    public Task<RestCallResult<GateUnifiedHistoricalLendingRates>> GetHistoricalLendingRatesAsync(string currency, int? tier = null, int? page = null, int? limit = null, CancellationToken ct = default)
        => GetHistoricalLendingRatesAsync(new GateUnifiedHistoricalLendingRatesQueryRequest
        {
            Currency = currency,
            Tier = tier?.ToString(),
            Page = page,
            Limit = limit,
        }, ct);

    /// <summary>
    /// Get historical lending rates
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateUnifiedHistoricalLendingRates>> GetHistoricalLendingRatesAsync(GateUnifiedHistoricalLendingRatesQueryRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("currency", request.Currency);
        parameters.AddOptional("tier", request.Tier);
        parameters.AddOptional("page", request.Page);
        parameters.AddOptional("limit", request.Limit);

        return _.SendRequestInternal<GateUnifiedHistoricalLendingRates>(_.GetUrl(api, v4, unified, "history_loan_rate"), HttpMethod.Get, ct, false, queryParameters: parameters);
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
        => SetCollateralCurrenciesAsync(type, enableList, disableList, ct);

    /// <summary>
    /// Set collateral currency
    /// </summary>
    /// <param name="type">Collateral Type</param>
    /// <param name="enableList">Currency list, where collateral_type=1(custom) indicates the addition logic</param>
    /// <param name="disableList">Disable list, indicating the disable logic</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateUnifiedIsSuccess>> SetCollateralCurrenciesAsync(GateUnifiedCollateralType type, IEnumerable<string> enableList = null, IEnumerable<string> disableList = null, CancellationToken ct = default)
        => SetCollateralCurrenciesAsync(new GateUnifiedCollateralCurrenciesRequest
        {
            Type = type,
            EnableList = enableList,
            DisableList = disableList,
        }, ct);

    /// <summary>
    /// Set collateral currency
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateUnifiedIsSuccess>> SetCollateralCurrenciesAsync(GateUnifiedCollateralCurrenciesRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("collateral_type", request.Type);
        parameters.AddOptional("enable_list", request.EnableList);
        parameters.AddOptional("disable_list", request.DisableList);

        return _.SendRequestInternal<GateUnifiedIsSuccess>(_.GetUrl(api, v4, unified, "collateral_currencies"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Get estimated quick repayment details. Available for cross-currency margin and portfolio margin unified accounts.
    /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/unified/#estimated-quick-repayment-details" /></para>
    /// </summary>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateUnifiedQuickRepaymentEstimate>> GetEstimatedQuickRepaymentAsync(CancellationToken ct = default)
    {
        return _.SendRequestInternal<GateUnifiedQuickRepaymentEstimate>(_.GetUrl(api, v4, unified, "estimated_quick_repayment"), HttpMethod.Get, ct, true);
    }

    /// <summary>
    /// Perform a quick repayment. Available for cross-currency margin and portfolio margin unified accounts.
    /// <para><a href="https://www.gate.com/docs/developers/apiv4/en/unified/#quick-repayment" /></para>
    /// </summary>
    /// <param name="debtCurrencies">Liability currencies to repay</param>
    /// <param name="availableCurrencies">Currencies to use for repayment</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateUnifiedQuickRepaymentResult>> CreateQuickRepaymentAsync(IEnumerable<string> debtCurrencies, IEnumerable<string> availableCurrencies, CancellationToken ct = default)
        => CreateQuickRepaymentAsync(new GateUnifiedQuickRepaymentRequest
        {
            DebtCurrencies = debtCurrencies,
            AvailableCurrencies = availableCurrencies,
        }, ct);

    /// <summary>
    /// Perform a quick repayment. Available for cross-currency margin and portfolio margin unified accounts.
    /// </summary>
    /// <param name="request">Quick repayment request</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateUnifiedQuickRepaymentResult>> CreateQuickRepaymentAsync(GateUnifiedQuickRepaymentRequest request, CancellationToken ct = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));
        if (request.DebtCurrencies == null)
            throw new ArgumentException("DebtCurrencies is required for quick repayment.", nameof(request));
        if (request.AvailableCurrencies == null)
            throw new ArgumentException("AvailableCurrencies is required for quick repayment.", nameof(request));

        var parameters = new ParameterCollection();
        parameters.Add("debt_currencies", request.DebtCurrencies);
        parameters.Add("available_currencies", request.AvailableCurrencies);

        return _.SendRequestInternal<GateUnifiedQuickRepaymentResult>(_.GetUrl(api, v4, unified, "quick_repayment"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }
}

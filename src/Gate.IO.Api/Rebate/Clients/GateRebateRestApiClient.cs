namespace Gate.IO.Api.Rebate;

/// <summary>
/// Gate.IO Rebate REST API Client
/// </summary>
public class GateRebateRestApiClient
{
    // Api
    private const string api = "api";
    private const string v4 = "4";
    private const string rebate = "rebate";

    // Root Client
    internal GateRestApiClient _ { get; }

    // Constructor
    internal GateRebateRestApiClient(GateRestApiClient root) => _ = root;

    private static void AddPaging(ParameterCollection parameters, int? limit, int? offset)
    {
        parameters.AddOptional("limit", limit);
        parameters.AddOptional("offset", offset);
    }

    private static void AddRawTimeRange(ParameterCollection parameters, long? from, long? to)
    {
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);
    }

    private static void AddTimeRange(ParameterCollection parameters, DateTime? from, DateTime? to)
    {
        parameters.AddOptional("from", from?.ConvertToSeconds());
        parameters.AddOptional("to", to?.ConvertToSeconds());
    }

    private async Task<RestCallResult<T>> SendRebateDataRequestAsync<T>(
        string endpoint,
        HttpMethod method,
        CancellationToken ct,
        ParameterCollection queryParameters = null) where T : class
    {
        var result = await _.SendRequestInternal<GateRebateResponse<T>>(_.GetUrl(api, v4, rebate, endpoint), method, ct, true, queryParameters: queryParameters).ConfigureAwait(false);
        return result.Success ? result.As(result.Data?.Data) : result.As<T>(default);
    }

    /// <summary>
    /// Broker obtains transaction history of recommended users
    /// Record query time range cannot exceed 30 days
    /// </summary>
    /// <param name="from">Time range beginning, default to 7 days before current time</param>
    /// <param name="to">Time range ending, default to current time</param>
    /// <param name="symbol">Specify the currency pair, if not specified, return all currency pairs</param>
    /// <param name="userId">User ID. If not specified, all user records will be returned</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateRebateTransactionHistory>> GetTransactionHistoryAsync(
        DateTime from,
        DateTime to,
        string symbol = null,
        long? userId = null,
        int limit = 100,
        int offset = 0,
        CancellationToken ct = default)
        => GetTransactionHistoryAsync(new GateRebateTransactionHistoryRequest { From = from, To = to, Symbol = symbol, UserId = userId, Limit = limit, Offset = offset }, ct);

    /// <summary>
    /// Broker obtains transaction history of recommended users
    /// Record query time range cannot exceed 30 days
    /// </summary>
    /// <param name="from">Time range beginning in Unix seconds, default to 7 days before current time</param>
    /// <param name="to">Time range ending in Unix seconds, default to current time</param>
    /// <param name="symbol">Specify the currency pair, if not specified, return all currency pairs</param>
    /// <param name="userId">User ID. If not specified, all user records will be returned</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateRebateTransactionHistory>> GetTransactionHistoryAsync(
        long? from = null,
        long? to = null,
        string symbol = null,
        long? userId = null,
        int limit = 100,
        int offset = 0,
        CancellationToken ct = default)
    {
        var parameters = CreateTransactionHistoryParameters(symbol, userId, from, to, limit, offset);
        return _.SendRequestInternal<GateRebateTransactionHistory>(_.GetUrl(api, v4, rebate, "agency/transaction_history"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Broker obtains transaction history of recommended users
    /// Record query time range cannot exceed 30 days
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateRebateTransactionHistory>> GetTransactionHistoryAsync(GateRebateTransactionHistoryRequest request, CancellationToken ct = default)
    {
        var parameters = CreateTransactionHistoryParameters(request.Symbol, request.UserId, request.Limit, request.Offset);
        AddTimeRange(parameters, request.From, request.To);

        return _.SendRequestInternal<GateRebateTransactionHistory>(_.GetUrl(api, v4, rebate, "agency/transaction_history"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Broker obtains transaction history of recommended users
    /// Record query time range cannot exceed 30 days
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateRebateTransactionHistory>> GetAgencyTransactionHistoryAsync(GateRebateTransactionHistoryRequest request, CancellationToken ct = default)
        => GetTransactionHistoryAsync(request, ct);

    /// <summary>
    /// Broker obtains rebate history of recommended users
    /// Record query time range cannot exceed 30 days
    /// </summary>
    /// <param name="from">Time range beginning, default to 7 days before current time</param>
    /// <param name="to">Time range ending, default to current time</param>
    /// <param name="currency">Filter by currency. Return all currency records if not specified</param>
    /// <param name="userId">User ID. If not specified, all user records will be returned</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="commissionType">Rebate type: 1 - Direct rebate, 2 - Indirect rebate, 3 - Self rebate</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateRebateCommissionHistory>> GetCommissionHistoryAsync(
        DateTime from,
        DateTime to,
        string currency = null,
        long? userId = null,
        int limit = 100,
        int offset = 0,
        GateRebateCommissionType? commissionType = null,
        CancellationToken ct = default)
        => GetCommissionHistoryAsync(new GateRebateCommissionHistoryRequest { From = from, To = to, Currency = currency, UserId = userId, Limit = limit, Offset = offset, CommissionType = commissionType }, ct);

    /// <summary>
    /// Broker obtains rebate history of recommended users
    /// Record query time range cannot exceed 30 days
    /// </summary>
    /// <param name="from">Time range beginning in Unix seconds, default to 7 days before current time</param>
    /// <param name="to">Time range ending in Unix seconds, default to current time</param>
    /// <param name="currency">Filter by currency. Return all currency records if not specified</param>
    /// <param name="userId">User ID. If not specified, all user records will be returned</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="commissionType">Rebate type: 1 - Direct rebate, 2 - Indirect rebate, 3 - Self rebate</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateRebateCommissionHistory>> GetCommissionHistoryAsync(
        long? from = null,
        long? to = null,
        string currency = null,
        long? userId = null,
        int limit = 100,
        int offset = 0,
        GateRebateCommissionType? commissionType = null,
        CancellationToken ct = default)
    {
        var parameters = CreateCommissionHistoryParameters(currency, userId, from, to, limit, offset);
        parameters.AddOptional("commission_type", commissionType.HasValue ? (int?)commissionType.Value : null);

        return _.SendRequestInternal<GateRebateCommissionHistory>(_.GetUrl(api, v4, rebate, "agency/commission_history"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Broker obtains rebate history of recommended users
    /// Record query time range cannot exceed 30 days
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateRebateCommissionHistory>> GetCommissionHistoryAsync(GateRebateCommissionHistoryRequest request, CancellationToken ct = default)
    {
        var parameters = CreateCommissionHistoryParameters(request.Currency, request.UserId, null, null, request.Limit, request.Offset);
        parameters.AddOptional("commission_type", request.CommissionType.HasValue ? (int?)request.CommissionType.Value : null);
        AddTimeRange(parameters, request.From, request.To);

        return _.SendRequestInternal<GateRebateCommissionHistory>(_.GetUrl(api, v4, rebate, "agency/commission_history"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Broker obtains rebate history of recommended users
    /// Record query time range cannot exceed 30 days
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateRebateCommissionHistory>> GetAgencyCommissionHistoryAsync(GateRebateCommissionHistoryRequest request, CancellationToken ct = default)
        => GetCommissionHistoryAsync(request, ct);

    /// <summary>
    /// Partner obtains transaction history of recommended users
    /// Record query time range cannot exceed 30 days
    /// </summary>
    /// <param name="from">Time range beginning</param>
    /// <param name="to">Time range ending</param>
    /// <param name="symbol">Specify the currency pair, if not specified, return all currency pairs</param>
    /// <param name="userId">User ID. If not specified, all user records will be returned</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateRebateTransactionHistory>> GetPartnerTransactionHistoryAsync(
        DateTime? from = null,
        DateTime? to = null,
        string symbol = null,
        long? userId = null,
        int limit = 100,
        int offset = 0,
        CancellationToken ct = default)
        => GetPartnerTransactionHistoryAsync(new GateRebateTransactionHistoryRequest { From = from, To = to, Symbol = symbol, UserId = userId, Limit = limit, Offset = offset }, ct);

    /// <summary>
    /// Partner obtains transaction history of recommended users
    /// Record query time range cannot exceed 30 days
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateRebateTransactionHistory>> GetPartnerTransactionHistoryAsync(GateRebateTransactionHistoryRequest request, CancellationToken ct = default)
    {
        var parameters = CreateTransactionHistoryParameters(request.Symbol, request.UserId, request.Limit, request.Offset);
        AddTimeRange(parameters, request.From, request.To);

        return _.SendRequestInternal<GateRebateTransactionHistory>(_.GetUrl(api, v4, rebate, "partner/transaction_history"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Partner obtains rebate records of recommended users
    /// Record query time range cannot exceed 30 days
    /// </summary>
    /// <param name="from">Time range beginning</param>
    /// <param name="to">Time range ending</param>
    /// <param name="currency">Filter by currency. Return all currency records if not specified</param>
    /// <param name="userId">User ID. If not specified, all user records will be returned</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateRebateCommissionHistory>> GetPartnerCommissionHistoryAsync(
        DateTime? from = null,
        DateTime? to = null,
        string currency = null,
        long? userId = null,
        int limit = 100,
        int offset = 0,
        CancellationToken ct = default)
        => GetPartnerCommissionHistoryAsync(new GateRebateCommissionHistoryRequest { From = from, To = to, Currency = currency, UserId = userId, Limit = limit, Offset = offset }, ct);

    /// <summary>
    /// Partner obtains rebate records of recommended users
    /// Record query time range cannot exceed 30 days
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateRebateCommissionHistory>> GetPartnerCommissionHistoryAsync(GateRebateCommissionHistoryRequest request, CancellationToken ct = default)
    {
        var parameters = CreateCommissionHistoryParameters(request.Currency, request.UserId, null, null, request.Limit, request.Offset);
        AddTimeRange(parameters, request.From, request.To);

        return _.SendRequestInternal<GateRebateCommissionHistory>(_.GetUrl(api, v4, rebate, "partner/commission_history"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Partner subordinate list
    /// </summary>
    /// <param name="userId">User ID. If not specified, all user records will be returned</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateRebatePartnerSubList>> GetPartnerSubListAsync(long? userId = null, int limit = 100, int offset = 0, CancellationToken ct = default)
        => GetPartnerSubListAsync(new GateRebatePartnerSubListRequest { UserId = userId, Limit = limit, Offset = offset }, ct);

    /// <summary>
    /// Partner subordinate list
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateRebatePartnerSubList>> GetPartnerSubListAsync(GateRebatePartnerSubListRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("user_id", request.UserId);
        AddPaging(parameters, request.Limit, request.Offset);

        return _.SendRequestInternal<GateRebatePartnerSubList>(_.GetUrl(api, v4, rebate, "partner/sub_list"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Broker obtains user's rebate records
    /// Record query time range cannot exceed 30 days
    /// </summary>
    /// <param name="from">Time range beginning</param>
    /// <param name="to">Time range ending</param>
    /// <param name="userId">User ID. If not specified, all user records will be returned</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateRebateBrokerCommissionHistory>> GetBrokerCommissionHistoryAsync(
        DateTime? from = null,
        DateTime? to = null,
        long? userId = null,
        int limit = 100,
        int offset = 0,
        CancellationToken ct = default)
        => GetBrokerCommissionHistoryAsync(new GateRebateBrokerHistoryRequest { From = from, To = to, UserId = userId, Limit = limit, Offset = offset }, ct);

    /// <summary>
    /// Broker obtains user's rebate records
    /// Record query time range cannot exceed 30 days
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateRebateBrokerCommissionHistory>> GetBrokerCommissionHistoryAsync(GateRebateBrokerHistoryRequest request, CancellationToken ct = default)
    {
        var parameters = CreateBrokerHistoryParameters(request);
        return _.SendRequestInternal<GateRebateBrokerCommissionHistory>(_.GetUrl(api, v4, rebate, "broker/commission_history"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Broker obtains user's trading history
    /// Record query time range cannot exceed 30 days
    /// </summary>
    /// <param name="from">Time range beginning</param>
    /// <param name="to">Time range ending</param>
    /// <param name="userId">User ID. If not specified, all user records will be returned</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateRebateBrokerTransactionHistory>> GetBrokerTransactionHistoryAsync(
        DateTime? from = null,
        DateTime? to = null,
        long? userId = null,
        int limit = 100,
        int offset = 0,
        CancellationToken ct = default)
        => GetBrokerTransactionHistoryAsync(new GateRebateBrokerHistoryRequest { From = from, To = to, UserId = userId, Limit = limit, Offset = offset }, ct);

    /// <summary>
    /// Broker obtains user's trading history
    /// Record query time range cannot exceed 30 days
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateRebateBrokerTransactionHistory>> GetBrokerTransactionHistoryAsync(GateRebateBrokerHistoryRequest request, CancellationToken ct = default)
    {
        var parameters = CreateBrokerHistoryParameters(request);
        return _.SendRequestInternal<GateRebateBrokerTransactionHistory>(_.GetUrl(api, v4, rebate, "broker/transaction_history"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// User obtains rebate information
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateRebateUserInfo>> GetUserInfoAsync(CancellationToken ct = default)
        => _.SendRequestInternal<GateRebateUserInfo>(_.GetUrl(api, v4, rebate, "user/info"), HttpMethod.Get, ct, true);

    /// <summary>
    /// User subordinate relationship
    /// </summary>
    /// <param name="userIds">Query user ID list, separated by commas. If more than 100, only 100 will be returned</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateRebateUserSubRelation>> GetUserSubRelationAsync(IEnumerable<long> userIds, CancellationToken ct = default)
        => GetUserSubRelationAsync(new GateRebateUserSubRelationRequest { UserIds = userIds }, ct);

    /// <summary>
    /// User subordinate relationship
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateRebateUserSubRelation>> GetUserSubRelationAsync(GateRebateUserSubRelationRequest request, CancellationToken ct = default)
    {
        var userIds = request.UserIds?.Take(100).ToList() ?? [];
        var parameters = new ParameterCollection
        {
            { "user_id_list", string.Join(",", userIds) },
        };

        return _.SendRequestInternal<GateRebateUserSubRelation>(_.GetUrl(api, v4, rebate, "user/sub_relation"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Get recent partner application records
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateRebatePartnerApplication>> GetRecentPartnerApplicationAsync(CancellationToken ct = default)
        => SendRebateDataRequestAsync<GateRebatePartnerApplication>("partner/applications/recent", HttpMethod.Get, ct);

    /// <summary>
    /// Check partner application eligibility
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateRebatePartnerEligibility>> CheckPartnerEligibilityAsync(CancellationToken ct = default)
        => SendRebateDataRequestAsync<GateRebatePartnerEligibility>("partner/eligibility", HttpMethod.Get, ct);

    /// <summary>
    /// Aggregated partner agent statistics
    /// </summary>
    /// <param name="startDate">Query start time, format: yyyy-mm-dd hh:ii:ss (UTC+8)</param>
    /// <param name="endDate">Query end time, format: yyyy-mm-dd hh:ii:ss (UTC+8)</param>
    /// <param name="businessType">Business type filter</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateRebatePartnerAggregatedData>> GetPartnerAggregatedDataAsync(string startDate = null, string endDate = null, GateRebateBusinessType? businessType = null, CancellationToken ct = default)
        => GetPartnerAggregatedDataAsync(new GateRebatePartnerAggregatedDataRequest { StartDate = startDate, EndDate = endDate, BusinessType = businessType }, ct);

    /// <summary>
    /// Aggregated partner agent statistics
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<GateRebatePartnerAggregatedData>> GetPartnerAggregatedDataAsync(GateRebatePartnerAggregatedDataRequest request, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("start_date", request.StartDate);
        parameters.AddOptional("end_date", request.EndDate);
        parameters.AddOptional("business_type", request.BusinessType.HasValue ? (int?)request.BusinessType.Value : null);

        return SendRebateDataRequestAsync<GateRebatePartnerAggregatedData>("partner/data/aggregated", HttpMethod.Get, ct, parameters);
    }

    private static ParameterCollection CreateTransactionHistoryParameters(string symbol, long? userId, long? from, long? to, int? limit, int? offset)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("currency_pair", symbol);
        parameters.AddOptionalParameter("user_id", userId);
        AddRawTimeRange(parameters, from, to);
        AddPaging(parameters, limit, offset);
        return parameters;
    }

    private static ParameterCollection CreateTransactionHistoryParameters(string symbol, long? userId, int? limit, int? offset)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("currency_pair", symbol);
        parameters.AddOptionalParameter("user_id", userId);
        AddPaging(parameters, limit, offset);
        return parameters;
    }

    private static ParameterCollection CreateCommissionHistoryParameters(string currency, long? userId, long? from, long? to, int? limit, int? offset)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalParameter("currency", currency);
        parameters.AddOptionalParameter("user_id", userId);
        AddRawTimeRange(parameters, from, to);
        AddPaging(parameters, limit, offset);
        return parameters;
    }

    private static ParameterCollection CreateBrokerHistoryParameters(GateRebateBrokerHistoryRequest request)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("user_id", request.UserId);
        AddTimeRange(parameters, request.From, request.To);
        AddPaging(parameters, request.Limit, request.Offset);
        return parameters;
    }
}

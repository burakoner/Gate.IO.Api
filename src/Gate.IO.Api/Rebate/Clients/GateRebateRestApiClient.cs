using System;

namespace Gate.IO.Api.Rebate;

/// <summary>
/// Gate.IO Rebate REST API Client
/// </summary>
public class GateRebateRestApiClient
{
    // Api
    private const string api = "api";
    private const string version = "4";
    private const string section = "rebate";

    // Endpoints
    private const string transactionHistoryEndpoint = "agency/transaction_history";
    private const string commissionHistoryEndpoint = "agency/commission_history";

    // Root Client
    internal GateRestApiClient _ { get; }

    // Constructor
    internal GateRebateRestApiClient(GateRestApiClient root) => _ = root;

    /// <summary>
    /// The broker obtains the transaction history of the recommended user
    /// Record time range cannot exceed 30 days
    /// </summary>
    /// <param name="from">Time range beginning, default to 7 days before current time</param>
    /// <param name="to">Time range ending, default to current time</param>
    /// <param name="symbol">Specify the currency pair, if not specified, return all currency pairs</param>
    /// <param name="userId">User ID. If not specified, all user records will be returned</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<GateRebateTransactionHistory>>> GetTransactionHistoryAsync(
        DateTime from,
        DateTime to,
        string symbol = null,
        long? userId = null,
        int limit = 100,
        int offset = 0,
        CancellationToken ct = default)
        => await GetTransactionHistoryAsync(from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), symbol, userId, limit, offset, ct).ConfigureAwait(false);


    /// <summary>
    /// The agency obtains the transaction history of the recommended user
    /// Record time range cannot exceed 30 days
    /// </summary>
    /// <param name="from">Time range beginning, default to 7 days before current time</param>
    /// <param name="to">Time range ending, default to current time</param>
    /// <param name="symbol">Specify the currency pair, if not specified, return all currency pairs</param>
    /// <param name="userId">User ID. If not specified, all user records will be returned</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<GateRebateTransactionHistory>>> GetTransactionHistoryAsync(
        long? from = null,
        long? to = null,
        string symbol = null,
        long? userId = null,
        int limit = 100,
        int offset = 0,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "limit", limit },
            { "offset", offset },
        };
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);
        parameters.AddOptionalParameter("currency_pair", symbol);
        parameters.AddOptionalParameter("user_id", userId);

        return await _.SendRequestInternal<List<GateRebateTransactionHistory>>(_.GetUrl(api, version, section, transactionHistoryEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// The agency obtains the commission history of the recommended user
    /// Record time range cannot exceed 30 days
    /// </summary>
    /// <param name="from">Time range beginning, default to 7 days before current time</param>
    /// <param name="to">Time range ending, default to current time</param>
    /// <param name="currency">Filter by currency. Return all currency records if not specified</param>
    /// <param name="userId">User ID. If not specified, all user records will be returned</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<GateRebateCommissionHistory>>> GetCommissionHistoryAsync(
        DateTime from,
        DateTime to,
        string currency = null,
        long? userId = null,
        int limit = 100,
        int offset = 0,
        CancellationToken ct = default)
        => await GetCommissionHistoryAsync(from.ConvertToMilliseconds(), to.ConvertToMilliseconds(), currency, userId, limit, offset, ct).ConfigureAwait(false);
    
    /// <summary>
    /// The agency obtains the commission history of the recommended user
    /// Record time range cannot exceed 30 days
    /// </summary>
    /// <param name="from">Time range beginning, default to 7 days before current time</param>
    /// <param name="to">Time range ending, default to current time</param>
    /// <param name="currency">Filter by currency. Return all currency records if not specified</param>
    /// <param name="userId">User ID. If not specified, all user records will be returned</param>
    /// <param name="limit">Maximum number of records to be returned in a single list</param>
    /// <param name="offset">List offset, starting from 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<GateRebateCommissionHistory>>> GetCommissionHistoryAsync(
        long? from = null,
        long? to = null,
        string currency = null,
        long? userId = null,
        int limit = 100,
        int offset = 0,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "limit", limit },
            { "offset", offset },
        };
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("to", to);
        parameters.AddOptionalParameter("currency", currency);
        parameters.AddOptionalParameter("user_id", userId);

        return await _.SendRequestInternal<List<GateRebateCommissionHistory>>(_.GetUrl(api, version, section, commissionHistoryEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }

    // TODO: Partner obtains transaction records of recommended users
    // TODO: Partner obtains commission records of recommended users
    // TODO: Partner subordinate list
    // TODO: The broker obtains the user's commission rebate records
    // TODO: The broker obtains the user's trading history
    // TODO: User retrieves rebate information
    // TODO: User-subordinate relationship
}
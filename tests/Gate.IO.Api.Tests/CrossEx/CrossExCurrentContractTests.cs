using Gate.IO.Api.CrossEx;
using Gate.IO.Api.Tests.Infrastructure;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Gate.IO.Api.Tests.CrossEx;

[Trait("Category", "Contract")]
public class CrossExCurrentContractTests
{
    private const string Symbol = "HYPERLIQUID_FUTURE_CXMT_USDC";

    [Fact]
    public void Symbol_cross_support_is_not_discarded()
    {
        var symbol = SymbolValue("{\"support_cross\":\"true\"}");
        var json = JObject.FromObject(symbol);
        Assert.Equal("true", (string?)json["support_cross"]);
    }

    [Fact]
    public void Historical_margin_mode_is_not_discarded()
    {
        var position = JsonConvert.DeserializeObject<GateCrossExHistoricalPosition>("{\"margin_mode\":\"ISOLATED\"}")!;
        Assert.Equal("ISOLATED", (string?)JObject.FromObject(position)["margin_mode"]);
    }

    [Theory]
    [InlineData("position_id")]
    [InlineData("user_id")]
    public void Fractional_historical_id_must_not_be_rounded_into_another_identity(string field)
        => Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject<GateCrossExHistoricalPosition>($"{{\"{field}\":9007199254740993.1}}"));

    [Fact]
    public void All_current_symbol_fields_decode_without_discarding_legacy_nullable_metadata()
    {
        var values = JsonFixture.Deserialize<List<GateCrossExSymbol>>("Docs/CrossEx/symbols.current.success.json");
        Assert.Equal(2, values.Count);
        var value = values[0];
        Assert.Equal("BINANCE_FUTURE_ADA_USDT", value.Symbol);
        Assert.Equal("BINANCE", value.ExchangeType);
        Assert.Equal("FUTURE", value.BusinessType);
        Assert.Equal("live", value.State);
        Assert.Equal(1m, value.MinimumSize);
        Assert.Equal(5m, value.MinimumNotional);
        Assert.Equal(1m, value.LotSize);
        Assert.Equal(0.00010m, value.TickSize);
        Assert.Equal(200L, value.MaximumNumberOfOrders);
        Assert.Equal(300000m, value.MaximumMarketSize);
        Assert.Equal(2000000m, value.MaximumLimitSize);
        Assert.Equal(1m, value.ContractSize);
        Assert.Equal(0.012500m, value.LiquidationFee);
        Assert.Equal(0L, value.DelistTime);
        Assert.False(value.SupportsRpi);
        Assert.True(value.SupportsCross);
        Assert.Null(value.DefaultLeverage);
        Assert.Equal("suspend", values[1].State);
        Assert.Equal(10000000000m, values[1].MaximumLimitSize);
        Assert.Equal(1762163297615L, values[1].DelistTime);

        var legacy = JsonFixture.Deserialize<List<GateCrossExSymbol>>("Live/CrossEx/symbols.KRAKEN_FUTURE_ADA_USD.json")[0];
        Assert.Null(legacy.MaximumMarketSize);
        Assert.Null(legacy.ContractSize);
        Assert.Equal(5m, legacy.DefaultLeverage);
        Assert.False(legacy.SupportsRpi);
        Assert.Null(legacy.SupportsCross);
        // Raw venue metadata already accommodates LIGHTER; order/transfer/quote support is a separate scope.
        Assert.Equal("LIGHTER", SymbolValue("{\"exchange_type\":\"LIGHTER\"}").ExchangeType);
    }

    [Fact]
    public void All_current_historical_position_fields_decode_with_exact_millisecond_times()
    {
        var value = Assert.Single(JsonFixture.Deserialize<List<GateCrossExHistoricalPosition>>("Docs/CrossEx/history_positions.current.success.json"));
        Assert.Equal(20064013106942976L, value.PositionId);
        Assert.Equal(12345678L, value.UserId);
        Assert.Equal("BINANCE_FUTURE_ADA_USDT", value.Symbol);
        Assert.Equal("COMPLETE_CLOSED", value.ClosedType);
        Assert.Equal(-0.001m, value.ClosedPnl);
        Assert.Equal(-0.001m, value.ClosedPnlRate);
        Assert.Equal(0.5598m, value.OpenAveragePrice);
        Assert.Equal(0.5597m, value.ClosedAveragePrice);
        Assert.Equal(10m, value.MaximumPositionQuantity);
        Assert.Equal(10m, value.ClosedQuantity);
        Assert.Equal(5.597m, value.ClosedValue);
        Assert.Equal(0.0055975m, value.Fee);
        Assert.Equal(0m, value.LiquidationFee);
        Assert.Equal(0m, value.FundingFee);
        Assert.Equal("LONG", value.PositionSide);
        Assert.Equal("DUAL", value.PositionMode);
        Assert.Equal(1m, value.Leverage);
        Assert.Equal("CROSS", value.MarginMode);
        Assert.Equal(DateTimeOffset.FromUnixTimeMilliseconds(1750941400632).UtcDateTime, value.CreateTime);
        Assert.Equal(DateTimeOffset.FromUnixTimeMilliseconds(1750941402661).UtcDateTime, value.UpdateTime);
        Assert.Null(value.BusinessType); // The official example omits this documented optional field.
        var variant = JsonFixture.Parse("Docs/CrossEx/history_positions.current.success.json")[0]!.DeepClone();
        variant["business_type"] = "FUTURE";
        Assert.Equal("FUTURE", variant.ToObject<GateCrossExHistoricalPosition>()!.BusinessType);
    }

    [Fact]
    public void Current_response_field_inventories_are_complete()
    {
        AssertFieldNames<GateCrossExSymbol>("symbol,exchange_type,business_type,state,min_size,min_notional,lot_size,tick_size,max_num_orders,max_market_size,max_limit_size,contract_size,liquidation_fee,delist_time,support_rpi,support_cross", "default_leverage");
        AssertFieldNames<GateCrossExHistoricalPosition>("position_id,user_id,symbol,closed_type,closed_pnl,closed_pnl_rate,open_avg_price,closed_avg_price,max_position_qty,closed_qty,closed_value,fee,liq_fee,funding_fee,position_side,position_mode,leverage,margin_mode,business_type,create_time,update_time");
        AssertFieldNames<GateCrossExIsolatedMarginResponse>("symbol,margin,position_side");
    }

    [Theory]
    [InlineData("\"true\"", true)]
    [InlineData("\"false\"", false)]
    [InlineData("true", true)]
    [InlineData("false", false)]
    public void Both_support_flags_read_documented_strings_and_legacy_boolean_tokens(string token, bool expected)
    {
        var value = SymbolValue($"{{\"support_rpi\":{token},\"support_cross\":{token}}}");
        Assert.Equal(expected, value.SupportsRpi);
        Assert.Equal(expected, value.SupportsCross);
        var json = JObject.FromObject(value);
        Assert.Equal(JTokenType.String, json["support_cross"]!.Type);
        Assert.Equal(expected ? "true" : "false", (string?)json["support_rpi"]);
    }

    [Theory]
    [InlineData("{}")]
    [InlineData("{\"support_rpi\":null,\"support_cross\":null}")]
    public void Missing_or_null_support_flags_do_not_infer_permissions(string json)
    {
        var value = SymbolValue(json);
        Assert.Null(value.SupportsRpi);
        Assert.Null(value.SupportsCross);
    }

    [Theory]
    [InlineData("1")]
    [InlineData("0")]
    [InlineData("\"1\"")]
    [InlineData("\"\"")]
    [InlineData("\"unknown\"")]
    [InlineData("\"TRUE\"")]
    [InlineData("[]")]
    [InlineData("{}")]
    public void Malformed_support_flags_are_not_defaulted_to_false(string token)
    {
        foreach (var field in new[] { "support_rpi", "support_cross" })
            Assert.Throws<JsonSerializationException>(() => SymbolValue($"{{\"{field}\":{token}}}"));
    }

    [Theory]
    [InlineData("\"CROSS\"", "CROSS")]
    [InlineData("\"ISOLATED\"", "ISOLATED")]
    [InlineData("\"FUTURE_MODE\"", "FUTURE_MODE")]
    [InlineData("\"\"", "")]
    [InlineData("null", null)]
    public void Historical_margin_mode_is_raw_metadata_without_a_default(string token, string? expected)
        => Assert.Equal(expected, JsonConvert.DeserializeObject<GateCrossExHistoricalPosition>($"{{\"margin_mode\":{token}}}")!.MarginMode);

    [Theory]
    [InlineData("9007199254740993", 9007199254740993L)]
    [InlineData("\"9007199254740993\"", 9007199254740993L)]
    [InlineData("\"9223372036854775807\"", long.MaxValue)]
    [InlineData("\"-9223372036854775808\"", long.MinValue)]
    [InlineData("0", 0L)]
    public void Existing_historical_identity_accessors_stay_long_and_exact(string token, long expected)
    {
        var value = JsonConvert.DeserializeObject<GateCrossExHistoricalPosition>($"{{\"position_id\":{token},\"user_id\":{token}}}")!;
        Assert.Equal(expected, value.PositionId);
        Assert.Equal(expected, value.UserId);
        Assert.Equal(JTokenType.Integer, JObject.FromObject(value)["position_id"]!.Type);
    }

    [Theory]
    [InlineData("true")]
    [InlineData("false")]
    [InlineData("1.0")]
    [InlineData("\"1.1\"")]
    [InlineData("\"1e3\"")]
    [InlineData("\"9223372036854775808\"")]
    [InlineData("9223372036854775808")]
    [InlineData("\"\"")]
    [InlineData("\"non-numeric\"")]
    public void Invalid_historical_identity_tokens_fail_instead_of_coercing(string token)
    {
        foreach (var field in new[] { "position_id", "user_id" })
            Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject<GateCrossExHistoricalPosition>($"{{\"{field}\":{token}}}"));
    }

    [Fact]
    public void Absent_or_null_historical_ids_remain_unknown()
    {
        foreach (var json in new[] { "{}", "{\"position_id\":null,\"user_id\":null}" })
        {
            var value = JsonConvert.DeserializeObject<GateCrossExHistoricalPosition>(json)!;
            Assert.Null(value.PositionId);
            Assert.Null(value.UserId);
            Assert.Null(value.MarginMode);
        }
    }

    [Fact]
    public async Task Symbol_queries_remain_public_even_with_credentials_and_enumerate_once()
    {
        var handler = Handler(JsonFixture.Read("Docs/CrossEx/symbols.current.success.json"));
        using var client = Client(handler);
        var enumerations = 0;
        IEnumerable<string> Symbols()
        {
            if (++enumerations > 1) throw new InvalidOperationException("Repeated enumeration");
            yield return "BINANCE_FUTURE_ADA_USDT";
            yield return "OKX_FUTURE_ADA_USDT";
        }
        var result = await client.CrossEx.GetSymbolsAsync(Symbols());
        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal(1, enumerations);
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/crossex/rule/symbols", request.RequestUri.AbsolutePath);
        Assert.Equal("BINANCE_FUTURE_ADA_USDT,OKX_FUTURE_ADA_USDT", Query(request.RequestUri)["symbols"]);
        Assert.Single(Query(request.RequestUri));
        Assert.Equal(string.Empty, request.Content);
        foreach (var header in new[] { "KEY", "SIGN", "Timestamp" }) Assert.False(request.Headers.ContainsKey(header));
    }

    [Fact]
    public async Task Omitted_and_empty_symbol_collections_explicitly_query_all_symbols()
    {
        var handler = Handler("[]");
        using var client = Client(handler);
        Assert.True((await client.CrossEx.GetSymbolsAsync()).Success);
        Assert.True((await client.CrossEx.GetSymbolsAsync(Array.Empty<string>())).Success);
        Assert.All(handler.Requests, request => Assert.Empty(Query(request.RequestUri)));
    }

    [Theory]
    [InlineData("")]
    [InlineData("null")]
    public async Task Missing_symbol_and_historical_arrays_are_not_fabricated_as_successful_lists(string json)
    {
        var handler = Handler(json);
        using var client = Client(handler);
        var symbols = await client.CrossEx.GetSymbolsAsync();
        var positions = await client.CrossEx.GetHistoricalPositionsAsync(new GateCrossExHistoryQueryRequest());
        Assert.False(symbols.Success);
        Assert.False(positions.Success);
        Assert.NotNull(symbols.Error);
        Assert.NotNull(positions.Error);
        Assert.Equal(HttpStatusCode.OK, symbols.Response!.StatusCode);
        Assert.Equal(HttpStatusCode.OK, positions.Response!.StatusCode);
        Assert.Equal(2, handler.Requests.Count);
    }

    [Fact]
    public async Task Missing_documented_required_symbol_limits_are_not_reported_as_zero()
    {
        foreach (var field in new[] { "symbol", "exchange_type", "business_type", "state", "min_size", "min_notional", "lot_size", "tick_size", "max_num_orders", "max_market_size", "max_limit_size", "contract_size", "liquidation_fee", "delist_time" })
        {
            var value = (JObject)JsonFixture.Parse("Docs/CrossEx/symbols.current.success.json")[0]!;
            value.Remove(field);
            var handler = Handler(new JArray(value).ToString(Formatting.None));
            using var client = Client(handler);
            var result = await client.CrossEx.GetSymbolsAsync();
            Assert.False(result.Success, $"Missing {field} fabricated successful metadata");
            Assert.Equal(HttpStatusCode.OK, result.Response!.StatusCode);
            Assert.Single(handler.Requests);
        }
    }

    [Theory]
    [InlineData("\"1e-29\"")]
    [InlineData("\"0.12345678901234567890123456789\"")]
    [InlineData("0.1234567890123456789")]
    [InlineData("true")]
    [InlineData("\"NaN\"")]
    [InlineData("\"\"")]
    public void Symbol_and_history_decimal_metadata_is_not_silently_rounded_or_zeroed(string token)
    {
        foreach (var field in new[] { "min_size", "min_notional", "lot_size", "tick_size", "max_market_size", "max_limit_size", "contract_size", "liquidation_fee", "default_leverage" })
            Assert.Throws<JsonSerializationException>(() => SymbolValue($"{{\"{field}\":{token}}}"));
        foreach (var field in new[] { "closed_pnl", "closed_pnl_rate", "open_avg_price", "closed_avg_price", "max_position_qty", "closed_qty", "closed_value", "fee", "liq_fee", "funding_fee", "leverage" })
            Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject<GateCrossExHistoricalPosition>($"{{\"{field}\":{token}}}"));
    }

    [Theory]
    [InlineData("1.0")]
    [InlineData("true")]
    [InlineData("\"1.1\"")]
    [InlineData("\"9223372036854775808\"")]
    public void Symbol_integer_limits_and_timestamps_are_not_rounded(string token)
    {
        foreach (var field in new[] { "max_num_orders", "delist_time" })
            Assert.Throws<JsonSerializationException>(() => SymbolValue($"{{\"{field}\":{token}}}"));
    }

    [Fact]
    public void Required_symbol_values_do_not_become_null_or_zero_but_legacy_nullable_values_remain_unknown()
    {
        foreach (var field in new[] { "symbol", "exchange_type", "business_type", "state", "min_size", "min_notional", "lot_size", "tick_size", "max_num_orders", "max_limit_size", "liquidation_fee", "delist_time" })
            Assert.Throws<JsonSerializationException>(() => SymbolValue($"{{\"{field}\":null}}"));
        var value = SymbolValue("{\"max_market_size\":null,\"contract_size\":null,\"default_leverage\":null}");
        Assert.Null(value.MaximumMarketSize);
        Assert.Null(value.ContractSize);
        Assert.Null(value.DefaultLeverage);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("BINANCE_FUTURE_ADA_USDT,OKX_FUTURE_ADA_USDT")]
    [InlineData(" BINANCE_FUTURE_ADA_USDT")]
    [InlineData("BINANCE_FUTURE_ADA_USDT\0")]
    public async Task Invalid_supplied_symbol_elements_do_not_silently_broaden_the_query(string? symbol)
    {
        var handler = Handler("[]");
        using var client = Client(handler);
        await Assert.ThrowsAsync<ArgumentException>(() => client.CrossEx.GetSymbolsAsync(new[] { symbol! }));
        Assert.Empty(handler.Requests);
    }

    [Fact]
    public async Task Historical_query_is_signed_and_uses_only_its_documented_millisecond_filters()
    {
        var handler = Handler(JsonFixture.Read("Docs/CrossEx/history_positions.current.success.json"));
        using var client = Client(handler);
        var result = await client.CrossEx.GetHistoricalPositionsAsync(new GateCrossExHistoryQueryRequest
        {
            Symbol = "BINANCE_FUTURE_ADA_USDT", Page = 0, Limit = 1000,
            From = DateTimeOffset.FromUnixTimeMilliseconds(1750941400123).UtcDateTime,
            To = DateTimeOffset.FromUnixTimeMilliseconds(1750941402789).UtcDateTime,
            Attributes = [GateCrossExOrderAttribute.Common],
        });
        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/v4/crossex/history_positions", request.RequestUri.AbsolutePath);
        var query = Query(request.RequestUri);
        Assert.Equal(5, query.Count);
        Assert.Equal("BINANCE_FUTURE_ADA_USDT", query["symbol"]);
        Assert.Equal("0", query["page"]); // No undocumented one-based lower bound is imposed.
        Assert.Equal("1000", query["limit"]);
        Assert.Equal("1750941400123", query["from"]);
        Assert.Equal("1750941402789", query["to"]);
        Assert.Equal(string.Empty, request.Content);
        AssertSignature(request);
    }

    [Fact]
    public async Task Optional_historical_filters_are_not_selected_by_the_client()
    {
        var handler = Handler("[]");
        using var client = Client(handler);
        var result = await client.CrossEx.GetHistoricalPositionsAsync(new GateCrossExHistoryQueryRequest());
        Assert.True(result.Success, result.Error?.ToString());
        Assert.Empty(result.Data);
        var request = Assert.Single(handler.Requests);
        Assert.Empty(Query(request.RequestUri));
        AssertSignature(request);
    }

    [Fact]
    public async Task Historical_range_validation_compares_the_transmitted_timestamps_not_local_wall_clocks()
    {
        var handler = Handler("[]");
        using var client = Client(handler);
        var from = DateTimeOffset.FromUnixTimeMilliseconds(1750941400123).UtcDateTime;
        var to = from.AddSeconds(1);
        foreach (var pair in new[]
        {
            (From: from.ToLocalTime(), To: to),
            (From: from, To: to.ToLocalTime()),
            (From: DateTime.SpecifyKind(from, DateTimeKind.Unspecified), To: DateTime.SpecifyKind(to, DateTimeKind.Unspecified)),
        })
        {
            var input = new GateCrossExHistoryQueryRequest { From = pair.From, To = pair.To };
            var result = await client.CrossEx.GetHistoricalPositionsAsync(input);
            Assert.True(result.Success, result.Error?.ToString());
            Assert.Equal(pair.From.Kind, input.From!.Value.Kind);
            Assert.Equal(pair.To.Kind, input.To!.Value.Kind);
        }
        Assert.Equal(3, handler.Requests.Count);
        foreach (var request in handler.Requests)
        {
            Assert.Equal("1750941400123", Query(request.RequestUri)["from"]);
            Assert.Equal("1750941401123", Query(request.RequestUri)["to"]);
            AssertSignature(request);
        }
    }

    [Fact]
    public async Task Null_requests_and_invalid_historical_filters_fail_before_io()
    {
        var handler = Handler("[]");
        using var client = Client(handler);
        await Assert.ThrowsAsync<ArgumentNullException>(() => client.CrossEx.GetSymbolsAsync((GateCrossExSymbolsQueryRequest)null!));
        await Assert.ThrowsAsync<ArgumentNullException>(() => client.CrossEx.GetHistoricalPositionsAsync(null!));
        await Assert.ThrowsAsync<ArgumentNullException>(() => client.CrossEx.UpdateIsolatedMarginAsync((GateCrossExIsolatedMarginRequest)null!));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.CrossEx.GetHistoricalPositionsAsync(new GateCrossExHistoryQueryRequest { Limit = 1001 }));
        await Assert.ThrowsAsync<ArgumentException>(() => client.CrossEx.GetHistoricalPositionsAsync(new GateCrossExHistoryQueryRequest { From = DateTime.UnixEpoch.AddDays(1), To = DateTime.UnixEpoch }));
        foreach (var symbol in new[] { "", " ", "BINANCE_FUTURE_ADA_USDT,OKX_FUTURE_ADA_USDT", "BINANCE_FUTURE_ADA_USDT\0" })
            await Assert.ThrowsAsync<ArgumentException>(() => client.CrossEx.GetHistoricalPositionsAsync(new GateCrossExHistoryQueryRequest { Symbol = symbol }));
        Assert.Empty(handler.Requests);
    }

    [Theory]
    [InlineData("-30.129", null, null)]
    [InlineData("30.129", GateCrossExPositionSide.None, "NONE")]
    [InlineData("0.01", GateCrossExPositionSide.Long, "LONG")]
    [InlineData("0.019", GateCrossExPositionSide.Short, "SHORT")]
    [InlineData("-0.019", GateCrossExPositionSide.None, "NONE")]
    public async Task Isolated_margin_preserves_explicit_amount_and_side_in_signed_json_without_retry(string margin, GateCrossExPositionSide? side, string? expectedSide)
    {
        var handler = Handler(JsonFixture.Read("Docs/CrossEx/isolated_margin.success.json"), HttpStatusCode.Accepted);
        using var client = Client(handler);
        var input = new GateCrossExIsolatedMarginRequest { Symbol = Symbol, Margin = decimal.Parse(margin, CultureInfo.InvariantCulture), PositionSide = side };
        var result = await client.CrossEx.UpdateIsolatedMarginAsync(input);
        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal(HttpStatusCode.Accepted, result.Response!.StatusCode);
        Assert.Equal(Symbol, result.Data.Symbol);
        Assert.Equal(-30m, result.Data.Margin); // Returned adjustment is preserved, not filled from input or treated as total margin.
        Assert.Equal("NONE", result.Data.PositionSide);
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v4/crossex/positions/margin", request.RequestUri.AbsolutePath);
        Assert.Empty(Query(request.RequestUri));
        var body = JObject.Parse(request.Content);
        Assert.Equal(expectedSide == null ? 2 : 3, body.Count);
        Assert.Equal(Symbol, (string?)body["symbol"]);
        Assert.Equal(JTokenType.String, body["margin"]!.Type);
        Assert.Equal(margin, (string?)body["margin"]);
        if (expectedSide == null) Assert.Null(body["position_side"]);
        else Assert.Equal(expectedSide, (string?)body["position_side"]);
        Assert.Equal(decimal.Parse(margin, CultureInfo.InvariantCulture), input.Margin);
        Assert.Equal(side, input.PositionSide);
        AssertSignature(request);
    }

    [Fact]
    public async Task Isolated_margin_convenience_overload_preserves_precision_and_cancellation_token()
    {
        var handler = Handler(JsonFixture.Read("Docs/CrossEx/isolated_margin.success.json"), HttpStatusCode.Accepted);
        using var client = Client(handler);
        using var cancellation = new CancellationTokenSource();
        var result = await client.CrossEx.UpdateIsolatedMarginAsync(Symbol, 30.123456789012345678901234567m, GateCrossExPositionSide.Short, cancellation.Token);
        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal("30.123456789012345678901234567", (string?)JObject.Parse(Assert.Single(handler.Requests).Content)["margin"]);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("HYPERLIQUID_FUTURE_")]
    [InlineData("HYPERLIQUID_FUTURE_CXMT")]
    [InlineData("HYPERLIQUID_FUTURE__USDC")]
    [InlineData("HYPERLIQUID_FUTURE_CXMT_")]
    [InlineData("HYPERLIQUID_FUTURE_CXMT_USDC\0")]
    [InlineData("BINANCE_FUTURE_ADA_USDT")]
    [InlineData("HYPERLIQUID_SPOT_CXMT_USDC")]
    [InlineData("HYPERLIQUID_FUTURE_CXMT_USDC,HYPERLIQUID_FUTURE_BTC_USDC")]
    [InlineData(" HYPERLIQUID_FUTURE_CXMT_USDC")]
    public async Task Invalid_isolated_margin_symbols_do_not_send_financial_instructions(string? symbol)
    {
        var handler = Handler("{}");
        using var client = Client(handler);
        await Assert.ThrowsAsync<ArgumentException>(() => client.CrossEx.UpdateIsolatedMarginAsync(symbol!, 30m));
        Assert.Empty(handler.Requests);
    }

    [Fact]
    public async Task Undefined_isolated_margin_side_is_not_replaced_with_one_way_default()
    {
        var handler = Handler("{}");
        using var client = Client(handler);
        await Assert.ThrowsAsync<ArgumentException>(() => client.CrossEx.UpdateIsolatedMarginAsync(Symbol, 30m, (GateCrossExPositionSide)99));
        Assert.Empty(handler.Requests);
    }

    [Fact]
    public async Task Isolated_margin_does_not_guess_an_alphanumeric_only_asset_grammar_or_lookup_market_state()
    {
        var handler = Handler(JsonFixture.Read("Docs/CrossEx/isolated_margin.success.json"), HttpStatusCode.Accepted);
        using var client = Client(handler);
        var result = await client.CrossEx.UpdateIsolatedMarginAsync("HYPERLIQUID_FUTURE_X:Y_USDC", 1m);
        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal("HYPERLIQUID_FUTURE_X:Y_USDC", (string?)JObject.Parse(Assert.Single(handler.Requests).Content)["symbol"]);
    }

    [Theory]
    [InlineData("\"-30.129\"", "-30.129")]
    [InlineData("-30", "-30")]
    [InlineData("\"0\"", "0")]
    [InlineData("\"0.1234567890123456789012345678\"", "0.1234567890123456789012345678")]
    public void Saved_isolated_margin_instructions_keep_exact_decimal_values(string token, string expected)
    {
        var value = JsonConvert.DeserializeObject<GateCrossExIsolatedMarginRequest>($"{{\"symbol\":\"{Symbol}\",\"margin\":{token}}}")!;
        Assert.Equal(decimal.Parse(expected, CultureInfo.InvariantCulture), value.Margin);
        Assert.Null(value.PositionSide);
        Assert.Equal(expected, (string?)JObject.FromObject(value)["margin"]);
    }

    [Theory]
    [InlineData("null")]
    [InlineData("true")]
    [InlineData("\"\"")]
    [InlineData("\"NaN\"")]
    [InlineData("\"1e-29\"")]
    [InlineData("\"0.12345678901234567890123456789\"")]
    [InlineData("\"79228162514264337593543950336\"")]
    [InlineData("30.129")]
    public void Malformed_saved_margin_values_are_not_rounded_or_replaced_with_zero(string token)
    {
        var json = $"{{\"symbol\":\"{Symbol}\",\"margin\":{token}}}";
        Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject<GateCrossExIsolatedMarginRequest>(json));
        Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject<GateCrossExIsolatedMarginResponse>(json));
        // A decimal-aware reader does not make floating-token precision recoverable.
        Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject<GateCrossExIsolatedMarginRequest>(json, new JsonSerializerSettings { FloatParseHandling = FloatParseHandling.Decimal }));
    }

    [Theory]
    [InlineData("\"NONE\"", GateCrossExPositionSide.None)]
    [InlineData("\"LONG\"", GateCrossExPositionSide.Long)]
    [InlineData("\"SHORT\"", GateCrossExPositionSide.Short)]
    [InlineData("null", null)]
    public void Saved_isolated_margin_sides_preserve_known_values_and_explicit_null(string token, GateCrossExPositionSide? expected)
    {
        var value = JsonConvert.DeserializeObject<GateCrossExIsolatedMarginRequest>($"{{\"symbol\":\"{Symbol}\",\"margin\":\"30\",\"position_side\":{token}}}")!;
        Assert.Equal(expected, value.PositionSide);
        if (expected == null) Assert.Null(JObject.FromObject(value)["position_side"]);
        else Assert.Equal(expected.ToString()!.ToUpperInvariant(), (string?)JObject.FromObject(value)["position_side"]);
    }

    [Theory]
    [InlineData("\"UNKNOWN\"")]
    [InlineData("\"long\"")]
    [InlineData("\"\"")]
    [InlineData("0")]
    [InlineData("1.0")]
    [InlineData("true")]
    public void Unknown_saved_sides_cannot_disappear_into_the_omitted_instruction(string token)
        => Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject<GateCrossExIsolatedMarginRequest>($"{{\"symbol\":\"{Symbol}\",\"margin\":\"30\",\"position_side\":{token}}}"));

    [Theory]
    [InlineData("{}")]
    [InlineData("[]")]
    [InlineData("{\"symbol\":\"HYPERLIQUID_FUTURE_CXMT_USDC\"}")]
    [InlineData("{\"margin\":\"30\"}")]
    [InlineData("{\"symbol\":null,\"margin\":\"30\"}")]
    public void Saved_margin_requests_require_both_symbol_and_explicit_amount(string json)
        => Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject<GateCrossExIsolatedMarginRequest>(json));

    [Theory]
    [InlineData("{}")]
    [InlineData("{\"symbol\":\"HYPERLIQUID_FUTURE_CXMT_USDC\"}")]
    [InlineData("{\"margin\":\"30\"}")]
    [InlineData("{\"symbol\":null,\"margin\":\"30\"}")]
    [InlineData("{\"symbol\":\"HYPERLIQUID_FUTURE_CXMT_USDC\",\"margin\":true}")]
    [InlineData("null")]
    [InlineData("[]")]
    [InlineData("")]
    public async Task Malformed_accepted_margin_responses_do_not_fabricate_success(string json)
    {
        var handler = Handler(json, HttpStatusCode.Accepted);
        using var client = Client(handler);
        var result = await client.CrossEx.UpdateIsolatedMarginAsync(Symbol, 30m);
        Assert.False(result.Success);
        Assert.NotNull(result.Error);
        Assert.Equal(HttpStatusCode.Accepted, result.Response!.StatusCode);
        Assert.Single(handler.Requests);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("FUTURE_SIDE")]
    public async Task Optional_returned_margin_side_is_preserved_without_inference(string? side)
    {
        var json = new JObject { ["symbol"] = "HYPERLIQUID_FUTURE_OTHER_USDC", ["margin"] = "29.12" };
        if (side != null) json["position_side"] = side;
        var handler = Handler(json.ToString(Formatting.None), HttpStatusCode.Accepted);
        using var client = Client(handler);
        var result = await client.CrossEx.UpdateIsolatedMarginAsync(Symbol, 30.129m);
        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal("HYPERLIQUID_FUTURE_OTHER_USDC", result.Data.Symbol);
        Assert.Equal(29.12m, result.Data.Margin);
        Assert.Equal(side, result.Data.PositionSide);
        Assert.Single(handler.Requests);
    }

    [Theory]
    [InlineData(HttpStatusCode.BadRequest)]
    [InlineData(HttpStatusCode.TooManyRequests)]
    [InlineData(HttpStatusCode.InternalServerError)]
    public async Task All_three_endpoints_preserve_http_and_error_metadata_without_retry(HttpStatusCode status)
    {
        var handler = Handler("{\"label\":\"CONTRACT_TEST_ERROR\",\"message\":\"Rejected by server\"}", status);
        using var client = Client(handler);
        var symbols = await client.CrossEx.GetSymbolsAsync();
        var positions = await client.CrossEx.GetHistoricalPositionsAsync(new GateCrossExHistoryQueryRequest());
        var margin = await client.CrossEx.UpdateIsolatedMarginAsync(Symbol, 30m);
        Assert.False(symbols.Success);
        Assert.False(positions.Success);
        Assert.False(margin.Success);
        Assert.Equal(status, symbols.Response!.StatusCode);
        Assert.Equal(status, positions.Response!.StatusCode);
        Assert.Equal(status, margin.Response!.StatusCode);
        Assert.Equal("CONTRACT_TEST_ERROR", symbols.Error!.Data);
        Assert.Equal("CONTRACT_TEST_ERROR", positions.Error!.Data);
        Assert.Equal("CONTRACT_TEST_ERROR", margin.Error!.Data);
        Assert.Contains("Rejected by server", margin.Error.Message);
        Assert.Equal(3, handler.Requests.Count);
    }

    private static void AssertFieldNames<T>(string names, string? legacy = null)
    {
        var expected = names.Split(',').Concat(legacy == null ? Array.Empty<string>() : new[] { legacy }).OrderBy(x => x);
        var contract = (Newtonsoft.Json.Serialization.JsonObjectContract)new Newtonsoft.Json.Serialization.DefaultContractResolver().ResolveContract(typeof(T));
        Assert.Equal(expected, contract.Properties.Select(x => x.PropertyName).OrderBy(x => x));
    }

    private static GateCrossExSymbol SymbolValue(string overrides)
    {
        var value = (JObject)JsonFixture.Parse("Docs/CrossEx/symbols.current.success.json")[0]!;
        value.Remove("support_rpi");
        value.Remove("support_cross");
        foreach (var property in JObject.Parse(overrides).Properties()) value[property.Name] = property.Value;
        return JsonConvert.DeserializeObject<GateCrossExSymbol>(value.ToString(Formatting.None))!;
    }

    private static RecordingHttpMessageHandler Handler(string json, HttpStatusCode status = HttpStatusCode.OK)
        => new(_ => new HttpResponseMessage(status) { Content = new StringContent(json, Encoding.UTF8, "application/json") });

    private static GateRestApiClient Client(RecordingHttpMessageHandler handler)
    {
        var client = new GateRestApiClient(new GateRestApiClientOptions { HttpClient = new HttpClient(handler) });
        client.SetApiCredentials("key", "secret");
        return client;
    }

    private static Dictionary<string, string> Query(Uri uri)
        => uri.Query.TrimStart('?').Split('&', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Split('=', 2))
            .ToDictionary(x => Uri.UnescapeDataString(x[0]), x => Uri.UnescapeDataString(x.Length > 1 ? x[1] : ""));

    private static void AssertSignature(RecordedHttpRequest request)
    {
        Assert.Equal("key", Assert.Single(request.Headers["KEY"]));
        var timestamp = Assert.Single(request.Headers["Timestamp"]);
        var bodyHash = Convert.ToHexString(SHA512.HashData(Encoding.UTF8.GetBytes(request.Content))).ToLowerInvariant();
        var payload = $"{request.Method.Method}\n{request.RequestUri.AbsolutePath}\n{request.RequestUri.Query.TrimStart('?')}\n{bodyHash}\n{timestamp}";
        var signature = Convert.ToHexString(HMACSHA512.HashData(Encoding.UTF8.GetBytes("secret"), Encoding.UTF8.GetBytes(payload))).ToLowerInvariant();
        Assert.Equal(signature, Assert.Single(request.Headers["SIGN"]));
    }
}

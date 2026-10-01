using Gate.IO.Api.Margin;
using Gate.IO.Api.Tests.Infrastructure;

namespace Gate.IO.Api.Tests.Margin;

[Trait("Category", "Contract")]
public class MarginContractTests
{
    [Fact]
    public void Documented_margin_account_responses_deserialize()
    {
        var accounts = JsonFixture.Deserialize<List<GateMarginBalance>>("Docs/Margin/accounts.success.json");
        var isolatedAccounts = JsonFixture.Deserialize<List<GateMarginBalance>>("Docs/Margin/isolated_accounts.success.json");
        var history = JsonFixture.Deserialize<List<GateMarginBalanceHistory>>("Docs/Margin/account_book.success.json");
        var funding = JsonFixture.Deserialize<List<GateMarginFundingBalance>>("Docs/Margin/funding_accounts.success.json");

        Assert.Single(accounts);
        Assert.Equal("BTC_USDT", accounts[0].Symbol);
        Assert.Equal(20m, accounts[0].Leverage);
        Assert.Equal(16.5949188975473644m, accounts[0].MMR);
        Assert.Single(isolatedAccounts);
        Assert.Single(history);
        Assert.Equal(123456, history[0].Id);
        Assert.Equal(1547633726123, history[0].TimeInMilliseconds);
        Assert.Equal(1.03m, history[0].Change);
        Assert.Single(funding);
        Assert.Equal(3.32m, funding[0].TotalLent);
    }

    [Fact]
    public void Documented_margin_settings_and_amount_responses_deserialize()
    {
        var autoRepay = JsonFixture.Deserialize<GateMarginAutoRepayment>("Docs/Margin/auto_repay.success.json");
        var transferable = JsonFixture.Deserialize<GateMarginAmount>("Docs/Margin/transferable.success.json");
        var borrowable = JsonFixture.Deserialize<GateMarginBorrowable>("Docs/Margin/borrowable.success.json");
        var leverage = JsonFixture.Deserialize<GateMarginLeverage>("Docs/Margin/leverage.success.json");

        Assert.Equal(GateMarginAutoRepaymentStatus.Enabled, autoRepay.Status);
        Assert.Equal("BTC_USDT", transferable.Symbol);
        Assert.Equal(1.1m, transferable.Amount);
        Assert.Equal(10000m, borrowable.Borrowable);
        Assert.Equal(10m, leverage.Leverage);
    }

    [Fact]
    public void Documented_public_margin_market_responses_deserialize()
    {
        var markets = JsonFixture.Deserialize<List<GateMarginMarket>>("Docs/Margin/currency_pairs.success.json");
        var market = JsonFixture.Deserialize<GateMarginMarket>("Docs/Margin/currency_pair.success.json");
        var estimateRates = JsonFixture.Deserialize<Dictionary<string, decimal>>("Docs/Margin/estimate_rate.success.json");
        var tiers = JsonFixture.Deserialize<List<GateMarginTier>>("Docs/Margin/loan_margin_tiers.success.json");

        Assert.Single(markets);
        foreach (var item in new[] { markets[0], market })
        {
            Assert.Equal("AE_USDT", item.Symbol);
            Assert.Equal(100m, item.MinimumBaseBorrowQuantity);
            Assert.Equal(100m, item.MinimumQuoteBorrowQuantity);
            Assert.Equal(3m, item.Leverage);
            Assert.Equal("enabled", item.Status);
            Assert.Equal(1786329745L, item.DelistedTime);
        }
        Assert.Equal(0.0000703m, estimateRates["BTC"]);
        Assert.Single(tiers);
        Assert.Equal(100m, tiers[0].UpperLimit);
        Assert.Equal(0.9m, tiers[0].MMR);
    }

    [Theory]
    [InlineData("enabled")]
    [InlineData("disabled")]
    [InlineData("future-status")]
    public void Margin_market_status_preserves_the_wire_string_without_inference(string status)
    {
        var body = new JObject { ["status"] = status, ["delisted_time"] = 1786329745L };

        var market = JsonConvert.DeserializeObject<GateMarginMarket>(body.ToString())!;

        Assert.Equal(status, market.Status);
        Assert.Equal(1786329745L, market.DelistedTime);
        Assert.Equal(status, JObject.FromObject(market)["status"]!.Value<string>());
    }

    [Theory]
    [InlineData("{}")]
    [InlineData("{\"status\":null,\"delisted_time\":null}")]
    public void Optional_margin_market_metadata_is_not_defaulted_to_disabled_or_zero(string json)
    {
        var market = JsonConvert.DeserializeObject<GateMarginMarket>(json)!;

        Assert.Null(market.Status);
        Assert.Null(market.DelistedTime);
        var serialized = JObject.FromObject(market);
        Assert.Null(serialized["status"]);
        Assert.Null(serialized["delisted_time"]);
    }

    [Theory]
    [InlineData(0L)]
    [InlineData(1786329745L)]
    [InlineData(long.MaxValue)]
    [InlineData(long.MinValue)]
    public void Margin_delisting_time_preserves_int64_values_without_date_or_sentinel_conversion(long time)
    {
        var body = new JObject { ["status"] = "enabled", ["delisted_time"] = time };

        var market = JsonConvert.DeserializeObject<GateMarginMarket>(body.ToString())!;
        var serialized = JObject.FromObject(market);

        Assert.Equal(time, market.DelistedTime);
        Assert.Equal("enabled", market.Status);
        Assert.Equal(JTokenType.Integer, serialized["delisted_time"]!.Type);
        Assert.Equal(time, serialized["delisted_time"]!.Value<long>());
    }

    [Theory]
    [InlineData("en-US")]
    [InlineData("tr-TR")]
    [InlineData("fr-FR")]
    public void Margin_market_decimal_strings_preserve_precision_without_changing_existing_accessors(string culture)
    {
        const string json = "{\"currency_pair\":\"AE_USDT\",\"base_min_borrow_amount\":\"0.1234567890123456789012345678\",\"quote_min_borrow_amount\":\"100.12345678901234567890123456\",\"leverage\":\"3.5\",\"status\":\"enabled\",\"delisted_time\":1786329745}";
        var previousCulture = System.Globalization.CultureInfo.CurrentCulture;
        try
        {
            System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo(culture);
            var market = JsonConvert.DeserializeObject<GateMarginMarket>(json)!;
            var serializedJson = JsonConvert.SerializeObject(market);
            var serialized = JObject.Parse(serializedJson);
            var roundTrip = JsonConvert.DeserializeObject<GateMarginMarket>(serializedJson)!;

            Assert.Equal(0.1234567890123456789012345678m, market.MinimumBaseBorrowQuantity);
            Assert.Equal(100.12345678901234567890123456m, market.MinimumQuoteBorrowQuantity);
            Assert.Equal(3.5m, market.Leverage);
            Assert.Equal(market.MinimumBaseBorrowQuantity, roundTrip.MinimumBaseBorrowQuantity);
            Assert.Equal(market.MinimumQuoteBorrowQuantity, roundTrip.MinimumQuoteBorrowQuantity);
            Assert.Equal(market.Leverage, roundTrip.Leverage);
            Assert.Equal(6, serialized.Count);
            var source = JObject.Parse(json);
            foreach (var field in new[] { "currency_pair", "base_min_borrow_amount", "quote_min_borrow_amount", "leverage", "status" })
                Assert.Equal(JTokenType.String, source[field]!.Type);
            Assert.Equal(JTokenType.String, serialized["status"]!.Type);
            Assert.Equal("enabled", serialized["status"]!.Value<string>());
            Assert.Equal(JTokenType.Integer, serialized["delisted_time"]!.Type);
        }
        finally
        {
            System.Globalization.CultureInfo.CurrentCulture = previousCulture;
        }
    }

    [Theory]
    [InlineData("base_min_borrow_amount", "Infinity")]
    [InlineData("quote_min_borrow_amount", "∞")]
    [InlineData("leverage", "")]
    [InlineData("leverage", " ")]
    public void Undocumented_margin_numeric_strings_are_not_silently_converted_to_zero(string field, string value)
    {
        var body = new JObject { [field] = value };

        Assert.ThrowsAny<JsonException>(() => JsonConvert.DeserializeObject<GateMarginMarket>(body.ToString()));
    }

    [Fact]
    public void Historical_margin_market_payloads_and_numeric_enum_mappings_remain_compatible()
    {
        const string json = "{\"currency_pair\":\"AE_USDT\",\"base_min_borrow_amount\":\"100\",\"quote_min_borrow_amount\":\"100\",\"leverage\":\"3\"}";

        var market = JsonConvert.DeserializeObject<GateMarginMarket>(json)!;

        Assert.Equal("AE_USDT", market.Symbol);
        Assert.Equal(100m, market.MinimumBaseBorrowQuantity);
        Assert.Equal(100m, market.MinimumQuoteBorrowQuantity);
        Assert.Equal(3m, market.Leverage);
        Assert.Null(market.Status);
        Assert.Null(market.DelistedTime);
        Assert.Equal((byte)0, (byte)GateMarginMarketStatus.Disabled);
        Assert.Equal((byte)1, (byte)GateMarginMarketStatus.Enabled);
        Assert.Equal("0", ApiSharp.Converters.MapConverter.GetString(GateMarginMarketStatus.Disabled));
        Assert.Equal("1", ApiSharp.Converters.MapConverter.GetString(GateMarginMarketStatus.Enabled));
    }

    [Fact]
    public void Documented_margin_loan_and_interest_responses_deserialize()
    {
        var loans = JsonFixture.Deserialize<List<GateMarginLoan>>("Docs/Margin/loans.success.json");
        var loanRecords = JsonFixture.Deserialize<List<GateMarginLoanRecord>>("Docs/Margin/loan_records.success.json");
        var interestRecords = JsonFixture.Deserialize<List<GateMarginInterest>>("Docs/Margin/interest_records.success.json");

        Assert.Single(loans);
        Assert.Equal("GT_USDT", loans[0].Symbol);
        Assert.Equal(GateMarginLoanType.Margin, loans[0].Type);
        Assert.NotNull(loans[0].UpdateTime);
        Assert.Single(loanRecords);
        Assert.Equal(GateMarginUniOrderType.Borrow, loanRecords[0].Type);
        Assert.Single(interestRecords);
        Assert.Equal(GateMarginUniInterestStatus.Success, interestRecords[0].Status);
        Assert.Equal(GateMarginLoanType.Margin, interestRecords[0].Type);
        Assert.Equal(0.01m, interestRecords[0].Interest);
    }

    [Fact]
    public void Captured_live_public_margin_responses_deserialize()
    {
        var markets = JsonFixture.Deserialize<List<GateMarginMarket>>("Live/Margin/currency_pairs.json");
        var market = JsonFixture.Deserialize<GateMarginMarket>("Live/Margin/currency_pairs.BTC_USDT.json");
        var tiers = JsonFixture.Deserialize<List<GateMarginTier>>("Live/Margin/loan_margin_tiers.BTC_USDT.json");

        Assert.NotEmpty(markets);
        Assert.Equal("BTC_USDT", market.Symbol);
        Assert.NotEmpty(tiers);
    }
}

namespace Gate.IO.Api.Tests.Infrastructure;

internal static class PublicEndpointCatalog
{
    public const string RestBaseUrl = "https://api.gateio.ws/api/v4";

    public static readonly IReadOnlyList<PublicEndpointCatalogEntry> Entries =
    [
        Entry("Wallet", "Currency chains", "GET", "/wallet/currency_chains?currency=GT", "wallet", "Wallet/currency_chains.GT.json"),

        Entry("Unified", "Currencies", "GET", "/unified/currencies?currency=BTC", "unified", "Unified/currencies.BTC.json"),
        Entry("Unified", "Historical lending rates", "GET", "/unified/history_loan_rate?currency=USDT&limit=1", "unified", "Unified/history_loan_rate.USDT.limit1.json"),
        Entry("Unified", "Currency discount tiers", "GET", "/unified/currency_discount_tiers", "unified", "Unified/currency_discount_tiers.json"),
        Entry("Unified", "Loan margin tiers", "GET", "/unified/loan_margin_tiers", "unified", "Unified/loan_margin_tiers.json"),
        Entry("Unified", "Portfolio calculator", "POST", "/unified/portfolio_calculator", "unified", null, false),

        Entry("Margin", "Isolated margin currency pairs", "GET", "/margin/uni/currency_pairs", "isolated-margin", "Margin/currency_pairs.json"),
        Entry("Margin", "Isolated margin currency pair", "GET", "/margin/uni/currency_pairs/BTC_USDT", "isolated-margin", "Margin/currency_pairs.BTC_USDT.json"),
        Entry("Margin", "Isolated margin loan tiers", "GET", "/margin/uni/loan_margin_tiers?currency_pair=BTC_USDT", "isolated-margin", "Margin/loan_margin_tiers.BTC_USDT.json"),

        Entry("Spot", "Currencies", "GET", "/spot/currencies", "spot", null, false),
        Entry("Spot", "Currency", "GET", "/spot/currencies/GT", "spot", "Spot/currencies.GT.json"),
        Entry("Spot", "Currency pairs", "GET", "/spot/currency_pairs", "spot", null, false),
        Entry("Spot", "Currency pair", "GET", "/spot/currency_pairs/BTC_USDT", "spot", "Spot/currency_pairs.BTC_USDT.json"),
        Entry("Spot", "Tickers", "GET", "/spot/tickers?currency_pair=BTC_USDT", "spot", "Spot/tickers.BTC_USDT.json"),
        Entry("Spot", "Order book", "GET", "/spot/order_book?currency_pair=BTC_USDT&limit=5", "spot", "Spot/order_book.BTC_USDT.limit5.json"),
        Entry("Spot", "Trades", "GET", "/spot/trades?currency_pair=BTC_USDT&limit=1", "spot", "Spot/trades.BTC_USDT.limit1.json"),
        Entry("Spot", "Candlesticks", "GET", "/spot/candlesticks?currency_pair=BTC_USDT&interval=1m&limit=1", "spot", "Spot/candlesticks.BTC_USDT.1m.limit1.json"),
        Entry("Spot", "Server time", "GET", "/spot/time", "spot", "Spot/time.json"),
        Entry("Spot", "Insurance history", "GET", "/spot/insurance_history?business=margin&currency=BTC&from=1727054547&to=1727054547&limit=1", "spot", "Spot/insurance_history.BTC.margin.json"),

        Entry("FlashSwap", "Currency pairs", "GET", "/flash_swap/currency_pairs?limit=1", "flash-swap", "Swap/currency_pairs.limit1.json"),
        Entry("FlashSwap", "Currency pairs by currency", "GET", "/flash_swap/currency_pairs?currency=BTC&limit=1", "flash-swap", "Swap/currency_pairs.BTC.limit1.json"),

        Entry("Futures", "Contracts", "GET", "/futures/usdt/contracts?limit=1", "futures", "Futures/contracts.usdt.limit1.json"),
        Entry("Futures", "Contract", "GET", "/futures/usdt/contracts/BTC_USDT", "futures", "Futures/contract.BTC_USDT.json"),
        Entry("Futures", "Tickers", "GET", "/futures/usdt/tickers?contract=BTC_USDT", "futures", "Futures/tickers.BTC_USDT.json"),
        Entry("Futures", "Order book", "GET", "/futures/usdt/order_book?contract=BTC_USDT&limit=5", "futures", "Futures/order_book.BTC_USDT.limit5.json"),
        Entry("Futures", "Trades", "GET", "/futures/usdt/trades?contract=BTC_USDT&limit=1", "futures", "Futures/trades.BTC_USDT.limit1.json"),
        Entry("Futures", "Candlesticks", "GET", "/futures/usdt/candlesticks?contract=BTC_USDT&interval=1m&limit=1", "futures", "Futures/candlesticks.BTC_USDT.1m.limit1.json"),
        Entry("Futures", "Funding rate history", "GET", "/futures/usdt/funding_rate?contract=BTC_USDT&limit=1", "futures", "Futures/funding_rate.BTC_USDT.limit1.json"),
        Entry("Futures", "Batch funding rate history", "POST", "/futures/usdt/funding_rates", "futures", "Futures/funding_rates.BTC_USDT.json"),
        Entry("Futures", "Insurance history", "GET", "/futures/usdt/insurance?limit=1", "futures", "Futures/insurance.usdt.limit1.json"),
        Entry("Futures", "Contract stats", "GET", "/futures/usdt/contract_stats?contract=BTC_USDT&interval=1h&limit=1", "futures", "Futures/contract_stats.BTC_USDT.1h.limit1.json"),
        Entry("Futures", "Index constituents", "GET", "/futures/usdt/index_constituents/BTC_USDT", "futures", "Futures/index_constituents.BTC_USDT.json"),
        Entry("Futures", "Liquidations", "GET", "/futures/usdt/liq_orders?contract=BTC_USDT&limit=1", "futures", "Futures/liq_orders.BTC_USDT.limit1.json"),
        Entry("Futures", "Risk limit tiers", "GET", "/futures/usdt/risk_limit_tiers?contract=BTC_USDT&limit=1", "futures", "Futures/risk_limit_tiers.BTC_USDT.limit1.json"),

        Entry("TradFi", "Symbol categories", "GET", "/tradfi/symbols/categories", "tradfi", "TradFi/categories.json"),
        Entry("TradFi", "Symbols", "GET", "/tradfi/symbols", "tradfi", "TradFi/symbols.json"),
        Entry("TradFi", "Ticker", "GET", "/tradfi/symbols/EURUSD/tickers", "tradfi", "TradFi/ticker.EURUSD.json"),
        Entry("TradFi", "Candlesticks", "GET", "/tradfi/symbols/EURUSD/candlesticks?interval=1m&limit=1", "tradfi", "TradFi/candlesticks.EURUSD.1m.limit1.json"),

        Entry("Delivery", "Contracts", "GET", "/delivery/usdt/contracts", "delivery", "Delivery/contracts.usdt.json"),
        Entry("Delivery", "Contract", "GET", "/delivery/usdt/contracts/{contract}", "delivery", "Delivery/contract.usdt.first.json"),
        Entry("Delivery", "Tickers", "GET", "/delivery/usdt/tickers?contract={contract}", "delivery", "Delivery/tickers.usdt.first.json"),
        Entry("Delivery", "Order book", "GET", "/delivery/usdt/order_book?contract={contract}&limit=5", "delivery", "Delivery/order_book.usdt.first.limit5.json"),
        Entry("Delivery", "Trades", "GET", "/delivery/usdt/trades?contract={contract}&limit=1", "delivery", "Delivery/trades.usdt.first.limit1.json"),
        Entry("Delivery", "Candlesticks", "GET", "/delivery/usdt/candlesticks?contract={contract}&interval=1m&limit=1", "delivery", "Delivery/candlesticks.usdt.first.1m.limit1.json"),
        Entry("Delivery", "Insurance history", "GET", "/delivery/usdt/insurance?limit=1", "delivery", "Delivery/insurance.usdt.limit1.json"),
        Entry("Delivery", "Risk limit tiers", "GET", "/delivery/usdt/risk_limit_tiers?contract={contract}&limit=1", "delivery", "Delivery/risk_limit_tiers.usdt.first.limit1.json"),

        Entry("Options", "Underlyings", "GET", "/options/underlyings", "options", "Options/underlyings.json"),
        Entry("Options", "Expirations", "GET", "/options/expirations?underlying=BTC_USDT", "options", "Options/expirations.BTC_USDT.json"),
        Entry("Options", "Contracts", "GET", "/options/contracts?underlying=BTC_USDT&expiration={expiration}", "options", "Options/contracts.BTC_USDT.first_expiration.json"),
        Entry("Options", "Contract", "GET", "/options/contracts/{contract}", "options", "Options/contract.BTC_USDT-20260507-86000-P.json"),
        Entry("Options", "Order book", "GET", "/options/order_book?contract={contract}&limit=5", "options", "Options/order_book.BTC_USDT-20260507-86000-P.limit5.json"),
        Entry("Options", "Contract tickers", "GET", "/options/tickers?underlying=BTC_USDT", "options", "Options/tickers.BTC_USDT.json"),
        Entry("Options", "Underlying ticker", "GET", "/options/underlying/tickers/BTC_USDT", "options", "Options/underlying_ticker.BTC_USDT.json"),
        Entry("Options", "Candlesticks", "GET", "/options/candlesticks?contract={contract}&interval=1m&limit=1", "options", "Options/candlesticks.BTC_USDT-20260507-86000-P.1m.limit1.json"),
        Entry("Options", "Underlying candlesticks", "GET", "/options/underlying/candlesticks?underlying=BTC_USDT&interval=1m&limit=1", "options", "Options/underlying_candlesticks.BTC_USDT.1m.limit1.json"),
        Entry("Options", "Trades", "GET", "/options/trades?limit=1", "options", "Options/trades.limit1.json"),
        Entry("Options", "Settlements", "GET", "/options/settlements?underlying=BTC_USDT&limit=1", "options", "Options/settlements.BTC_USDT.limit1.json", false),

        Entry("EarnUni", "Currencies", "GET", "/earn/uni/currencies", "earnuni", "EarnUni/currencies.json"),
        Entry("EarnUni", "Currency", "GET", "/earn/uni/currencies/BTC", "earnuni", "EarnUni/currency.BTC.json"),

        Entry("MultiCollateralLoan", "Supported currencies", "GET", "/loan/multi_collateral/currencies", "multi-collateral-loan", "MultiCollateralLoan/currencies.json"),
        Entry("MultiCollateralLoan", "LTV", "GET", "/loan/multi_collateral/ltv", "multi-collateral-loan", "MultiCollateralLoan/ltv.json"),
        Entry("MultiCollateralLoan", "Fixed rates", "GET", "/loan/multi_collateral/fixed_rate", "multi-collateral-loan", "MultiCollateralLoan/fixed_rate.json"),
        Entry("MultiCollateralLoan", "Current rates", "GET", "/loan/multi_collateral/current_rate?currencies=BTC,GT", "multi-collateral-loan", "MultiCollateralLoan/current_rate.BTC_GT.json"),

        Entry("Earn", "Dual investment plans", "GET", "/earn/dual/investment_plan?coin=BTC&page=1&page_size=1", "earn", "Earn/dual_investment_plan.BTC.json"),
        Entry("Earn", "Fixed-term products", "GET", "/earn/fixed-term/product?page=1&limit=1", "earn", "Earn/fixed_term_product.page1.limit1.json"),
        Entry("Earn", "Fixed-term products by asset", "GET", "/earn/fixed-term/product/USDT/list", "earn", "Earn/fixed_term_product.USDT.list.json"),

        Entry("CrossEx", "Symbols", "GET", "/crossex/rule/symbols?symbols=BINANCE_FUTURE_ADA_USDT", "crossex", "CrossEx/symbols.BINANCE_FUTURE_ADA_USDT.json"),
        Entry("CrossEx", "Risk limits", "GET", "/crossex/rule/risk_limits?symbols=BINANCE_FUTURE_ADA_USDT", "crossex", "CrossEx/risk_limits.BINANCE_FUTURE_ADA_USDT.json"),
        Entry("CrossEx", "Transfer coins", "GET", "/crossex/transfers/coin?coin=USDT", "crossex", "CrossEx/transfer_coins.USDT.json"),

        Entry("Alpha", "Currencies", "GET", "/alpha/currencies?limit=1", "https://www.gate.com/docs/developers/alpha/en/", "Alpha/currencies.limit1.json"),
        Entry("Alpha", "Tickers", "GET", "/alpha/tickers?limit=1", "https://www.gate.com/docs/developers/alpha/en/", "Alpha/tickers.limit1.json"),
        Entry("Alpha", "Tokens", "GET", "/alpha/tokens?page=1", "https://www.gate.com/docs/developers/alpha/en/", "Alpha/tokens.page1.json"),
    ];

    public static readonly IReadOnlyCollection<string> ModulesWithClientSmokeTests =
        Entries.Where(x => x.HasClientSmokeTest)
            .Select(x => x.Module)
            .Distinct(StringComparer.Ordinal)
            .OrderBy(x => x, StringComparer.Ordinal)
            .ToArray();

    private static PublicEndpointCatalogEntry Entry(
        string module,
        string name,
        string method,
        string pathAndQuery,
        string documentationSlugOrUrl,
        string? liveFixturePath,
        bool hasClientSmokeTest = true)
    {
        var documentationUrl = documentationSlugOrUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase)
            ? documentationSlugOrUrl
            : $"https://www.gate.com/docs/developers/apiv4/en/{documentationSlugOrUrl}/";

        return new PublicEndpointCatalogEntry(
            module,
            name,
            method,
            pathAndQuery,
            documentationUrl,
            liveFixturePath,
            hasClientSmokeTest);
    }
}

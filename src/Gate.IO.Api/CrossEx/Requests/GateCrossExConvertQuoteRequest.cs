namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx flash swap quote request
/// </summary>
public record GateCrossExConvertQuoteRequest
{
    /// <summary>
    /// BINANCE/OKX/GATE/BYBIT/HYPERLIQUID/KRAKEN/LIGHTER; not the shared enum's CROSSEX or DERIBIT members.
    /// LIGHTER supports LIGHTER_USDC / CROSSEX_USDT swaps in cross-exchange mode only.
    /// The client does not change account mode or execute the quote automatically.
    /// </summary>
    [JsonProperty("exchange_type", Required = Required.Always)]
    [JsonConverter(typeof(GateCrossExInstructionEnumConverter))]
    public GateCrossExExchangeType ExchangeType { get; set; }

    /// <summary>
    /// Asset sold, passed unchanged. For LIGHTER use LIGHTER_USDC or CROSSEX_USDT; no asset-name translation is performed.
    /// </summary>
    [JsonProperty("from_coin", Required = Required.Always)]
    [JsonConverter(typeof(GateCrossExActionValueConverter))]
    public string FromCoin { get; set; }

    /// <summary>
    /// Asset bought. OKX/GATE document BTC/ETH/USDT; BYBIT/BINANCE document USDT; HYPERLIQUID USDT/USDC;
    /// KRAKEN USDT; LIGHTER USDT/USDC swaps. Venue-prefixed assets and eligibility are server-side; no pair is chosen automatically.
    /// </summary>
    [JsonProperty("to_coin", Required = Required.Always)]
    [JsonConverter(typeof(GateCrossExActionValueConverter))]
    public string ToCoin { get; set; }

    /// <summary>
    /// Positive amount to sell with at most 16 decimal places. The client rejects excess transmitted scale rather than rounding it.
    /// </summary>
    [JsonProperty("from_amount", Required = Required.Always)]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal FromAmount { get; set; }
}

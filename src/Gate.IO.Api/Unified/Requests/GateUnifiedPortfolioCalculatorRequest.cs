namespace Gate.IO.Api.Unified;

/// <summary>
/// Gate Unified Portfolio Calculator Request
/// </summary>
public record GateUnifiedPortfolioCalculatorRequest
{
    /// <summary>
    /// Spot Balances
    /// </summary>
    [JsonProperty("spot_balances", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<GateUnifiedPortfolioCalculatorRequestSpotBalance> SpotBalances { get; set; } = [];

    /// <summary>
    /// Spot Orders
    /// </summary>
    [JsonProperty("spot_orders", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<GateUnifiedPortfolioCalculatorRequestSpotOrder> SpotOrders { get; set; } = [];

    /// <summary>
    /// Futures Positions
    /// </summary>
    [JsonProperty("futures_positions", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<GateUnifiedPortfolioCalculatorRequestFuturesPosition> FuturesPositions { get; set; } = [];

    /// <summary>
    /// Futures Orders
    /// </summary>
    [JsonProperty("futures_orders", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<GateUnifiedPortfolioCalculatorRequestFuturesOrder> FuturesOrders { get; set; } = [];

    /// <summary>
    /// Options Positions
    /// </summary>
    [JsonProperty("options_positions", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<GateUnifiedPortfolioCalculatorRequestOptionsPosition> OptionsPositions { get; set; } = [];

    /// <summary>
    /// Options Orders
    /// </summary>
    [JsonProperty("options_orders", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<GateUnifiedPortfolioCalculatorRequestOptionsOrder> OptionsOrders { get; set; } = [];

    /// <summary>
    /// Whether to enable spot hedging
    /// </summary>
    [JsonProperty("spot_hedge", NullValueHandling = NullValueHandling.Ignore)]
    public bool SpotHedge { get; set; }
}

/// <summary>
/// Gate Unified Portfolio Calculator Request -> Spot Balance
/// </summary>
public record GateUnifiedPortfolioCalculatorRequestSpotBalance
{
    /// <summary>
    /// Currency name
    /// </summary>
    [JsonProperty("currency", NullValueHandling = NullValueHandling.Ignore)]
    public string Currency { get; set; }

    /// <summary>
    /// Currency equity, where equity = balance - borrowed, represents the net delta exposure of your spot positions, which can be negative. Currently only supports BTC and ETH
    /// </summary>
    [JsonProperty("equity", NullValueHandling = NullValueHandling.Ignore)]
    public string Equity { get; set; }

    /// <summary>
    /// Freeze
    /// </summary>
    [JsonProperty("freeze", NullValueHandling = NullValueHandling.Ignore)]
    public string Freeze { get; set; }
}

/// <summary>
/// Gate Unified Portfolio Calculator Request -> Spot Order
/// </summary>
public record GateUnifiedPortfolioCalculatorRequestSpotOrder
{
    /// <summary>
    /// Market
    /// </summary>
    [JsonProperty("currency_pairs", NullValueHandling = NullValueHandling.Ignore)]
    public string Symbol { get; set; }

    /// <summary>
    /// Price
    /// </summary>
    [JsonProperty("order_price", NullValueHandling = NullValueHandling.Ignore)]
    public string Price { get; set; }

    /// <summary>
    /// Initial order quantity for spot trading pairs, not involved in actual calculation. Currently only supports BTC and ETH Currently only supports three currencies: BTC, ETH
    /// </summary>
    [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
    public string Count { get; set; }

    /// <summary>
    /// Unfilled quantity, involved in actual calculation
    /// </summary>
    [JsonProperty("left", NullValueHandling = NullValueHandling.Ignore)]
    public string Left { get; set; }

    /// <summary>
    /// Order type, sell - sell order, buy - buy order
    /// </summary>
    [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(MapConverter))]
    public GateSpotOrderSide Type { get; set; }
}

/// <summary>
/// Gate Unified Portfolio Calculator Request -> Futures Position
/// </summary>
public record GateUnifiedPortfolioCalculatorRequestFuturesPosition
{
    /// <summary>
    /// Futures name, currently only supports USDT perpetual contracts for BTC and ETH
    /// </summary>
    [JsonProperty("contract", NullValueHandling = NullValueHandling.Ignore)]
    public string Symbol { get; set; }

    /// <summary>
    /// Position size, measured in contract quantity
    /// </summary>
    [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
    public string Size { get; set; }
}

/// <summary>
/// Gate Unified Portfolio Calculator Request -> Futures Order
/// </summary>
public record GateUnifiedPortfolioCalculatorRequestFuturesOrder
{
    /// <summary>
    /// Futures name, currently only supports USDT perpetual contracts for BTC and ETH
    /// </summary>
    [JsonProperty("contract", NullValueHandling = NullValueHandling.Ignore)]
    public string Symbol { get; set; }

    /// <summary>
    /// Contract quantity, representing the initial order quantity, not involved in actual settlement
    /// </summary>
    [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
    public string Size { get; set; }

    /// <summary>
    /// Unfilled contract quantity, involved in actual calculation
    /// </summary>
    [JsonProperty("left", NullValueHandling = NullValueHandling.Ignore)]
    public string Left { get; set; }
}

/// <summary>
/// Gate Unified Portfolio Calculator Request -> Options Position
/// </summary>
public record GateUnifiedPortfolioCalculatorRequestOptionsPosition
{
    /// <summary>
    /// Option name, currently only supports USDT options for BTC and ETH
    /// </summary>
    [JsonProperty("options_name", NullValueHandling = NullValueHandling.Ignore)]
    public string Symbol { get; set; }

    /// <summary>
    /// Position size, measured in contract quantity
    /// </summary>
    [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
    public string Size { get; set; }
}

/// <summary>
/// Gate Unified Portfolio Calculator Request -> Options Order
/// </summary>
public record GateUnifiedPortfolioCalculatorRequestOptionsOrder
{
    /// <summary>
    /// Options name, currently only supports USDT perpetual contracts for BTC and ETH
    /// </summary>
    [JsonProperty("options_name", NullValueHandling = NullValueHandling.Ignore)]
    public string Symbol { get; set; }

    /// <summary>
    /// Initial order quantity, not involved in actual calculation
    /// </summary>
    [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
    public string Size { get; set; }

    /// <summary>
    /// Unfilled contract quantity, involved in actual calculation
    /// </summary>
    [JsonProperty("left", NullValueHandling = NullValueHandling.Ignore)]
    public string Left { get; set; }
}

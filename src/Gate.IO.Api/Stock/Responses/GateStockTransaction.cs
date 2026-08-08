namespace Gate.IO.Api.Stock;

/// <summary>
/// Stock account transaction
/// </summary>
public record GateStockTransaction
{
    /// <summary>Gets or sets the asset.</summary>
    [JsonProperty("asset")]
    public string Asset { get; set; }
    /// <summary>Gets or sets the symbol.</summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; }
    /// <summary>Gets or sets the displayed symbol.</summary>
    [JsonProperty("symbol_display")]
    public string SymbolDisplay { get; set; }
    /// <summary>Gets or sets the transaction type.</summary>
    [JsonProperty("type"), JsonConverter(typeof(MapConverter))]
    public GateStockTransactionType Type { get; set; }
    /// <summary>Gets or sets the transaction type description.</summary>
    [JsonProperty("type_desc")]
    public string TypeDescription { get; set; }
    /// <summary>Gets or sets the balance change.</summary>
    [JsonProperty("change")]
    public decimal Change { get; set; }
    /// <summary>Gets or sets the resulting balance.</summary>
    [JsonProperty("balance")]
    public decimal Balance { get; set; }
    /// <summary>Gets or sets the reference identifier.</summary>
    [JsonProperty("ref_id")]
    public string ReferenceId { get; set; }
    /// <summary>Gets or sets the transaction time.</summary>
    [JsonProperty("time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
    /// <summary>Gets or sets the display unit.</summary>
    [JsonProperty("unit_text")]
    public string UnitText { get; set; }
    /// <summary>Gets or sets endpoint-specific transaction details.</summary>
    [JsonProperty("detail")]
    public JObject Detail { get; set; }
}

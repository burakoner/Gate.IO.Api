namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi trading symbol category
/// </summary>
public record GateTradFiCategory
{
    [JsonProperty("category_id")]
    public long CategoryId { get; set; }

    [JsonProperty("is_favorite")]
    public bool IsFavorite { get; set; }

    [JsonProperty("category_name")]
    public string CategoryName { get; set; }
}

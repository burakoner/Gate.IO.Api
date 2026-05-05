namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi trading symbol category
/// </summary>
public record GateTradFiCategory
{
    /// <summary>
    /// Gets or sets the Category ID.
    /// </summary>
    [JsonProperty("category_id")]
    public long CategoryId { get; set; }

    /// <summary>
    /// Gets or sets the Is Favorite.
    /// </summary>
    [JsonProperty("is_favorite")]
    public bool IsFavorite { get; set; }

    /// <summary>
    /// Gets or sets the Category Name.
    /// </summary>
    [JsonProperty("category_name")]
    public string CategoryName { get; set; }
}

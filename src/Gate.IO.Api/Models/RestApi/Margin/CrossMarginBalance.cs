namespace Gate.IO.Api.Models.RestApi.Margin;

public class CrossMarginBalance
{
    /// <summary>
    /// Available amount
    /// </summary>
    [JsonProperty("available")]
    public decimal Available { get; set; }

    /// <summary>
    /// Locked amount
    /// </summary>
    [JsonProperty("freeze")]
    public decimal Frozen { get; set; }

    /// <summary>
    /// Borrowed amount
    /// </summary>
    [JsonProperty("borrowed")]
    public decimal Borrowed { get; set; }

    /// <summary>
    /// Unpaid interests
    /// </summary>
    [JsonProperty("interest")]
    public decimal Interest { get; set; }
}

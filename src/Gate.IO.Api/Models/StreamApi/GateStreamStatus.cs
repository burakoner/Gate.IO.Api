namespace Gate.IO.Api.Models.StreamApi;

/// <summary>
/// Represents the Gate Stream Status.
/// </summary>
public record GateStreamStatus
{
    /// <summary>
    /// Gets or sets the Status.
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }
}

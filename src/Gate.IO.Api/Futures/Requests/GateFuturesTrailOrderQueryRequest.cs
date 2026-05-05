namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures trail order list query request
/// </summary>
public record GateFuturesTrailOrderQueryRequest
{
    /// <summary>
    /// Gets or sets the Contract.
    /// </summary>
    public string Contract { get; set; }
    /// <summary>
    /// Gets or sets the Is Finished.
    /// </summary>
    public bool? IsFinished { get; set; }
    /// <summary>
    /// Gets or sets the Start At.
    /// </summary>
    public DateTime? StartAt { get; set; }
    /// <summary>
    /// Gets or sets the End At.
    /// </summary>
    public DateTime? EndAt { get; set; }
    /// <summary>
    /// Gets or sets the Page Number.
    /// </summary>
    public int? PageNumber { get; set; }
    /// <summary>
    /// Gets or sets the Page Size.
    /// </summary>
    public int? PageSize { get; set; }
    /// <summary>
    /// Gets or sets the Sort By.
    /// </summary>
    public int? SortBy { get; set; }
    /// <summary>
    /// Gets or sets the Hide Cancel.
    /// </summary>
    public bool? HideCancel { get; set; }
    /// <summary>
    /// Gets or sets the Related Position.
    /// </summary>
    public int? RelatedPosition { get; set; }
    /// <summary>
    /// Gets or sets the Sort By Trigger.
    /// </summary>
    public bool? SortByTrigger { get; set; }
    /// <summary>
    /// Gets or sets the Reduce Only.
    /// </summary>
    public int? ReduceOnly { get; set; }
    /// <summary>
    /// Gets or sets the Side.
    /// </summary>
    public int? Side { get; set; }
}

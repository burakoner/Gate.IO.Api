namespace Gate.IO.Api.Announcements;

/// <summary>Public Gate announcement REST queries, separate from announcement stream subscriptions.</summary>
public class GateAnnouncementsRestApiClient
{
    private readonly GateRestApiClient root;

    internal GateAnnouncementsRestApiClient(GateRestApiClient root) => this.root = root;

    /// <summary>Query announcement articles in an unsigned JSON POST. No automatic pagination or link following.</summary>
    /// <param name="titleQuery">Optional title filter.</param>
    /// <param name="page">Optional page number, sent unchanged as a string.</param>
    /// <param name="size">Optional page size, sent unchanged as a string.</param>
    /// <param name="tags">Optional tags.</param>
    /// <param name="timer">Optional last N days, passed as a string.</param>
    /// <param name="categoryName">Optional category name.</param>
    /// <param name="categoryLevel">Optional category level: "1" or "2".</param>
    /// <param name="subWebsiteId">Optional subsite string: "0" main, "177" Turkey. Omission leaves the server default.</param>
    /// <param name="pinned">Optional 1 to include pinned articles or 0 to exclude them; server default 1.</param>
    /// <param name="updateAfter">Optional raw integer update timestamp; no date conversion is performed.</param>
    /// <param name="language">Optional raw language code, for example "cn". Not restricted to the stream language enum.</param>
    /// <param name="filterEmptyContent">Optional 1 to exclude empty content or 0 to keep it; server default 1.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Complete response, including the uninterpreted business code, message and version.</returns>
    public Task<RestCallResult<GateAnnouncementArticleListResponse>> GetArticlesAsync(
        string titleQuery = null, string page = null, string size = null, string tags = null,
        string timer = null, string categoryName = null, string categoryLevel = null,
        string subWebsiteId = null, int? pinned = null, int? updateAfter = null,
        string language = null, int? filterEmptyContent = null, CancellationToken ct = default)
        => GetArticlesAsync(new GateAnnouncementArticleListRequest
        {
            TitleQuery = titleQuery, Page = page, Size = size, Tags = tags, Timer = timer,
            CategoryName = categoryName, CategoryLevel = categoryLevel, SubWebsiteId = subWebsiteId,
            Pinned = pinned, UpdateAfter = updateAfter, Language = language, FilterEmptyContent = filterEmptyContent,
        }, ct);

    /// <summary>
    /// POST /ann/list_article without authentication, even when credentials are configured.
    /// https://github.com/gate/gateapi-csharp/blob/44299691284ddace333b9c259c88b1d3f34336bf/docs/AnnouncementApi.md
    /// </summary>
    /// <remarks>
    /// The official schema defines a business code but no successful value. Success reports HTTP/deserialization
    /// only; inspect Data.Code rather than assuming zero or discarding the envelope. All filters are optional;
    /// an empty request sends {} and retains server defaults. No request or response strings are normalized.
    /// </remarks>
    /// <param name="request">Required body with optional filters.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>All schema-required article and pagination fields, with raw time strings and Int64 identities.</returns>
    public async Task<RestCallResult<GateAnnouncementArticleListResponse>> GetArticlesAsync(
        GateAnnouncementArticleListRequest request, CancellationToken ct = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        if (request.CategoryLevel != null && request.CategoryLevel != "1" && request.CategoryLevel != "2")
            throw new ArgumentException("Category level must be 1 or 2", nameof(request.CategoryLevel));
        if (request.Pinned.HasValue && request.Pinned != 0 && request.Pinned != 1)
            throw new ArgumentOutOfRangeException(nameof(request.Pinned));
        if (request.FilterEmptyContent.HasValue && request.FilterEmptyContent != 0 && request.FilterEmptyContent != 1)
            throw new ArgumentOutOfRangeException(nameof(request.FilterEmptyContent));

        var body = new ParameterCollection();
        // A body is required even when every filter is omitted; SetBody also preserves {}.
        body.SetBody(request);

        var serializer = JsonSerializer.Create(new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });
        var result = await root.SendRequestInternal<JToken>(
            root.GetUrl("api", "4", "ann", "list_article"), HttpMethod.Post, ct,
            signed: false, bodyParameters: body, deserializer: serializer).ConfigureAwait(false);
        if (!result.Success)
            return result.Error is DeserializeError
                ? result.AsError<GateAnnouncementArticleListResponse>(new DeserializeError("Invalid or incomplete announcement envelope", null))
                : result.As<GateAnnouncementArticleListResponse>(null);
        try
        {
            var token = result.Data;
            // RawResponse's dependency path parses date-looking strings before the supplied serializer.
            if (!string.IsNullOrEmpty(result.Raw))
            {
                using var reader = new JsonTextReader(new System.IO.StringReader(result.Raw)) { DateParseHandling = DateParseHandling.None };
                token = serializer.Deserialize<JToken>(reader);
            }
            var response = token?.ToObject<GateAnnouncementArticleListResponse>(serializer);
            if (response != null) return result.As(response);
        }
        catch (JsonException) { }
        return result.AsError<GateAnnouncementArticleListResponse>(new DeserializeError("Invalid or incomplete announcement envelope", null));
    }
}

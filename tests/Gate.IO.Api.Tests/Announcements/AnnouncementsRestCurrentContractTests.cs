using Gate.IO.Api.Announcements;
using Gate.IO.Api.Tests.Infrastructure;
using System.Net;
using System.Text;

namespace Gate.IO.Api.Tests.Announcements;

[Trait("Category", "Contract")]
public class AnnouncementsRestCurrentContractTests
{
    // Schema-authored offline data, NOT a production capture or an official response example.
    // Contract: gate/gateapi-csharp @ 44299691284ddace333b9c259c88b1d3f34336bf, openapi.yaml.
    private const string ResponseJson = """
        {"code":17,"message":"2026-10-02T12:34:56.1234567+03:00","version":"future-version",
         "data":{"total":34,"list":[{"id":9007199254740993,"title":"Duyuru / 公告",
          "brief":"<a href='https://example.invalid/article'>raw text</a>",
          "created":"2026-09-17T01:02:03.1234567+03:00","updated":"1770000000.123456789",
          "release_time":"unknown time format","views":25,"author":"Author","author_id":9223372036854775807,
          "tags":"new,tag","cate":"new-category","is_top":9,"cate_id":2147483648,"source":"new-source",
          "content_lang_list":[{"code":"tr-TR","name":"Türkçe"},{"code":"jp","name":"日本語"}]}]}}
        """;

    private static readonly string[] RequestFields =
    ["title_query", "page", "size", "tags", "timer", "cate_name", "cate_level", "sub_website_id", "pinned", "update_after", "lang", "filter_empty_content"];

    private static readonly string[] ArticleFields =
    ["id", "title", "brief", "created", "updated", "release_time", "views", "author", "author_id", "tags", "cate", "is_top", "cate_id", "source", "content_lang_list"];

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task Full_JSON_body_is_public_and_matches_saved_and_convenience_requests(bool credentials)
    {
        var handler = Handler(ResponseJson);
        using var client = Client(handler, credentials: credentials);
        var input = Request();
        var saved = JsonConvert.SerializeObject(input);
        var copy = JsonConvert.DeserializeObject<GateAnnouncementArticleListRequest>(saved)!;
        Assert.Equal(input, copy);
        var body = JObject.Parse(saved);
        AssertFields(body, RequestFields);
        Assert.Equal(JTokenType.String, body["page"]!.Type);
        Assert.Equal(JTokenType.String, body["size"]!.Type);
        Assert.Equal(JTokenType.Integer, body["pinned"]!.Type);
        Assert.Equal(JTokenType.Integer, body["update_after"]!.Type);
        Assert.Equal(JTokenType.Integer, body["filter_empty_content"]!.Type);

        Assert.True((await client.Announcements.GetArticlesAsync(copy)).Success);
        Assert.True((await client.Announcements.GetArticlesAsync(input.TitleQuery, input.Page, input.Size, input.Tags,
            input.Timer, input.CategoryName, input.CategoryLevel, input.SubWebsiteId, input.Pinned,
            input.UpdateAfter, input.Language, input.FilterEmptyContent)).Success);
        Assert.Equal(2, handler.Requests.Count);
        foreach (var wire in handler.Requests)
        {
            Assert.Equal(HttpMethod.Post, wire.Method);
            Assert.Equal("api.gateio.ws", wire.RequestUri.Host);
            Assert.Equal("/api/v4/ann/list_article", wire.RequestUri.AbsolutePath);
            Assert.Empty(wire.RequestUri.Query);
            Assert.True(JToken.DeepEquals(body, JObject.Parse(wire.Content)));
            Assert.Contains("application/json", Assert.Single(wire.Headers["Content-Type"]));
            Assert.False(wire.Headers.ContainsKey("KEY"));
            Assert.False(wire.Headers.ContainsKey("SIGN"));
            Assert.False(wire.Headers.ContainsKey("Timestamp"));
        }
    }

    [Fact]
    public async Task No_filters_sends_required_empty_JSON_body_and_does_not_inject_defaults()
    {
        var handler = Handler(ResponseJson);
        using var client = Client(handler);
        Assert.Equal("{}", JsonConvert.SerializeObject(new GateAnnouncementArticleListRequest()));
        Assert.True((await client.Announcements.GetArticlesAsync()).Success);
        Assert.Equal("{}", Assert.Single(handler.Requests).Content);
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData("1", null)]
    [InlineData(null, "5")]
    [InlineData("0001", "0005")]
    public async Task Pagination_is_independently_optional_and_never_normalized(string? page, string? size)
    {
        var handler = Handler(ResponseJson);
        using var client = Client(handler);
        await client.Announcements.GetArticlesAsync(page: page, size: size);
        var body = JObject.Parse(Assert.Single(handler.Requests).Content);
        Assert.Equal(page, (string?)body["page"]);
        Assert.Equal(size, (string?)body["size"]);
        Assert.Equal((page == null ? 0 : 1) + (size == null ? 0 : 1), body.Count);
        if (page != null) Assert.Equal(JTokenType.String, body["page"]!.Type);
        if (size != null) Assert.Equal(JTokenType.String, body["size"]!.Type);
    }

    [Theory]
    [InlineData("1", 0, 0)]
    [InlineData("2", 1, 1)]
    public async Task Flags_and_category_levels_use_the_documented_tokens_without_stream_enum_restrictions(string level, int pinned, int empty)
    {
        var handler = Handler(ResponseJson);
        using var client = Client(handler);
        await client.Announcements.GetArticlesAsync(categoryLevel: level, pinned: pinned,
            filterEmptyContent: empty, language: "tr-TR", subWebsiteId: "177", updateAfter: 0);
        var body = JObject.Parse(Assert.Single(handler.Requests).Content);
        Assert.Equal(pinned, (int)body["pinned"]!);
        Assert.Equal(empty, (int)body["filter_empty_content"]!);
        Assert.Equal(0, (int)body["update_after"]!);
        Assert.Equal("177", (string)body["sub_website_id"]!);
        Assert.Equal("tr-TR", (string)body["lang"]!);
    }

    [Theory]
    [InlineData("category", "0")]
    [InlineData("category", " 1")]
    [InlineData("pinned", "-1")]
    [InlineData("pinned", "2")]
    [InlineData("empty", "-1")]
    [InlineData("empty", "2")]
    [InlineData("null", "")]
    public async Task Invalid_explicit_flags_and_null_body_fail_before_HTTP(string field, string value)
    {
        var handler = Handler(ResponseJson);
        using var client = Client(handler);
        var input = new GateAnnouncementArticleListRequest();
        if (field == "category") input.CategoryLevel = value;
        if (field == "pinned") input.Pinned = int.Parse(value);
        if (field == "empty") input.FilterEmptyContent = int.Parse(value);
        await Assert.ThrowsAnyAsync<ArgumentException>(() => client.Announcements.GetArticlesAsync(field == "null" ? null! : input));
        Assert.Empty(handler.Requests);
    }

    public static IEnumerable<object[]> InvalidSavedFilterTokens()
    {
        foreach (var field in RequestFields)
        {
            var integer = field is "pinned" or "update_after" or "filter_empty_content";
            foreach (var invalid in integer ? new[] { "\"1\"", "1.5", "true", "2147483648" } : new[] { "1", "true", "[]", "{}" })
                yield return [field, invalid];
        }
    }

    [Theory]
    [MemberData(nameof(InvalidSavedFilterTokens))]
    public void Saved_filters_cannot_be_coerced_into_a_different_request(string field, string value)
    {
        var json = JObject.FromObject(Request());
        json[field] = JToken.Parse(value);
        Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject<GateAnnouncementArticleListRequest>(json.ToString()));
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task Complete_response_keeps_business_envelope_exact_ids_raw_strings_and_unknown_values(bool raw)
    {
        var handler = Handler(ResponseJson);
        using var client = Client(handler, raw);
        var result = await client.Announcements.GetArticlesAsync(size: "5");
        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal(HttpStatusCode.OK, result.Response!.StatusCode);
        if (raw) Assert.Equal(ResponseJson, result.Raw);
        var data = result.Data;
        Assert.Equal(17, data.Code); // No successful business code is specified; do not invent one.
        Assert.Equal("2026-10-02T12:34:56.1234567+03:00", data.Message);
        Assert.Equal("future-version", data.Version);
        Assert.Equal(34, data.Data.Total);
        var article = Assert.Single(data.Data.List);
        Assert.Equal(9007199254740993L, article.Id);
        Assert.Equal(long.MaxValue, article.AuthorId);
        Assert.Equal(2147483648L, article.CategoryId);
        Assert.Equal("Duyuru / 公告", article.Title);
        Assert.Equal("<a href='https://example.invalid/article'>raw text</a>", article.Brief);
        Assert.Equal("2026-09-17T01:02:03.1234567+03:00", article.Created);
        Assert.Equal("1770000000.123456789", article.Updated);
        Assert.Equal("unknown time format", article.ReleaseTime);
        Assert.Equal(25, article.Views);
        Assert.Equal("Author", article.Author);
        Assert.Equal("new,tag", article.Tags);
        Assert.Equal("new-category", article.Category);
        Assert.Equal(9, article.IsTop);
        Assert.Equal("new-source", article.Source);
        Assert.Equal(new[] { "tr-TR", "jp" }, article.ContentLanguages.Select(x => x.Code));
        Assert.Equal(new[] { "Türkçe", "日本語" }, article.ContentLanguages.Select(x => x.Name));
        Assert.Single(handler.Requests); // No automatic paging or following links in the brief/source.

        var saved = JsonConvert.SerializeObject(data);
        Assert.True(JToken.DeepEquals(JObject.Parse(ResponseJson), JObject.Parse(saved)));
        var copy = JsonConvert.DeserializeObject<GateAnnouncementArticleListResponse>(saved)!;
        Assert.Equal(data.Message, copy.Message);
        Assert.Equal(article.Created, Assert.Single(copy.Data.List).Created);
        Assert.True(JToken.DeepEquals(JObject.Parse(saved), JObject.Parse(JsonConvert.SerializeObject(copy))));
        AssertFields(JObject.Parse(saved), ["code", "data", "message", "version"]);
        AssertFields(JObject.Parse(saved)["data"]!, ["list", "total"]);
        AssertFields(JObject.Parse(saved)["data"]!["list"]![0]!, ArticleFields);
        AssertFields(JObject.Parse(saved)["data"]!["list"]![0]!["content_lang_list"]![0]!, ["code", "name"]);
    }

    [Fact]
    public void All_individual_saved_models_preserve_date_looking_strings()
    {
        var article = JsonConvert.DeserializeObject<GateAnnouncementArticle>(JObject.Parse(ResponseJson)["data"]!["list"]![0]!.ToString())!;
        Assert.Equal("2026-09-17T01:02:03.1234567+03:00", article.Created);
        var language = JsonConvert.DeserializeObject<GateAnnouncementArticleLanguage>("{\"code\":\"2026-10-02T00:00:00+03:00\",\"name\":\"2026-10-02T00:00:00+03:00\"}")!;
        Assert.Equal("2026-10-02T00:00:00+03:00", language.Code);
        Assert.Equal(language.Code, language.Name);
        var request = Request();
        request.TitleQuery = request.Tags = request.Timer = request.CategoryName = request.Language = request.SubWebsiteId
            = request.Page = request.Size = "2026-10-02T00:00:00+03:00";
        Assert.Equal(request, JsonConvert.DeserializeObject<GateAnnouncementArticleListRequest>(JsonConvert.SerializeObject(request)));
    }

    public static IEnumerable<object[]> RequiredResponseFields()
    {
        foreach (var path in new[] { "code", "data", "message", "version", "data.list", "data.total" }
            .Concat(ArticleFields.Select(x => "data.list[0]." + x))
            .Concat(new[] { "data.list[0].content_lang_list[0].code", "data.list[0].content_lang_list[0].name" }))
            foreach (var raw in new[] { false, true }) yield return [path, raw];
    }

    [Theory]
    [MemberData(nameof(RequiredResponseFields))]
    public async Task Every_required_field_must_be_present_and_non_null(string path, bool raw)
    {
        foreach (var omit in new[] { false, true })
        {
            var json = JObject.Parse(ResponseJson);
            var property = (JProperty)json.SelectToken(path)!.Parent!;
            if (omit) property.Remove(); else property.Value = JValue.CreateNull();
            await Failed(json.ToString(), raw);
            Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject<GateAnnouncementArticleListResponse>(json.ToString()));
        }
    }

    public static IEnumerable<object[]> InvalidResponseTokens()
    {
        foreach (var path in new[] { "code", "data.total", "data.list[0].id", "data.list[0].views", "data.list[0].author_id", "data.list[0].is_top", "data.list[0].cate_id" })
            foreach (var value in new[] { "\"1\"", "1.5", "true", path.EndsWith(".id") || path.EndsWith("_id") ? "9223372036854775808" : "2147483648" })
                yield return [path, value];
        foreach (var path in new[] { "message", "version" }.Concat(ArticleFields.Except(["id", "views", "author_id", "is_top", "cate_id", "content_lang_list"]).Select(x => "data.list[0]." + x))
            .Concat(new[] { "data.list[0].content_lang_list[0].code", "data.list[0].content_lang_list[0].name" }))
            foreach (var value in new[] { "123", "true" }) yield return [path, value];
        foreach (var pair in new[] { ("data", "[]"), ("data.list", "{}"), ("data.list[0]", "null"), ("data.list[0]", "[]"),
            ("data.list[0].content_lang_list", "{}"), ("data.list[0].content_lang_list[0]", "null"), ("data.list[0].content_lang_list[0]", "[]") })
            yield return [pair.Item1, pair.Item2];
    }

    [Theory]
    [MemberData(nameof(InvalidResponseTokens))]
    public async Task Response_tokens_cannot_be_coerced_rounded_or_replaced_with_default_values(string path, string value)
    {
        var json = JObject.Parse(ResponseJson);
        if (json.SelectToken(path)!.Parent is JProperty property) property.Value = JToken.Parse(value);
        else if (json.SelectToken(path)!.Parent is JArray array) array[0] = JToken.Parse(value);
        await Failed(json.ToString());
        Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject<GateAnnouncementArticleListResponse>(json.ToString()));
    }

    [Theory]
    [InlineData(0L)]
    [InlineData(long.MinValue)]
    [InlineData(long.MaxValue)]
    public async Task All_identity_fields_remain_exact_Int64_numeric_tokens(long identity)
    {
        var json = JObject.Parse(ResponseJson);
        var article = json["data"]!["list"]![0]!;
        foreach (var field in new[] { "id", "author_id", "cate_id" }) article[field] = identity;
        var handler = Handler(json.ToString());
        using var client = Client(handler);
        var result = await client.Announcements.GetArticlesAsync();
        Assert.True(result.Success, result.Error?.ToString());
        var item = Assert.Single(result.Data.Data.List);
        Assert.Equal(identity, item.Id);
        Assert.Equal(identity, item.AuthorId);
        Assert.Equal(identity, item.CategoryId);
        Assert.Equal(JTokenType.Integer, JObject.FromObject(item)["id"]!.Type);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task Empty_article_and_language_arrays_are_valid_without_fabricating_records(bool raw)
    {
        var json = JObject.Parse(ResponseJson);
        json["data"]!["list"]![0]!["content_lang_list"] = new JArray();
        var handler = Handler(json.ToString());
        using (var client = Client(handler, raw))
        {
            var result = await client.Announcements.GetArticlesAsync();
            Assert.True(result.Success, result.Error?.ToString());
            Assert.Empty(Assert.Single(result.Data.Data.List).ContentLanguages);
        }
        json["data"]!["list"] = new JArray();
        json["data"]!["total"] = 0;
        handler = Handler(json.ToString());
        using var emptyClient = Client(handler, raw);
        var empty = await emptyClient.Announcements.GetArticlesAsync();
        Assert.True(empty.Success, empty.Error?.ToString());
        Assert.Empty(empty.Data.Data.List);
        Assert.Equal(0, empty.Data.Data.Total);
    }

    [Theory]
    [InlineData("")]
    [InlineData("null")]
    [InlineData("[]")]
    [InlineData("{}")]
    [InlineData("{\"code\":0,\"data\":null,\"message\":\"ok\",\"version\":\"1\"}")]
    [InlineData("{\"code\":0")]
    public async Task Empty_or_malformed_HTTP_success_is_not_a_valid_article_page(string json)
    {
        await Failed(json);
        await Failed(json, raw: true);
    }

    [Theory]
    [InlineData(400)]
    [InlineData(429)]
    [InlineData(500)]
    public async Task HTTP_errors_keep_status_raw_and_label_without_retrying(int status)
    {
        const string json = "{\"label\":\"INVALID_ARGUMENT\",\"message\":\"server detail\"}";
        var handler = Handler(json, (HttpStatusCode)status);
        using var client = Client(handler, raw: true);
        var result = await client.Announcements.GetArticlesAsync();
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal((HttpStatusCode)status, result.Response!.StatusCode);
        Assert.Equal(json, result.Raw);
        Assert.Equal("INVALID_ARGUMENT", result.Error!.Data);
        Assert.Equal("server detail", result.Error.Message);
        Assert.Single(handler.Requests);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(false, true)]
    [InlineData(true, false)]
    [InlineData(true, true)]
    public async Task Parser_errors_are_payload_free_while_explicit_raw_response_is_retained(bool malformed, bool raw)
    {
        const string secret = "announcement-private-sentinel";
        var json = malformed ? "{\"private\":\"announcement-private-sentinel\","
            : "{\"code\":{\"private\":\"announcement-private-sentinel\"},\"data\":{\"list\":[],\"total\":0},\"message\":\"ok\",\"version\":\"1\"}";
        var handler = Handler(json);
        using var client = Client(handler, raw);
        var result = await client.Announcements.GetArticlesAsync();
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal(HttpStatusCode.OK, result.Response!.StatusCode);
        Assert.NotNull(result.Error);
        Assert.Null(result.Error.Data);
        Assert.DoesNotContain(secret, result.Error.ToString());
        if (raw) Assert.Equal(json, result.Raw);
        Assert.Single(handler.Requests);
    }

    private static GateAnnouncementArticleListRequest Request() => new()
    {
        TitleQuery = "  duyuru / 上线  ", Page = "1", Size = "5", Tags = "new,tag", Timer = "10",
        CategoryName = "new-category", CategoryLevel = "2", SubWebsiteId = "177", Pinned = 0,
        UpdateAfter = 1770000000, Language = "tr-TR", FilterEmptyContent = 0,
    };

    private static void AssertFields(JToken json, IEnumerable<string> fields)
        => Assert.Equal(fields.OrderBy(x => x), ((JObject)json).Properties().Select(x => x.Name).OrderBy(x => x));

    private static async Task Failed(string json, bool raw = false)
    {
        var handler = Handler(json);
        using var client = Client(handler, raw);
        var result = await client.Announcements.GetArticlesAsync();
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.NotNull(result.Error);
        Assert.Equal(HttpStatusCode.OK, result.Response!.StatusCode);
        if (raw) Assert.Equal(json, result.Raw);
        Assert.Single(handler.Requests);
    }

    private static RecordingHttpMessageHandler Handler(string json, HttpStatusCode status = HttpStatusCode.OK)
        => new(_ => new HttpResponseMessage(status) { Content = new StringContent(json, Encoding.UTF8, "application/json") });

    private static GateRestApiClient Client(RecordingHttpMessageHandler handler, bool raw = false, bool credentials = false)
    {
        var client = new GateRestApiClient(new GateRestApiClientOptions { HttpClient = new HttpClient(handler), RawResponse = raw });
        if (credentials) client.SetApiCredentials("dummy-api-key", "dummy-api-secret");
        return client;
    }
}

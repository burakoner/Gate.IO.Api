using ApiSharp.WebSocket;
using Gate.IO.Api.Base;
using Gate.IO.Api.Tests.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Gate.IO.Api.Tests;

[Trait("Category", "Unit")]
public class LoggingTests
{
    [Fact]
    public async Task Rest_shared_request_path_logs_started_and_succeeded_messages()
    {
        var logger = new TestLogger();
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse("[]"));
        var client = CreateRestClient(logger, handler);

        var result = await client.Alpha.GetCurrenciesAsync(limit: 1);

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Contains(logger.Entries, x => x.Level == LogLevel.Debug && x.Message.Contains("Gate REST request started"));
        Assert.Contains(logger.Entries, x => x.Level == LogLevel.Debug && x.Message.Contains("Gate REST request succeeded"));
        Assert.Contains(logger.Entries, x => x.Message.Contains("/api/v4/alpha/currencies"));
    }

    [Fact]
    public async Task Rest_shared_request_path_logs_failed_messages()
    {
        var logger = new TestLogger();
        var handler = new RecordingHttpMessageHandler(_ => JsonResponse(
            """{"label":"INVALID_PARAM_VALUE","message":"Invalid limit"}""",
            System.Net.HttpStatusCode.BadRequest));
        var client = CreateRestClient(logger, handler);

        var result = await client.Alpha.GetCurrenciesAsync(limit: -1);

        Assert.False(result.Success);
        Assert.Contains(logger.Entries, x => x.Level == LogLevel.Debug && x.Message.Contains("Gate REST request started"));
        Assert.Contains(logger.Entries, x => x.Level == LogLevel.Warning && x.Message.Contains("Gate REST request failed"));
        Assert.Contains(logger.Entries, x => x.Message.Contains("/api/v4/alpha/currencies"));
    }

    [Fact]
    public async Task WebSocket_shared_unsubscribe_path_logs_started_and_disconnected_skip_messages()
    {
        var logger = new TestLogger();
        var stream = new GateWebSocketClient(logger);
        var socket = new WebSocketClient(logger, new WebSocketParameters(new Uri("wss://example.test/ws"), false));
        var connection = new WebSocketConnection(logger, stream.Base, socket, "test");
        var subscription = WebSocketSubscription.CreateForRequest(
            1,
            new GateStreamRequest
            {
                Id = 1,
                Channel = "spot.trades",
                Event = StreamRequestEvent.Subscribe,
                Payload = new[] { "BTC_USDT" },
            },
            userSubscription: true,
            authenticated: false,
            dataHandler: _ => { });

        var result = await stream.Base.BaseUnsubscribeAsync(connection, subscription);

        Assert.True(result);
        Assert.Contains(logger.Entries, x => x.Level == LogLevel.Debug && x.Message.Contains("Gate WebSocket unsubscribe started"));
        Assert.Contains(logger.Entries, x => x.Level == LogLevel.Debug && x.Message.Contains("unsubscribe skipped because socket is disconnected"));
        Assert.Contains(logger.Entries, x => x.Message.Contains("spot.trades"));
    }

    private static GateRestApiClient CreateRestClient(TestLogger logger, RecordingHttpMessageHandler handler)
        => new(logger, new GateRestApiClientOptions
        {
            HttpClient = new HttpClient(handler),
        });

    private static HttpResponseMessage JsonResponse(string json, System.Net.HttpStatusCode statusCode = System.Net.HttpStatusCode.OK)
        => new(statusCode)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json"),
        };
}

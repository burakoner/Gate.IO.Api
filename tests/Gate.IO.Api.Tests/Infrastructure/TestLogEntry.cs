using Microsoft.Extensions.Logging;

namespace Gate.IO.Api.Tests.Infrastructure;

internal sealed record TestLogEntry(LogLevel Level, EventId EventId, string Message, Exception? Exception);

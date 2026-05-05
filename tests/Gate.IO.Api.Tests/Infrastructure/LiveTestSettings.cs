namespace Gate.IO.Api.Tests.Infrastructure;

internal static class LiveTestSettings
{
    public static bool Enabled
        => string.Equals(Environment.GetEnvironmentVariable("GATEIO_RUN_LIVE_TESTS"), "1", StringComparison.OrdinalIgnoreCase)
        || string.Equals(Environment.GetEnvironmentVariable("GATEIO_RUN_LIVE_TESTS"), "true", StringComparison.OrdinalIgnoreCase);

    public static void SkipIfDisabled()
    {
        if (!Enabled)
            return;
    }
}

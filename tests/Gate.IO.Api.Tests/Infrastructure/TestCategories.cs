namespace Gate.IO.Api.Tests.Infrastructure;

internal static class TestCategories
{
    public const string Contract = "Contract";
    public const string LiveCapture = "LiveCapture";
    public const string LiveWebSocket = "LiveWebSocket";
    public const string PublicIntegration = "PublicIntegration";
    public const string RequiresCredentials = "RequiresCredentials";
    public const string Unit = "Unit";

    public static readonly IReadOnlySet<string> All = new HashSet<string>(StringComparer.Ordinal)
    {
        Contract,
        LiveCapture,
        LiveWebSocket,
        PublicIntegration,
        RequiresCredentials,
        Unit,
    };
}

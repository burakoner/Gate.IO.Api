namespace Gate.IO.Api.Tests.Infrastructure;

internal static class LiveCaptureSettings
{
    private const string CaptureEnabledVariable = "GATEIO_CAPTURE_PUBLIC_FIXTURES";
    private const string CaptureFilterVariable = "GATEIO_CAPTURE_PUBLIC_FIXTURE_FILTER";
    private const string CaptureRootVariable = "GATEIO_CAPTURE_FIXTURE_ROOT";

    public static bool Enabled
        => IsEnabled(Environment.GetEnvironmentVariable(CaptureEnabledVariable));

    public static string FixtureRoot
    {
        get
        {
            var configuredRoot = Environment.GetEnvironmentVariable(CaptureRootVariable);
            var root = string.IsNullOrWhiteSpace(configuredRoot)
                ? Path.Combine(FindRepositoryRoot(), "tests", "Gate.IO.Api.Tests", "Fixtures", "Live")
                : configuredRoot;

            return Path.GetFullPath(root);
        }
    }

    public static bool MatchesFilter(PublicEndpointCatalogEntry entry)
    {
        var filter = Environment.GetEnvironmentVariable(CaptureFilterVariable);
        if (string.IsNullOrWhiteSpace(filter))
            return true;

        var parts = filter.Split([';', ','], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        return parts.Any(part =>
            entry.Module.Contains(part, StringComparison.OrdinalIgnoreCase)
            || entry.Name.Contains(part, StringComparison.OrdinalIgnoreCase)
            || entry.PathAndQuery.Contains(part, StringComparison.OrdinalIgnoreCase)
            || entry.CapturePath.Contains(part, StringComparison.OrdinalIgnoreCase)
            || (entry.LiveFixturePath?.Contains(part, StringComparison.OrdinalIgnoreCase) ?? false));
    }

    public static string GetFixturePath(PublicEndpointCatalogEntry entry)
    {
        if (string.IsNullOrWhiteSpace(entry.LiveFixturePath))
            throw new InvalidOperationException($"{entry.Module} {entry.Name} does not have a live fixture path.");

        var root = FixtureRoot;
        var relativePath = entry.LiveFixturePath.Replace('/', Path.DirectorySeparatorChar);
        var fixturePath = Path.GetFullPath(Path.Combine(root, relativePath));
        var rootWithSeparator = root.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) + Path.DirectorySeparatorChar;
        var comparison = OperatingSystem.IsWindows() ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

        if (!fixturePath.StartsWith(rootWithSeparator, comparison))
            throw new InvalidOperationException($"Fixture path escapes the live fixture root: {entry.LiveFixturePath}");

        return fixturePath;
    }

    private static bool IsEnabled(string? value)
        => string.Equals(value, "1", StringComparison.OrdinalIgnoreCase)
        || string.Equals(value, "true", StringComparison.OrdinalIgnoreCase);

    private static string FindRepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory != null)
        {
            if (File.Exists(Path.Combine(directory.FullName, "Gate.IO Api Client.sln")))
                return directory.FullName;

            directory = directory.Parent;
        }

        return Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
    }
}

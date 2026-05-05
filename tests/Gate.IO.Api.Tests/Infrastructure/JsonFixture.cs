namespace Gate.IO.Api.Tests.Infrastructure;

internal static class JsonFixture
{
    public static string Read(string relativePath)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Fixtures", relativePath.Replace('/', Path.DirectorySeparatorChar));
        if (!File.Exists(path))
            throw new FileNotFoundException($"Fixture file was not found: {path}", path);

        return File.ReadAllText(path);
    }

    public static T Deserialize<T>(string relativePath)
    {
        var value = JsonConvert.DeserializeObject<T>(Read(relativePath));
        Assert.NotNull(value);
        return value!;
    }

    public static JToken Parse(string relativePath)
        => JToken.Parse(Read(relativePath));
}

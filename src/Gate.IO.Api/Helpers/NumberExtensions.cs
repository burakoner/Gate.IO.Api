namespace Gate.IO.Api.Helpers;

internal static class NumberExtensions
{
    public static CultureInfo CI { get; } = CultureInfo.InvariantCulture;

    public static string ToGateString(this decimal @this)
    {
        return @this.ToString(CI);
    }
}

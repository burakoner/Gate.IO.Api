namespace Gate.IO.Api.Futures;

// Futures REST filters only. Preserve the legacy Unspecified-as-UTC interpretation and second rounding.
internal static class GateFuturesRequestTime
{
    internal static long? Seconds(DateTime? time)
        => time.HasValue ? (time.Value.Kind == DateTimeKind.Local ? time.Value.ToUniversalTime() : time.Value).ConvertToSeconds() : null;
}

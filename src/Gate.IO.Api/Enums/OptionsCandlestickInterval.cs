namespace Gate.IO.Api.Enums;

public enum OptionsCandlestickInterval
{
    [EnumMember(Value = "1m")]
    OneMinute = 60,

    [EnumMember(Value = "5m")]
    FiveMinutes = 300,

    [EnumMember(Value = "15m")]
    FifteenMinutes = 900,

    [EnumMember(Value = "30m")]
    ThirtyMinutes = 1800,

    [EnumMember(Value = "1h")]
    OneHour = 3600,
}
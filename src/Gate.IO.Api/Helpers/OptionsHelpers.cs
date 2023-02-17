using System.Text.RegularExpressions;

namespace Gate.IO.Api.Helpers;

internal static class OptionsHelpers
{
    public static void ValidateUnderlyingSymbol(string symbol)
    {
        if (string.IsNullOrEmpty(symbol))
            throw new ArgumentException("Symbol is not provided");

        if (!Regex.IsMatch(symbol, "^([A-Z|0-9]{2,})_([A-Z|0-9]{2,})$"))
            throw new ArgumentException($"{symbol} is not a valid symbol. Should be [BaseAsset]_[QuoteAsset], e.g. BTC_USDT");
    }

    public static void ValidateContractSymbol(string symbol)
    {
        if (string.IsNullOrEmpty(symbol))
            throw new ArgumentException("Symbol is not provided");

        if (!Regex.IsMatch(symbol, "^([A-Z|0-9]{2,})_([A-Z|0-9]{2,})-([0-9]{8})-([0-9])-([C|P])$"))
            throw new ArgumentException($"{symbol} is not a valid symbol. Should be [BaseAsset]_[QuoteAsset]-[Date]-[Number]-[C|P], e.g. BTC_USDT-20230310-30000-P");
    }
}

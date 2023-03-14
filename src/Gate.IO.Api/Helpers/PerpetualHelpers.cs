namespace Gate.IO.Api.Helpers;

internal static class PerpetualHelpers
{
    public static void ValidateContractSymbol(string symbol)
    {
        if (string.IsNullOrEmpty(symbol))
            throw new ArgumentException("Symbol is not provided");

        if (!Regex.IsMatch(symbol, "^([A-Z|0-9]{2,})_([A-Z|0-9]{2,})$"))
            throw new ArgumentException($"{symbol} is not a valid symbol. Should be [BaseAsset]_[QuoteAsset], e.g. BTC_USDT");
    }
}

namespace Gate.IO.Api.Helpers;

internal static class DeliveryHelpers
{
    public static void ValidateContractSymbol(string symbol)
    {
        if (string.IsNullOrEmpty(symbol))
            throw new ArgumentException("Symbol is not provided");

        if (!Regex.IsMatch(symbol, "^([A-Z|0-9]{2,})_([A-Z|0-9]{2,})_([0-9]{8})$"))
            throw new ArgumentException($"{symbol} is not a valid symbol. Should be [BaseAsset]_[QuoteAsset]_[ExpireDate], e.g. BTC_USDT_202303311");
    }
}

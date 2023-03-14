namespace Gate.IO.Api.Helpers;

internal static class ExchangeHelpers
{
    public static void ValidateAssetSymbol(string symbol)
    {
        if (string.IsNullOrEmpty(symbol))
            throw new ArgumentException("Symbol is not provided");

        if (!Regex.IsMatch(symbol, "^([A-Z|0-9]{2,})$"))
            throw new ArgumentException($"{symbol} is not a valid symbol.");
    }

    public static void ValidateClientOrderId(string coid, bool allowEmpty)
    {
        if (allowEmpty && string.IsNullOrEmpty(coid))
            return;

        if (!Regex.IsMatch(coid, "^(t-)([a-zA-Z0-9-_.]{0,28})$"))
            throw new ArgumentException($"{coid} is not a valid Client Order Id. Should be match ^(t-)([a-zA-Z0-9-_.]{{0,28}})$, e.g. t-abcdef_GHIJKL.123456");
    }

    /// <summary>
    /// Clamp a quantity between a min and max quantity and floor to the closest step
    /// </summary>
    /// <param name="minQuantity"></param>
    /// <param name="maxQuantity"></param>
    /// <param name="stepSize"></param>
    /// <param name="quantity"></param>
    /// <returns></returns>
    public static decimal ClampQuantity(decimal minQuantity, decimal maxQuantity, decimal stepSize, decimal quantity)
    {
        quantity = Math.Min(maxQuantity, quantity);
        quantity = Math.Max(minQuantity, quantity);
        if (stepSize == 0)
            return quantity;
        quantity -= quantity % stepSize;
        quantity = Floor(quantity);
        return quantity;
    }

    /// <summary>
    /// Clamp a price between a min and max price
    /// </summary>
    /// <param name="minPrice"></param>
    /// <param name="maxPrice"></param>
    /// <param name="price"></param>
    /// <returns></returns>
    public static decimal ClampPrice(decimal minPrice, decimal maxPrice, decimal price)
    {
        price = Math.Min(maxPrice, price);
        price = Math.Max(minPrice, price);
        return price;
    }

    /// <summary>
    /// Floor a price to the closest tick
    /// </summary>
    /// <param name="tickSize"></param>
    /// <param name="price"></param>
    /// <returns></returns>
    public static decimal FloorPrice(decimal tickSize, decimal price)
    {
        price -= price % tickSize;
        price = Floor(price);
        return price;
    }

    /// <summary>
    /// Floor
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static decimal Floor(decimal number)
    {
        return Math.Floor(number * 100000000) / 100000000;
    }
}

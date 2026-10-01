namespace Gate.IO.Api.Futures;

// Single standard-order preflight only. Do not infer account mode or rewrite trading instructions.
internal static class GateFuturesOrderValidation
{
    internal static void Create(GateFuturesOrderRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        GateFuturesPriceOrderValidation.Contract(request.Contract, required: true);
        ClientTag(request.ClientOrderId, allowEmpty: true);
        Defined(request.TimeInForce, nameof(request.TimeInForce));
        Defined(request.SelfTradeAction, nameof(request.SelfTradeAction));
        Defined(request.PositionMarginMode, nameof(request.PositionMarginMode));
        Defined(request.ActionMode, nameof(request.ActionMode));
        if (request.AutoSize.HasValue && request.AutoSize != GateFuturesOrderAutoSize.CloseLong
            && request.AutoSize != GateFuturesOrderAutoSize.CloseShort)
            throw new ArgumentException("Use close_long or close_short, or omit auto_size with null", nameof(request.AutoSize));
        if (request.Close == true && request.Size != 0)
            throw new ArgumentException("close=true requires size=0", nameof(request.Size));
        if (request.AutoSize.HasValue && (request.Size != 0 || request.ReduceOnly != true))
            throw new ArgumentException("Hedge-mode full close requires size=0 and reduce_only=true", nameof(request.AutoSize));
        if (request.Price == 0 && request.TimeInForce != GateFuturesTimeInForce.ImmediateOrCancel)
            throw new ArgumentException("Market orders require explicit tif=ioc", nameof(request.TimeInForce));
    }

    internal static string Identity(long? orderId, string clientOrderId)
    {
        if (orderId.HasValue == !string.IsNullOrEmpty(clientOrderId))
            throw new ArgumentException("Specify exactly one orderId or clientOrderId");
        if (orderId.HasValue)
        {
            GateFuturesPriceOrderValidation.OrderId(orderId.Value);
            return orderId.Value.ToString(CultureInfo.InvariantCulture);
        }
        ClientTag(clientOrderId, allowEmpty: false);
        return clientOrderId;
    }

    internal static void Defined<T>(T? value, string name) where T : struct, Enum
    {
        if (value.HasValue && !Enum.IsDefined(typeof(T), value.Value)) throw new ArgumentOutOfRangeException(name);
    }

    private static void ClientTag(string value, bool allowEmpty)
    {
        if (allowEmpty && string.IsNullOrEmpty(value)) return;
        if (value == null || !Regex.IsMatch(value, @"\At-[A-Za-z0-9_.-]{0,28}\z"))
            throw new ArgumentException("Use t- followed by at most 28 ASCII letters, digits, _, - or .", nameof(value));
    }
}

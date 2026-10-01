namespace Gate.IO.Api.Futures;

// Futures-only preflight rules. The shared DTOs are also used by Delivery clients.
internal static class GateFuturesPriceOrderValidation
{
    internal static void Create(GateFuturesPriceTriggeredOrderRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        if (request.Order == null) throw new ArgumentException("initial is required", nameof(request));
        if (request.Trigger == null) throw new ArgumentException("trigger is required", nameof(request));
        if (request is GateFuturesPriceTriggeredOrder)
            throw new ArgumentException("Create a request explicitly; response orders contain read-only fields", nameof(request));

        Contract(request.Order.Contract, required: true);
        NonBlank(request.Order.Price, nameof(request.Order.Price));
        NonBlank(request.Trigger.Price, nameof(request.Trigger.Price));
        OptionalNonBlank(request.Order.Amount, nameof(request.Order.Amount));
        Defined(request.Type, nameof(request.Type));
        if (request.Type == GateFuturesTriggerType.CloseLongOrder || request.Type == GateFuturesTriggerType.CloseShortOrder)
            throw new ArgumentException("Order take-profit/stop-loss types are read-only", nameof(request.Type));
        Defined(request.PositionMarginMode, nameof(request.PositionMarginMode));
        AutoSize(request.Order.AutoSize);
        if (request.Order.TimeInForce.HasValue && request.Order.TimeInForce != GateFuturesTimeInForce.GoodTillCancelled
            && request.Order.TimeInForce != GateFuturesTimeInForce.ImmediateOrCancel)
            throw new ArgumentException("Price-triggered orders support only gtc and ioc", nameof(request.Order.TimeInForce));

        // Detect exact textual zero without decimal/double rounding of a tiny nonzero price.
        if (Regex.IsMatch(request.Order.Price.Trim(), @"\A[+-]?(?:0+(?:\.0*)?|\.0+)(?:[eE][+-]?[0-9]+)?\z")
            && request.Order.TimeInForce != GateFuturesTimeInForce.ImmediateOrCancel)
            throw new ArgumentException("Market orders require explicit ioc; omitted tif defaults to gtc", nameof(request.Order.TimeInForce));
        if (request.Order.IsClose.HasValue || request.Order.IsReduceOnly.HasValue)
            throw new ArgumentException("is_close and is_reduce_only are read-only; use close and reduce_only", nameof(request.Order));
        if (request.Trigger.StrategyType.HasValue && request.Trigger.StrategyType != GateFuturesTriggerStrategy.ByPrice)
            throw new ArgumentException("The current Futures contract supports only strategy_type=0", nameof(request.Trigger.StrategyType));
        Defined(request.Trigger.PriceType, nameof(request.Trigger.PriceType));
        if (!Enum.IsDefined(typeof(GateSpotTriggerCondition), request.Trigger.Rule))
            throw new ArgumentOutOfRangeException(nameof(request.Trigger.Rule));
        if (request.Trigger.Expiration < 0) throw new ArgumentOutOfRangeException(nameof(request.Trigger.Expiration));
    }

    internal static void Amend(GateFuturesSettlement settlement, GateFuturesPriceTriggeredOrderUpdateRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        OrderId(request.OrderId);
        if (request.Settlement != null && !string.Equals(request.Settlement, MapConverter.GetString(settlement), StringComparison.Ordinal))
            throw new ArgumentException("Body settlement must match the selected Futures client", nameof(request.Settlement));
        OptionalNonBlank(request.Amount, nameof(request.Amount));
        OptionalNonBlank(request.Price, nameof(request.Price));
        OptionalNonBlank(request.TriggerPrice, nameof(request.TriggerPrice));
        Defined(request.PriceType, nameof(request.PriceType));
        AutoSize(request.AutoSize);
        // Existing tif and position mode are not known here. Do not fetch them or invent amendments.
    }

    internal static void Query(GateSpotTriggerFilter status, string contract, int limit, int offset)
    {
        if (!Enum.IsDefined(typeof(GateSpotTriggerFilter), status)) throw new ArgumentOutOfRangeException(nameof(status));
        Contract(contract);
        if (limit <= 0) throw new ArgumentOutOfRangeException(nameof(limit));
        if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
    }

    internal static void Contract(string contract, bool required = false)
    {
        if (contract == null && !required) return;
        NonBlank(contract, nameof(contract));
        // Do not inherit the shared helper's accidental '|' allowance or an undocumented minimum asset length.
        if (!Regex.IsMatch(contract, @"\A[A-Z0-9]+_[A-Z0-9]+\z"))
            throw new ArgumentException("Specify one literal BASE_QUOTE Futures contract without whitespace", nameof(contract));
    }

    internal static void OrderId(long orderId)
    {
        if (orderId <= 0) throw new ArgumentOutOfRangeException(nameof(orderId), "Specify an actual created order ID");
    }

    private static void NonBlank(string value, string name)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Value is required and cannot be blank", name);
    }

    private static void OptionalNonBlank(string value, string name)
    {
        if (value != null) NonBlank(value, name);
    }

    private static void Defined<T>(T? value, string name) where T : struct, Enum
    {
        if (value.HasValue && !Enum.IsDefined(typeof(T), value.Value)) throw new ArgumentOutOfRangeException(name);
    }

    private static void AutoSize(GateFuturesOrderAutoSize? value)
    {
        if (value.HasValue && value != GateFuturesOrderAutoSize.CloseLong && value != GateFuturesOrderAutoSize.CloseShort)
            throw new ArgumentException("Use close_long or close_short, or omit auto_size with null", nameof(value));
    }
}

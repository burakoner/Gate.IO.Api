namespace Gate.IO.Api.Futures;

// Endpoint-specific documented constraints; no account lookups or inferred trading instructions.
internal static class GateFuturesStrategyValidation
{
    internal static void Trail(GateFuturesTrailOrderRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        GateFuturesPriceOrderValidation.Contract(request.Contract, required: true);
        if (request.PriceType.HasValue && (request.PriceType < GateFuturesTrailPriceType.Latest || request.PriceType > GateFuturesTrailPriceType.Mark))
            throw new ArgumentOutOfRangeException(nameof(request.PriceType));
        if (request.PositionRelated == true && request.ReduceOnly != true)
            throw new ArgumentException("position_related=true requires reduce_only=true", nameof(request.ReduceOnly));
        if (!string.IsNullOrEmpty(request.ClientOrderId) && request.ClientOrderId != "apiv4")
            GateFuturesOrderValidation.ClientTag(request.ClientOrderId, allowEmpty: false);
        GateFuturesOrderValidation.Defined(request.PositionMarginMode, nameof(request.PositionMarginMode));
        PositionMode(request.PositionMode);
    }

    internal static void Chase(GateFuturesChaseOrderRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        Contract(request.Contract, required: true);
        var amount = Decimal(request.Amount, nameof(request.Amount));
        if (amount == 0) throw new ArgumentException("Chase amount cannot be zero", nameof(request.Amount));
        var priceLimit = Decimal(request.PriceLimit, nameof(request.PriceLimit));
        if (request.OffsetLimit != null)
        {
            Decimal(request.OffsetLimit, nameof(request.OffsetLimit));
            if (priceLimit != 0) throw new ArgumentException("offset_limit and a nonzero price_limit are mutually exclusive", nameof(request.OffsetLimit));
        }
        if (request.PriceGapValue != null) Decimal(request.PriceGapValue, nameof(request.PriceGapValue));
        GateFuturesOrderValidation.Defined(request.PriceType, nameof(request.PriceType));
        GateFuturesOrderValidation.Defined(request.PriceGapType, nameof(request.PriceGapType));
        GateFuturesOrderValidation.Defined(request.PositionMarginMode, nameof(request.PositionMarginMode));
        Settlement(request.Settlement);
        // Unlike trail orders, the chase contract does not specify a restricted custom-tag grammar.
    }

    private static decimal Decimal(string value, string name)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("A decimal string is required", name);
        try { return JsonConvert.DeserializeObject<decimal>(JsonConvert.SerializeObject(value), new GateFuturesOrderDecimalStringConverter()); }
        catch (JsonException exception) { throw new ArgumentException("Specify an exact, finite decimal string", name, exception); }
    }

    internal static void Contract(string contract, bool required = false)
    {
        if (contract == null && !required) return;
        // Chase explicitly accepts lower-case contract names and normalizes them on the server.
        if (contract == null || !Regex.IsMatch(contract, @"\A[A-Za-z0-9]+_[A-Za-z0-9]+\z"))
            throw new ArgumentException("Specify one literal BASE_QUOTE contract", nameof(contract));
    }

    internal static void Settlement(GateFuturesSettlement? value)
    {
        if (value.HasValue && value != GateFuturesSettlement.BTC && value != GateFuturesSettlement.USDT && value != GateFuturesSettlement.USD1)
            throw new ArgumentOutOfRangeException(nameof(value));
    }

    internal static void PositionMode(string value)
    {
        if (value != null && value != "single" && value != "dual" && value != "dual_plus")
            throw new ArgumentException("Use single, dual or dual_plus", nameof(value));
    }

    internal static void Range(long? from, long? to)
    {
        if (from.HasValue && to.HasValue && from > to) throw new ArgumentException("Start time cannot be after end time");
    }

    internal static void Page(int? number, int? size)
    {
        if (number <= 0) throw new ArgumentOutOfRangeException(nameof(number), "Page numbers start from 1");
        if (size <= 0) throw new ArgumentOutOfRangeException(nameof(size));
    }

    internal static void Filter(int? value, string name)
    {
        if (value.HasValue && value != 1 && value != 2) throw new ArgumentOutOfRangeException(name);
    }

    internal static void ChaseId(string value)
    {
        // Existing chase IDs are strings. Validate digits without changing their public type or imposing an Int64 bound.
        if (value == null || !Regex.IsMatch(value, @"\A[0-9]+\z") || value.All(c => c == '0'))
            throw new ArgumentException("Order ID must be a nonzero positive integer string", nameof(value));
    }
}

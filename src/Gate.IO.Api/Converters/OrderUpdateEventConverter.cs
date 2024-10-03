using Gate.IO.Api.Spot;

namespace Gate.IO.Api.Converters;

internal class OrderUpdateEventConverter : BaseConverter<GateSpotOrderUpdateEvent>
{
    public OrderUpdateEventConverter() : this(true) { }
    public OrderUpdateEventConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<GateSpotOrderUpdateEvent, string>> Mapping => new()
    {
        new KeyValuePair<GateSpotOrderUpdateEvent, string>(GateSpotOrderUpdateEvent.Put, "put"),
        new KeyValuePair<GateSpotOrderUpdateEvent, string>(GateSpotOrderUpdateEvent.Update, "update"),
        new KeyValuePair<GateSpotOrderUpdateEvent, string>(GateSpotOrderUpdateEvent.Finish, "finish"),
    };
}
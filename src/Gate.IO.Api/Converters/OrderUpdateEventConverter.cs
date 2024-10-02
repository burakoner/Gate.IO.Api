using Gate.IO.Api.Spot;

namespace Gate.IO.Api.Converters;

internal class OrderUpdateEventConverter : BaseConverter<SpotOrderUpdateEvent>
{
    public OrderUpdateEventConverter() : this(true) { }
    public OrderUpdateEventConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<SpotOrderUpdateEvent, string>> Mapping => new()
    {
        new KeyValuePair<SpotOrderUpdateEvent, string>(SpotOrderUpdateEvent.Put, "put"),
        new KeyValuePair<SpotOrderUpdateEvent, string>(SpotOrderUpdateEvent.Update, "update"),
        new KeyValuePair<SpotOrderUpdateEvent, string>(SpotOrderUpdateEvent.Finish, "finish"),
    };
}
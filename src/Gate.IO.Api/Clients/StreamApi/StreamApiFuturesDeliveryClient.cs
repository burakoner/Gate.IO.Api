using Gate.IO.Api.Models.StreamApi;

namespace Gate.IO.Api.Clients.StreamApi;

public class StreamApiFuturesDeliveryClient
{
    // Internal
    internal GateStreamClient RootClient { get; }
    internal StreamApiBaseClient BaseClient { get; }
    internal GateStreamClientOptions ClientOptions { get; }

    internal StreamApiFuturesDeliveryClient(GateStreamClient root)
    {
        RootClient = root;
        BaseClient = root.Base;
        ClientOptions = root.ClientOptions;
    }
    public async Task UnsubscribeAsync(int subscriptionId)
    => await BaseClient.UnsubscribeAsync(subscriptionId).ConfigureAwait(false);

    public async Task UnsubscribeAsync(UpdateSubscription subscription)
        => await BaseClient.UnsubscribeAsync(subscription).ConfigureAwait(false);

    public async Task UnsubscribeAllAsync()
        => await BaseClient.UnsubscribeAllAsync().ConfigureAwait(false);
}
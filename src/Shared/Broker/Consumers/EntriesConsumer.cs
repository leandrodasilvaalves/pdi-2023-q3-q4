using Shared.Extensions;
using Shared.Requests;

namespace Shared.Broker.Consumers
{
    public class EntriesConsumer : Consumer<CreateEntryRequest>
    {
        public EntriesConsumer(IServiceProvider provider)
            : base(provider, "bacen.entries") { }

        public override Task ConsumeAsync(CreateEntryRequest message, CancellationToken stoppingToken)
        {
            message.LogJson();
            return Task.CompletedTask;
        }
    }
}
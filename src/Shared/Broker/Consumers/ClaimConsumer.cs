using Shared.Contracts.Enums;
using Shared.Contracts.Repositories;
using Shared.Entities;
using Shared.Extensions;

namespace Shared.Broker.Consumers
{
    public class ClaimConsumer : Consumer<Claim>
    {
        private readonly IEntryRepository _repository;
        public ClaimConsumer(IServiceProvider provider, IEntryRepository repository) : base(provider, "bacen.claims")
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public override async Task ConsumeAsync(Claim message, CancellationToken stoppingToken)
        {
            var entry = await _repository.GetByAsync(message.AddressingKey);
            if(entry is not null)
            {
                entry.Status = EntryStatus.LOCKED;                
                await _repository.UpdateAsync(entry);
            }
            entry?.LogJson();
        }
    }
}
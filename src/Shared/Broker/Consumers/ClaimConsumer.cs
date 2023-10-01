using Microsoft.Extensions.Options;
using Shared.Contracts.Enums;
using Shared.Contracts.Models;
using Shared.Contracts.Repositories;
using Shared.Entities;

namespace Shared.Broker.Consumers
{
    public class ClaimConsumer : Consumer<Claim>
    {
        private readonly IEntryRepository _repository;
        private readonly IPublisher<AddressingKeyForAccountModel> _publisher;
        private readonly KafkaTopcis _options;

        public ClaimConsumer(IServiceProvider provider,
                             IEntryRepository repository,
                             IPublisher<AddressingKeyForAccountModel> publisher, 
                             IOptions<KafkaTopcis> options) : base(provider, options.Value?.Claims)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public override async Task ConsumeAsync(Claim message, CancellationToken stoppingToken)
        {
            var processMessageAsync = message.Status switch 
            {
                //TODO: remover chave quando estiver doando portabilidade
                //se necessario criar um consumer especializado
                ClaimStatus.OPEN => LockAddressingKeyAsync(message),
                ClaimStatus.CONFIRMED => ChangeOwnerAsync(message),
                _ => Task.CompletedTask,
            };
            await processMessageAsync;
        }

        private async Task LockAddressingKeyAsync(Claim claim)
        {
            var entry = await _repository.GetByAsync(claim.AddressingKey);
            if (entry is not null)
            {
                entry.Status = EntryStatus.LOCKED;
                await _repository.UpdateAsync(entry);
            }
        }

        private async Task ChangeOwnerAsync(Claim claim)
        {
            var entry = await _repository.GetByAsync(claim.AddressingKey);
            if (entry is not null)
            {
                var updateForAccount = new AddressingKeyForAccountModel(entry.Account, claim.Claimer.Account, claim.AddressingKey);
                entry.Status = EntryStatus.OWNED;
                entry.Account = claim.Claimer.Account;
                await _repository.UpdateAsync(entry);
                await _publisher.PublishAsync(_options.Entries, updateForAccount);
            }
        }
    }
}
using Microsoft.Extensions.Options;
using Shared.Broker;
using Shared.Broker.Consumers;
using Shared.Contracts.Enums;
using Shared.Contracts.Models;
using Shared.Contracts.Repositories;
using Shared.Entities;
using Star.Claims.Contracts;

namespace Star.Claims.Consumers
{
    public class ClaimConsumer : Consumer<Claim>
    {
        protected readonly IEntryRepository _repository;
        protected readonly IPublisher<AddressingKeyForAccountModel> _publisher;
        protected readonly KafkaTopcis _options;

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
                ClaimStatus.OPEN when message.Donor?.Account?.Ispb == Constants.ISPB => LockAddressingKeyAsync(message),
                ClaimStatus.CONFIRMED when message.Donor?.Account?.Ispb == Constants.ISPB => ExcludeAddressingKeyAsync(message),
                ClaimStatus.CONFIRMED when message.Claimer?.Account?.Ispb == Constants.ISPB => RegisterAddressingKeyAsync(message),
                _ => Task.CompletedTask,
            };
            await processMessageAsync;
        }

        protected async Task LockAddressingKeyAsync(Claim claim)
        {
            var entry = await _repository.GetByAsync(claim.AddressingKey);
            if (entry is not null)
            {
                entry.Status = EntryStatus.LOCKED;
                await _repository.UpdateAsync(entry);
            }
        }

        protected async Task RegisterAddressingKeyAsync(Claim claim)
        {
            var entry = new Entry
            {
                AddressingKey = claim.AddressingKey,
                Account = claim.Claimer.Account,
                Status = EntryStatus.OWNED,
            };

            var updateForAccount = new AddressingKeyForAccountModel(entry.Account, entry.AddressingKey);
            await _repository.InsertAsync(entry);
            await _publisher.PublishAsync(_options.Entries, updateForAccount);
        }

        protected async Task ExcludeAddressingKeyAsync(Claim claim)
        {
            var entry = await _repository.GetByAsync(claim.AddressingKey);
            if (entry is not null)
            {
                var updateForAccount = new AddressingKeyForAccountModel(entry.Account, claim.Claimer.Account, claim.AddressingKey);
                entry.Status = EntryStatus.OWNED;
                entry.Account = claim.Claimer.Account;
                await _repository.DeleteAsync(entry.Id);
                await _publisher.PublishAsync(_options.Entries, updateForAccount);
            }
        }
    }
}
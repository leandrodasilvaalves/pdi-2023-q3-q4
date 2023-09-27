using Shared.Contracts.Repositories;
using Shared.Entities;

namespace Shared.Broker.Consumers
{
    public class EntriesConsumer : Consumer<Entry>
    {
        private readonly IAccountRepository _repository;
        public EntriesConsumer(IServiceProvider provider, IAccountRepository repository)
            : base(provider, "bacen.entries")
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public override async Task ConsumeAsync(Entry message, CancellationToken stoppingToken)
        {
            var account = await _repository.GetByAsync(message.Account.Branch, message.Account.Number, message.Account.Ispb);
            if (account is not null)
            {
                account.Add(message.AddressingKey);
                await _repository.UpdateAsync(account);
            }
        }
    }
}
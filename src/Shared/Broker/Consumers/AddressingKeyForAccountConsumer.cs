using Shared.Contracts.Models;
using Shared.Contracts.Repositories;

namespace Shared.Broker.Consumers
{
    public class AddressingKeyForAccountConsumer : Consumer<AddressingKeyForAccountModel>
    {
        private readonly IAccountRepository _repository;
        public AddressingKeyForAccountConsumer(IServiceProvider provider, IAccountRepository repository)
            : base(provider, KnownTopics.ENTRIES)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public override async Task ConsumeAsync(AddressingKeyForAccountModel message, CancellationToken stoppingToken)
        {
            var process = new List<Task>(2);
            if(message.OldOnwerAccount is not null)
            {
                process.Add(RemoveAddressingKeyForAccount(message.OldOnwerAccount, message.AddressingKey));
            }

            process.Add(AddAddressingKeyForAccount(message.NewOnwerAccount, message.AddressingKey));
            await Task.WhenAll(process);
        }

        private async Task AddAddressingKeyForAccount(Contracts.Models.Account account, Contracts.Models.AddressingKey addressingKey)
        {
            var ownerAccount = await _repository.GetByAsync(account.Branch, account.Number, account.Ispb);
            if (ownerAccount is not null)
            {
                ownerAccount.Add(addressingKey);
                await _repository.UpdateAsync(ownerAccount);
            }
        }

        private async Task RemoveAddressingKeyForAccount(Contracts.Models.Account account, Contracts.Models.AddressingKey addressingKey)
        {
            var ownerAccount = await _repository.GetByAsync(account.Branch, account.Number, account.Ispb);
            if (ownerAccount is not null)
            {
                ownerAccount.Remove(addressingKey);
                await _repository.UpdateAsync(ownerAccount);
            }
        }
    }
}
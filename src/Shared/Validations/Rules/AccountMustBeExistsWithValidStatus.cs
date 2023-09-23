using Shared.Contracts.Errors;
using Shared.Contracts.Repositories;
using Shared.Contracts.Validations;
using Shared.Entities;

namespace Shared.Validations.Rules
{
    public interface IAccountMustBeExistsWithValidStatus : IRule { }

    public class AccountMustBeExistsWithValidStatus : Rule<Entry>, IAccountMustBeExistsWithValidStatus
    {
        private readonly IAccountRepository _repository;

        public AccountMustBeExistsWithValidStatus(IAccountRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public override async Task Apply(Entry entry)
        {
            var resutl = await _repository.GetByAsync(entry.Account.Branch, entry.Account.Number, entry.Account.Ispb);
            if (resutl is null)
            {
                Error = KnownErrors.ACCOUNT_DOES_NOT_EXISTIS;
            }
        }
    }
}
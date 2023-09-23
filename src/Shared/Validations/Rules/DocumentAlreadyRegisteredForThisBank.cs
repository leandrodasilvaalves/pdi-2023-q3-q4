using Shared.Contracts.Errors;
using Shared.Contracts.Repositories;
using Shared.Contracts.Validations;
using Shared.Entities;

namespace Shared.Validations.Rules
{
    public interface IDocumentAlreadyRegisteredForThisBank : IRule
    { }

    public class DocumentAlreadyRegisteredForThisBank : Rule<Account>, IDocumentAlreadyRegisteredForThisBank
    {
        private readonly IAccountRepository _repository;

        public DocumentAlreadyRegisteredForThisBank(IAccountRepository repository)
        {
            _repository = repository;
        }

        public override async Task Apply(Account account)
        {
            var resutl = await _repository.GetByAsync(account.Owner.Document, account.Ispb);
            if (resutl is not null)
            {
                Error = KnownErrors.DOCUMENT_ALREADY_REGISTERED;
            }
        }
    }
}
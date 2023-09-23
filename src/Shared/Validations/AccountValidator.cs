using Shared.Contracts.Validations;
using Shared.Entities;
using Shared.Validations.Rules;

namespace Shared.Validations
{
    public interface IAccountValidator : IAbstractValidator<Account>
    { }

    public class AccountValidator : AbstractValidator<Account>, IAccountValidator
    {
        private readonly IDocumentAlreadyRegisteredForThisBank _documentAlreadyRegisteredForThisBank;
        private readonly IOwnerValidator _ownerValidator;

        public AccountValidator(
            IDocumentAlreadyRegisteredForThisBank documentAlreadyRegisteredForThisBank,
            IOwnerValidator ownerValidator)
        {
            _documentAlreadyRegisteredForThisBank = documentAlreadyRegisteredForThisBank
                ?? throw new ArgumentNullException(nameof(documentAlreadyRegisteredForThisBank));

            _ownerValidator = ownerValidator ?? throw new ArgumentNullException(nameof(ownerValidator));
            RegisterAsyncRules();
        }

        protected override void RegisterRules()
        {
            AddRule(new AccountBranchrRule());
            AddRule(new AccountNumberRule());
            AddRule(new AccountIspbrRule());
        }

        private void RegisterAsyncRules()
        {
            AddRule(_documentAlreadyRegisteredForThisBank);
        }

        public override async Task<AbstractValidator<Account>> Validate(Account model)
        {
            await Task.WhenAll(base.Validate(model), _ownerValidator.Validate(model.Owner));
            AddErrors(_ownerValidator.Errors);
            return this;
        }
    }
}
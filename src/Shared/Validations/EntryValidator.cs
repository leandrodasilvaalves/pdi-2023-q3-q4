using Shared.Contracts.Validations;
using Shared.Entities;
using Shared.Validations.Rules;

namespace Shared.Validations
{
    public interface IEntryValidator : IAbstractValidator<Entry> { }

    public class EntryValidator : AbstractValidator<Entry>, IEntryValidator
    {
        private readonly IAccountMustBeExistsWithValidStatus _accountMustBeExistsWithValidStatus;
        private readonly IAddressingKeyMustBeUniqueRule _addressingKeyMustBeUniqueRule;

        public EntryValidator(IAccountMustBeExistsWithValidStatus accountMustBeExistsWithValidStatus,
                              IAddressingKeyMustBeUniqueRule addressingKeyMustBeUniqueRule)
        {
            _accountMustBeExistsWithValidStatus = accountMustBeExistsWithValidStatus ?? throw new ArgumentNullException(nameof(accountMustBeExistsWithValidStatus));
            _addressingKeyMustBeUniqueRule = addressingKeyMustBeUniqueRule ?? throw new ArgumentNullException(nameof(addressingKeyMustBeUniqueRule));
            RegisterAsyncRules();
        }

        protected override void RegisterRules()
        {
            AddRule(new AccountBranchrRule());
            AddRule(new AccountNumberRule());
            AddRule(new AccountIspbrRule());
            AddRule(new AddressingKeyValueTypeRule());            
        }

        private void RegisterAsyncRules()
        {
            AddRule(_accountMustBeExistsWithValidStatus);
            AddRule(_addressingKeyMustBeUniqueRule);
        }
    }
}
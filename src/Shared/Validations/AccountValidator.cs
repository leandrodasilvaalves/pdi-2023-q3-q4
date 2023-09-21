using Shared.Contracts.Validations;
using Shared.Models;
using Shared.Validations.Rules;

namespace Shared.Validations
{
    public interface IAccountValidator: IAbstractValidator<Account>
    {}

    public class AccountValidator : AbstractValidator<Account>, IAccountValidator
    {
        public AccountValidator(IOwnerValidator ownerValidator) => OwnerValidator = ownerValidator;

        private IOwnerValidator OwnerValidator { get; }

        public override void RegisterRules()
        {
            AddRule(new AccountBranchrRule());
            AddRule(new AccountNumberRule());
            AddRule(new AccountIspbrRule());
        }

        public override AbstractValidator<Account> Validate(Account model)
        {
            base.Validate(model);
            AddErrors(OwnerValidator.Validate(model.Owner).Errors);
            return this;
        }
    }
}
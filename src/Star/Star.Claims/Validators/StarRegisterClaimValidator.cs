using Shared.Contracts.Validations;
using Shared.Requests;
using Shared.Validations;
using Shared.Validations.Rules;

namespace Star.Claims.Validators
{
    public class StarRegisterClaimValidator : AbstractValidator<RegisterClaimRequest>, IRegisterClaimValidator
    {
        public StarRegisterClaimValidator(IAddressingKeyAlreadyHasAnOpenClaim anAddressingKeyMustHaveOnlyOneOpenClaim)
        {
            AddRule(anAddressingKeyMustHaveOnlyOneOpenClaim);
        }

        protected override void RegisterRules()
        {
            AddRule(new DocumentRule());
            AddRule(new AddressingKeyValueTypeRule());
            AddRule(new AccountBranchrRule());
            AddRule(new AccountNumberRule());
            AddRule(new CannotRegisterClaimForEvpAddressingKey());
        }
    }
}
using Shared.Contracts.Validations;
using Shared.Requests;
using Shared.Validations;
using Shared.Validations.Rules;

namespace Vulture.Validators
{
    public class VultureRegisterClaimValidator : AbstractValidator<RegisterClaimRequest>, IRegisterClaimValidator
    {
        public VultureRegisterClaimValidator(IAddressingKeyAlreadyHasAnOpenClaim anAddressingKeyMustHaveOnlyOneOpenClaim)
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
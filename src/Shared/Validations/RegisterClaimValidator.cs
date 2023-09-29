using Shared.Contracts.Validations;
using Shared.Requests;
using Shared.Validations.Rules;

namespace Shared.Validations
{
    public interface IRegisterClaimValidator : IAbstractValidator<RegisterClaimRequest> { }

    public class RegisterClaimValidator : AbstractValidator<RegisterClaimRequest>, IRegisterClaimValidator
    {
        private readonly IAddressingKeyMustBeExists _addressingKeyMustBeExists;
        private readonly IAddressingKeyAlreadyHasAnOpenClaim _anAddressingKeyMustHaveOnlyOneOpenClaim;

        public RegisterClaimValidator(IAddressingKeyMustBeExists addressingKeyMustBeExists,
                                      IAddressingKeyAlreadyHasAnOpenClaim anAddressingKeyMustHaveOnlyOneOpenClaim)
        {
            _addressingKeyMustBeExists = addressingKeyMustBeExists ?? throw new ArgumentNullException(nameof(addressingKeyMustBeExists));
            _anAddressingKeyMustHaveOnlyOneOpenClaim = anAddressingKeyMustHaveOnlyOneOpenClaim ?? throw new ArgumentNullException(nameof(anAddressingKeyMustHaveOnlyOneOpenClaim));
            RegisterAsyncRules();
        }

        protected override void RegisterRules()
        {
            AddRule(new DocumentRule());
            AddRule(new AddressingKeyValueTypeRule());
            AddRule(new AccountBranchrRule());
            AddRule(new AccountNumberRule());
            AddRule(new AccountIspbrRule());
            AddRule(new CannotRegisterClaimForEvpAddressingKey());
        }

        private void RegisterAsyncRules()
        {
            AddRule(_addressingKeyMustBeExists);
            AddRule(_anAddressingKeyMustHaveOnlyOneOpenClaim);
        }
    }
}
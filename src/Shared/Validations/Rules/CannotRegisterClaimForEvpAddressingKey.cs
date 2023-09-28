using Shared.Contracts.Enums;
using Shared.Contracts.Errors;
using Shared.Contracts.Validations;
using Shared.Requests;

namespace Shared.Validations.Rules
{
    public class CannotRegisterClaimForEvpAddressingKey : Rule<RegisterClaimRequest>
    {
        public override Task Apply(RegisterClaimRequest instance)
        {
            if (instance.AddressingKey.Type is AddressingKeyType.EVP)
            {
                Error = KnownErrors.CANNOT_REGISTER_CLAIM_FOR_ADDRESSING_KEY_EVP;
            }
            return Task.CompletedTask;
        }
    }
}
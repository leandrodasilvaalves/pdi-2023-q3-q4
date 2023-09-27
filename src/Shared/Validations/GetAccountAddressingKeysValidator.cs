using Shared.Contracts.Validations;
using Shared.Requests;
using Shared.Validations.Rules;

namespace Shared.Validations
{
    public interface IGetAccountAddressingKeysValidator : IAbstractValidator<GetAccountAddressingKeysRequest> { }

    public class GetAccountAddressingKeysValidator : AbstractValidator<GetAccountAddressingKeysRequest>, IGetAccountAddressingKeysValidator
    {
        protected override void RegisterRules()
        {
            AddRule(new AccountBranchrRule());
            AddRule(new AccountNumberRule());
            AddRule(new AccountIspbrRule());
        }
    }
}
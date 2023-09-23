using Shared.Contracts.Validations;
using Shared.Entities;
using Shared.Validations.Rules;

namespace Shared.Validations
{
    public interface IOwnerValidator : IAbstractValidator<Owner>
    { }

    public class OwnerValidator : AbstractValidator<Owner>, IOwnerValidator
    {
        protected override void RegisterRules()
        {
            AddRule(new OwnerNameRule());
            AddRule(new OwnerPhoneRule());
            AddRule(new OwnerEmailRule());
            AddRule(new OwnerDocumentRule());
        }
    }
}
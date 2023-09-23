using Shared.Contracts.Validations;
using Shared.Entities;
using Shared.Validations.Rules;

namespace Shared.Validations
{
    public interface IEntryValidator : IAbstractValidator<Entry> { }

    public class EntryValidator : AbstractValidator<Entry>, IEntryValidator
    {
        protected override void RegisterRules()
        {
            AddRule(new AccountBranchrRule());
            AddRule(new AccountNumberRule());
            AddRule(new AccountIspbrRule());
            AddRule(new AddressingKeyValueTypeRule());
        }
    }
}
using Shared.Contracts.Enums;
using Shared.Contracts.Errors;
using Shared.Contracts.Models;
using Shared.Contracts.Validations;
using Shared.Entities;

namespace Shared.Validations.Rules
{
    public class AddressingKeyValueTypeRule : Rule<Entry>
    {
        public override async Task Apply(Entry instance)
        {
            var addressingKey = instance.AddressingKey;
            IRule rule = addressingKey.Type switch
            {
                AddressingKeyType.CPF => new DocumentRule(),
                AddressingKeyType.EMAIL => new EmailRule(),
                AddressingKeyType.PHONE => new PhoneRule(),
                AddressingKeyType.EVP => new AddressingKeyEvpRule(),
                _ => null
            };

            var castedRule = ((IRule<Entry>)rule);
            await castedRule.Apply(instance);
            if(castedRule.IsFailure)
            {
                Error = KnownErrors.INVALID_ADDRESSING_KEY;
            }
        }

        private class AddressingKeyEvpRule : Rule<string>, IRule<AddressingKey>
        {
            public override Task Apply(string instance)
            {
                if (Guid.TryParseExact(instance, "D", out var _) is false)
                {
                    Error = KnownErrors.INVALID_ADDRESSING_KEY;
                }
                return Task.CompletedTask;
            }
            public Task Apply(AddressingKey instance) => Apply(instance.Value);

        }
    }

}
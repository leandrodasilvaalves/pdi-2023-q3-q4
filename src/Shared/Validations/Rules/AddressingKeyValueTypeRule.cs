using Shared.Contracts.Enums;
using Shared.Contracts.Errors;
using Shared.Contracts.Models;
using Shared.Contracts.Validations;
using Shared.Entities;
using Shared.Requests;

namespace Shared.Validations.Rules
{
    public class AddressingKeyValueTypeRule : Rule<AddressingKey>, IRule<Entry>, IRule<RegisterClaimRequest>
    {
        public override async Task Apply(AddressingKey addressingKey)
        {
            IRule rule = addressingKey.Type switch
            {
                AddressingKeyType.CPF => new DocumentRule(),
                AddressingKeyType.EMAIL => new EmailRule(),
                AddressingKeyType.PHONE => new PhoneRule(),
                AddressingKeyType.EVP => new AddressingKeyEvpRule(),
                _ => null
            };

            var castedRule = ((IRule<AddressingKey>)rule);
            await castedRule.Apply(addressingKey);
            if(castedRule.IsFailure)
            {
                Error = KnownErrors.INVALID_ADDRESSING_KEY;
            }
        }

        public Task Apply(Entry instance) => Apply(instance.AddressingKey);
        public Task Apply(RegisterClaimRequest instance) => Apply(instance.AddressingKey);

        private class AddressingKeyEvpRule : Rule<string>, IRule<AddressingKey>, IRule<Entry>
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
            public Task Apply(Entry instance) => Apply(instance.AddressingKey.Value);
        }
    }
}
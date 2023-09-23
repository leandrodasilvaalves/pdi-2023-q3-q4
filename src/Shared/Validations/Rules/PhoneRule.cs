using System.Text.RegularExpressions;
using Shared.Contracts.Errors;
using Shared.Contracts.Validations;
using Shared.Entities;

namespace Shared.Validations.Rules
{
    public class PhoneRule : Rule<string>, IRule<Owner>, IRule<Entry>
    {
        public override Task Apply(string phone)
        {
            if (!Regex.IsMatch(phone, @"^\+55\d{2}9\d{8}$", RegexOptions.Compiled))
            {
                Error = KnownErrors.INVALID_PHONE_NUMBER;
            }
            return Task.CompletedTask;
        }

        public Task Apply(Owner instance) => Apply(instance.Phone);

        public Task Apply(Entry instance) => Apply(instance.AddressingKey.Value);
    }
}
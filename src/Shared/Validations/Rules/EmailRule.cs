using System.Text.RegularExpressions;
using Shared.Contracts.Errors;
using Shared.Contracts.Validations;
using Shared.Entities;

namespace Shared.Validations.Rules
{
    public class EmailRule : Rule<string>, IRule<Owner>, IRule<Entry>
    {
        public override Task Apply(string email)
        {
            if (!Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", RegexOptions.Compiled))
            {
                Error = KnownErrors.INVALID_EMAIL;
            }
            return Task.CompletedTask;
        }

        public Task Apply(Owner instance) => Apply(instance.Email);

        public Task Apply(Entry instance) => Apply(instance.AddressingKey.Value);
    }
}
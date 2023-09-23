using System.Text.RegularExpressions;
using Shared.Contracts.Errors;
using Shared.Contracts.Validations;
using Shared.Entities;

namespace Shared.Validations.Rules
{
    public class PersonNameRule : Rule<string>, IRule<Owner>
    {
        public override Task Apply(string name)
        {
            if (!Regex.IsMatch(name, @"^[\p{L}\s]{5,50}$", RegexOptions.Compiled))
            {
                Error = KnownErrors.INVALID_OWNER_NAME;
            }
            return Task.CompletedTask;
        }

        public Task Apply(Owner instance) => Apply(instance.Name);
    }
}
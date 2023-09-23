using System.Text.RegularExpressions;
using Shared.Contracts;
using Shared.Contracts.Validations;
using Shared.Entities;

namespace Shared.Validations.Rules
{
    public class OwnerEmailRule : Rule<Owner>
    {
        public override Task Apply(Owner owner)
        {
            if (!Regex.IsMatch(owner.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", RegexOptions.Compiled))
            {
                Error = KnownErrors.INVALID_EMAIL;
            }
            return Task.CompletedTask;
        }
    }
}
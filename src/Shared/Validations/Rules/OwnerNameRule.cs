using System.Text.RegularExpressions;
using Shared.Contracts;
using Shared.Contracts.Validations;
using Shared.Models;

namespace Shared.Validations.Rules
{
    public class OwnerNameRule : Rule<Owner>
    {
        public override Task Apply(Owner owner)
        {
            if (!Regex.IsMatch(owner.Name, @"^[\p{L}\s]{5,50}$", RegexOptions.Compiled))
            {
                Error = KnownErrors.INVALID_OWNER_NAME;
            }
            return Task.CompletedTask;
        }
    }
}
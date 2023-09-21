using System.Text.RegularExpressions;
using Shared.Contracts;
using Shared.Contracts.Validations;
using Shared.Models;

namespace Shared.Validations.Rules
{
    public class OwnerEmailRule : Rule<Owner>
    {
        public override void Apply(Owner owner)
        {
            if (!Regex.IsMatch(owner.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", RegexOptions.Compiled))
            {
                Error = KnownErrors.INVALID_EMAIL;
            }
        }
    }
}
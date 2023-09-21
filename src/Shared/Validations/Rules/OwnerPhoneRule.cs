using System.Text.RegularExpressions;
using Shared.Contracts;
using Shared.Contracts.Validations;
using Shared.Models;

namespace Shared.Validations.Rules
{
    public class OwnerPhoneRule : Rule<Owner>
    {
        public override void Apply(Owner owner)
        {
            if (!Regex.IsMatch(owner.Phone, @"^\+55\d{2}9\d{8}$", RegexOptions.Compiled))
            {
                Error = KnownErrors.INVALID_PHONE_NUMBER;
            }
        }
    }
}
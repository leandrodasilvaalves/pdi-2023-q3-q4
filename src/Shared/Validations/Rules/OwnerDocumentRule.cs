using System.Text.RegularExpressions;
using Shared.Contracts;
using Shared.Contracts.Validations;
using Shared.Entities;

namespace Shared.Validations.Rules
{
    public class OwnerDocumentRule : Rule<Owner>
    {
        public override Task Apply(Owner owner)
        {
            if (!Regex.IsMatch(owner.Document, @"^[0-9]{11}$", RegexOptions.Compiled))
            {
                Error = KnownErrors.INVALID_OWNER_DOCUMENT;
            }
            return Task.CompletedTask;
        }
    }
}
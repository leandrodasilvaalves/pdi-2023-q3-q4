using System.Text.RegularExpressions;
using Shared.Contracts.Errors;
using Shared.Contracts.Models;
using Shared.Contracts.Validations;
using Shared.Entities;
using Shared.Requests;

namespace Shared.Validations.Rules
{
    public class DocumentRule : Rule<string>, IRule<Owner>, IRule<AddressingKey>, IRule<RegisterClaimRequest>
    {
        public override Task Apply(string document)
        {
            if (!Regex.IsMatch(document, @"^[0-9]{11}$", RegexOptions.Compiled))
            {
                Error = KnownErrors.INVALID_DOCUMENT;
            }
            return Task.CompletedTask;
        }

        public Task Apply(Owner instance) => Apply(instance.Document);

        public Task Apply(AddressingKey instance) => Apply(instance.Value);

        public Task Apply(RegisterClaimRequest instance) => Apply(instance.Claimer.Document);
    }
}
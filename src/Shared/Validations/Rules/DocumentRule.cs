using System.Text.RegularExpressions;
using Shared.Contracts.Errors;
using Shared.Contracts.Validations;
using Shared.Entities;

namespace Shared.Validations.Rules
{
    public class DocumentRule : Rule<string>, IRule<Owner>, IRule<Entry>
    {
        public override Task Apply(string document)
        {
            if (!Regex.IsMatch(document, @"^[0-9]{11}$", RegexOptions.Compiled))
            {
                Error = KnownErrors.INVALID_OWNER_DOCUMENT;
            }
            return Task.CompletedTask;
        }

        public Task Apply(Owner instance)=> Apply(instance.Document);

        public Task Apply(Entry instance)=> Apply(instance.AddressingKey.Value);
    }
}
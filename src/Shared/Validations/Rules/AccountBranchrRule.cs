using System.Text.RegularExpressions;
using Shared.Contracts.Errors;
using Shared.Contracts.Validations;
using Shared.Entities;
using Shared.Requests;

namespace Shared.Validations.Rules
{
    public class AccountBranchrRule : Rule<string>, IRule<Account>, IRule<Entry>, IRule<GetAccountAddressingKeysRequest>, IRule<RegisterClaimRequest>
    {
        public override Task Apply(string branch)
        {
            if (!Regex.IsMatch(branch, @"^[0-9]{4}$", RegexOptions.Compiled))
            {
                Error = KnownErrors.INVALID_ACCOUNT_BRANCH;
            }
            return Task.CompletedTask;
        }

        public Task Apply(Entry instance) => Apply(instance.Account.Branch);

        public Task Apply(Account instance) => Apply(instance.Branch);

        public Task Apply(GetAccountAddressingKeysRequest instance) => Apply(instance.Branch);

        public Task Apply(RegisterClaimRequest instance) => Apply(instance.Claimer.Account.Branch);
    }
}
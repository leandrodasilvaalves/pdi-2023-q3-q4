using System.Text.RegularExpressions;
using Shared.Contracts;
using Shared.Contracts.Validations;
using Shared.Models;

namespace Shared.Validations.Rules
{
    public class AccountBranchrRule : Rule<Account>
    {
        public override Task Apply(Account account)
        {
            if (!Regex.IsMatch(account.Branch, @"^[0-9]{4}$", RegexOptions.Compiled))
            {                
                Error = KnownErrors.INVALID_ACCOUNT_BRANCH;
            }
            return Task.CompletedTask;
        }
    }
}
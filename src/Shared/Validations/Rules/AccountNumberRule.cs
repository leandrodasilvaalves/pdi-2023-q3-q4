using System.Text.RegularExpressions;
using Shared.Contracts;
using Shared.Contracts.Validations;
using Shared.Models;

namespace Shared.Validations.Rules
{
    public class AccountNumberRule : Rule<Account>
    {
        public override Task Apply(Account account)
        {
            if (!Regex.IsMatch(account.Number, @"^[0-9]{6}$", RegexOptions.Compiled))
            {                
                Error = KnownErrors.INVALID_ACCOUNT_NUMBER;
            }
            return Task.CompletedTask;
        }
    }
}
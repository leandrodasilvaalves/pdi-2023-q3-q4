using System.Text.RegularExpressions;
using Shared.Contracts.Errors;
using Shared.Contracts.Validations;
using Shared.Entities;

namespace Shared.Validations.Rules
{
    public class AccountNumberRule : Rule<string>, IRule<Account>, IRule<Entry>
    {
        public override Task Apply(string number)
        {
            if (!Regex.IsMatch(number, @"^[0-9]{6}$", RegexOptions.Compiled))
            {                
                Error = KnownErrors.INVALID_ACCOUNT_NUMBER;
            }
            return Task.CompletedTask;
        }

        public Task Apply(Account instance) => Apply(instance.Number);

        public Task Apply(Entry instance) => Apply(instance.Account.Number);
    }
}
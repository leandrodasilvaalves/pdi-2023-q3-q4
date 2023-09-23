using System.Text.RegularExpressions;
using Shared.Contracts.Errors;
using Shared.Contracts.Validations;
using Shared.Entities;

namespace Shared.Validations.Rules
{
    public class AccountIspbrRule : Rule<string>, IRule<Account>, IRule<Entry>
    {
        public override Task Apply(string ispb)
        {
            if (!Regex.IsMatch(ispb, @"^[0-9]{8}$", RegexOptions.Compiled))
            {                
                Error = KnownErrors.INVALID_ISPB;
            }
            return Task.CompletedTask;
        }

        public Task Apply(Account instance) => Apply(instance.Ispb);

        public Task Apply(Entry instance) => Apply(instance.Account.Ispb);
    }
}
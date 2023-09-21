using System.Text.RegularExpressions;
using Shared.Contracts;
using Shared.Contracts.Validations;
using Shared.Models;

namespace Shared.Validations.Rules
{
    public class AccountIspbrRule : Rule<Account>
    {
        public override void Apply(Account account)
        {
            if (!Regex.IsMatch(account.Ispb, @"^[0-9]{8}$", RegexOptions.Compiled))
            {                
                Error = KnownErrors.INVALID_ISPB;
            }
        }
    }
}
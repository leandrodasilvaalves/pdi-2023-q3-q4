using Shared.Enum;
using Shared.Entities;

namespace Shared.Requests
{
    public class CreateAccountRequest : Shared.Contracts.Models.Account
    {
        public Owner Owner { get; set; }

        public double Balance { get; set; }

        public Account ToEntity() => new()
        {
            Branch = Branch,
            Number = Number,
            Balance = Balance,
            Ispb = Ispb,
            Owner = Owner,
            CreatedAt = DateTime.UtcNow,
            Status = AccountStatus.ACTIVE,
        };
    }
}
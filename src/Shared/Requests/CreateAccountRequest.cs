using Shared.Entities;
using Shared.Contracts.Enums;

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
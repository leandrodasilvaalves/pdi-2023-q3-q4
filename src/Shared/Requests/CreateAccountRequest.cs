using Shared.Enum;
using Shared.Models;

namespace Shared.Requests
{
    public class CreateAccountRequest
    {
        public string Branch { get; set; }
        public string Number { get; set; }
        public double Balance { get; set; }
        public string Ispb { get; set; }
        public Owner Owner { get; set; }

        public Account ToAccount() => new()
        {
            Branch= Branch,
            Number= Number,
            Balance= Balance,
            Ispb= Ispb,
            Owner= Owner,
            CreatedAt= DateTime.UtcNow,
            Status = AccountStatus.ACTIVE,
        };
    }
}
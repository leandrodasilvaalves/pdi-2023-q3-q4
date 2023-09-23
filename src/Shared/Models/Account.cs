using Shared.Enum;

namespace Shared.Models
{
    public class Account : BaseModel
    {
        public string Branch { get; set; }
        public string Number { get; set; }
        public double Balance { get; set; }
        public string Ispb { get; set; }
        public Owner Owner { get; set; }
        public DateTime CreatedAt { get; set; }
        public AccountStatus Status { get; set; }
    }
}
using Shared.Contracts.Enums;
using Shared.Contracts.Models;

namespace Shared.Entities
{
    public class Claim : EntityBase
    {
        public Claim()
        {
            Status = ClaimStatus.OPEN;
        }

        public ClaimType Type { get; set; }
        public ClaimerModel Claimer { get; set; }
        public ClaimerModel Donor { get; set; }
        public AddressingKey AddressingKey { get; set; }
        public ClaimStatus Status { get; set; }
    }
}
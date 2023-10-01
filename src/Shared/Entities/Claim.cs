using Shared.Contracts.Enums;
using Shared.Contracts.Models;

namespace Shared.Entities
{
    public class Claim : EntityBase
    {
        public Claim()
        {
            UpdatedAt = DateTime.UtcNow;
            CreatedAt = DateTime.UtcNow;
            ResolutionLimitDate = CreatedAt.AddDays(7);
            ConclusionLimitDate = ResolutionLimitDate.AddDays(7);
            Status = ClaimStatus.OPEN;
        }

        public ClaimType Type { get; set; }
        public ClaimerModel Claimer { get; set; }
        public ClaimerModel Donor { get; set; }
        public AddressingKey AddressingKey { get; set; }
        public ClaimStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ResolutionLimitDate { get; set; }
        public DateTime ConclusionLimitDate { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
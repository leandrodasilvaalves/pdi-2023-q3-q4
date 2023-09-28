using Shared.Contracts.Enums;
using Shared.Contracts.Models;

namespace Shared.Entities
{
    public class Claim : EntityBase
    {
        public Claim()
        {
            CreatedAt = DateTime.UtcNow;
            ResolutionLimitDate = CreatedAt.AddDays(7);
            ConclusionLimitDate = ResolutionLimitDate.AddDays(7);  
            Status = ClaimStatus.OPEN;          
        }

        public ClaimType Type { get; set; }
        public ClaimerModel Claimer { get; set; }
        public AddressingKey AddressingKey { get; set; }
        public ClaimStatus Status { get; set; }
        public DateTime CreatedAt { get; }
        public DateTime ResolutionLimitDate { get; }
        public DateTime ConclusionLimitDate { get; }
    }
}
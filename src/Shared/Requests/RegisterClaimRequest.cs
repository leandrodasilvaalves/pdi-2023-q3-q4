using Shared.Contracts.Enums;
using Shared.Contracts.Models;
using Shared.Entities;

namespace Shared.Requests
{
    public class RegisterClaimRequest
    {
        public ClaimType Type { get; set; }
        public ClaimerModel Claimer { get; set; }
        public AddressingKey AddressingKey { get; set; }

        public Claim ToEntity() => new()
        {
            Type = Type,
            Claimer = Claimer,
            AddressingKey = AddressingKey,
        };
    }
}
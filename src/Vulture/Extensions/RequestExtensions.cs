using Shared.Entities;
using Shared.Requests;
using Vulture.Contracts;

namespace Vulture.Extensions
{
    public static class RequestExtensions
    {
        public static Entry ToVultureEntity(this CreateEntryRequest self)
        {
            self.Account.Ispb = Constants.ISPB;
            return self.ToEntity();
        }

        public static Claim ToVultureEntity(this RegisterClaimRequest self, string claimdId)
        {
            self.Claimer.Account.Ispb = Constants.ISPB;
            var claim = self.ToEntity();
            claim.Id = claimdId;
            return claim;
        }

        public static Account ToVultureEntity(this CreateAccountRequest self)
        {
            self.Ispb = Constants.ISPB;
            return self.ToEntity();
        }
    }
}
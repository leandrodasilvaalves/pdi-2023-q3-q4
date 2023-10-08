using Shared.Entities;
using Shared.Requests;
using Star.Claims.Contracts;

namespace Star.Accounts.Extensions
{
    public static class RequestExtensions
    {
        public static Entry ToStarEntity(this CreateEntryRequest self)
        {
            self.Account.Ispb = Constants.ISPB;
            return self.ToEntity();
        }

        public static Claim ToStarEntity(this RegisterClaimRequest self, string claimdId)
        {
            self.Claimer.Account.Ispb = Constants.ISPB;
            var claim = self.ToEntity();
            claim.Id = claimdId;
            return claim;
        }

        public static Account ToStarEntity(this CreateAccountRequest self)
        {
            self.Ispb = Constants.ISPB;
            return self.ToEntity();
        }
    }
}

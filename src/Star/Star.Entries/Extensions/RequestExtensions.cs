using Shared.Entities;
using Shared.Requests;
using Star.Entries.Contracts;

namespace Star.Accounts.Extensions
{
    public static class RequestExtensions
    {
        public static Entry ToStarEntity(this CreateEntryRequest self)
        {
            self.Account.Ispb = Constants.ISPB;
            return self.ToEntity();
        }
    }
}
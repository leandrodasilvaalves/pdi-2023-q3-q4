using Shared.Entities;
using Shared.Requests;
using Star.Accounts.Contracts;

namespace Star.Accounts.Extensions
{
    public static class RequestExtensions
    {
        public static Account ToStarEntity(this CreateAccountRequest self)
        {
            self.Ispb = Constants.ISPB;
            return self.ToEntity();
        }
    }
}
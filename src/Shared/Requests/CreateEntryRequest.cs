using Shared.Entities;

namespace Shared.Requests
{
    public class CreateEntryRequest : Entry
    {
        public Entry ToEntity() => this;

        public CreateEntryRequest WithIspb(string ispb)
        {
            Account.Ispb = ispb;
            return this;
        }
    }
}
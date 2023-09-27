using Shared.Entities;

namespace Shared.Requests
{
    public class CreateEntryRequest : Entry
    {
        public Entry ToEntity() => this;
    }
}
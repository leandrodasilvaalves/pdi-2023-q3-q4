using Shared.Contracts.Models;
using Models = Shared.Contracts.Models;

namespace Shared.Entities
{
    public class Entry : BaseModel
    {
        public Entry()
        {
            Account = new();
            AddressingKey = new();
        }
        public Models.Account Account { get; set; }
        public AddressingKey AddressingKey { get; set; }
    }
}
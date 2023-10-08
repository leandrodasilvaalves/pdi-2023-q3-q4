using Shared.Contracts.Enums;
using Shared.Contracts.Models;
using Models = Shared.Contracts.Models;

namespace Shared.Entities
{
    public class Entry : EntityBase
    {
        public Entry()
        {
            Account = new();
            Owner = new();
            AddressingKey = new();
            Status = EntryStatus.OWNED;
        }
        
        public Models.Account Account { get; set; }
        public Owner Owner { get; set; }
        public AddressingKey AddressingKey { get; set; }
        public EntryStatus Status { get; set; }
    }
}
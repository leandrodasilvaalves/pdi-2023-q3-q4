using Shared.Contracts.Enums;
using Shared.Contracts.Models;

namespace Shared.Entities
{
    public class Account : EntityBase
    {
        public string Branch { get; set; }
        public string Number { get; set; }
        public double Balance { get; set; }
        public string Ispb { get; set; }
        public Owner Owner { get; set; }
        public DateTime CreatedAt { get; set; }
        public AccountStatus Status { get; set; }

        public List<AddressingKey> AddressingKeys { get; set; }

        public void Add(AddressingKey addressingKey)
        {
            if (AddressingKeys is null)
            {
                AddressingKeys = new List<AddressingKey>();
            }
            AddressingKeys.Add(addressingKey);
        }

        public void Remove(AddressingKey addressingKey)
        {
            AddressingKeys?.RemoveAll(x => x.Value == addressingKey.Value);
        }
    }
}
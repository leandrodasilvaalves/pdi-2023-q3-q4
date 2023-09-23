using Shared.Contracts.Enums;

namespace Shared.Contracts.Models
{
    public struct AddressingKey
    {
        public string Value { get; set; }
        public AddressingKeyType Type { get; set; }
    }
}
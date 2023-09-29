namespace Shared.Contracts.Models
{
    public class AddressingKeyForAccountModel
    {
        public AddressingKeyForAccountModel() { }
        public AddressingKeyForAccountModel(Account newOnwerAccount, AddressingKey addressingKey)
        {
            NewOnwerAccount = newOnwerAccount;
            AddressingKey = addressingKey;
        }

        public AddressingKeyForAccountModel(Account oldOnwerAccount, Account newOnwerAccount, AddressingKey addressingKey)
                                            : this(newOnwerAccount, addressingKey)
        {
            OldOnwerAccount = oldOnwerAccount;
            AddressingKey = addressingKey;
        }

        public Account OldOnwerAccount { get; set; }
        public Account NewOnwerAccount { get; set; }
        public AddressingKey AddressingKey { get; set; }
    }
}

namespace Shared.Contracts.Models
{
    public class ClaimerModel
    {
        public Models.Account Account { get; set; }
        public string Document { get; set; }

        public static explicit operator ClaimerModel(Models.Account account) 
            => new(){ Account = account };
    }
}
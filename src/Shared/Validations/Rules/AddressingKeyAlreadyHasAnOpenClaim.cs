using Shared.Contracts.Enums;
using Shared.Contracts.Errors;
using Shared.Contracts.Repositories;
using Shared.Contracts.Validations;
using Shared.Requests;

namespace Shared.Validations.Rules
{
    public interface IAddressingKeyAlreadyHasAnOpenClaim : IRule { }

    public class AddressingKeyAlreadyHasAnOpenClaim : Rule<RegisterClaimRequest>, IAddressingKeyAlreadyHasAnOpenClaim
    {
        private readonly IEntryRepository _repository;

        public AddressingKeyAlreadyHasAnOpenClaim(IEntryRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public override async Task Apply(RegisterClaimRequest instance)
        {
            var resutl = await _repository.GetByAsync(instance.AddressingKey);
            if (resutl?.Status == EntryStatus.LOCKED)
            {
                Error = KnownErrors.ADDRESSING_KEY_ALREADY_HAS_AN_OPEN_CLAIM;
            }
        }
    }
}
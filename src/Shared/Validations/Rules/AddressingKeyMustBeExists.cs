using Shared.Contracts.Errors;
using Shared.Contracts.Models;
using Shared.Contracts.Repositories;
using Shared.Contracts.Validations;
using Shared.Requests;

namespace Shared.Validations.Rules
{
    public interface IAddressingKeyMustBeExists : IRule { }

    public class AddressingKeyMustBeExists : Rule<AddressingKey>, IAddressingKeyMustBeExists, IRule<RegisterClaimRequest>
    {
        private readonly IEntryRepository _repository;

        public AddressingKeyMustBeExists(IEntryRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public override async Task Apply(AddressingKey instance)
        {
            var resutl = await _repository.GetByAsync(instance);
            if (resutl is null)
            {
                Error = KnownErrors.ADDRESSING_KEY_DOES_NOT_EXISTS;
            }
        }

        public Task Apply(RegisterClaimRequest instance) => Apply(instance.AddressingKey);
    }
}
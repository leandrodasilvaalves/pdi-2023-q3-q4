using Shared.Contracts.Errors;
using Shared.Contracts.Repositories;
using Shared.Contracts.Validations;
using Shared.Entities;

namespace Shared.Validations.Rules
{
    public interface IAddressingKeyMustBeUniqueRule : IRule { }

    public class AddressingKeyMustBeUniqueRule : Rule<Entry>, IAddressingKeyMustBeUniqueRule
    {
        private readonly IEntryRepository _repository;

        public AddressingKeyMustBeUniqueRule(IEntryRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public override async Task Apply(Entry instance)
        {
            var resutl = await _repository.GetByAsync(instance.AddressingKey);
            if (resutl is not null)
            {
                Error = KnownErrors.ADDRESSING_KEY_ALREADY_EXISTS;
            }
        }
    }
}
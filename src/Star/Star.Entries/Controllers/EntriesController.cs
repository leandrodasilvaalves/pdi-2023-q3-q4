using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.Broker;
using Shared.Contracts.Models;
using Shared.Contracts.Repositories;
using Shared.Entities;
using Shared.Extensions;
using Shared.HttpClients;
using Shared.Requests;
using Shared.Validations;
using Star.Accounts.Extensions;
using Star.Entries.Contracts;

namespace Star.Entries.Controllers
{
    [ApiController]
    [Route("api/pix/entries")]
    public class EntriesController : ControllerBase
    {
        private readonly IEntryRepository _repository;
        private readonly IEntryValidator _validator;
        private readonly IPublisher<AddressingKeyForAccountModel> _publisher;
        private readonly KafkaTopcis _options;
        private readonly IBacenEntryClient _entryClient;

        public EntriesController(IEntryRepository repository,
                                 IEntryValidator validator,
                                 IPublisher<AddressingKeyForAccountModel> publisher,
                                 IBacenEntryClient entryClient,
                                 IOptions<KafkaTopcis> options)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _entryClient = entryClient ?? throw new ArgumentNullException(nameof(entryClient));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateEntryRequest request)
        {
            var entry = request.ToStarEntity();
            if ((await _validator.Validate(entry)).IsFailure)
            {
                return BadRequest(new Response<Entry>(_validator.Errors));
            }

            var result = await _entryClient.RegisterAsync(request.WithIspb(Constants.ISPB));
            if (result.IsFailure())
            {
                return BadRequest(await result.ToFailureAsync());
            }

            await _repository.InsertAsync(entry);
            await _publisher.PublishAsync(_options.Entries, new AddressingKeyForAccountModel(entry.Account, entry.AddressingKey));
            return Ok(new Response<Entry>(entry));
        }
    }
}
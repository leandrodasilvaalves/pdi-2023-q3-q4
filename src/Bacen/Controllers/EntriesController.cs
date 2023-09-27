using Microsoft.AspNetCore.Mvc;
using Shared.Broker;
using Shared.Contracts.Repositories;
using Shared.Entities;
using Shared.Requests;
using Shared.Validations;

namespace Bacen.Controllers
{
    [ApiController]
    [Route("api/pix/entries")]
    public class EntriesController : ControllerBase
    {
        private readonly IEntryRepository _repository;
        private readonly IEntryValidator _validator;
        private readonly IPublisher<Entry> _publisher;

        public EntriesController(IEntryRepository repository,
                                 IEntryValidator validator,
                                 IPublisher<Entry> publisher)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateEntryRequest request)
        {
            var entry = request.ToEntity();
            if((await _validator.Validate(entry)).IsFailure)
            {
                return BadRequest(new Response<Entry>(_validator.Errors));
            }
            
            await _repository.InsertAsync(entry);
            await _publisher.PublishAsync("bacen.entries", entry);
            return Ok(new Response<Entry>(entry));
        }
    }
}
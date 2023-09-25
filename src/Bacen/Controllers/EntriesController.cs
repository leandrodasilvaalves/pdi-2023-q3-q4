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
        private readonly IPublisher<CreateEntryRequest> _publisher;

        public EntriesController(IEntryRepository repository,
                                 IEntryValidator validator,
                                 IPublisher<CreateEntryRequest> publisher)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateEntryRequest request)
        {
            if((await _validator.Validate(request)).IsFailure)
            {
                return BadRequest(new Response<Entry>(_validator.Errors));
            }
            
            await _repository.InsertAsync(request);
            await _publisher.PublishAsync("bacen.entries", request);
            return Ok();
        }
    }
}
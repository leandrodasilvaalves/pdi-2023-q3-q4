using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Repositories;
using Shared.Extensions;
using Shared.Requests;

namespace Bacen.Controllers
{
    [ApiController]
    [Route("api/pix/entries")]
    public class EntriesController : ControllerBase
    {
        private readonly IEntryRepository _repository;

        public EntriesController(IEntryRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateEntryRequest request)
        {
            await _repository.InsertAsync(request);
            return Ok();
        }
    }
}
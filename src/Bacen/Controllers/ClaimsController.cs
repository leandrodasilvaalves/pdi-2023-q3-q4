using Microsoft.AspNetCore.Mvc;
using Shared.Broker;
using Shared.Contracts.Repositories;
using Shared.Entities;
using Shared.Requests;
using Shared.Validations;

namespace Bacen.Controllers
{
    [ApiController]
    [Route("api/pix/claims")]
    public class ClaimsController : ControllerBase
    {
        private readonly IClaimRepository _repository;
        private readonly IPublisher<Claim> _publisher;

        public ClaimsController(IClaimRepository repository, IPublisher<Claim> publisher)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] RegisterClaimRequest claimRequest, [FromServices]IRegisterClaimValidator validator)
        {   
            if((await validator.Validate(claimRequest)).IsFailure)
            {
                return BadRequest(new Response<List<RegisterClaimRequest>>(validator.Errors));
            }
            var claim = claimRequest.ToEntity();
            await _repository.InsertAsync(claim);
            await _publisher.PublishAsync("bacen.claims", claim);
            return Ok(new Response<Claim>(claim));
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.Broker;
using Shared.Contracts.Enums;
using Shared.Contracts.Errors;
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
        private readonly KafkaTopcis _options;

        public ClaimsController(IClaimRepository repository, IPublisher<Claim> publisher, IOptions<KafkaTopcis> options)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] RegisterClaimRequest claimRequest, [FromServices]IRegisterClaimValidator validator)
        {   
            if((await validator.Validate(claimRequest)).IsFailure)
            {
                return BadRequest(new Response<Claim>(validator.Errors));
            }
            var claim = claimRequest.ToEntity();
            await _repository.InsertAsync(claim);
            await _publisher.PublishAsync(_options.Claims, claim);
            return Ok(new Response<Claim>(claim));
        }

        [HttpPatch("{id}/confirm")]
        public async Task<IActionResult> ConfirmAsync([FromRoute] string id)
        {
            var claim = await _repository.GetByAsync(id);
            if(claim is null)
            {
                return NotFound(new Response<Claim>(KnownErrors.CLAIM_DOES_NOT_EXISTS));
            }
            claim.Status = ClaimStatus.CONFIRMED;
            await _repository.UpdateAsync(claim);
            await _publisher.PublishAsync(_options.Claims, claim);
            return Ok(new Response<Claim>(claim));
        }

    }
}
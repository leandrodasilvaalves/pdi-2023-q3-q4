using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.Broker;
using Shared.Contracts.Enums;
using Shared.Contracts.Errors;
using Shared.Contracts.Repositories;
using Shared.Entities;
using Shared.HttpClients;
using Shared.Requests;
using Shared.Validations;
using Vulture.Extensions;
using Shared.Extensions;

namespace Vulture.Controllers
{
    [ApiController]
    [Route("api/pix/claims")]
    public class ClaimsController : ControllerBase
    {
        private readonly IClaimRepository _repository;
        private readonly IPublisher<Claim> _publisher;
        private readonly KafkaTopcis _options;
        private readonly IBacenClaimClient _claimClient;

        public ClaimsController(IClaimRepository repository,
                                IPublisher<Claim> publisher,
                                IOptions<KafkaTopcis> options,
                                IBacenClaimClient claimClient)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _claimClient = claimClient ?? throw new ArgumentNullException(nameof(claimClient));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] RegisterClaimRequest claimRequest, [FromServices] IRegisterClaimValidator validator)
        {
            if ((await validator.Validate(claimRequest)).IsFailure)
            {
                return BadRequest(new Response<List<RegisterClaimRequest>>(validator.Errors));
            }

            var result = await _claimClient.RegisterAsync(claimRequest);
            if (result.IsFailure())
            {
                return BadRequest(await result.ToFailureAsync());
            }

            var claim = claimRequest.ToVultureEntity(result.GetId());
            await _repository.InsertAsync(claim);
            await _publisher.PublishAsync(_options.Claims, claim);
            return Ok(new Response<Claim>(claim));
        }

        [HttpPatch("{id}/confirm")]
        public async Task<IActionResult> ConfirmAsync([FromRoute] string id)
        {
            var claim = await _repository.GetByAsync(id);
            if (claim is null)  
            {
                return NotFound(new Response<Claim>(KnownErrors.CLAIM_DOES_NOT_EXISTS));
            }

            var result = await _claimClient.ConfirmAsync(id);
            if (result.IsFailure())
            {
                return BadRequest(await result.ToFailureAsync());
            }

            claim.Status = ClaimStatus.CONFIRMED;
            await _repository.UpdateAsync(claim);
            await _publisher.PublishAsync(_options.Claims, claim);
            return Ok(new Response<Claim>(claim));
        } 
    }
}
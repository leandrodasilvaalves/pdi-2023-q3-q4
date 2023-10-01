using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.Broker;
using Shared.Contracts.Enums;
using Shared.Contracts.Errors;
using Shared.Contracts.Models;
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
        private readonly IClaimRepository _claimRepository;
        private readonly IEntryRepository _entryRepository;
        private readonly IPublisher<Claim> _publisher;
        private readonly KafkaTopcis _options;

        public ClaimsController(IClaimRepository claimRepository,
                                IEntryRepository entryRepository,
                                IPublisher<Claim> publisher,
                                IOptions<KafkaTopcis> options)
        {
            _claimRepository = claimRepository ?? throw new ArgumentNullException(nameof(claimRepository));
            _entryRepository = entryRepository ?? throw new ArgumentNullException(nameof(entryRepository));
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

            var entry = await _entryRepository.GetByAsync(claimRequest.AddressingKey);
            
            var claim = claimRequest.ToEntity();
            claim.Donor = (ClaimerModel)entry.Account;

            await _claimRepository.InsertAsync(claim);
            await _publisher.PublishAsync(_options.Claims, claim);
            return Ok(new Response<Claim>(claim));
        }

        [HttpPatch("{id}/confirm")]
        public async Task<IActionResult> ConfirmAsync([FromRoute] string id)
        {
            var claim = await _claimRepository.GetByAsync(id);
            if(claim is null)
            {
                return NotFound(new Response<Claim>(KnownErrors.CLAIM_DOES_NOT_EXISTS));
            }
            claim.Status = ClaimStatus.CONFIRMED;
            await _claimRepository.UpdateAsync(claim);
            await _publisher.PublishAsync(_options.Claims, claim);
            return Ok(new Response<Claim>(claim));
        }

        [HttpGet("{ispb}")]
        public async Task<IActionResult> GetAsync([FromRoute] string ispb, [FromQuery]DateTime startDate, [FromQuery]DateTime endDate)
        {
            var claim = await _claimRepository.GetByAsync(ispb, startDate, endDate);
            if(claim is null)
            {
                return NotFound(new Response<IEnumerable<Claim>>(KnownErrors.CLAIM_DOES_NOT_EXISTS));
            }
            return Ok(new Response<IEnumerable<Claim>>(claim));
        }    
    }
}
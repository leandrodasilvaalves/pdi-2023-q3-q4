using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Errors;
using AddressingKey = Shared.Contracts.Models.AddressingKey;
using Shared.Contracts.Repositories;
using Shared.Entities;
using Shared.Requests;
using Shared.Validations;

namespace Bacen.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _repository;

        public AccountController(IAccountRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateAccountRequest request, [FromServices] IAccountValidator validator)
        {
            var account = request.ToEntity();
            if ((await validator.Validate(account)).IsFailure)
            {
                return BadRequest(new Response<Account>(validator.Errors));
            }

            await _repository.InsertAsync(account);
            return Ok(new Response<Account>(account));
        }

        [HttpGet("{ispb}/{branch}/{account}/addressing-keys")]
        public async Task<IActionResult> GetAsync([FromRoute] GetAccountAddressingKeysRequest request, [FromServices] IGetAccountAddressingKeysValidator validator)
        {

            if ((await validator.Validate(request)).IsFailure)
            {
                return BadRequest( new Response<List<AddressingKey>>(validator.Errors));
            }

            var account = await _repository.GetByAsync(request.Branch, request.Account, request.Ispb);
            if (account is null)
                return NotFound(new Response<List<AddressingKey>>(KnownErrors.ACCOUNT_DOES_NOT_EXISTIS));

            if (account.AddressingKeys is null)
                return UnprocessableEntity(new Response<List<AddressingKey>>(KnownErrors.DOES_NOT_HAVE_ADDRESSING_KEYS));

            return Ok(new Response<List<AddressingKey>>(account.AddressingKeys));
        }
    }

}
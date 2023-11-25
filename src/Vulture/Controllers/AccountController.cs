using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Errors;
using Shared.Contracts.Repositories;
using Shared.Entities;
using Shared.Requests;
using Shared.Validations;
using Shared.HttpClients;
using Vulture.Extensions;
using Shared.Extensions;
using Vulture.Contracts;
using AddressingKey = Shared.Contracts.Models.AddressingKey;

namespace Vulture.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _repository;
        private readonly IBacenAccountClient _accountClient;

        public AccountController(IAccountRepository repository, IBacenAccountClient accountClient)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _accountClient = accountClient ?? throw new ArgumentNullException(nameof(accountClient));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateAccountRequest request, [FromServices] IAccountValidator validator)
        {
            var account = request.ToVultureEntity();
            if ((await validator.Validate(account)).IsFailure)
            {
                return BadRequest(new Response<Account>(validator.Errors));
            }

            var result = await _accountClient.RegisterAsync(request);
            if (result.IsFailure())
            {
                return BadRequest(await result.ToFailureAsync());
            }

            await _repository.InsertAsync(account);
            return Ok(new Response<Account>(account));
        }

        [HttpGet("{branch}/{account}/addressing-keys")]
        public async Task<IActionResult> GetAsync([FromRoute] GetAccountAddressingKeysRequest request, [FromServices] IGetAccountAddressingKeysValidator validator)
        {
            request.Ispb = Constants.ISPB;
            if ((await validator.Validate(request)).IsFailure)
            {
                return BadRequest(new Response<List<AddressingKey>>(validator.Errors));
            }

            var account = await _repository.GetByAsync(request.Branch, request.Account, request.Ispb);
            if (account is null)
                return NotFound(new Response<List<AddressingKey>>(KnownErrors.ACCOUNT_DOES_NOT_EXISTIS));

            if (account.AddressingKeys is null || account.AddressingKeys.Count == 0)
                return UnprocessableEntity(new Response<List<AddressingKey>>(KnownErrors.DOES_NOT_HAVE_ADDRESSING_KEYS));

            return Ok(new Response<List<AddressingKey>>(account.AddressingKeys));
        }
    }
}
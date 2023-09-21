using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;
using Shared.Models;
using Shared.Requests;
using Shared.Validations;

namespace Bacen.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _repository;
        private readonly IAccountValidator _validator;

        public AccountController(IAccountRepository repository, IAccountValidator validator)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateAccountRequest request)
        {
            var account = request.ToAccount();
            if (_validator.Validate(account).IsFailure)
            {
                return BadRequest(new Response<Account>(_validator.Errors));
            }
            
            await _repository.InsertAsync(account);
            return Ok(new Response<Account>(account));
        }
    }
}
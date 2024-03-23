using BackOffice.WebApi.Application;
using BackOffice.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackOffice.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(ILogger<AccountsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetAllAccounts")]
        public async Task<ActionResult<IEnumerable<AccountApiDto>>> GetAllAccounts(
            [FromServices] GetAllAccountsCommandHandler commandHandler)
        {
            var accounts = await commandHandler.Execute();
            return Ok(accounts.Select(a => a.ToApiDto()));
        }

        [HttpPost(Name = "CreateAccount")]
        public async Task<ActionResult<AccountApiDto>> CreateAccount(
            [FromBody] CreateAccountApiDto createAccountApiDto,
            [FromServices] CreateAccountCommandHandler commandHandler)
        {
            var cmdInput = createAccountApiDto.ToCommandInput();
            var account = await commandHandler.Execute(cmdInput);
            return Ok(account.ToApiDto());
        }

    }
}

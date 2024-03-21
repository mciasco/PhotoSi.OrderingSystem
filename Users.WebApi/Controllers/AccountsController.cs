using Microsoft.AspNetCore.Mvc;
using Users.WebApi.Application;
using Users.WebApi.Models;

namespace Users.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(ILogger<AccountsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetAllAccounts")]
        public async Task<ActionResult<IEnumerable<AccountDto>>> GetAllAccounts([FromServices] GetAllAccountsCommandHandler commandHandler)
        {
            var users = await commandHandler.Execute();
            return Ok(users.Select(u => u.ToAccountDto()));
        }

        [HttpGet("{accountId}", Name = "GetAccountById")]
        public async Task<ActionResult<AccountDto>> GetAccountById(
            [FromRoute] string accountId,
            [FromServices] GetAccountByIdCommandHandler commandHandler)
        {
            var user = await commandHandler.Execute(accountId);
            return user is null
                ? NotFound($"No account found with id {accountId}")
                : Ok(user.ToAccountDto());
        }

        [HttpGet("usernames/{username}", Name = "GetAccountsByUsername")]
        public async Task<ActionResult<IEnumerable<AccountDto>>> GetAccountsByUsername(
            [FromRoute] string username,
            [FromServices] GetAccountsByUsernameCommandHandler commandHandler)
        {
            var users = await commandHandler.Execute(username);
            return users is null || !users.Any()
                ? NotFound($"No accounts found with username {username}")
                : Ok(users.Select(u => u.ToAccountDto()));
        }
    }
}

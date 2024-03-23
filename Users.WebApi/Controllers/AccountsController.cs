using Microsoft.AspNetCore.Mvc;
using Products.WebApi.Application;
using Users.WebApi.Application;
using Users.WebApi.Models;

namespace Users.WebApi.Controllers
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
        public async Task<ActionResult<IEnumerable<AccountApiDto>>> GetAllAccounts([FromServices] GetAllAccountsCommandHandler commandHandler)
        {
            var users = await commandHandler.Execute();
            return Ok(users.Select(u => u.ToApiDto()));
        }

        [HttpGet("{accountId}", Name = "GetAccountById")]
        public async Task<ActionResult<AccountApiDto>> GetAccountById(
            [FromRoute] string accountId,
            [FromServices] GetAccountByIdCommandHandler commandHandler)
        {
            var user = await commandHandler.Execute(accountId);
            return user is null
                ? NotFound($"No account found with id {accountId}")
                : Ok(user.ToApiDto());
        }

        [HttpGet("usernames/{username}", Name = "GetAccountsByUsername")]
        public async Task<ActionResult<IEnumerable<AccountApiDto>>> GetAccountsByUsername(
            [FromRoute] string username,
            [FromServices] GetAccountsByUsernameCommandHandler commandHandler)
        {
            var users = await commandHandler.Execute(username);
            return users is null || !users.Any()
                ? NotFound($"No accounts found with username {username}")
                : Ok(users.Select(u => u.ToApiDto()));
        }


        [HttpPost(Name = "RegisterNewAccount")]
        public async Task<ActionResult<IEnumerable<AccountApiDto>>> RegisterNewAccount(
            [FromBody] RegisterNewAccountApiDto registerNewAccountApiDto,
            [FromServices] RegisterNewAccountCommandHandler commandHandler)
        {
            var cmdInput = registerNewAccountApiDto.ToCommandInput();
            var userCreated = await commandHandler.Execute(cmdInput);
            return Ok(userCreated.ToApiDto());
        }


        [HttpDelete("{accountId}", Name = "DeleteAccountById")]
        public async Task<ActionResult<bool>> DeleteAccountById(
            [FromRoute] string accountId,
            [FromServices] DeleteAccountByIdCommandHandler commandHandler)
        {
            var deleted = await commandHandler.Execute(accountId);
            return Ok(deleted);
        }
    }
}

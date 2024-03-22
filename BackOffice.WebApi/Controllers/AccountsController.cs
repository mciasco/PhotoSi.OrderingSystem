using BackOffice.WebApi.Application;
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
        public async Task<ActionResult<IEnumerable<Models.AccountDto>>> GetAllAccounts(
            [FromServices] GetAllAccountsCommandHandler commandHandler)
        {
            var accounts = await commandHandler.Execute();
            return Ok(accounts.Select(a => a.ToAccountDto()));
        }
    }
}

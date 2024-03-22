using AddressBook.WebApi.Application;
using AddressBook.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressesController : ControllerBase
    {
        private readonly ILogger<AddressesController> _logger;

        public AddressesController(ILogger<AddressesController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetAllAddresses")]
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetAllAddresses(
            [FromServices] GetAllAddressesCommandHandler commandHandler)
        {
            var addresses = await commandHandler.Execute();
            return Ok(addresses.Select(a => a.ToAccountDto()));
        }

        [HttpGet("accounts/{accountId}", Name = "GetAddressesByAccountId")]
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetAddressesByAccountId(
            [FromRoute] string accountId,
            [FromServices] GetAddressesByAccountIdCommandHandler commandHandler)
        {
            var addresses = await commandHandler.Execute(accountId);
            return Ok(addresses.Select(a => a.ToAccountDto()));
        }
    }
}

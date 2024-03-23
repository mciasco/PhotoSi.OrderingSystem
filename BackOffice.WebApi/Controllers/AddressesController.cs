using BackOffice.WebApi.Application;
using Microsoft.AspNetCore.Mvc;

namespace BackOffice.WebApi.Controllers
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
        public async Task<ActionResult<IEnumerable<Models.AddressApiDto>>> GetAllAddresses(
            [FromServices] GetAllAddressesCommandHandler commandHandler)
        {
            var addresses = await commandHandler.Execute();
            return Ok(addresses.Select(a => a.ToApiDto()));
        }
    }
}

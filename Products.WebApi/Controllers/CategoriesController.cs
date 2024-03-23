using Microsoft.AspNetCore.Mvc;
using Products.WebApi.Application;
using Products.WebApi.Models;

namespace Products.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ILogger<CategoriesController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetAllCategories")]
        public async Task<ActionResult<IEnumerable<CategoryApiDto>>> GetAllCategories([FromServices] GetAllCategoriesCommandHandler commandHandler)
        {
            var categories = await commandHandler.Execute().ConfigureAwait(false);

            return Ok(categories.Select(c => c.ToApiDto()));
        }
    }
}

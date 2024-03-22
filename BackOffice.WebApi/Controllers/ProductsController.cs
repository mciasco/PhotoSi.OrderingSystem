using BackOffice.WebApi.Application;
using Microsoft.AspNetCore.Mvc;

namespace BackOffice.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetAllProducts")]
        public async Task<ActionResult<IEnumerable<Models.ProductDto>>> GetAllProducts(
            [FromServices] GetAllProductsCommandHandler commandHandler)
        {
            var products = await commandHandler.Execute();
            return Ok(products.Select(p => p.ToProductDto()));
        }


        [HttpGet("categories", Name = "GetAllCategories")]
        public async Task<ActionResult<IEnumerable<Models.CategoryDto>>> GetAllCategories(
            [FromServices] GetAllCategoriesCommandHandler commandHandler)
        {
            var categories = await commandHandler.Execute();
            return Ok(categories.Select(c => c.ToCategoryDto()));
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Products.WebApi.Application;
using Products.WebApi.Models;

namespace Products.WebApi.Controllers
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
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts([FromServices] GetAllProductsCommandHandler commandHandler)
        {
            var products = await commandHandler.Execute().ConfigureAwait(false);

            return Ok(products.Select(p => p.ToProductDto()));
        }

        [HttpGet("{productId}", Name = "GetProductById")]
        public async Task<ActionResult<ProductDto>> GetProductById(
            [FromRoute] string productId,
            [FromServices] GetProductByIdCommandHandler commandHandler)
        {
            var product = await commandHandler.Execute(productId).ConfigureAwait(false);
                          
            return product is null
                ? NotFound($"No product found with id {productId}") 
                : Ok(product.ToProductDto());

        }


        [HttpGet("Categories/{categoryName}", Name = "GetProductsByCategory")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory(
            [FromRoute] string categoryName,
            [FromServices] GetProductsByCategoryCommandHandler commandHandler)
        {
            var products = await commandHandler.Execute(categoryName).ConfigureAwait(false);

            return Ok(products.Select(p => p.ToProductDto()));
        }
    }
}

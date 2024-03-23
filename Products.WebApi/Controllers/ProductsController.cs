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
        public async Task<ActionResult<IEnumerable<ProductApiDto>>> GetAllProducts([FromServices] GetAllProductsCommandHandler commandHandler)
        {
            var products = await commandHandler.Execute().ConfigureAwait(false);

            return Ok(products.Select(p => p.ToApiDto()));
        }

        [HttpGet("{productId}", Name = "GetProductById")]
        public async Task<ActionResult<ProductApiDto>> GetProductById(
            [FromRoute] string productId,
            [FromServices] GetProductByIdCommandHandler commandHandler)
        {
            var product = await commandHandler.Execute(productId).ConfigureAwait(false);
                          
            return product is null
                ? NotFound($"No product found with id {productId}") 
                : Ok(product.ToApiDto());

        }


        [HttpGet("Categories/{categoryName}", Name = "GetProductsByCategory")]
        public async Task<ActionResult<IEnumerable<ProductApiDto>>> GetProductsByCategory(
            [FromRoute] string categoryName,
            [FromServices] GetProductsByCategoryCommandHandler commandHandler)
        {
            var products = await commandHandler.Execute(categoryName).ConfigureAwait(false);

            return Ok(products.Select(p => p.ToApiDto()));
        }


        [HttpPost(Name = "CreateNewProduct")]
        public async Task<ActionResult<IEnumerable<ProductApiDto>>> CreateNewProduct(
            [FromBody] CreateNewProductApiDto createNewProductDto,
            [FromServices] CreateNewProductCommandHandler commandHandler)
        {
            var cmdInput = createNewProductDto.ToCommandInput();
            var newProduct = await commandHandler.Execute(cmdInput);
            return Ok(newProduct.ToApiDto());
        }

        [HttpDelete("{productId}", Name = "DeleteProductById")]
        public async Task<ActionResult<string>> DeleteProductById(
            [FromRoute] string productId,
            [FromServices] DeleteProductByIdCommandHandler commandHandler)
        {
            await commandHandler.Execute(productId);
            return Ok(productId);
        }
    }
}

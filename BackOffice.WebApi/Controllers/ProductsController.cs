using BackOffice.WebApi.Application;
using BackOffice.WebApi.Models;
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
        public async Task<ActionResult<IEnumerable<Models.ProductApiDto>>> GetAllProducts(
            [FromServices] GetAllProductsCommandHandler commandHandler)
        {
            var products = await commandHandler.Execute();
            return Ok(products.Select(p => p.ToApiDto()));
        }

        [HttpPost(Name = "CreateProduct")]
        public async Task<ActionResult<Models.ProductApiDto>> CreateProduct(
            [FromBody] CreateProductApiDto createProductDto,
            [FromServices] CreateProductCommandHandler commandHandler)
        {
            var cmdInput = createProductDto.ToCommandInput();
            var productDto = await commandHandler.Execute(cmdInput);
            return Ok(productDto.ToApiDto());
        }


        [HttpDelete("{productId}", Name = "DeleteProductById")]
        public async Task<ActionResult<string>> DeleteProduct(
            [FromRoute] string productId,
            [FromServices] DeleteProductCommandHandler commandHandler)
        {
            var deletedId = await commandHandler.Execute(productId);
            return Ok(deletedId);
        }






        [HttpGet("categories", Name = "GetAllCategories")]
        public async Task<ActionResult<IEnumerable<CategoryApiDto>>> GetAllCategories(
            [FromServices] GetAllCategoriesCommandHandler commandHandler)
        {
            var categories = await commandHandler.Execute();
            return Ok(categories.Select(c => c.ToApiDto()));
        }



        


    }
}

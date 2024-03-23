using Microsoft.AspNetCore.Mvc;
using Orders.WebApi.Application;
using Orders.WebApi.Models;

namespace Orders.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(ILogger<OrdersController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetAllOrders")]
        public async Task<ActionResult<IEnumerable<OrderApiDto>>> GetAllOrders(
            [FromServices] GetAllOrdersCommandHandler commandHandler)
        {
            var orders = await commandHandler.Execute().ConfigureAwait(false);

            return Ok(orders.Select(o => o.ToApiDto()));
        }

        [HttpGet("{orderId}", Name = "GetOrderById")]
        public async Task<ActionResult<OrderApiDto>> GetOrderById(
            [FromRoute] string orderId, 
            [FromServices] GetOrderByIdCommandHandler commandHandler)
        {
            var orderFound = await commandHandler.Execute(orderId).ConfigureAwait(false);

            if(orderFound is null)
                return NotFound("No order found with id " + orderId);
            else
                return Ok(orderFound.ToApiDto());
        }

        [HttpPost(Name = "CreateNewOrder")]
        public async Task<ActionResult<OrderApiDto>> CreateNewOrder(
            [FromBody] CreateNewOrderApiDto createNewOrderDto,
            [FromServices] CreateNewOrderCommandHandler commandHandler)
        {
            var cmdInput = createNewOrderDto.ToCommandInput();
            var newOrder = await commandHandler.Execute(cmdInput);

            return Ok(newOrder.ToApiDto());
        }
    }


    
}

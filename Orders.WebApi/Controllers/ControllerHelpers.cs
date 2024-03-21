using Orders.Contracts.Domain;
using Orders.WebApi.Models;

namespace Orders.WebApi.Controllers
{
    public static class ControllerHelpers
    {
        public static OrderDto ToOrderDto(this Order order)
        {
            return new OrderDto()
            {
                Id = order.Id,
                Description = order.Description,
                CreatedAt = order.CreatedAt,
                ProductIds = order.OrderedProducts.Select(op => op.ProductId).ToList(),
                TotalPrice = order.OrderedProducts.Sum(op => op.Price * op.Qty),
            };
        }
    }
}

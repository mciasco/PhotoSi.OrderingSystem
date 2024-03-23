using Orders.Contracts.Domain;
using Orders.WebApi.Application;
using Orders.WebApi.Models;

namespace Orders.WebApi.Controllers
{
    public static class ControllerHelpers
    {
        public static OrderApiDto ToApiDto(this Order order)
        {
            return new OrderApiDto()
            {
                Id = order.Id,
                Description = order.Description,
                CreatedAt = order.CreatedAt,
                CustomerAccountId = order.CustomerAccountId,
                ShippingAddressId = order.ShippingAddressId,
                ProductIds = order.OrderedProducts.Select(op => op.ProductId).ToList(),
                TotalPrice = order.OrderedProducts.Sum(op => op.Price * op.Qty),
            };
        }


        public static CreateNewOrderCommandInput ToCommandInput(this CreateNewOrderApiDto inputDto)
        {
            var cmdInput = new CreateNewOrderCommandInput();
            cmdInput.Description = inputDto.Description;
            cmdInput.ProductItems = new List<CreateNewOrderProductItem>();
            if (inputDto.ProductItems != null && inputDto.ProductItems.Any())
            {
                cmdInput.ProductItems.AddRange(inputDto.ProductItems.Select(pi => new CreateNewOrderProductItem()
                {
                    ProductId = pi.ProductId,
                    Quantity = pi.Quantity,
                }));
            }

            cmdInput.CustomerAccountId = inputDto.CustomerAccountId;
            cmdInput.ShippingAddressId = inputDto.ShippingAddressId;

            return cmdInput;
        }
    }
}

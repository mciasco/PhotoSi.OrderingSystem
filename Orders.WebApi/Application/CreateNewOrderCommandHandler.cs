using Orders.Contracts.Clients;
using Orders.Contracts.Domain;
using Orders.Contracts.Persistence;

namespace Orders.WebApi.Application
{
    public class CreateNewOrderCommandInput
    {
        public List<CreateNewOrderProductItem> ProductItems { get; set; }
        public string Description { get; set; }
    }

    public class CreateNewOrderProductItem
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }


    public class CreateNewOrderCommandHandler : BaseCommandHandlerWithInputWithOutput<CreateNewOrderCommandInput, Order>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductsServiceClient _productsServiceClient;

        public CreateNewOrderCommandHandler(
            IOrdersRepository ordersRepository, 
            IUnitOfWork unitOfWork,
            IProductsServiceClient productsServiceClient)
        {
            this._ordersRepository = ordersRepository;
            this._unitOfWork = unitOfWork;
            this._productsServiceClient = productsServiceClient;
        }

        public override async Task<Order> Execute(CreateNewOrderCommandInput input)
        {
            var listOfOrderedProducts = new List<OrderedProduct>();
            foreach (var item in input.ProductItems) 
            { 
                var productDto = await _productsServiceClient.GetProductById(item.ProductId);

                if (item.Quantity > productDto.QtyStock)
                    throw new ArgumentException($"Product '{productDto.Name}' (ID: {productDto.Id}) is out of stock");

                listOfOrderedProducts.Add(OrderedProduct.Create(productDto.Id, item.Quantity, productDto.Price));
            }

            var newOrder = Order.Create(input.Description, listOfOrderedProducts);

            await _ordersRepository.AddOrder(newOrder);

            await _unitOfWork.SaveChangesAsync();

            return newOrder;
        }
    }
}

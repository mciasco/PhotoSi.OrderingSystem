using Orders.Contracts.Clients;
using Orders.Contracts.Domain;
using Orders.Contracts.Persistence;
using Commons.WebApi.Application;
using Commons.Contracts.Persistence;

namespace Orders.WebApi.Application
{
    public class CreateNewOrderCommandInput
    {
        public List<CreateNewOrderProductItem> ProductItems { get; set; }
        public string Description { get; set; }
        public string CustomerAccountId { get; internal set; }
        public string ShippingAddressId { get; internal set; }
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
        private readonly IAddressBookServiceClient _addressBookServiceClient;
        private readonly IUsersServiceClient _usersServiceClient;

        public CreateNewOrderCommandHandler(
            IOrdersRepository ordersRepository, 
            IUnitOfWork unitOfWork,
            IProductsServiceClient productsServiceClient,
            IAddressBookServiceClient addressBookServiceClient,
            IUsersServiceClient usersServiceClient)
        {
            this._ordersRepository = ordersRepository;
            this._unitOfWork = unitOfWork;
            this._productsServiceClient = productsServiceClient;
            this._addressBookServiceClient = addressBookServiceClient;
            this._usersServiceClient = usersServiceClient;
        }

        public override async Task<Order> Execute(CreateNewOrderCommandInput input)
        {
            // verifica esistenza prodotti e disponibilità
            var listOfOrderedProducts = new List<OrderedProduct>();
            foreach (var item in input.ProductItems) 
            { 
                var productDto = await _productsServiceClient.GetProductById(item.ProductId);

                if (item.Quantity > productDto.QtyStock)
                    throw new ArgumentException($"Product '{productDto.Name}' (ID: {productDto.Id}) is out of stock");

                listOfOrderedProducts.Add(OrderedProduct.Create(productDto.Id, item.Quantity, productDto.Price));
            }

            // verifica esistenza utente
            var accountDto = await _usersServiceClient.GetAccountById(input.CustomerAccountId);
            if(accountDto is null)
                throw new ArgumentException($"No account found with ID '{input.CustomerAccountId}'");

            // verifica esistenza indirizzo spedizione...
            var addressDtos = await _addressBookServiceClient.GetAddressByAccountId(input.CustomerAccountId);
            if(addressDtos is null || !addressDtos.Any())
                throw new ArgumentException($"No shipping address associated to account with ID '{input.CustomerAccountId}'");

            //...e corrispondenza con indirizzi utente
            var selectedAddressDto = addressDtos.FirstOrDefault(adr => adr.AddressId == input.ShippingAddressId);
            if(selectedAddressDto is null)
                throw new ArgumentException($"No corresponding shipping address with ID '{input.ShippingAddressId}' associated to account with ID '{input.CustomerAccountId}'");

            // crea nuova entita' Order
            var newOrder = Order.Create(
                input.Description, 
                listOfOrderedProducts, 
                input.CustomerAccountId, 
                input.ShippingAddressId);

            // salva su DB
            await _ordersRepository.AddOrder(newOrder);
            await _unitOfWork.SaveChangesAsync();

            return newOrder;
        }
    }
}

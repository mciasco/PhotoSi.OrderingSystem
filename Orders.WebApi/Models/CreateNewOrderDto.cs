namespace Orders.WebApi.Models
{
    public class CreateNewOrderDto
    {
        public List<CreateNewOrderProductItemDto> ProductItems { get; set; }
        public string Description { get; set; }

        public string CustomerAccountId { get; set; }
        public string ShippingAddressId { get; set; }
    }

    public class CreateNewOrderProductItemDto
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

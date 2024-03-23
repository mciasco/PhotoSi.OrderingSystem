namespace Orders.WebApi.Models
{
    public class CreateNewOrderApiDto
    {
        public List<CreateNewOrderProductItemApiDto> ProductItems { get; set; }
        public string Description { get; set; }

        public string CustomerAccountId { get; set; }
        public string ShippingAddressId { get; set; }
    }

    public class CreateNewOrderProductItemApiDto
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

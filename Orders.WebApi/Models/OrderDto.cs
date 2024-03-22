namespace Orders.WebApi.Models
{
    public class OrderDto
    {
        public OrderDto()
        {
        }

        public string Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<string> ProductIds { get; set; }
        public decimal TotalPrice { get; set; }
        public string CustomerAccountId { get; internal set; }
        public string ShippingAddressId { get; internal set; }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Contracts.Domain
{
    public class Order
    {
        protected Order() { }

        public static Order Create(
            string description, 
            List<OrderedProduct> orderedProducts,
            string customerAccountId,
            string shippingAddressId)
        {
            var now = DateTime.UtcNow;
            var newOrder = new Order()
            {
                Id = $"ORD_{now.ToString("yyyyMMddHHmmssfff")}",
                Description = description,
                CreatedAt = now,
                OrderedProducts = new List<OrderedProduct>(),
                CustomerAccountId = customerAccountId,
                ShippingAddressId = shippingAddressId
            };

            foreach (var product in orderedProducts)
            {
                product.Order = newOrder;
                product.OrderId = newOrder.Id;
                newOrder.OrderedProducts.Add(product);
            }

            return newOrder;
        }

        public string Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CustomerAccountId { get; set; }
        public string ShippingAddressId { get; set; }

        public virtual ICollection<OrderedProduct> OrderedProducts { get; set; }
    }


    public class OrderedProduct
    {
        protected OrderedProduct() { }

        public static OrderedProduct Create(string productId, int qty, decimal price)
        {
            return new OrderedProduct
            {
                ProductId = productId,
                Qty = qty,
                Price = price,
            };
        }

        public string OrderId { get; set; }
        public virtual Order? Order { get; set; }

        public string ProductId { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }
}

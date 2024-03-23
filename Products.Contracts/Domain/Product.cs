namespace Products.Contracts.Domain
{
    public class Product
    {
        protected Product()
        {
        }

        public static Product CreateEmpty()
        {
            var product = new Product();
            product.Id = $"PRD_{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}";
            return product;
        }

        public static Product Create(
            string name,
            string description,
            Category category,
            int qtyStock,
            decimal price)
        {
            var product = new Product();
            product.Id = $"PRD_{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}";
            product.Name = name;
            product.Description = description;
            product.Category = category;
            product.CategoryName = category.Name;
            product.QtyStock = qtyStock;
            product.Price = price;
            return product;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public string? CategoryName { get; set; }
        public virtual Category? Category { get; set; }


        public int QtyStock { get; set; }
        public decimal Price { get; set; }
    }
}

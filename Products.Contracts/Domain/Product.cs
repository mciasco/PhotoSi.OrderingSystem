namespace Products.Contracts.Domain
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public string? CategoryName { get; set; }
        public virtual Category? Category { get; set; }


        public int QtyStock { get; set; }
        public decimal Price { get; set; }
    }
}

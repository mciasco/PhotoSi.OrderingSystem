﻿namespace BackOffice.WebApi.Models
{
    public class ProductDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public int QtyStock { get; set; }
        public decimal Price { get; set; }
    }
}

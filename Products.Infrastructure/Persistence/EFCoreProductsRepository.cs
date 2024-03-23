﻿using Microsoft.EntityFrameworkCore;
using Products.Contracts.Domain;
using Products.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Infrastructure.Persistence
{
    public class EFCoreProductsRepository : IProductsRepository
    {
        private readonly ProductsDbContext _dbContext;

        public EFCoreProductsRepository(ProductsDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task AddProduct(Product product)
        {
            await _dbContext.Products.AddAsync(product);
        }

        public async Task DeleteProduct(string input)
        {
            // Per evitare una inutile doppia chiamata al DB dove prima recupero l'entità e poi la elimino
            // uso la tecnica di marcare l'entità con l'id corrispondente come DELETED
            // così il tracker interno di EFCore la eliminerà
            var productToDelete = Product.CreateEmpty();
            productToDelete.Id = input;
            var trackedProductToDelete = _dbContext.Products.Attach(productToDelete);
            trackedProductToDelete.State = EntityState.Deleted;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _dbContext
                .Products
                .Include(p => p.Category)
                .ToArrayAsync();
        }

        public async Task<Product> GetProductById(string productId)
        {
            return await _dbContext
                .Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<Product> GetProductByName(string productName)
        {
            return await _dbContext
                .Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Name == productName);
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string categoryName)
        {
            var productsByCategoryQry = _dbContext
                .Products
                .Include(p => p.Category)
                .Where(p => p.Category.Name == categoryName);

            var productsByCategoryArray = await productsByCategoryQry.ToArrayAsync();

            return productsByCategoryArray;
        }
    }
}
